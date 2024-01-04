using SmartProperty.DataGeneration.Commands;

namespace SmartProperty.Commands
{
    internal class DataCommandContext : IDataCommandContext
    {
        IDataCommand IDataCommandContext.GetCommand(string command)
        {
            return command switch
            {
                "date" => new DateCommand(),
                "bogus" => new BogusCommand(),
                _ => throw new ArgumentException($"No command class could be found with the identifier : {command}")
            };
        }
    }
}
