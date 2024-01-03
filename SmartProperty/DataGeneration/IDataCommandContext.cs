namespace SmartProperty.Commands
{
    internal interface IDataCommandContext
    {
        IDataCommand GetCommand(string command);
    }
}
