using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AgentPortal.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgentPortal.Controllers
{
    public class AgentController : Controller
    {
        private readonly AgentData _agentData;

        public AgentController(AgentData agentData)
        {
            _agentData = agentData;
        }

        public IActionResult Index()
        {
            return View(_agentData.GetAllAgents());
        }

        public IActionResult Detail(string id)
        {
            return View(_agentData.GetAgent(id));
        }
    }
}
