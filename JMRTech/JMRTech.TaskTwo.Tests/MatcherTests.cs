using JMRTech.TaskTwo.Models;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using Shouldly;

namespace JMRTech.TaskTwo.Tests
{
    public class MatcherTests
    {

        [Fact]
        public void Match_SimpleData_ReturnsCorrectData()
        {
            // Arrange
            var gf = new DataFactory();
            var data = gf.GetAllData(20);


            // Act
            IEnumerable<(Account a, Person p)> result = Matcher.MatchPersonToAccount(data.groups, data.accounts, data.emails);

            // Assert 
            result.Select(x => x.a.EmailAddress.Email).ShouldBeUnique();
        }
    }
}
