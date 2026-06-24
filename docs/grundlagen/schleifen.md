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
