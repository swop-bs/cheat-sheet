# Vererbung (Inheritance)

Vererbung ermöglicht es, eine neue Klasse auf Basis einer bestehenden Klasse zu erstellen. Die neue Klasse (abgeleitete Klasse) übernimmt Eigenschaften und Methoden der Basisklasse und kann diese erweitern oder anpassen.

## Syntax
In C# wird Vererbung durch einen Doppelpunkt (`:`) gekennzeichnet.

=== "C#"

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

=== "Java"

    ``` java
    // Basisklasse (Elternklasse)
    public class Tier {
        public String name;

        public void essen() {
            System.out.println(name + " isst.");
        }
    }

    // Abgeleitete Klasse (Kindklasse)
    public class Hund extends Tier {
        public void bellen() {
            System.out.println("Wuff!");
        }
    }
    ```

## Verwendung
Ein Objekt der Klasse `Hund` hat Zugriff auf die Member von `Tier` und `Hund`.

=== "C#"

    ```csharp
    Hund bello = new Hund();
    bello.Name = "Bello"; // Geerbt von Tier
    bello.Essen();        // Geerbt von Tier
    bello.Bellen();       // Spezifisch für Hund
    ```

=== "Java"

    ``` java
    Hund bello = new Hund();
    bello.name = "Bello"; // Geerbt von Tier
    bello.essen();        // Geerbt von Tier
    bello.bellen();       // Spezifisch für Hund
    ```

## base-Keyword
Mit `base` kann auf Konstruktoren oder Methoden der Basisklasse zugegriffen werden.

=== "C#"

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

=== "Java"

    ``` java
    public class Hund extends Tier {
        public Hund(String name) {
            // Feld der Basisklasse setzen
            this.name = name; 
            // Oder den Konstruktor der Basisklasse aufrufen (wenn vorhanden):
            // super(name); 
        }
    }
    ```
