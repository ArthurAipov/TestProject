using System.Collections.Generic;

namespace TestProject.Models
{
    public class Project
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<Task> tasks = new List<Task>();
    }
}
