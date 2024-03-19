# Objekte zur Laufzeit erstellen

Ein zentraler Aspekt von Reflection in C# ist die Fähigkeit, Objekte zur Laufzeit zu erstellen. Diese Fähigkeit ist besonders nützlich in Szenarien, in denen Typen dynamisch zur Laufzeit geladen oder instanziiert werden müssen, ohne dass sie zur Kompilierungszeit bekannt sind. Ein häufig verwendetes Werkzeug dafür ist die Klasse `Activator`, die Teil des .NET Frameworks ist.

## Verwendung des Activators

Die `Activator`-Klasse bietet mehrere Methoden, um Instanzen von Typen zur Laufzeit zu erstellen. Eine der gebräuchlichsten Methoden ist `CreateInstance`. Diese Methode hat mehrere Überladungen, die es ermöglichen, Objekte auf verschiedene Arten zu instanziieren, unter anderem durch Angabe des Typs, der Parameter für den Konstruktor und sogar durch Angabe des Assemblynamens, in dem sich der Typ befindet.

Um ein Objekt vom Typ `Mitarbeiter` zur Laufzeit zu erstellen, können wir folgenden Ansatz verfolgen:

```csharp
// Erstellen eines Objekts vom Typ Mitarbeiter ohne Parameter (nutzt den Standardkonstruktor)
Mitarbeiter mitarbeiter1 = (Mitarbeiter)Activator.CreateInstance(typeof(Mitarbeiter));
// Initialisierung der Attribute für mitarbeiter1
mitarbeiter1.Vorname = "Anna";
mitarbeiter1.Nachname = "Müller";
mitarbeiter1.Gehalt = 40000.0;
mitarbeiter1.Arbeitsort = "Hamburg";

// Erstellen eines Objekts vom Typ Mitarbeiter mit Parametern für den Konstruktor
object[] parameters = { "Max", "Mustermann", 50000.0, "Berlin" };
Mitarbeiter mitarbeiter2 = (Mitarbeiter)Activator.CreateInstance(typeof(Mitarbeiter), parameters);

// Ausgabe zur Demonstration
mitarbeiter1.AnzeigenInformationen();
mitarbeiter2.AnzeigenInformationen();
```

??? quote "Output"
	``` text
    Vorname: Anna, Nachname: Müller, Gehalt: 40000, Arbeitsort: Hamburg
    Vorname: Max, Nachname: Mustermann, Gehalt: 50000, Arbeitsort: Berlin
	```

### Erklärung

- **`Activator.CreateInstance(typeof(Mitarbeiter))`**: Diese Zeile erstellt eine neue Instanz von `Mitarbeiter` unter Verwendung des Standardkonstruktors. Da `CreateInstance` ein Objekt vom Typ `object` zurückgibt, ist ein explizites Casting auf den Typ `Mitarbeiter` notwendig.
  
- **`Activator.CreateInstance(typeof(Mitarbeiter), parameters)`**: Hier wird eine neue Instanz von `Mitarbeiter` erstellt, wobei ein Array von Objekten als Parameter für den Konstruktor übergeben wird. Diese Methode ist nützlich, wenn der zu verwendende Konstruktor Parameter erwartet. Auch hier ist ein Casting erforderlich.

### Vorteile und Überlegungen

Die Verwendung des `Activator` zum Erstellen von Objekten bietet eine flexible Lösung für die dynamische Instanziierung von Klassen zur Laufzeit. Dies kann in Situationen nützlich sein, in denen der exakte Typ eines Objekts erst zur Laufzeit bekannt ist, beispielsweise beim Laden von Plugins oder bei der Implementierung von Fabrikmethoden in Designmustern.

Es ist jedoch wichtig zu beachten, dass der Einsatz von Reflection und insbesondere des `Activator` zur Objekterstellung Overhead verursacht und die Performance beeinträchtigen kann. Deshalb sollte der Einsatz wohlüberlegt und in Performance-kritischen Bereichen vermieden oder minimiert werden.

