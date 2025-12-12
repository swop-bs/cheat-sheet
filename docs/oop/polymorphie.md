# Polymorphie (Vielgestaltigkeit)

Polymorphie bedeutet, dass ein Objekt viele Formen annehmen kann. Meistens bezieht es sich darauf, dass Methoden in abgeleiteten Klassen unterschiedlich implementiert werden können, aber über die Basisklasse aufgerufen werden.

## `virtual` und `override`

*   **virtual**: Markiert eine Methode in der Basisklasse als "überschreibbar".
*   **override**: Überschreibt die Methode in der abgeleiteten Klasse.

```csharp
public class Tier
{
    public virtual void MachGeraeusch() // Kann überschrieben werden
    {
        Console.WriteLine("...");
    }
}

public class Hund : Tier
{
    public override void MachGeraeusch() // Neue Implementierung
    {
        Console.WriteLine("Wuff!");
    }
}

public class Katze : Tier
{
    public override void MachGeraeusch()
    {
        Console.WriteLine("Miau!");
    }
}
```

## Beispiel der Anwendung

Wir können eine Liste von verschiedenen Tieren haben und alle gleich behandeln (als `Tier`), aber jedes verhält sich seiner Art entsprechend.

```csharp
List<Tier> meineTiere = new List<Tier>();
meineTiere.Add(new Hund());
meineTiere.Add(new Katze());

foreach (Tier tier in meineTiere)
{
    tier.MachGeraeusch(); 
    // Ruft beim Hund "Wuff" und bei der Katze "Miau" auf
}
```
