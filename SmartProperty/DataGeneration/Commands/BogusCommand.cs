using Bogus;
using SmartProperty.Commands;
using SmartProperty.Models;
using System;

namespace SmartProperty.DataGeneration.Commands
{

    internal class BogusCommand : IDataCommand
    {
        Dictionary<string, Func<List<string>, string>> methods = new Dictionary<string, Func<List<string>, string>>();

        public string Name => "Date";

        public BogusCommand()
        {
            methods.Add("parse", Parse);
            methods.Add("parselocale", ParseLocale);
            methods.Add("parselocalewithseed", ParseLocaleWithSeed);
        }

        public string Generate(SmartPropertyField field)
        {
            // Attempt to get a method to invoke
            if (methods.TryGetValue(field.Method, out var methodToInvoke))
            {
                return methodToInvoke.Invoke(field.Arguments);
            }

            throw new ArgumentException($"No method {field.Method} could be found in the command class {field.Command}");
        }

        private string Parse(List<string> args)
        {
            if (args.Count != 1)
            {
                throw new ArgumentException("Parse method must have 1 argument, the string to parse.");
            }

            var toParse = args[0];

            var parsed = new Faker().Parse(toParse);

            return parsed;
        }

        private string ParseLocale(List<string> args)
        {
            if (args.Count != 2)
            {
                throw new ArgumentException("ParseLocale method must have 2 arguments, the string to parse and a locale.");
            }

            var toParse = args[0];
            var locale = args[1];

            var parsed = new Faker(locale).Parse(toParse);

            return parsed;
        }

        private string ParseLocaleWithSeed(List<string> args)
        {
            if (args.Count != 3)
            {
                throw new ArgumentException("ParseLocaleSeed method must have 3 arguments, the string to parse, a locale and a seed.");
            }

            var toParse = args[0];
            var locale = args[1];
            var seed = args[2];

            var randomizer = new Randomizer(int.Parse(seed));
            var faker = new Faker(locale);
            faker.Random = randomizer;
            var parsed = faker.Parse(toParse);

            return parsed;
        }
    }
}