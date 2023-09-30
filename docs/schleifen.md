# Wiederholung von Vorgängen durch Schleifen
Die `while`-Anweisung prüft eine Bedingung und führt die Anweisung oder den Anweisungsblock nach `while` aus. Damit wird die Bedingung wiederholt überprüft und die Ausführung dieser Anweisungen wiederholt, bis die Bedingung "false" lautet.
``` cs
while(BEDINGUNG == true)
{
  // Codeblock wird solange ausgeführt, bis BEDINGUNG nicht mehr TRUE
}
```
!!! warning "Wichtig!"
	Stellen Sie sicher, dass die Schleifenbedingung `while` zu "false" wechselt, nachdem Sie den Code ausgeführt haben. Andernfalls erstellen Sie eine Endlosschleife, durch die das Programm niemals beendet wird.

## while

``` cs
int counter = 0;
while (counter < 5)
{
    Console.WriteLine($"Hello World! The counter is {counter}");
    counter++;
}

// This example produces the following output:
//
// Hello World! The counter is 0
// Hello World! The counter is 1
// Hello World! The counter is 2
// Hello World! The counter is 3
// Hello World! The counter is 4
```
	
### do...while
Die `do...while`-Schleife führt den Code zuerst aus und überprüft anschließend die Bedingung. Die `do...while`-Schleife wird im folgenden Code gezeigt:
``` cs
int counter = 0;
do
{
    Console.WriteLine($"Hello World! The counter is {counter}");
    counter++;
} while (counter < 5);

// This example produces the following output:
//
// Hello World! The counter is 0
// Hello World! The counter is 1
// Hello World! The counter is 2
// Hello World! The counter is 3
// Hello World! The counter is 4
```

## for-Schleife
Da die Operationen INITIALISIERUNG, Prüfung der BEDINGUNG und die WERTVERÄNDERUNG sehr oft in einer Schleife benötigt werden, wird hierfür oft die `for-Schleife` verwendet. Diese ist übersichtlicher, da die drei Operationen direkt an einem Ort stehen:
``` cs
for (INITIALISIERUNG; BEDINGUNG; WERTVERÄNDERUNG) 
{
    // auszuführender Quellcode
}
```

Jede `for`-Schleife lässt sich in eine `while`-Schleife übersetzen:
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

``` cs
for (int counter = 0; counter < 5; counter++) 
{
    Console.WriteLine($"Hello World! The counter is {counter}");
}

// This example produces the following output:
//
// Hello World! The counter is 0
// Hello World! The counter is 1
// Hello World! The counter is 2
// Hello World! The counter is 3
// Hello World! The counter is 4
```

#### Array
``` cs
string[] cars = {"Volvo", "BMW", "Ford", "Mazda"};

for(int i = 0; i < cars.Length; i++) 
{
  Console.WriteLine(cars[i]);
}

// This example produces the following output:
//
// Volvo
// BMW
// Ford
// Mazda
```
Diese Schleife ist equivalent zum [Array-Beispiel der foreach-Schleife](#beispiel-array_1).


Dieses Beispiel hat das gleiche Verhalten wie die [while-Schleife](#kopfgesteuerte-while-schleife), jedoch sind die Initialisierung, Bedingung und Wertänderung an einer Stelle. Der Vorteil zeigt sich vor allem bei längeren Codeblöcken, bei denen bei der `while`-Schleife erst am Ende des Blocks die Wertänderung stattfinden würde.

## `foreach`-Schleife

Beim Iterieren von Listen und Arrays wird der Index der `for`-Schleife oft nur geführt, um auf ein Element zuzugreifen.

Eine indexlose Alternative bietet die `foreach`-Schleife:

``` cs
foreach(type variableName in arrayName) 
{
	// auszuführender Quellcode
}
```

In der `foreach`-Schleife wird `variableName` in jedem Durchgang mit dem nächsten Array- bzw. Listenelement belegt.

### Beispiele

#### Array

``` cs
string[] cars = {"Volvo", "BMW", "Ford", "Mazda"};

foreach (string car in cars) 
{
  Console.WriteLine(car);
}

// This example produces the following output:
//
// Volvo
// BMW
// Ford
// Mazda
```
Diese Schleife ist equivalent zum [Array-Beispiel der for-Schleife](#beispiel-array).


#### Liste
``` cs
List<string> cars = new List<string>;
cars.Add("Volvo");
cars.Add("BMW");
cars.Add("Ford");
cars.Add("Mazda");

foreach (string car in cars) 
{
  Console.WriteLine(car);
}

// This example produces the following output:
//
// Volvo
// BMW
// Ford
// Mazda
```