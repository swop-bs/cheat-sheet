# Kapselung (Encapsulation)

Kapselung ist das Prinzip, den inneren Zustand eines Objekts zu verbergen und den Zugriff darauf zu kontrollieren. Dies schützt vor ungewollten Änderungen und inkonsistenten Zuständen.

## Zugriffsmodifizierer
In C# steuern Modifizierer, wer auf Klassenmember zugreifen darf:

*   **public**: Zugriff von überall.
*   **private**: Zugriff nur innerhalb der eigenen Klasse.
*   **protected**: Zugriff innerhalb der Klasse und in abgeleiteten Klassen (siehe Vererbung).
*   **internal**: Zugriff innerhalb derselben Assembly (Projekt).

## Properties (Eigenschaften)
Statt öffentliche Felder (`public int Speed;`) zu nutzen, verwendet man in C# **Properties** mit `get` und `set` Accessoren.

```csharp
public class BankKonto
{
    private decimal _kontostand; // Privates Feld

    public decimal Kontostand
    {
        get { return _kontostand; } // Lesezugriff erlaubt
        // set ist privat oder fehlt -> Schreibschutz von außen
    }

    public void Einzahlen(decimal betrag)
    {
        if (betrag > 0)
        {
            _kontostand += betrag;
        }
    }
}
```

Mit Kapselung zwingen wir den Benutzer der Klasse, die Methode `Einzahlen` zu nutzen (die Validierung enthalten kann), anstatt den `_kontostand` direkt beliebig zu ändern.
