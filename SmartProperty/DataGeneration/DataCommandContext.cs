using SmartProperty.DataGeneration.Commands;

namespace SmartProperty.Commands
{
    internal class DataCommandContext : IDataCommandContext
    {
        IDataCommand IDataCommandContext.GetCommand(string command)
        {
            if (command == "date")
            {
                return new DateCommand();
            }
            else if(command == "address")
            {
                return new AddressCommand();
            }
            else if (command == "phone")
            {
                return new PhoneCommand();
            }
            else if (command == "bogus")
            {
                return new BogusCommand();
            }
            else
            {
                throw new ArgumentException($"No command class could be found with the identifier : {command}");
            }
        }
    }
}
