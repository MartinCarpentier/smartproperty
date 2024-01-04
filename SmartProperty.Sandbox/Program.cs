
using SmartProperty;

// The issue - Same readtime generated each time - WHAT TO DO
// Solution -> Smart properties

// SmartPropertyField

var generator = SmartPropertyFactory.Create();

string field = "[Bogus.ParseLocaleWithSeed('{{name.firstname(Male)}} {{name.lastname}}', 'en_US', 1241241)]";
string generatedProperty = "";
if (generator.IsValidSmartProperty(field, out var errorMessage))
{
    for (int i = 0; i < 100; i++)
    {
        generatedProperty = generator.Generate(field);
        Console.WriteLine(generatedProperty);
    }
}
else
{
    Console.WriteLine(errorMessage);
}


Console.ReadKey();