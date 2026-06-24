# Abstraktion

Abstraktion bedeutet, sich auf das Wesentliche zu konzentrieren und Implementierungsdetails zu verbergen oder gar nicht erst festzulegen, wenn sie noch nicht bekannt sind.

## Abstrakte Klassen
Eine `abstract class` kann nicht direkt instanziiert werden. Sie dient nur als Basis für andere Klassen. Sie kann `abstract` Methoden enthalten, die keine Implementierung haben (nur den Methodenkopf). Die abgeleiteten Klassen **müssen** diese Methoden implementieren.

=== "C#"

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

=== "Java"

    ``` java
    public abstract class Form {
        // Abstrakte Methode ohne Body
        public abstract double berechneFlaeche();
    }

    public class Kreis extends Form {
        private double radius;

        public Kreis(double radius) {
            this.radius = radius;
        }

        @Override
        public double berechneFlaeche() {
            return Math.PI * radius * radius;
        }
    }
    ```

## Interfaces (Schnittstellen)
Ein Interface ist ein reiner Vertrag. Es enthält nur Methodendefinitionen (keine Implementierung und keine Felder). Eine Klasse, die ein Interface implementiert, **muss** alle definierten Methoden bereitstellen.

Klassen beginnen konventionell mit einem `I`.

=== "C#"

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

=== "Java"

    ``` java
    public interface Speicherbar {
        void speichern();
    }

    public class Datei implements Speicherbar {
        @Override
        public void speichern() {
            System.out.println("Speichere in Datei...");
        }
    }

    public class Datenbank implements Speicherbar {
        @Override
        public void speichern() {
            System.out.println("Speichere in DB...");
        }
    }
    ```
