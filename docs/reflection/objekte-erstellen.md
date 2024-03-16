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

## Erklärung

- **`Activator.CreateInstance(typeof(Mitarbeiter))`**: Diese Zeile erstellt eine neue Instanz von `Mitarbeiter` unter Verwendung des Standardkonstruktors. Da `CreateInstance` ein Objekt vom Typ `object` zurückgibt, ist ein explizites Casting auf den Typ `Mitarbeiter` notwendig.
  
- **`Activator.CreateInstance(typeof(Mitarbeiter), parameters)`**: Hier wird eine neue Instanz von `Mitarbeiter` erstellt, wobei ein Array von Objekten als Parameter für den Konstruktor übergeben wird. Diese Methode ist nützlich, wenn der zu verwendende Konstruktor Parameter erwartet. Auch hier ist ein Casting erforderlich.

## Vorteile und Überlegungen

Die Verwendung des `Activator` zum Erstellen von Objekten bietet eine flexible Lösung für die dynamische Instanziierung von Klassen zur Laufzeit. Dies kann in Situationen nützlich sein, in denen der exakte Typ eines Objekts erst zur Laufzeit bekannt ist, beispielsweise beim Laden von Plugins oder bei der Implementierung von Fabrikmethoden in Designmustern.

Es ist jedoch wichtig zu beachten, dass der Einsatz von Reflection und insbesondere des `Activator` zur Objekterstellung Overhead verursacht und die Performance beeinträchtigen kann. Deshalb sollte der Einsatz wohlüberlegt und in Performance-kritischen Bereichen vermieden oder minimiert werden.