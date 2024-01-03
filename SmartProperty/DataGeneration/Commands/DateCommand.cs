using Bogus;
using SmartProperty.Commands;
using SmartProperty.Models;
using System;

namespace SmartProperty.DataGeneration.Commands
{

    internal class DateCommand : IDataCommand
    {
        Dictionary<string, Func<List<string>, string>> methods = new Dictionary<string, Func<List<string>, string>>();

        public string Name => "Date";

        public DateCommand()
        {
            methods.Add("utcnowwithoffset", UtcNowWithOffset);
            methods.Add("utcnow", UtcNow);
            methods.Add("between", Between);
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

        private string UtcNow(List<string> args)
        {
            if (args.Count > 0)
            {
                throw new ArgumentException("UtcNow method does not have any arguments. Some arguments where supplied.");
            }
            var date = DateTime.UtcNow;
            var result = date.ToString("o", System.Globalization.CultureInfo.InvariantCulture);

            return result;
        }

        private string UtcNowWithOffset(List<string> args)
        {
            if (args.Count > 1 || args.Count == 0)
            {
                throw new ArgumentException("Incorrect number of arguments for UtcNowWithOffset. There must only be one in the format of -1d:2h:30m:20s");
            }

            var timeArg = args[0];
            TimeSpan span = GenerateTimeSpanFromString(timeArg);

            var date = DateTime.UtcNow;
            var withOffset = date.AddSeconds(span.TotalSeconds);
            var result = withOffset.ToString("o", System.Globalization.CultureInfo.InvariantCulture);

            return result;
        }

        private string Between(List<string> args)
        {
            if (args.Count != 2)
            {
                throw new ArgumentException("Between method must have 2 arguments, a From offset and To offset.");
            }

            var fromTimeArg = args[0];
            TimeSpan fromSpan = GenerateTimeSpanFromString(fromTimeArg);
            var toTimeArg = args[1];
            TimeSpan toSpan = GenerateTimeSpanFromString(toTimeArg);

            var date = DateTime.UtcNow;
            var dateFromWithOffset = date.AddSeconds(fromSpan.TotalSeconds);
            var dateToWithOffset = date.AddSeconds(toSpan.TotalSeconds);

            var fakeDate = new Faker().Date.Between(dateFromWithOffset, dateToWithOffset);

            return fakeDate.ToString("o", System.Globalization.CultureInfo.InvariantCulture);
        }

        private static TimeSpan GenerateTimeSpanFromString(string timeArg)
        {
            var timeElements = timeArg.Split(":");

            TimeSpan span = new TimeSpan();
            foreach (var element in timeElements)
            {
                var ToAddSpan = new TimeSpan();
                if (element.Contains("d"))
                {
                    ToAddSpan = TimeSpan.FromDays(int.Parse(element.Replace("d", "")));
                }
                else if (element.Contains("h"))
                {
                    ToAddSpan = TimeSpan.FromHours(int.Parse(element.Replace("h", "")));
                }
                else if (element.Contains("m"))
                {
                    ToAddSpan = TimeSpan.FromMinutes(int.Parse(element.Replace("m", "")));
                }
                else if (element.Contains("s"))
                {
                    ToAddSpan = TimeSpan.FromSeconds(int.Parse(element.Replace("s", "")));
                }

                span = TimeSpan.FromSeconds(ToAddSpan.TotalSeconds + span.TotalSeconds);
            }

            return span;
        }
    }
}
