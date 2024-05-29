# Übersicht über das Projekt

In diesem Abschnitt geben wir eine Übersicht über das vorgefertigte Projekt, das wir für die Kursteilnehmer vorbereitet haben. Da wir das Spiel in der Programmiersprache C# erstellen und nicht die Godot-Oberfläche verwenden, konzentrieren wir uns auf den vorhandenen Code und dessen Struktur.

## Projektstruktur

Das Projekt enthält aktuell eine Datei namens `Root.cs`. Der Inhalt dieser Datei ist wie folgt:

```csharp
using Godot;

public partial class Root : Node2D
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
```

## Erklärung der Klasse `Root`

Die Klasse `Root` erbt von `Node2D`, einer von Godot vordefinierten Klasse. Godot bietet viele vordefinierte Klassen, von denen geerbt werden kann, um verschiedene Funktionalitäten im Spiel zu implementieren.

### Wichtige Eigenschaften von `Node2D`

- **Position**: Die Position der Node im Raum.
- **Rotation**: Die Rotation der Node.
- **Scale**: Die Skalierung der Node.
- **Modulate**: Definiert die Farbe der Node.

### Methoden der Klasse `Root`

- **_Ready()**: Diese Methode wird aufgerufen, sobald das Objekt das erste Mal verarbeitet wird. Sie eignet sich für Initialisierungen.
- **_Process(double delta)**: Diese Methode wird jeden Frame einmal aufgerufen. Der Parameter `delta` gibt die vergangene Zeit seit dem vorherigen Frame an.

!!! note
    Die überschriebenen Methoden `_Ready()` und `_Process(double delta)` werden von Godot automatisch zu einem bestimmten Zeitpunkt aufgerufen. Sie sind essenziell für die Spielentwicklung, da sie grundlegende Lebenszyklusmethoden eines Nodes darstellen.

Im weiteren Verlauf werden wir diese Klasse erweitern und die grundlegenden Spielmechaniken implementieren. Dabei werden wir auf die Eigenschaften und Methoden von `Node2D` und anderen Godot-Klassen zurückgreifen, um unser Spiel zu entwickeln.

### Funktion der Klasse Root

Die Klasse Root wird in unserem Projekt kein eigentliches Objekt im Spiel sein. Stattdessen ist diese Klasse der Einstiegspunkt für weitere Klassen, die von dieser Klasse erstellt und verwaltet werden. Dies ermöglicht eine klare Struktur und Verwaltung der verschiedenen Spielkomponenten und -logiken.

Im weiteren Verlauf werden wir diese Klasse erweitern und die grundlegenden Spielmechaniken implementieren. Dabei werden wir auf die Eigenschaften und Methoden von Node2D und anderen Godot-Klassen zurückgreifen, um unser Spiel zu entwickeln.