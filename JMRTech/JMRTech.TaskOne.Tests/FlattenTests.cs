using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace JMRTech.TaskOne.Tests
{
    public class FlattenTests
    {
        private string nameRegex => @"^([a-zA-Z0-9]*_[a-zA-Z0-9]*)$";
        
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
                            EmailType = EmailAddress.MainEmail,
                            Email = mainEmail
                        },
                        new EmailAddress()
                        {
                            EmailType = EmailAddress.AlternativeEmail,
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
            result.FirstOrDefault().SanitizedNameWithId.ShouldMatch(nameRegex);
        }

        [Fact]
        public void Flatten_NameWithDiacritics_RemovesDiacritics()
        {
            // Arrange
            var mainEmail = "Andrzej@gmail.com";
            var nameWithDiacritics = "¥ndr¿êj";
            var nameWithoutDiacritics = "Andrzej";

            var testUser = new Person()
            {
                Id = "123123",
                Name = nameWithDiacritics,
                Emails = new List<EmailAddress>()
                    {
                        new EmailAddress()
                        {
                            EmailType = EmailAddress.MainEmail,
                            Email = mainEmail
                        },
                        new EmailAddress()
                        {
                            EmailType = EmailAddress.AlternativeEmail,
                            Email = "Andrzej_2@Gmail.com"
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
            result.FirstOrDefault().SanitizedNameWithId.ShouldContain(nameWithoutDiacritics);
            result.FirstOrDefault().SanitizedNameWithId.ShouldMatch(nameRegex);
        }

        [Fact]
        public void Flatten_NameWithSpecialCharacters_RemovesSpecialCharacters()
        {
            // Arrange
            var mainEmail = "Andrzej@gmail.com";
            var name = "¥ndr¿êj-+=/.,<>;;'[]\\|!@#$%^&*()";
            var correctName = "Andrzej";

            var testUser = new Person()
            {
                Id = "123123",
                Name = name,
                Emails = new List<EmailAddress>()
                    {
                        new EmailAddress()
                        {
                            EmailType = EmailAddress.MainEmail,
                            Email = mainEmail
                        },
                        new EmailAddress()
                        {
                            EmailType = EmailAddress.AlternativeEmail,
                            Email = "Andrzej_2@Gmail.com"
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
            result.FirstOrDefault().SanitizedNameWithId.ShouldContain(correctName);
            result.FirstOrDefault().SanitizedNameWithId.ShouldMatch(nameRegex);
        }
    }
}
