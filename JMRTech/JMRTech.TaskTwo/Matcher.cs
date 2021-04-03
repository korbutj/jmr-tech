using JMRTech.TaskTwo.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JMRTech.TaskTwo
{
    public static class Matcher
    {
        /// <summary>
        /// Z treści zadania możemy wywnioskować, że jedna osoba może mieć wiele kont,
        /// ale jedna osoba należy TYLKO do jednej grupy, co implikuje, że
        /// wszystkie multi-konta muszą należeć do tej samej grupy
        /// </summary>
        public static IEnumerable<(Account, Person)> MatchPersonToAccount(
            IEnumerable<Group> groups,
            IEnumerable<Account> accounts,
            IEnumerable<string> emails)
        {
            var personAccountMatches = new List<(Account account, Person person)>();

            IEnumerable<Account> acs = accounts.Where(a => emails.Contains(a.EmailAddress.Email));

            var matches = groups
                .SelectMany(g => g.People)
                .Join<Person, Account, IEnumerable<EmailAddress>, (Account, Person)>(
                acs, 
                x => x.Emails.Select(e => e), 
                i => new List<EmailAddress>() { i.EmailAddress }, 
                (p, ac) => (ac, p));
            personAccountMatches.AddRange(matches);

            return personAccountMatches;
        }
    }
}
