using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgentPortal.Models
{
    public class Agent
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string WorkingArea { get; set; }
        public double Commission { get; set; }
        public string PhoneNo { get; set; }
    }
}