## Dynamische Wertzuweisung zu Properties von Objekten

In Szenarien, wo die Properties eines Objekts zur Laufzeit nicht bekannt sind, bietet Reflection in C# die Möglichkeit, dynamisch auf diese Properties zuzugreifen und ihnen Werte zuzuweisen. Dies kann besonders nützlich sein, wenn man mit Objekten arbeitet, deren Struktur erst zur Laufzeit bestimmt wird, wie es beispielsweise bei der dynamischen Erzeugung von Objekten aus Datenbankabfragen oder bei der Deserialisierung von JSON-Objekten der Fall ist.

Um dieses Konzept zu demonstrieren, verwenden wir wieder die `Mitarbeiter`-Klasse. Wir zeigen, wie man die Properties eines `Mitarbeiter`-Objekts dynamisch abfragen und mit Standardwerten initialisieren kann.

### Dynamische Initialisierung von Properties

Der Schlüssel zur dynamischen Arbeit mit Properties ist die Verwendung der `GetProperty`- und `SetValue`-Methoden des `Type`-Objekts, die über Reflection zugänglich sind. Hier ist ein grundlegendes Beispiel, das die Properties mit Standardwerten belegt:

```csharp
// Erstellen eines Objekts vom Typ Mitarbeiter zur Laufzeit
Mitarbeiter mitarbeiter = (Mitarbeiter)Activator.CreateInstance(typeof(Mitarbeiter));

// Dynamische Initialisierung von Properties
PropertyInfo[] properties = typeof(Mitarbeiter).GetProperties();
foreach (var property in properties)
{
    if (property.PropertyType == typeof(string))
    {
        property.SetValue(mitarbeiter, "Standardwert");
    }
    else if (property.PropertyType == typeof(double))
    {
        property.SetValue(mitarbeiter, 0.0);
    }
    // Fügen Sie hier weitere Typüberprüfungen und -zuweisungen hinzu, je nach Bedarf
}

// Ausgabe zur Demonstration der dynamischen Initialisierung
mitarbeiter.AnzeigenInformationen();
```

??? quote "Output"
	``` text
    Vorname: Standardwert, Nachname: Standardwert, Gehalt: 0, Arbeitsort: Standardwert
	```

### Erklärung

- Zunächst wird ein `Mitarbeiter`-Objekt zur Laufzeit erstellt.
- Anschließend wird das `Type`-Objekt des `Mitarbeiter`-Typs verwendet, um alle öffentlichen Properties zu erhalten.
- Für jede Property wird überprüft, welchen Typ sie hat (`string`, `double` usw.), und es wird ein entsprechender Standardwert zugewiesen. Im Beispiel wird allen `string`-Properties der Wert `"Standardwert"` und allen `double`-Properties der Wert `0.0` zugewiesen.
- Schließlich wird eine Methode des Objekts aufgerufen, um die Ergebnisse der dynamischen Property-Zuweisungen zu überprüfen.

### Wichtig zu beachten

- Diese Methode der dynamischen Wertzuweisung ist sehr flexibel, kann aber auch fehleranfällig sein, wenn die Typen der Properties nicht korrekt gehandhabt werden.
- Reflection kann die Performance beeinträchtigen, daher sollte sie mit Bedacht eingesetzt werden, insbesondere in leistungskritischen Anwendungen.
- Die Sicherheit des Zugriffs auf Properties sollte ebenfalls berücksichtigt werden, da die Verwendung von Reflection potenziell unsichere oder unerwünschte Zugriffe auf Objekte ermöglichen kann.

Diese Technik ermöglicht es Entwicklern, sehr dynamisch mit Objekten zu arbeiten, deren Struktur im Voraus nicht vollständig bekannt ist, und bietet eine mächtige Methode zur Laufzeitmanipulation und -initialisierung von Objekten in C#.