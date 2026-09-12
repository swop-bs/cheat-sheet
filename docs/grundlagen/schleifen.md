# Wiederholung von Vorgängen durch Schleifen
Die `while`-Anweisung prüft eine Bedingung und führt die Anweisung oder den Anweisungsblock nach `while` aus. Damit wird die Bedingung wiederholt überprüft und die Ausführung dieser Anweisungen wiederholt, bis die Bedingung "false" lautet.

=== "C# / Java"

    ``` cs
    while(BEDINGUNG == true)
    {
      // Codeblock wird solange ausgeführt, bis BEDINGUNG nicht mehr TRUE
    }
    ```

!!! warning "Wichtig!"
    Stellen Sie sicher, dass die Schleifenbedingung `while` zu "false" wechselt, nachdem Sie den Code ausgeführt haben. Andernfalls erstellen Sie eine Endlosschleife, durch die das Programm niemals beendet wird.

## while

=== "C#"

    ``` cs
    int counter = 0;
    while (counter < 5)
    {
        Console.WriteLine($"Hello World! The counter is {counter}");
        counter++;
    }
    ```

=== "Java"

    ``` java
    int counter = 0;
    while (counter < 5) {
        System.out.println("Hello World! The counter is " + counter);
        counter++;
    }
    ```

??? quote "Output"
    ``` text
    Hello World! The counter is 0
    Hello World! The counter is 1
    Hello World! The counter is 2
    Hello World! The counter is 3
    Hello World! The counter is 4
    ```

### do...while
Die `do...while`-Schleife führt den Code zuerst aus und überprüft anschließend die Bedingung. Die `do...while`-Schleife wird im folgenden Code gezeigt:

=== "C#"

    ``` cs
    int counter = 0;
    do
    {
        Console.WriteLine($"Hello World! The counter is {counter}");
        counter++;
    } while (counter < 5);
    ```

=== "Java"

    ``` java
    int counter = 0;
    do {
        System.out.println("Hello World! The counter is " + counter);
        counter++;
    } while (counter < 5);
    ```

??? quote "Output"
    ``` text
    Hello World! The counter is 0
    Hello World! The counter is 1
    Hello World! The counter is 2
    Hello World! The counter is 3
    Hello World! The counter is 4
    ```

## for-Schleife
Da die Operationen INITIALISIERUNG, Prüfung der BEDINGUNG und die WERTVERÄNDERUNG sehr oft in einer Schleife benötigt werden, wird hierfür oft die `for-Schleife` verwendet. Diese ist übersichtlicher, da die drei Operationen direkt an einem Ort stehen:

=== "C# / Java"

    ``` cs
    for (INITIALISIERUNG; BEDINGUNG; WERTVERÄNDERUNG) 
    {
        // auszuführender Quellcode
    }
    ```

Jede `for`-Schleife lässt sich in eine `while`-Schleife übersetzen:

=== "C# / Java"

    ``` cs
    INITIALISIERUNG;

    while(BEDINGUNG) 
    {
        // auszuführender Quellcode
        WERTVERÄNDERUNG // (immer die letzte Anweisung)
    }
    ```


### Beispiele

#### counter

=== "C#"

    ``` cs
    for (int counter = 0; counter < 5; counter++) 
    {
        Console.WriteLine($"Hello World! The counter is {counter}");
    }
    ```

=== "Java"

    ``` java
    for (int counter = 0; counter < 5; counter++) {
        System.out.println("Hello World! The counter is " + counter);
    }
    ```

??? quote "Output"
    ``` text
    Hello World! The counter is 0
    Hello World! The counter is 1
    Hello World! The counter is 2
    Hello World! The counter is 3
    Hello World! The counter is 4
    ```

#### Array

=== "C#"

    ``` cs
    string[] cars = {"Volvo", "BMW", "Ford", "Mazda"};

    for(int i = 0; i < cars.Length; i++) 
    {
      Console.WriteLine(cars[i]);
    }
    ```

=== "Java"

    ``` java
    String[] cars = {"Volvo", "BMW", "Ford", "Mazda"};

    for(int i = 0; i < cars.length; i++) { // length ist in Java ein Feld, keine Methode/Property (kleingeschrieben)
      System.out.println(cars[i]);
    }
    ```

??? quote "Output"
    ``` text
    Volvo
    BMW
    Ford
    Mazda
    ```

