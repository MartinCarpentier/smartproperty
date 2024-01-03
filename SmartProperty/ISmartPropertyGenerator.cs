using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartProperty
{
    public interface ISmartPropertyGenerator
    {
        /// <summary>
        /// Generate a value from a smart property
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        string Generate(string property);
        /// <summary>
        /// Try to generate a value from a smart property.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        bool TryGenerate(string smartProperty, out string generatedProperty);
        /// <summary>
        /// Check if value is a valid smart property. 
        /// If not, output error message.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        bool IsValidSmartProperty(string property, out string description);
    }
}
