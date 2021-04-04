using System;
using System.Collections.Generic;
using System.Linq;

namespace JMTech.TaskThree
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new List<IEnumerable<string>>()
            {
                new List<string>()
                {
                    ":", ";", "."
                },
                new List<string>()
                {
                    ":", ";", ".", ";", ".", ";", ".", ";", "."
                },
                new List<string>()
                {
                    ":", ";", ".", ";", ".", ";", ".", ";", ".", ";", ".", ";", "."
                },
                new List<string>()
                {
                    ":", ";", "."
                },
            };

            Console.WriteLine(OnlyBigCollections(test).Count());
        }

        public static IEnumerable<IEnumerable<string>> 
            OnlyBigCollections(List<IEnumerable<string>> toFilter)
        {
            Func<IEnumerable<string>, bool> predicate =
                    list => 
                    {
                        var enumerator = list.GetEnumerator();
                        for (int i = 0; i < 6; i++) 
                        {
                            if(!enumerator.MoveNext())
                            {
                                return false;
                            }
                        }
                        return true;
                    };

            return toFilter.Where(predicate);
        }

    }
}