Diese Schleife ist equivalent zum [Array-Beispiel der foreach-Schleife](#array_1).


Dieses Beispiel hat das gleiche Verhalten wie die [while-Schleife](#while), jedoch sind die Initialisierung, Bedingung und Wertänderung an einer Stelle. Der Vorteil zeigt sich vor allem bei längeren Codeblöcken, bei denen bei der `while`-Schleife erst am Ende des Blocks die Wertänderung stattfinden würde.

## `foreach`-Schleife

Beim Iterieren von Listen und Arrays wird der Index der `for`-Schleife oft nur geführt, um auf ein Element zuzugreifen.

Eine indexlose Alternative bietet die `foreach`-Schleife:

=== "C#"

    ``` cs
    foreach(type variableName in arrayName) 
    {
        // auszuführender Quellcode
    }
    ```

=== "Java"

    ``` java
    for(type variableName : arrayName) 
    {
        // auszuführender Quellcode
    }
    ```

In der `foreach`-Schleife wird `variableName` in jedem Durchgang mit dem nächsten Array- bzw. Listenelement belegt.

### Beispiele

#### Array

=== "C#"

    ``` cs
    string[] cars = {"Volvo", "BMW", "Ford", "Mazda"};

    foreach (string car in cars) 
    {
      Console.WriteLine(car);
    }
    ```

=== "Java"

    ``` java
    String[] cars = {"Volvo", "BMW", "Ford", "Mazda"};

    for (String car : cars) { // Java nutzt 'for' auch für foreach (enhanced for loop)
      System.out.println(car);
    }
    ```

??? quote "Output"
    ``` text
    Volvo
    BMW
    Ford
    Mazda
    ```

Diese Schleife ist equivalent zum [Array-Beispiel der for-Schleife](#array).


#### Liste

=== "C#"

    ``` cs
    List<string> cars = new List<string>();
    cars.Add("Volvo");
    cars.Add("BMW");
    cars.Add("Ford");
    cars.Add("Mazda");

    foreach (string car in cars) 
    {
      Console.WriteLine(car);
    }
    ```

=== "Java"

    ``` java
    import java.util.ArrayList;
    import java.util.List;

    List<String> cars = new ArrayList<>();
    cars.add("Volvo");
    cars.add("BMW");
    cars.add("Ford");
    cars.add("Mazda");

    for (String car : cars) {
      System.out.println(car);
    }
    ```

??? quote "Output"
    ``` text
    Volvo
    BMW
    Ford
    Mazda
    ```

## Eindeutige Werte in einer Liste sammeln

Häufig steht ein Wert in vielen Datensätzen mehrfach, und man braucht ihn nur einmal – zum Beispiel, um anschließend Gruppe für Gruppe weiterzuarbeiten.

Das Muster dafür ist immer gleich: eine zweite, zunächst leere Liste anlegen, alle Datensätze durchlaufen und jeden Wert nur dann aufnehmen, wenn er noch nicht enthalten ist. Ob er schon enthalten ist, beantwortet `Contains`.

=== "C#"

    ``` csharp
    List<string> bestellungen = new List<string>();
    bestellungen.Add("Fürth");
    bestellungen.Add("Ansbach");
    bestellungen.Add("Fürth");
    bestellungen.Add("Bamberg");
    bestellungen.Add("Ansbach");

    List<string> orte = new List<string>();

    foreach (string ort in bestellungen)
    {
        // Contains liefert true, wenn der Wert schon in der Liste steht (1)
        if (!orte.Contains(ort))
        {
            orte.Add(ort);
        }
    }

    foreach (string ort in orte)
    {
        Console.WriteLine(ort);
    }
    ```

    1. Das Ausrufezeichen kehrt die Bedingung um: aufgenommen wird nur, was **noch nicht** enthalten ist.

=== "Java"

    ``` java
    import java.util.ArrayList;
    import java.util.List;

    List<String> bestellungen = new ArrayList<>();
    bestellungen.add("Fürth");
    bestellungen.add("Ansbach");
    bestellungen.add("Fürth");
    bestellungen.add("Bamberg");
    bestellungen.add("Ansbach");

    List<String> orte = new ArrayList<>();

    for (String ort : bestellungen) {
        if (!orte.contains(ort)) {
            orte.add(ort);
        }
    }

    for (String ort : orte) {
        System.out.println(ort);
    }
    ```

??? quote "Output"
    ``` text
    Fürth
    Ansbach
    Bamberg
    ```

Die Reihenfolge ist dabei die des ersten Auftretens, nicht die alphabetische.

### Danach gruppenweise weiterarbeiten

Mit der Liste der eindeutigen Werte lässt sich anschließend für jede Gruppe getrennt rechnen. Dazu wird die Ausgangsliste je Gruppe noch einmal durchlaufen:

=== "C#"

    ``` csharp
    foreach (string ort in orte)
    {
        int anzahl = 0;

        foreach (string bestellung in bestellungen)
        {
            if (bestellung == ort)
            {
                anzahl++;
            }
        }

        Console.WriteLine(ort + ": " + anzahl);
    }
    ```

=== "Java"

    ``` java
    for (String ort : orte) {
        int anzahl = 0;

        for (String bestellung : bestellungen) {
            if (bestellung.equals(ort)) {
                anzahl++;
            }
        }

        System.out.println(ort + ": " + anzahl);
    }
    ```

??? quote "Output"
    ``` text
    Fürth: 2
    Ansbach: 2
    Bamberg: 1
    ```

!!! info "Zwei verschachtelte Schleifen"
	Die äußere Schleife läuft über die Gruppen, die innere über alle Datensätze. Das ist für überschaubare Datenmengen völlig ausreichend und gut nachvollziehbar. Für sehr große Datenmengen gibt es schnellere Verfahren; die kommen später im Jahr.

!!! warning "Vergleich von Zeichenketten"
	In C# vergleicht `==` bei `string` den Inhalt. In Java muss dafür `equals` verwendet werden – `==` würde dort prüfen, ob es dasselbe Objekt ist, und liefert oft `false`, obwohl der Text gleich aussieht.
