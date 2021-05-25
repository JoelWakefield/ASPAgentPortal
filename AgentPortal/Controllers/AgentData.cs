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

        public List<Agent> GetAllAgents()
        {
            List<Agent> agents = new List<Agent>();

            using (var conn = new SqlConnection(_configuration.GetConnectionString("default")))
            {
                conn.Open();

                var com = new SqlCommand();
                com.Connection = conn;
                com.CommandType = System.Data.CommandType.Text;
                com.CommandText = "SELECT * FROM AGENTS";

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
    }
}
