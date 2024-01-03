using SmartProperty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SmartProperty.Parser
{
    internal class SmartPropertyParser
    {
        public bool IsSmartProperty(string property)
        {
            // Regex to check if string starts and ends with a [ and a ]
            string smartPropertyRegex = "^\\[[^\\]]*\\]$";
            return Regex.IsMatch(property, smartPropertyRegex);
        }

        public SmartPropertyField Parse(string field)
        {
            if(!IsSmartProperty(field))
            {
                throw new ArgumentException("Field is not a smart property");
            }

            var processedField = TrimField(field);

            if (!IsSmartPropertyCommandMethod(processedField))
            {
                throw new ArgumentException($"Field {processedField} is not in a smart method format");
            }

            string command = GetCommand(processedField);
            string method = GetMethod(processedField);
            List<string> arguments = GetArguments(processedField);

            return new SmartPropertyField(processedField, command, method, arguments);
        }

        string regex = "(?<command>[A-Za-z0-9]+)\\.(?<method>[A-Za-z0-9]+)\\((?<parameters>.*)\\)";
        private string GetCommand(string field)
        {
            var match = Regex.Match(field, regex);

            string command = match.Groups["command"].Value;

            return command;
        }

        private string GetMethod(string field)
        {
            var match = Regex.Match(field, regex);

            string method = match.Groups["method"].Value;

            return method;
        }

        private List<string> GetArguments(string field)
        {
            var match = Regex.Match(field, regex);

            string argumentSection = match.Groups["parameters"].Value;
            if (string.IsNullOrEmpty(argumentSection))
                return new List<string>();

            // This regex can pull each parameter correctly
            List<string> arguments = new List<string>();
            string argument = "";
            bool stringStarted = false;
            char previousCharacter = '\0';
            foreach (var character in argumentSection)
            {
                // Begin next argument if not comma was in a string
                if ((character == ',' && !stringStarted) ||
                    (character == ')' && !stringStarted))
                {
                    string currentArgument = argument;
                    arguments.Add(currentArgument);
                    argument = "";
                    previousCharacter = character;
                    continue;
                }

                // Check if a string and skip adding to arguments
                if (character == '\'')
                {
                    stringStarted = !stringStarted;
                    previousCharacter = character;
                    continue;
                }

                // Check if a string is used in a number
                if (!stringStarted && char.IsLetter(character))
                {
                    throw new ArgumentException("A char was used as an argument which was interpreted as a number");
                }

                previousCharacter = character;
                argument += character;
            }

            arguments.Add(argument);

            return arguments.ToList();
        }

        private string TrimField(string field)
        {
            var builder = new StringBuilder(field);

            if (field.StartsWith("[") && field.EndsWith("]"))
            {
                builder.Remove(0, 1)
                    .Remove(builder.Length - 1, 1);
            }

            var property = builder.ToString();

            string trimmed = "";
            bool isWithinString = false;
            char previousCharacter = '\0';
            foreach(var value in property)
            {
                if(value == '\'' && previousCharacter != '\\')
                {
                    isWithinString = !isWithinString;
                }
                else if(!isWithinString && value == ' ')
                {
                    previousCharacter = value;
                    continue;
                }

                previousCharacter = value;
                if (isWithinString)
                {
                    trimmed += value;
                }
                else
                {
                    trimmed += char.ToLower(value);
                }
            }

            // After finishing the trimming, isWithinString should be false. If it's true, that means the string was not closed property
            if(isWithinString)
            {
                throw new ArgumentException("A string inside of the parameters was not closed");
            }

            return trimmed;
        }

        private bool IsSmartPropertyCommandMethod(string field)
        {
            // Regex to check if string starts and ends with a [ and a ]
            string isSmartCommandMethod = @"(?<smartpropertygroup>[A-Za-z]+\.[A-Za-z]+\(+.*\)+)|(?<textgroup>\'[A-Za-z0-9]*\')|(?<numbergroup>[+-]?([0-9]*[.])?[0-9]+)";

            return Regex.IsMatch(field, isSmartCommandMethod);
        }
    }
}
