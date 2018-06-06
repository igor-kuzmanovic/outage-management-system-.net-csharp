using OutageManagementSystem.Common.Models;
using System.Collections.Generic;

namespace OutageManagementSystem.Service.Contexts
{
    class TestContext
    {
        private int id = 0;

        public TestContext()
        {
            Outages = new List<Outage>();
        }

        public ICollection<Outage> Outages { get; set; }

        public int GenerateID()
        {
            return ++id;
        }
    }
}
