using System;
using System.Collections.Generic;
using System.Text;

namespace JMRTech.TaskTwo.Models
{
    public class Group
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public IEnumerable<Person> People { get; set; }
    }
}
