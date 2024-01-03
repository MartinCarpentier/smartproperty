
using SmartProperty;

// The issue - Same readtime generated each time - WHAT TO DO
// Solution -> Smart properties

// SmartPropertyField
string field = "[date.utcnowwithoffset('-1d')]";

var generator = SmartPropertyFactory.Create();

if (generator.IsValidSmartProperty(field, out var errorMessage))
{
    for (int i = 0; i < 1000; i++)
    {
        var generatedProperty = generator.Generate(field);
        Console.WriteLine(generatedProperty);
    }
}
else
{
    Console.WriteLine(errorMessage);
}

Console.ReadKey();