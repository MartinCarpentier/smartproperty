using Bogus;
using SmartProperty.Commands;
using SmartProperty.Models;
using static Bogus.DataSets.Name;

namespace SmartProperty.DataGeneration.Commands
{

    internal class PhoneCommand : IDataCommand
    {
        Dictionary<string, Func<List<string>, string>> methods = new Dictionary<string, Func<List<string>, string>>();

        public string Name => "Phone";

        public PhoneCommand()
        {
            methods.Add("phonenumber", PhoneNumber);
            methods.Add("phonenumberlocale", PhoneNumberLocale);
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

        private string PhoneNumber(List<string> args)
        {
            if (args.Count > 0)
            {
                throw new ArgumentException("PhoneNumber method does not have any arguments. Some arguments where supplied.");
            }
            var number = new Faker().Phone.PhoneNumber();
            return number;
        }

        private string PhoneNumberLocale(List<string> args)
        {
            if (args.Count != 1)
            {
                throw new ArgumentException("Incorrect number of arguments for PhoneNumberFormat. There must only be one in the format of the current locale");
            }

            var locale = args[0];
            var faker = new Faker();
            faker.Phone.Locale = locale;
            var number = faker.Phone.PhoneNumber();

            return number;
        }

    }
}
