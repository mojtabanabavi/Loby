# loby
Lobby is an open-source and lightweight library for .Net that includes a set of tools and extensions for easy and fast development.

## Installation package (.Net Core ≥ 3.1)
```shell
PM> Install-Package Loby
```

## Features

### Tools For
- Converting types
- Serializing and Deserializing
- Validating
- Pluralizing and Singularizing words
- Hashing
- Mailing
- Converting date
- Randomizing

### Extensions For
- StringExtensions
- ObjectExtensions
- IdentityExtensions
- EnumExtensions
- MathExtensions
- EnumerableExtensions

## Samples

### Loby.Serializer
Provides functionality to serialize objects or value types to JSON, XML and to deserialize JSON, XML into objects or value types.
```csharp
    var data = new string[] { "a", "b", "c" };

    var toJsonData = Serializer.ToJson(data);
    var fromJsonData = Serializer.FromJson<string[]>(toJsonData);

    var toXmlData = Serializer.ToXml(data);
    var fromXmlData = Serializer.FromXml<string[]>(toXmlData);
```
### Loby.Validator
Includes a set of practical methods for validation.
```csharp
    var input = "http://www.jackwill.com";

    bool isValidUrl = Validator.IsValidUrl(input);
    bool isValidEmail = Validator.IsValidEmail(input);
```
### Loby.Pluralizer
Includes a set of practical methods for pluralizing and singularizing words (english only).
```csharp
    var pluralWord = "people";
    var singularWord = "person";

    string pluralizedWord = Pluralizer.Pluralize(singularWord);
    string singularizeWord = Pluralizer.Singularize(pluralWord);
```
### Loby.PasswordHasher
An implementation of PBKDF2 hash algorithm for hashing and verifing passwords.
```csharp
    var password = "p4$$w0rd";

    string hashedPassword = PasswordHasher.Hash(password);
    bool verifyingHashResult = PasswordHasher.Verify(password, hashedPassword);
```
### Loby.Mailer
Allows applications to send email by using the Simple Mail Transfer Protocol (SMTP).
```csharp
    // custom smtp server
    var mailer_1 = new Mailer("smtp.gmail.com", 587, "username", "password");

    // predefined smtp servers
    var mailer_2 = new Mailer(Mailer.ClientTypes.Gmail, "username", "password");

    // sending email
    mailer_1.Send(recipient: "test@gmail.com", subject: "my subject", body: "my text");
    mailer_1.SendAsync(recipient: "test@gmail.com", subject: "my subject", body: "my text");

    var recipients = new string[] { "test1@gmail.com", "test2@gmail.com" };

    // sending email
    mailer_2.Send(recipients: recipients, subject: "my subject", body: "my text");
    mailer_2.SendAsync(recipients: recipients, subject: "my subject", body: "my text");
```
### Loby.Dater
A set of methods for date conversions along with other practical methods.
```csharp
    var dateTime = DateTime.Now;

    // Iranian Date
    var toIranianDate = Dater.ToIranSolar(dateTime, format: "yyyy MMMM dd");
    var fromIranianDate = Dater.FromIranSolar(toIranianDate);

    // Afghanistanian Date
    var toAfghanistanianDate = Dater.ToAfghanistanSolar(dateTime, format: "yyyy MMMM dd");
    var fromAfghanistanianDate = Dater.FromAfghanistanSolar(toAfghanistanianDate);

    // Custom - based on culture name
    var toCanadaDate = Dater.ToSolar(dateTime, format: "yyyy/MMMM/dd", culture: "ca");
    var fromCanadaDate = Dater.FromSolar(toCanadaDate, culture: "ca");
```
### Loby.Randomizer
Represents a pseudo-random data generator, which is an algorithm that produces a sequence of data that meet certain statistical requirements for randomness.
```csharp
    // Random byte
    byte randomByte = Randomizer.RandomByte();
    byte[] randomBytes = Randomizer.RandomBytes(8);

    // Rnadom int32
    int randomInt_1 = Randomizer.RandomInt();
    int randomInt_2 = Randomizer.RandomInt(maxValue: 100);
    int randomInt_3 = Randomizer.RandomInt(minValue: 10, maxValue: 100);

    // Random int64
    long randomLong_1 = Randomizer.RandomLong();
    long randomLong_2 = Randomizer.RandomLong(maxValue: 100);
    long randomLong_3 = Randomizer.RandomLong(minValue: 10, maxValue: 100);

    // Random float
    float randomFloat = Randomizer.RandomFloat();

    // Random double
    double randomDouble = Randomizer.RandomDouble();

    // Rnadom bool
    bool randomBool = Randomizer.RandomBool();

    // Random color
    Color randomColor = Randomizer.RandomColor();

    // Random date time
    DateTime now = DateTime.Now;

    DateTime randomDate_1 = Randomizer.RandomDateTime();
    DateTime randomDate_2 = Randomizer.RandomDateTime(fromDate: now.AddDays(-10), toDate: now);

    // Random GUID
    string guid = Randomizer.RandomGuid();

    // Random AlphaNmerics
    string alphaNmerics = Randomizer.RandomAlphaNmeric(10);

    // Random Word(s)
    string randomWord = Randomizer.RandomWord();
    string randomWords_1 = Randomizer.RandomWords(count: 2);
    string randomWords_2 = Randomizer.RandomWords(minCount: 3, maxCount: 5);
            
    // Plus too many other methods (explore).
```