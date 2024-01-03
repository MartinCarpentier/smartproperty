using SmartProperty.Commands;
using SmartProperty.Parser;
using System.Runtime.CompilerServices;

namespace SmartProperty
{
    public static class SmartPropertyFactory
    {
        public static ISmartPropertyGenerator Create()
        {
            var parser = new SmartPropertyParser();
            var commandContext = new DataCommandContext();

            return new SmartPropertyGenerator(parser, commandContext);
        }
    }
}
