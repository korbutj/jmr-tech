using System;
using System.Collections.Generic;
using System.Text;

namespace JMRTech.TaskOne
{
    public class Person
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<EmailAddress> Emails { get; set; }
    }
}
