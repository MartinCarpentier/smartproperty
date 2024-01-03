# Introduction
A simple tool for generating fake date from a simple string.

The idea was to use this for data simulation purposes, where you want to create a dataset through an api.
The first idea was to just use the bogus parse api to do this, but bogus did not support all that i wanted to do in terms of date generation, and i didn't want this api to be locked on Bogus.
The idea os the smart property is, that there is a smart command (Bogus, Date etc.) and methods connected to these.
SmartProperties are not case sensitive.

## Quickstart
The SmartProperty library is extremely easy to use.

First you must use the smart property factory to create a SmartPropertyGenerator instance.

``` csharp
var generator = SmartPropertyFactory.Create();
```

The generator can then be used to generate fake data from a string like so.

``` csharp
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
```

You can also use the TryGenerate if you want a failsafe way to generate date, but this method will swallow potential errors.
The recommented approach is using the IsValidSmartProperty together with the generate method.

### Locales
Some methods support setting Bogus locales.
These are locales which ensures that the data generated is generated in a way that is the standard within a certain area.
Supported locales can be seen on the bogus locales list here:

https://github.com/bchavez/Bogus?tab=readme-ov-file#locales

# Commands and Methods

- Bogus
- Date

Below these commands will be shown, and which methods that can be called aswell

## Bogus

### Parse('ParsableString')
Simple Bogus parse handlebars, which supports the full dataset from Bogus. You can read more about Bogus Parse Handlebars here: https://github.com/bchavez/Bogus?tab=readme-ov-file#parse-handlebars

**Example**:
``` csharp
string field = "[Bogus.Parse('{{Name.FirstName(Male)}} {{Name.LastName}} {{Name.Suffix}}')]";
```

### ParseLocale('ParsableString', 'en_US')
The same as parse, but where a fake bogus data locale can also be specified.

**Example**:
``` csharp
string field = "[Bogus.ParseLocale('{{Name.FirstName(Male)}} {{Name.LastName}} {{Name.Suffix}}', 'en_US')]";
```

### ParseLocaleWithSeed('ParsableString', 'en_US', seedasaninteger)
The same as parselocale, but where you can also specify a seed. This method is somewhat dangerous, since it will generate the same fake dataset every single time it is called with the same parameters.

**Example**:
``` csharp
string field = "[Bogus.ParseLocaleWithSeed('{{Name.FirstName(Male)}} {{Name.LastName}} {{Name.Suffix}}', 'en_US', 1241241)]";
```

## Date

### UtcNow()
Generates an ISO_8601 compliant date from the current utc timestamp

**Example**:
``` csharp
string field = "[Date.UtcNow()]";
```

### UtcNowWithOffset('OffsetHere')
Generates an ISO_8601 compliant date from the current utc timestamp offset by the specified amount.
The format for specifying offset is like so.

**Example**:

``` csharp
string yesterday = "-1d";
string twoHoursAgo = "-2h";
string fullOffset = "-2d:-12h:-30m:40s";

string field = "[Date.UtcNowWithOffset('-2d:-12h:-30m:40s')]";
```

#### Between('FromOffset', 'ToOffset')
Between generates a random date within 2 specified offsets, using the same format as explained in the UtcNowWithOffset method.

The example below generates a datetime the is at most 1 day ago, and at best 2 hours old offset from the current utc timestamp.

**Example**:
``` csharp
string field = "[Date.Between('-1d', '-2h')]";
```