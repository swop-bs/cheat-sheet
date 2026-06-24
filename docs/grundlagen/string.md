# String

## Initialisieren
=== "C#"

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

=== "Java"

    ``` java
    // Deklaration ohne Initialisierung
    String message1;
    // Initialisierung mit String
    String message2 = "World";
    // Inhalt setzen
    message1 = "Hello";

    // Konzatenation
    String message3 = message1 + " " + message2 + "!";

    // Ausgabe
    System.out.println(message1 + " " + message2 + "!");
    System.out.println(message3);
    ```

??? quote "Output"
	``` text
	Hello World!
	Hello World!
	```

## Contains (Zeichenfolge finden)
Die Methode Contains() findet eine beliebige Zeichenfolge in einem String. Sie gibt TRUE zurück, wenn die Zeichenfolge gefunden wurde, andernfalls FALSE.
=== "C#"

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

=== "Java"

    ``` java
    String s1 = "The quick brown fox jumps over the lazy dog";
    String s2 = "fox";
    boolean b = s1.contains(s2); // contains ist kleingeschrieben in Java
    if(b) {
        System.out.println("Found!");
    } else {
        System.out.println("Not found!");
    }
    ```

??? quote "Output"
	``` text
	Found!
	```

## Replace (Zeichen ersetzen)
Die Methode Replace() ersetzt die angegebenen Zeichen bzw. Zeichenfolgen im angegebenen String:
=== "C#"

    ``` cs
    string str = "1 2 3 4 5 6 7 8 9";
    Console.WriteLine($"Original string: \"{str}\"");
    string str2 = str.Replace(' ', ',');
    Console.WriteLine($"New string:      \"{str}\"");
    ```

=== "Java"

    ``` java
    String str = "1 2 3 4 5 6 7 8 9";
    System.out.println("Original string: \"" + str + "\"");
    String str2 = str.replace(' ', ',');
    System.out.println("New string:      \"" + str2 + "\""); 
    // Achtung: Strings sind immutable, str bleibt unverändert, str2 ist neu.
    // Im C# Beispiel oben wurde 'str' ausgegeben statt 'str2', was ein Fehler sein könnte wenn die Änderung gezeigt werden soll.
    ```

??? quote "Output"
	``` text
	Original string: "1 2 3 4 5 6 7 8 9"
	New string:      "1,2,3,4,5,6,7,8,9"
	```

## Split (String Teilen)
Mit Split() wird ein String am angegebenen Zeichen aufgeteilt. Sie gibt ein Array zurück, das die Teile des Strings ohne das Trennzeichen enthält. Das Trennzeichen wird dabei entfernt.

=== "C#"

    ``` cs title="ToUpper, ToLower"
    string s = "You win some. You lose some.";

    string[] subs = s.Split(' ');

    Console.WriteLine($"Substring: {subs[0]}");
    Console.WriteLine($"Substring: {subs[4]}");
    Console.WriteLine($"Substring: {subs[5]}");
    ```

=== "Java"

    ``` java
    String s = "You win some. You lose some.";

    String[] subs = s.split(" "); // split erwartet Regex/String, nicht char

    System.out.println("Substring: " + subs[0]);
    System.out.println("Substring: " + subs[4]);
    System.out.println("Substring: " + subs[5]);
    ```

??? quote "Output"
	``` text
	Substring: You<br>
	Substring: lose<br>
	Substring: some.
	```

## ToUpper, ToLower (Groß- und Kleinbuchstaben)
Mit den Methoden ToUpper() und ToLower() wird ein String in Groß- bzw. Kleinbuchstaben umgewandelt:

=== "C#"

    ```` cs title="ToUpper, ToLower"
    string karen = "i wAnT tO sPEak wiTh yOuR mAnaGer";

    string upperKaren = karen.ToUpper();
    string lowerKaren = karen.ToLower();

    Console.WriteLine($"Uppercase: {upperKaren}");
    Console.WriteLine($"Lowercase: {lowerKaren}");
    ````

=== "Java"

    ``` java
    String karen = "i wAnT tO sPEak wiTh yOuR mAnaGer";

    String upperKaren = karen.toUpperCase();
    String lowerKaren = karen.toLowerCase();

    System.out.println("Uppercase: " + upperKaren);
    System.out.println("Lowercase: " + lowerKaren);
    ```

??? quote "Output"
	``` text
	Uppercase: I WANT TO SPEAK WITH YOUR MANAGER
	Lowercase: i want to speak with your manager
	```

## Trim (Überflüssige Leerzeichen entfernen)
Mit Trim() werden Leerzeichen am Anfang bzw. am Ende entfernt. TrimStart() entfernt Leerzeichen vor dem ersten Zeichen, TrimEnd() entfernt Leerzeichen am Ende. Trim() entfernt die Leerzeichen am Anfang und am Ende.

=== "C#"

    ```` cs title="Trim()"
    string whiteSpace = "    Teststring    ";

    string trimStart = whiteSpace.TrimStart();
    string trimEnd = whiteSpace.TrimEnd();
    string trim = whiteSpace.Trim();

    Console.WriteLine($"TrimStart: \"{trimStart}\"");
    Console.WriteLine($"TrimEnd: \"{trimEnd}\"");
    Console.WriteLine($"Trim: \"{trim}\"");
    ````

=== "Java"

    ``` java
    String whiteSpace = "    Teststring    ";

    // Java 11+ kennt strip(), stripLeading(), stripTrailing()
    // Älteres Java kennt nur trim() (beidseitig)

    String trimStart = whiteSpace.stripLeading();
    String trimEnd = whiteSpace.stripTrailing();
    String trim = whiteSpace.trim(); // oder strip() ab Java 11

    System.out.println("TrimStart: \"" + trimStart + "\"");
    System.out.println("TrimEnd: \"" + trimEnd + "\"");
    System.out.println("Trim: \"" + trim + "\"");
    ```

??? quote "Output"
	``` text
	TrimStart: "Teststring    "
	TrimEnd: "    Teststring"
	Trim: "Teststring"
	```
