using SmartProperty.Commands;
using SmartProperty.Models;
using SmartProperty.Parser;
using System.Text.RegularExpressions;

namespace SmartProperty
{
    internal class SmartPropertyGenerator : ISmartPropertyGenerator
    {
        private readonly SmartPropertyParser parser;
        private readonly IDataCommandContext commandContext;

        public SmartPropertyGenerator(SmartPropertyParser parser,
            IDataCommandContext commandContext)
        {
            this.parser = parser;
            this.commandContext = commandContext;
        }

        public string Generate(string field)
        {
            if(!parser.IsSmartProperty(field))
            {
                throw new ArgumentException("argument is not a smart property");
            }

            // Parse to three structure of smart properties/ arguments
            var property = parser.Parse(field);

            var command = commandContext.GetCommand(property.Command);

            var value = command.Generate(property);

            return value;
        }

        public bool TryGenerate(string smartProperty, out string generatedProperty)
        {
            generatedProperty = "";
            try
            {
                generatedProperty = Generate(smartProperty);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if the smart property is valid by attempting to generate it and catch potential errors
        /// </summary>
        /// <param name="property"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool IsValidSmartProperty(string property, out string errorMessage)
        {
            errorMessage = "";
            try
            {
                var result = Generate(property);
                return true;
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}
