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
message3 = message1 + " " + message2 + "!";

// Yields the same output
Console.WriteLine($"{message1} {message2}!");
Console.WriteLine(message3);
```
??? quote "Output"
	``` text
	Hello World!
	Hello World!
	```

## Contains (Zeichenfolge finden)
Die Methode Contains() findet eine beliebige Zeichenfolge in einem String. Sie gibt TRUE zurück, wenn die Zeichenfolge gefunden wurde, andernfalls FALSE.
``` cs
string s1 = "The quick brown fox jumps over the lazy dog";
string s2 = "fox";
bool b = s1.Contains(s2);
if(b) {
	Console.WriteLine("Found!);
} else {
	Console.WriteLine("Not found!");
}
```

??? quote "Output"
	``` text
	Found!
	```

## Replace (Zeichen ersetzen)
Die Methode Replace() ersetzt die angegebenen Zeichen bzw. Zeichenfolgen im angegebenen String:
``` cs
string str = "1 2 3 4 5 6 7 8 9";
Console.WriteLine($"Original string: \"{str}\"");
string str2 = str.Replace(' ', ',');
Console.WriteLine($"New string:      \"{str}\"");
```
??? quote "Output"
	``` text
	Original string: "1 2 3 4 5 6 7 8 9"
	New string:      "1,2,3,4,5,6,7,8,9"
	```

## Split (String Teilen)
Mit Split() wird ein String am angegebenen Zeichen aufgeteilt. Sie gibt ein Array zurück, das die Teile des Strings ohne das Trennzeichen enthält. Das Trennzeichen wird dabei entfernt.

``` cs title="ToUpper, ToLower"
string s = "You win some. You lose some.";

string[] subs = s.Split(' ');

Console.WriteLine($"Substring: {subs[0]}");
Console.WriteLine($"Substring: {subs[4]}");
Console.WriteLine($"Substring: {subs[5]}");
```

??? quote "Output"
	``` text
	Substring: You<br>
	Substring: lose<br>
	Substring: some.
	```

## ToUpper, ToLower (Groß- und Kleinbuchstaben)
Mit den Methoden ToUpper() und ToLower() wird ein String in Groß- bzw. Kleinbuchstaben umgewandelt:

```` cs title="ToUpper, ToLower"
string karen = "i wAnT tO sPEak wiTh yOuR mAnaGer";

string upperKaren = karen.ToUpper();
string lowerKaren = karen.ToLower();

Console.WriteLine($"Uppercase: {upperKaren}");
Console.WriteLine($"Lowercase: {lowerKaren}");
````

??? quote "Output"
	``` text
	Uppercase: I WANT TO SPEAK WITH YOUR MANAGER
	Lowercase: i want to speak with your manager
	```

## Trim (Überflüssige Leerzeichen entfernen)
Mit Trim() werden Leerzeichen am Anfang bzw. am Ende entfernt. TrimStart() entfernt Leerzeichen vor dem ersten Zeichen, TrimEnd() entfernt Leerzeichen am Ende. Trim() entfernt die Leerzeichen am Anfang und am Ende.

```` cs title="Trim()"
string whiteSpace = "    Teststring    ";

string trimStart = whiteSpace.TrimStart();
string trimEnd = whiteSpace.TrimEnd();
string trim = whiteSpace.Trim();

Console.WriteLine($"TrimStart: \"{trimStart}\"");
Console.WriteLine($"TrimEnd: \"{trimEnd}\"");
Console.WriteLine($"Trim: \"{trim}\"");
````

??? quote "Output"
	``` text
	TrimStart: "Teststring    "
	TrimEnd: "    Teststring"
	Trim: "Teststring"
	```