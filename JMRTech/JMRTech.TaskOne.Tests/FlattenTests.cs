using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace JMRTech.TaskOne.Tests
{
    public class FlattenTests
    {
        [Fact]
        public void Flatten_SimpleData_ReturnsFlattenedPerson()
        {
            // Arrange
            var mainEmail = "Rachel@Gmail.com";
            var testUser = new Person()
            {
                Id = "123123",
                Name = "Rachel",
                Emails = new List<EmailAddress>()
                    {
                        new EmailAddress()
                        {
                            EmailType = "Main",
                            Email = mainEmail
                        },
                        new EmailAddress()
                        {
                            EmailType = "Alternative",
                            Email = "Rachel_2@Gmail.com"
                        }
                    }
            };

            var testData = new List<Person>()
            {
                testUser
            };

            // Act

            var result = testData.Flatten();

            // Assert
            result.FirstOrDefault().FormattedEmail.ShouldBe(mainEmail);
            result.FirstOrDefault().SanitizedNameWithId.ShouldContain(testUser.Id);
            result.FirstOrDefault().SanitizedNameWithId.ShouldContain(testUser.Name);
        }
    }
}
