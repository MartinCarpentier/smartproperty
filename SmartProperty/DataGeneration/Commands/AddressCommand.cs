using Bogus;
using SmartProperty.Commands;
using SmartProperty.Models;

namespace SmartProperty.DataGeneration.Commands
{

    internal class AddressCommand : IDataCommand
    {
        Dictionary<string, Func<List<string>, string>> methods = new Dictionary<string, Func<List<string>, string>>();

        public string Name => "Address";

        public AddressCommand()
        {
            methods.Add("zipcodelocale", ZipCodeLocale);
            methods.Add("fulladdresslocale", FullAddressLocale);
            methods.Add("fulladdress", FullAddress);
            methods.Add("fulladdresswithseed", FullAddressWithSeed);
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

        private string ZipCodeLocale(List<string> args)
        {
            if (args.Count != 1)
            {
                throw new ArgumentException("Incorrect number of arguments for ZipCodeLocale. There must only be one in the format of the current locale");
            }

            var locale = args[0];
            var faker = new Faker(locale);
            var zipcode = faker.Address.ZipCode();

            return zipcode;
        }

        private string FullAddress(List<string> args)
        {
            if (args.Count != 0)
            {
                throw new ArgumentException("FullAddress method does not have any arguments. Some arguments where supplied.");
            }

            var faker = new Faker();
            var address = faker.Address.FullAddress();

            return address;
        }

        private string FullAddressWithSeed(List<string> args)
        {
            if (args.Count != 1)
            {
                throw new ArgumentException("FullAddressWithSeed method only have one argument, which is a seed value. Incorrect amount of values supplied.");
            }

            var seed = args[0];
            var faker = new Faker();
            faker.Address.Random = new Randomizer(int.Parse(seed));
            var address = faker.Address.FullAddress();

            return address;
        }

        private string FullAddressLocale(List<string> args)
        {
            if (args.Count != 1)
            {
                throw new ArgumentException("Incorrect number of arguments for ZipCodeLocale. There must only be one in the format of the current locale");
            }

            var locale = args[0];
            var faker = new Faker(locale);
            var address = faker.Address.FullAddress();

            return address;
        }

    }
}
