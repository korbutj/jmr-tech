using JMRTech.TaskOne.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace JMRTech.TaskOne
{
    public static class FlattenPeopleExtension
    {

        /// <summary>
        /// Jako, że brakowało trochę danych musiałem przyjąć kilka założeń:
        /// 1) emailType = { "main", "alternative" }
        /// 2) usuwanie znaków diakrytycznych z tekstu jest dosyć tricky
        ///     i spędziłem nad tym trochę czasu w jednym z projektów przy generowaniu emaili
        ///     stąd wiem, że nie obsłużę prawdopodobnie wszystkich przypadków (np. ł - nie jest diakrytyczne w .Netcie)
        ///     (artykuł z którego ostatnio korzystałem na temat usuwania znaków diakrytycznych: http://archives.miloush.net/michkap/archive/2007/05/14/2629747.html)
        /// 3) gdy którakolwiek z danych jest null/pusty string po prostu przypisuje null/pusty string.
        /// 4) założyłem, że emaile oraz id'ki są w poprawnym formacie i nie normalizowałem ich.
        /// 
        /// Odpowiedź:
        /// Takie mapowanie może zostać użyte gdy chcemy pozbyć się problematycznych znaków diakrytycznych, które 
        /// mogę sprawiać problemy przy późniejszej obróbce danych. Konsekwencją usunięcia tych znaków jest oczywiście
        /// utrata części informacji, które mogą być później użyteczne.
        /// </summary>
        public static IEnumerable<PersonWithEmail> Flatten(this IEnumerable<Person> people)
        {
            var flattenedPeople = new List<PersonWithEmail>();

            foreach(var person in people)
            {
                var email = person.Emails?.FirstOrDefault(email => email.EmailType == "main")?.Email;
                var normalizedNameWithId = FlattenName(person);

                var flattenedPerson = new PersonWithEmail()
                {
                    FormattedEmail = email,
                    SanitizedNameWithId = normalizedNameWithId
                };

                flattenedPeople.Add(flattenedPerson);
            }

            return flattenedPeople;
        }

        private static string FlattenName(Person person)
        {
            var name = NormalizeName(person);

            var normalizedNameWithId = $"{name}_{person.Id}";
            return normalizedNameWithId;
        }

        private static string NormalizeName(Person person)
        {
            if (person.Name is null)
                return null;

            var normalizedName = person.Name?.Normalize(NormalizationForm.FormD);
            var name = new string(normalizedName
                .ToCharArray()
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark && Char.IsLetterOrDigit(c))
                .ToArray());

            return name;
        }
    }
}
