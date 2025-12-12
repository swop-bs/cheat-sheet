# Abstraktion

Abstraktion bedeutet, sich auf das Wesentliche zu konzentrieren und Implementierungsdetails zu verbergen oder gar nicht erst festzulegen, wenn sie noch nicht bekannt sind.

## Abstrakte Klassen
Eine `abstract class` kann nicht direkt instanziiert werden. Sie dient nur als Basis für andere Klassen. Sie kann `abstract` Methoden enthalten, die keine Implementierung haben (nur den Methodenkopf). Die abgeleiteten Klassen **müssen** diese Methoden implementieren.

```csharp
public abstract class Form
{
    // Jede Form muss eine Fläche berechnen können, 
    // aber wie das geht, wissen wir hier noch nicht.
    public abstract double BerechneFlaeche();
}

public class Kreis : Form
{
    public double Radius { get; set; }

    public override double BerechneFlaeche()
    {
        return Math.PI * Radius * Radius;
    }
}
```

## Interfaces (Schnittstellen)
Ein Interface ist ein reiner Vertrag. Es enthält nur Methodendefinitionen (keine Implementierung und keine Felder). Eine Klasse, die ein Interface implementiert, **muss** alle definierten Methoden bereitstellen.

Klassen beginnen konventionell mit einem `I`.

```csharp
public interface ISpeicherbar
{
    void Speichern();
}

public class Datei : ISpeicherbar
{
    public void Speichern()
    {
        Console.WriteLine("Speichere in Datei...");
    }
}

public class Datenbank : ISpeicherbar
{
    public void Speichern()
    {
        Console.WriteLine("Speichere in DB...");
    }
}
```
