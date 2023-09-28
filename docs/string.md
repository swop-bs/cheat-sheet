# String

## Initialisieren
``` cs
// Declare without initializing.
string message1;
// Initialize with string
string message2 = "World";
// Set string content
message1 = "Hello";

// String concatination
message3 = message1 + " " + message2;

// Yields the same output
Console.WriteLine($"{message1} {message2}!");
Console.WriteLine(message3 + "!");

// This example produces the following output:
// Hello World!
// Hello World
```

## Zeichenfolge finden (Contains)
Die Methode Contains() findet eine beliebige Zeichenfolge in einem String. Gibt TRUE zurück, wenn die Zeichenfolge gefunden wurde, FALSE sonst.
``` cs
string s1 = "The quick brown fox jumps over the lazy dog";
string s2 = "fox";
bool b = s1.Contains(s2);
if(b) {
	Console.WriteLine("Found!);
} else {
	Console.WriteLine("Not found!");
}

// This example produces the following output:
// Found!
```

## Zeichen ersetzen (Replace)
Die Methode Replace() Ersetzt die angegebenen Zeichen bzw. Zeichenfolgen im angegebenen String:
``` cs
string str = "1 2 3 4 5 6 7 8 9";
Console.WriteLine($"Original string: \"{str}\"");
string str2 = str.Replace(' ', ',');
Console.WriteLine($"New string:      \"{str}\"");

// This example produces the following output:
// Original string: "1 2 3 4 5 6 7 8 9"
// New string:      "1,2,3,4,5,6,7,8,9"
```

## Spring Splitten (Split)

## Upper- und Lowercase
toUpper, ToLower

## Überflüssige Leerzeichen entfernen (Trim)