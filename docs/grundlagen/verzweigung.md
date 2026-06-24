# Verzweigungen

Verzweigungen ermöglichen es, Code nur unter bestimmten Bedingungen auszuführen.

## if / else

Die `if`-Anweisung prüft eine Bedingung. Ist diese `true`, wird der nachfolgende Codeblock ausgeführt. Optional kann mit `else` ein alternativer Codeblock definiert werden, der ausgeführt wird, wenn die Bedingung `false` ist.

=== "C#"

    ```csharp
    int zahl = 10;

    if (zahl > 10)
    {
        Console.WriteLine("Die Zahl ist größer als 10.");
    }
    else if (zahl == 10)
    {
        Console.WriteLine("Die Zahl ist genau 10.");
    }
    else
    {
        Console.WriteLine("Die Zahl ist kleiner als 10.");
    }
    ```

=== "Java"

    ``` java
    int zahl = 10;

    if (zahl > 10) {
        System.out.println("Die Zahl ist größer als 10.");
    } else if (zahl == 10) {
        System.out.println("Die Zahl ist genau 10.");
    } else {
        System.out.println("Die Zahl ist kleiner als 10.");
    }
    ```

??? quote "Output"
    ``` text
    Die Zahl ist genau 10.
    ```

## switch

Die `switch`-Anweisung ist eine Alternative zu mehreren `if-else`-Blöcken, wenn eine Variable gegen verschiedene konstante Werte geprüft werden soll.

=== "C#"

    ```csharp
    int tag = 3;

    switch (tag)
    {
        case 1:
            Console.WriteLine("Montag");
            break;
        case 2:
            Console.WriteLine("Dienstag");
            break;
        case 3:
            Console.WriteLine("Mittwoch");
            break;
        default:
            Console.WriteLine("Anderer Tag");
            break;
    }
    ```

=== "Java"

    ``` java
    int tag = 3;

    switch (tag) {
        case 1:
            System.out.println("Montag");
            break;
        case 2:
            System.out.println("Dienstag");
            break;
        case 3:
            System.out.println("Mittwoch");
            break;
        default:
            System.out.println("Anderer Tag");
            break;
    }
    ```

??? quote "Output"
    ``` text
    Mittwoch
    ```

## Bedingte Zuweisung (Ternary Operator)

Der ternäre Operator `?:` ermöglicht eine kurze Schreibweise für einfache `if-else`-Zuweisungen.

**Syntax:** `Bedingung ? Wert_wenn_wahr : Wert_wenn_falsch`

=== "C#"

    ```csharp
    int alter = 18;
    string status = (alter >= 18) ? "Volljährig" : "Minderjährig";

    Console.WriteLine(status); // Ausgabe: Volljährig
    ```

=== "Java"

    ``` java
    int alter = 18;
    String status = (alter >= 18) ? "Volljährig" : "Minderjährig";

    System.out.println(status); // Ausgabe: Volljährig
    ```

??? quote "Output"
    ``` text
    Volljährig
    ```
