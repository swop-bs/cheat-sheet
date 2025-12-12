# Lesen und Schreiben von Dateien

In diesem Abschnitt wird beschrieben, wie Sie mit Hilfe der Objekte StreamReader und StreamWriter Dateien lesen bzw. schreiben können.

## Dateien öffnen und schließen

Um eine Datei zu lesen bzw. zu schreiben muss diese zuerst geöffnet werden.

Ist eine Datei geöffnet, kann Sie (je nach Verwendungszweck) eventuell nicht von anderen Prozessen verwendet werden.

Um die Datei für andere Prozesse wieder frei zu geben, muss der Stream bzw. die Datei wieder geschlossen werden:

### Close()
``` cs
// StreamReader initialisieren (Datei öffnen) (1)
StreamReader sr = new StreamReader("TestDatei.txt");

// StreamReader wird in diesem Abschnitt verwendet

// Datei freigeben (2)
sr.Close();
```

1. Beim Erstellen des StreamReader bzw. StreamWriter-Objekts wird die Datei TestDatei.txt geöffnet.
2. Close() schließt die Datei und gibt sie somit wieder frei.

### using
Da das Schließen der Datei oft vergessen wird, kann alternativ `using` verwendet werden:

``` cs
// StreamReader initialisieren (1)
using (StreamReader sr = new StreamReader("TestDatei.txt"))
{
	// StreamReader wird in diesem Codeblock verwendet
} 
// Datei wird automatisch freigegeben (2)
```

1. Beim Erstellen des StreamReader bzw. StreamWriter-Objkets wird die Datei TestDatei.txt geöffnet.
2. Beim Verlassen des Codeblocks wird der StreamReader automatisch geschlossen. Kein explizites aufrufen von Close() nötig.

## StreamReader (Datei lesen)

In der Regel werden Dateien zeilenweise mittels ReadLine() gelesen:

```csharp
// StreamReader Initialisieren (1)
using (StreamReader sr = new StreamReader("TestDatei.txt"))
{ 
	// Erste Zeile einlesen (2)
	string line = sr.ReadLine();
    // Solange es etwas zu lesen gibt
    while (line != null)
    {
        // Verarbeiten der Zeile (3)
        Console.WriteLine(line);
        // Einlesen der nächsten Zeile (4)
        line = sr.ReadLine();
    }
}

```

!!! info

	1. Das [`using`-Keyword](#using) stellt sicher, dass der StreamReader wieder geschlossen wird
	1. Hier wird line mit der ersten Zeile von TestDatei.txt initialisiert
	1. Das "Verarbeiten" ist hier eine einfach Konsolenausgabe. Natürlich kann hier alles erdenkliche mit der Zeile passieren.
	1. Hier wird die nächste Zeile eingelesen. Dieser Schritt ist in der Regel immer der letzte Schritt in der Schleife, da danach wieder geprüft werden muss, ob es noch etwas einzulesen gibt.


### Methoden

| Methode                                                                                                                                                                                  | Erklärung                                                                                                                                                                                                   |
| ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| [Close()](https://learn.microsoft.com/en-us/dotnet/api/system.io.streamreader.close?view=net-7.0#system-io-streamreader-close)                                                           | Closes the StreamReader object and the underlying stream, and releases any system resources associated with the reader. |
| [Peek()](https://learn.microsoft.com/en-us/dotnet/api/system.io.streamreader.peek?view=net-7.0#system-io-streamreader-peek)                                                              | Returns the next available character but does not consume it. Calling this method multiple times will always return the same character (as Opposed to Read()).                                                                                                                                               |
| [Read()](https://learn.microsoft.com/en-us/dotnet/api/system.io.streamreader.read?view=net-7.0#system-io-streamreader-read)                                                              | Reads the next character from the input stream and advances the character position by one character.                                                                                                        |
| [Read(Char[], Int32, Int32)](https://learn.microsoft.com/en-us/dotnet/api/system.io.streamreader.read?view=net-7.0#system-io-streamreader-read(system-char()-system-int32-system-int32)) | Reads a specified maximum of characters from the current stream into a buffer, beginning at the specified index.                                                                                            |
| [ReadLine()](https://learn.microsoft.com/en-us/dotnet/api/system.io.streamreader.readline?view=net-7.0#system-io-streamreader-readline)                                                  | Reads a line of characters from the current stream and returns the data as a string.                                                                                                                        |

## StreamWriter (Datei schreiben)

In der Regel werden Dateien zeilenweise mittels WriteLine() geschrieben:

```csharp
// StreamWriter Initialisieren (1)
using (StreamWriter sw = new StreamWriter("TestDatei.txt"))
{
	// Schreibe erste Zeile in TestDatei.txt
	sw.WriteLine("Hello World!!");
	// Schreibe zweite Zeile in TestDatei.txt
	sw.WriteLine("From the StreamWriter class");
} 
```

!!! info

	[`using`](#using) stellt sicher, dass die Datei wieder freigegeben wird.

### Methoden

| Methode                                                                                                                                             | Erklärung                                                         |
| --------------------------------------------------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------- |
| [Close()](https://learn.microsoft.com/en-us/dotnet/api/system.io.streamwriter.close?view=net-7.0#system-io-streamwriter-close)                      | Closes the current StreamWriter object and the underlying stream. |
| [Write(String)](https://learn.microsoft.com/en-us/dotnet/api/system.io.streamwriter.write?view=net-7.0#system-io-streamwriter-write(system-string)) | Writes a string to the stream.                                    |
| [WriteLine()](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeline?view=net-7.0#system-io-textwriter-writeline)              | Writes a line terminator to the text stream.                      |

## Beispiel

In diesem Beispiel wird ein Text einer Datei in Großbuchstaben umgewandlet:

```csharp
class Program
{
	static void main()
	{
		// Streams initialisieren (1)
		using (StreamReader sr = new StreamReader("kleinBuchstaben.txt"))
		using (StreamWriter sw = new StreamWriter("grossBuchstaben.txt"))
		{
			// Erste Zeile lesen
			string zeile = sr.ReadLine();
			// Solange es etwas zu lesen gibt
			while (zeile != null)
			{
				// Inhalt der Zeile groß schreiben
				string zeileGross = zeile.ToUpper();
				// Grossgeschriebene Zeile in Textdatei schreiben
				sw.WriteLine(zeileGross);
				// Nächste Zeile lesen
				zeile = sr.ReadLine();
			}
		}
		// Streams werden automatisch geschlossen
	}
}

```

!!! info
	using kann auch hintereinander geschrieben werden, um mehrere Objekte zu verwalten.