# Vererbung (Inheritance)

Vererbung ermöglicht es, eine neue Klasse auf Basis einer bestehenden Klasse zu erstellen. Die neue Klasse (abgeleitete Klasse) übernimmt Eigenschaften und Methoden der Basisklasse und kann diese erweitern oder anpassen.

## Syntax
In C# wird Vererbung durch einen Doppelpunkt (`:`) gekennzeichnet.

```csharp
// Basisklasse (Elternklasse)
public class Tier
{
    public string Name { get; set; }

    public void Essen()
    {
        Console.WriteLine($"{Name} isst.");
    }
}

// Abgeleitete Klasse (Kindklasse)
public class Hund : Tier
{
    public void Bellen()
    {
        Console.WriteLine("Wuff!");
    }
}
```

## Verwendung
Ein Objekt der Klasse `Hund` hat Zugriff auf die Member von `Tier` und `Hund`.

```csharp
Hund bello = new Hund();
bello.Name = "Bello"; // Geerbt von Tier
bello.Essen();        // Geerbt von Tier
bello.Bellen();       // Spezifisch für Hund
```

## base-Keyword
Mit `base` kann auf Konstruktoren oder Methoden der Basisklasse zugegriffen werden.

```csharp
public class Hund : Tier
{
    public Hund(string name) 
    {
        // Name Property der Basisklasse setzen
        base.Name = name;
    }
}
```
