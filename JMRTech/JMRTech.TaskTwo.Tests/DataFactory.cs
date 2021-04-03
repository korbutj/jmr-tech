using JMRTech.TaskTwo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMRTech.TaskTwo.Tests
{
    public class DataFactory
    {
        public (Group group, IEnumerable<string> emails) GetGroup(int people, int emails, string groupName)
        {
            var group = new Group();
            var listOfPeople = new List<Person>();
            var allMails = new List<string>();
            for(int i = 0; i < people; i++)
            {
                var person = new Person();
                var personEmails = new List<EmailAddress>();
                for(int j = 0; j < emails; j++)
                {
                    personEmails.Add(new EmailAddress() { Email = groupName + i + j + "@gmail.com" });
                }
                person.Emails = personEmails;
                allMails.AddRange(personEmails.Select(x => x.Email));
                listOfPeople.Add(person);
            }

            group.People = listOfPeople;
            return (group, allMails);
        }

        public (IEnumerable<Group> groups, IEnumerable<string> emails, IEnumerable<Account> accounts) GetAllData(int n)
        {
            var groups = new List<Group>();
            var allMails = new List<string>();
            var accounts = new List<Account>();
            for (int i = 0; i < n; i++)
            {
                var gen = GetGroup(1000, 4, ((char)('a' + i)).ToString());
                groups.Add(gen.group);
                allMails.AddRange(gen.emails);
            }

            foreach(var email in allMails)
            {
                accounts.Add(new Account()
                {
                    EmailAddress = new EmailAddress() { Email = email }
                });
            }

            return (groups, allMails, accounts);
        }
    }
}
