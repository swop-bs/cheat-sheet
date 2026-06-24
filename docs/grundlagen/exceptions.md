# Exception Handling

Exception Handling (Ausnahmebehandlung) ist ein Mechanismus, um Laufzeitfehler kontrolliert abzufangen und darauf zu reagieren, anstatt dass das Programm abstürzt.

Wenn ein Fehler auftritt (z.B. Division durch Null, Datei nicht gefunden), "wirft" das Programm eine Exception (Ausnahme). Diese Exception kann mit einem `try-catch`-Block "gefangen" werden.

### Try-Catch

Der Code, der möglicherweise einen Fehler verursacht, kommt in den `try`-Block. Der Code zur Fehlerbehandlung kommt in den `catch`-Block.

=== "C#"

    ``` csharp
    try
    {
        int a = 10;
        int b = 0;
        int ergebnis = a / b; // Verursacht DivideByZeroException
        Console.WriteLine(ergebnis);
    }
    catch (DivideByZeroException ex)
    {
        Console.WriteLine("Fehler: Division durch Null ist nicht erlaubt!");
        Console.WriteLine(ex.Message);
    }
    catch (Exception ex)
    {
        // Fängt alle anderen Fehler ab
        Console.WriteLine("Ein unerwarteter Fehler ist aufgetreten: " + ex.Message);
    }
    ```

=== "Java"

    ``` java
    try {
        int a = 10;
        int b = 0;
        int ergebnis = a / b; // Verursacht ArithmeticException
        System.out.println(ergebnis);
    } catch (ArithmeticException ex) {
        System.out.println("Fehler: Division durch Null ist nicht erlaubt!");
        System.out.println(ex.getMessage());
    } catch (Exception ex) {
        // Fängt alle anderen Fehler ab
        System.out.println("Ein unerwarteter Fehler ist aufgetreten: " + ex.getMessage());
    }
    ```

### Finally

Der `finally`-Block wird **immer** ausgeführt, egal ob eine Exception aufgetreten ist oder nicht. Er eignet sich gut, um Ressourcen freizugeben (z.B. Dateien schließen, Datenbankverbindungen trennen).

=== "C#"

    ``` csharp
    FileStream fs = null;
    try
    {
        fs = new FileStream("test.txt", FileMode.Open);
        // ... Arbeiten mit der Datei ...
    }
    catch (FileNotFoundException ex)
    {
        Console.WriteLine("Datei nicht gefunden.");
    }
    finally
    {
        if (fs != null)
        {
            fs.Close();
            Console.WriteLine("Datei geschlossen.");
        }
    }
    ```

=== "Java"

    ``` java
    import java.io.FileInputStream;
    import java.io.IOException;

    FileInputStream fis = null;
    try {
        fis = new FileInputStream("test.txt");
        // ... Arbeiten mit der Datei ...
    } catch (IOException ex) {
        System.out.println("Fehler beim Lesen der Datei.");
    } finally {
        if (fis != null) {
            try {
                fis.close();
                System.out.println("Datei geschlossen.");
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
    }
    ```

!!! info "Info"
    Moderne Sprachen bieten oft bessere Wege, Ressourcen automatisch zu schließen (z.B. `using` in C# oder `try-with-resources` in Java), wodurch der `finally`-Block für diesen Zweck oft nicht mehr explizit nötig ist.

## Exceptions werfen (Throw)

Man kann auch selbst Exceptions auslösen, wenn ein bestimmter fehlerhafter Zustand eintritt.

=== "C#"

    ``` csharp
    public void SetzeAlter(int alter)
    {
        if (alter < 0)
        {
            throw new ArgumentOutOfRangeException("alter", "Das Alter darf nicht negativ sein.");
        }
        this.alter = alter;
    }
    ```

=== "Java"

    ``` java
    public void setzeAlter(int alter) {
        if (alter < 0) {
            throw new IllegalArgumentException("Das Alter darf nicht negativ sein.");
        }
        this.alter = alter;
    }
    ```

## Häufige Exceptions

Hier einige häufige Exception-Typen in C# und Java:

| Beschreibung | C# | Java |
| :--- | :--- | :--- |
| Allgemeiner Fehler | `Exception` | `Exception` |
| Null-Referenz Zugriff | `NullReferenceException` | `NullPointerException` |
| Ungültiges Argument | `ArgumentException` | `IllegalArgumentException` |
| Index außerhalb des Bereichs | `IndexOutOfRangeException` | `ArrayIndexOutOfBoundsException` |
| Datei nicht gefunden | `FileNotFoundException` | `FileNotFoundException` |
| Rechenfehler (z.B. / 0) | `DivideByZeroException` | `ArithmeticException` |
