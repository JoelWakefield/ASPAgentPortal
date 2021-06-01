using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AgentPortal.Models;
using Microsoft.Extensions.Configuration;

namespace AgentPortal.Controllers
{
    public class AgentData
    {
        private readonly IConfiguration _configuration;

        public AgentData(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Agent> GetAllAgents(bool onlyActive = true)
        {
            List<Agent> agents = new List<Agent>();

            using (var conn = new SqlConnection(_configuration.GetConnectionString("default")))
            {
                conn.Open();

                var com = new SqlCommand();
                com.Connection = conn;
                com.CommandType = System.Data.CommandType.Text;

                if (onlyActive)
                {
                    com.Parameters.Add(new SqlParameter { ParameterName = "@active", Value = true, SqlDbType = System.Data.SqlDbType.Bit });
                    com.CommandText = "SELECT * FROM AGENTS WHERE IsActive = @active";
                }
                else
                {
                    com.CommandText = "SELECT * FROM AGENTS";
                }

                var reader = com.ExecuteReader();

                while (reader.Read())
                {
                    agents.Add(new Agent
                    {
                        Code = (string)reader["AgentCode"],
                        Name = (string)reader["AgentName"],
                        WorkingArea = (string)reader["WorkingArea"],
                        Commission = Convert.ToDouble(reader["Commission"]),
                        PhoneNo = (string)reader["PhoneNo"]
                    });
                }

                conn.Close();
            }

            return agents;
        }

        public Agent GetAgent(string code)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("default")))
            {
                conn.Open();

                var com = new SqlCommand();
                com.Connection = conn;
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.AddWithValue("@Code", code);
                com.CommandText = "SELECT * FROM AGENTS WHERE AgentCode=@Code";

                var reader = com.ExecuteReader();

                while (reader.Read())
                {
                    return new Agent
                    {
                        Code = (string)reader["AgentCode"],
                        Name = (string)reader["AgentName"],
                        WorkingArea = (string)reader["WorkingArea"],
                        Commission = Convert.ToDouble(reader["Commission"]),
                        PhoneNo = (string)reader["PhoneNo"]
                    };
                }

                conn.Close();
            }

            return null;
        }

        public void CreateAgent(Agent agent)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("default")))
            {
                conn.Open();

                var com = new SqlCommand();
                com.Connection = conn;
                com.CommandType = System.Data.CommandType.Text;

                com.Parameters.Add(new SqlParameter { ParameterName = "@code", Value = agent.Code, SqlDbType = System.Data.SqlDbType.Char });
                com.Parameters.Add(new SqlParameter { ParameterName = "@name", Value = agent.Name, SqlDbType = System.Data.SqlDbType.VarChar });
                com.Parameters.Add(new SqlParameter { ParameterName = "@area", Value = agent.WorkingArea, SqlDbType = System.Data.SqlDbType.VarChar });
                com.Parameters.Add(new SqlParameter { ParameterName = "@comm", Value = agent.Commission, SqlDbType = System.Data.SqlDbType.Decimal });
                com.Parameters.Add(new SqlParameter { ParameterName = "@phno", Value = agent.PhoneNo, SqlDbType = System.Data.SqlDbType.Char });
                com.Parameters.Add(new SqlParameter { ParameterName = "@active", Value = agent.IsActive, SqlDbType = System.Data.SqlDbType.Char });
                
                com.CommandText = "INSERT INTO Agents VALUES (@code, @name, @area, @comm, @phno, @active)";

                com.ExecuteNonQuery();

                conn.Close();
            }
        }

        public void UpdateAgent(Agent agent)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("default")))
            {
                conn.Open();

                var com = new SqlCommand();
                com.Connection = conn;
                com.CommandType = System.Data.CommandType.Text;

                com.Parameters.Add(new SqlParameter { ParameterName = "@name", Value = agent.Name, SqlDbType = System.Data.SqlDbType.VarChar });
                com.Parameters.Add(new SqlParameter { ParameterName = "@area", Value = agent.WorkingArea, SqlDbType = System.Data.SqlDbType.VarChar });
                com.Parameters.Add(new SqlParameter { ParameterName = "@comm", Value = agent.Commission, SqlDbType = System.Data.SqlDbType.Decimal });
                com.Parameters.Add(new SqlParameter { ParameterName = "@phno", Value = agent.PhoneNo, SqlDbType = System.Data.SqlDbType.Char });
                com.Parameters.Add(new SqlParameter { ParameterName = "@active", Value = agent.IsActive, SqlDbType = System.Data.SqlDbType.Char });

                com.CommandText = "UPDATE Agents SET " +
                    "AgentName = @name" +
                    "WorkingArea = @area" +
                    "Commission = @comm" +
                    "PhoneNo = @phno" +
                    "IsActive = @active" +
                    "WHERE AgentCode = @code";

                com.ExecuteNonQuery();

                conn.Close();
            }
        }

        public void DeactivateAgent(string code)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("default")))
            {
                conn.Open();

                var com = new SqlCommand();
                com.Connection = conn;
                com.CommandType = System.Data.CommandType.Text;

                com.Parameters.Add(new SqlParameter { ParameterName = "@code", Value = code, SqlDbType = System.Data.SqlDbType.Char });
                com.Parameters.Add(new SqlParameter { ParameterName = "@active", Value = false, SqlDbType = System.Data.SqlDbType.Bit });

                com.CommandText = "UPDATE Agents SET IsActive = @active WHERE AgentCode = @code";

                com.ExecuteNonQuery();

                conn.Close();
            }
        }
    }
}
