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
            return View(_agentData.GetAllAgents(onlyActive: true));
        }

        public IActionResult Detail(string id)
        {
            return View(_agentData.GetAgent(id));
        }

        [HttpGet]
        public IActionResult NewAgent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewAgent(Agent agent)
        {
            _agentData.CreateAgent(agent);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveAgent(string id)
        {
            _agentData.DeactivateAgent(id);
            return RedirectToAction("Index");
        }
    }
}
