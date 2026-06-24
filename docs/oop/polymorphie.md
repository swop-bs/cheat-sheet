# Polymorphie (Vielgestaltigkeit)

Polymorphie bedeutet, dass ein Objekt viele Formen annehmen kann. Meistens bezieht es sich darauf, dass Methoden in abgeleiteten Klassen unterschiedlich implementiert werden können, aber über die Basisklasse aufgerufen werden.

## `virtual` und `override`

*   **virtual**: Markiert eine Methode in der Basisklasse als "überschreibbar".
*   **override**: Überschreibt die Methode in der abgeleiteten Klasse.

=== "C#"

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

=== "Java"

    ``` java
    public class Tier {
        public void machGeraeusch() { // In Java sind Methoden standardmäßig überschreibbar
            System.out.println("...");
        }
    }

    public class Hund extends Tier {
        @Override // Annotation ist optional, aber empfohlen
        public void machGeraeusch() {
            System.out.println("Wuff!");
        }
    }

    public class Katze extends Tier {
        @Override
        public void machGeraeusch() {
            System.out.println("Miau!");
        }
    }
    ```

## Beispiel der Anwendung

Wir können eine Liste von verschiedenen Tieren haben und alle gleich behandeln (als `Tier`), aber jedes verhält sich seiner Art entsprechend.

=== "C#"

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

=== "Java"

    ``` java
    import java.util.ArrayList;
    import java.util.List;

    List<Tier> meineTiere = new ArrayList<>();
    meineTiere.add(new Hund());
    meineTiere.add(new Katze());

    for (Tier tier : meineTiere) {
        tier.machGeraeusch(); 
        // Ruft beim Hund "Wuff" und bei der Katze "Miau" auf
    }
    ```
