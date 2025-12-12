# Klassen und Objekte

## Klasse
Eine **Klasse** ist ein Bauplan für Objekte. Sie definiert Attribute (Eigenschaften) und das Verhalten (Methoden), das die späteren Objekte haben sollen.

```csharp
public class Auto
{
    // Felder (Fields) - interne Variablen
    private int _geschwindigkeit;

    // Eigenschaften (Properties) - öffentlicher Zugriff
    public string Marke { get; set; }
    public string Farbe { get; set; }

    // Konstruktor - wird beim Erstellen aufgerufen
    public Auto(string marke, string farbe)
    {
        Marke = marke;
        Farbe = farbe;
        _geschwindigkeit = 0;
    }

    // Methoden - Verhalten
    public void Beschleunigen()
    {
        _geschwindigkeit += 10;
        Console.WriteLine($"Das Auto {_marke} fährt jetzt {_geschwindigkeit} km/h.");
    }
}
```

## Objekt
Ein **Objekt** ist eine konkrete Instanz einer Klasse. Während die Klasse der abstrakte Plan ist, ist das Objekt das "Ding", das im Arbeitsspeicher existiert.

```csharp
// Instanziierung: Ein neues Objekt vom Typ 'Auto' erstellen
Auto meinAuto = new Auto("BMW", "Schwarz");

// Zugriff auf Eigenschaften und Methoden
meinAuto.Beschleunigen();
```
