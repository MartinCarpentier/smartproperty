
using SmartProperty;

// The issue - Same readtime generated each time - WHAT TO DO
// Solution -> Smart properties

// SmartPropertyField

var generator = SmartPropertyFactory.Create();

string field = "[bogus.parse('{{Name.FirstName}}')]";
string generatedProperty = "";
if (generator.IsValidSmartProperty(field, out var errorMessage))
{
    generatedProperty = generator.Generate(field);
}
else
{
    Console.WriteLine(errorMessage);
}

Console.WriteLine(generatedProperty);

Console.ReadKey();