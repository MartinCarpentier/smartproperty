using SmartProperty.Models;

namespace SmartProperty.Commands
{

    internal interface IDataCommand
    {
        public string Generate(SmartPropertyField field);
        public string Name { get; }
    }
}
