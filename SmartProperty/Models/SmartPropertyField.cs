using System.Text;
using System.Text.RegularExpressions;

namespace SmartProperty.Models
{
    public class SmartPropertyField
    {
        private string _command = string.Empty;
        private string _method = string.Empty;
        private List<string> _arguments = new List<string>();
        private string _field = string.Empty;

        public SmartPropertyField(string field, 
            string command,
            string method,
            List<string> arguments)
        {
            _command = command;
            _method = method;
            _arguments = arguments;
            _field = field;
        }

        //private SmartPropertyField(string field)
        //{
        //    _command = "";
        //    _method = "";
        //    _arguments = new List<SmartPropertyField>();
        //    _field = field;
        //    _isCommandMethodField = false;
        //}

        public string Command => _command;
        public string Method => _method;
        public string Field => _field;
        public List<string> Arguments => _arguments;
    }
}
