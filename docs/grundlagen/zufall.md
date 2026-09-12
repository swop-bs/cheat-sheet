# Zufall

Ein Computer kann nicht wirklich würfeln. Er rechnet aus einem **Startwert** eine Zahlenfolge aus, die sich nicht vorhersagen lässt, wenn man den Startwert nicht kennt. Man spricht deshalb von *Pseudozufall*. Für Testdaten, Spiele und Stichproben reicht das völlig.

| Was?                                                | C#       | Java                    |
| --------------------------------------------------- | -------- | ----------------------- |
| Quelle für Zufallszahlen                            | `Random` | `java.util.Random`      |

## Ein Zufallsobjekt anlegen

=== "C#"

    ``` csharp
    Random zufall = new Random();
    ```

=== "Java"

    ``` java
    import java.util.Random;

    Random zufall = new Random();
    ```

!!! warning "Nur **ein** Zufallsobjekt je Programm"
	Legen Sie das Objekt **einmal** an – am besten als privates Feld Ihrer Klasse – und verwenden Sie es überall wieder.

	Wer in einer Schleife jedes Mal ein neues `Random` anlegt, bekommt reihenweise **dieselbe** Zahl. Der Grund: Ohne Startwert nimmt `Random` die aktuelle Uhrzeit. Zwei Objekte, die im selben Augenblick entstehen, starten also mit demselben Startwert und rechnen dieselbe Folge aus.

	``` csharp
	// FALSCH - liefert oft zehnmal dieselbe Zahl
	for (int i = 0; i < 10; i++)
	{
	    Random zufall = new Random();
	    Console.WriteLine(zufall.Next(1, 7));
	}
	```

## Ganze Zahlen ziehen

=== "C#"

    ``` csharp
    Random zufall = new Random();

    int a = zufall.Next();          // irgendeine Zahl ab 0
    int b = zufall.Next(6);         // 0, 1, 2, 3, 4 oder 5      (1)
    int c = zufall.Next(1, 7);      // 1, 2, 3, 4, 5 oder 6      (2)
    ```

    1. Die obere Grenze ist **nicht** dabei. `Next(6)` liefert nie eine 6.
    2. Die untere Grenze ist dabei, die obere nicht. Für einen Würfel also `Next(1, 7)`.

=== "Java"

    ``` java
    Random zufall = new Random();

    int a = zufall.nextInt();       // irgendeine Zahl
    int b = zufall.nextInt(6);      // 0 bis 5
    int c = zufall.nextInt(1, 7);   // 1 bis 6  (ab Java 17)
    ```

!!! info "Die obere Grenze ist ausgeschlossen"
	Das ist die häufigste Fehlerquelle beim Zufall. Wenn ein Wert **einschließlich** der oberen Grenze vorkommen soll, muss die Grenze um eins erhöht werden: Für „0 bis 100 einschließlich" schreibt man `Next(0, 101)`.

## Ein Element aus einer Liste ziehen

Der gültige Bereich der Listenplätze ist `0` bis `liste.Count - 1`. Genau das liefert `Next(liste.Count)` – ohne Rechnerei mit `-1`.

=== "C#"

    ``` csharp
    List<string> farben = new List<string>();
    farben.Add("rot");
    farben.Add("gruen");
    farben.Add("blau");

    Random zufall = new Random();

    string farbe = farben[zufall.Next(farben.Count)];
    ```

=== "Java"

    ``` java
    List<String> farben = new ArrayList<>();
    farben.add("rot");
    farben.add("gruen");
    farben.add("blau");

    Random zufall = new Random();

    String farbe = farben.get(zufall.nextInt(farben.size()));
    ```

!!! warning "Leere Liste"
	`Next(0)` liefert 0, und `liste[0]` wirft dann eine Ausnahme. Prüfen Sie vorher, ob die Liste überhaupt Einträge hat.

## Eine Entscheidung mit Wahrscheinlichkeit

=== "C#"

    ``` csharp
    // Kopf oder Zahl
    if (zufall.Next(2) == 0)
    {
        Console.WriteLine("Kopf");
    }
    else
    {
        Console.WriteLine("Zahl");
    }

    // In etwa 30 Prozent der Fälle
    if (zufall.Next(100) < 30)
    {
        Console.WriteLine("Treffer");
    }
    ```

!!! info "In etwa, nicht genau"
	`Next(100) < 30` trifft auf lange Sicht 30 Prozent, bei 1000 Durchläufen aber vielleicht 287 oder 314. Wenn ein Anteil **genau** stimmen muss, würfelt man nicht bei jedem Durchlauf neu, sondern zählt mit, wie viele Treffer noch offen sind, und zieht daraus.

## Kommazahlen

=== "C#"

    ``` csharp
    double d = zufall.NextDouble();          // 0.0 bis knapp unter 1.0
    double preis = 10.0 + zufall.NextDouble() * 5.0;   // 10.0 bis knapp unter 15.0
    ```

=== "Java"

    ``` java
    double d = zufall.nextDouble();
    double preis = 10.0 + zufall.nextDouble() * 5.0;
    ```

## Startwert: denselben Lauf wiederholen

Bekommt `Random` einen Startwert (englisch *seed*), rechnet es **immer dieselbe** Folge aus. Das ist kein Fehler, sondern oft genau das, was man braucht: Ein Testlauf lässt sich damit auf jedem Rechner Zeile für Zeile wiederholen, und ein gemeldeter Fehler ist reproduzierbar.

=== "C#"

    ``` csharp
    Random mitStartwert = new Random(4711);

    Console.WriteLine(mitStartwert.Next(1, 7));   // immer dieselbe Zahl
    Console.WriteLine(mitStartwert.Next(1, 7));   // immer dieselbe zweite Zahl
    ```

=== "Java"

    ``` java
    Random mitStartwert = new Random(4711);

    System.out.println(mitStartwert.nextInt(1, 7));
    System.out.println(mitStartwert.nextInt(1, 7));
    ```

Üblich ist, beides anzubieten: einen Konstruktor **ohne** Startwert für den Normalfall und einen **mit** Startwert für Tests und Vorführungen.

``` csharp
public class Beispielklasse
{
    private Random _zufall;

    public Beispielklasse()
    {
        _zufall = new Random();
    }

    public Beispielklasse(int startwert)
    {
        _zufall = new Random(startwert);
    }
}
```

!!! danger "Nicht für Passwörter und Schlüssel"
	`Random` ist vorhersagbar, wenn jemand den Startwert kennt oder genügend Werte beobachtet. Für Passwörter, Sitzungskennungen oder Schlüssel gibt es eigene Klassen (`System.Security.Cryptography.RandomNumberGenerator` in C#, `java.security.SecureRandom` in Java).

## Zufälliges Datum in einem Zeitraum

Zeitpunkte lassen sich nicht direkt auswürfeln. Der Weg führt über die **Anzahl der Tage** zwischen zwei Zeitpunkten (siehe [Datum und Zeit](datum_zeit.md)).

=== "C#"

    ``` csharp
    DateTime von = new DateTime(2020, 1, 1);
    DateTime bis = new DateTime(2020, 12, 31);

    int tage = (int)(bis - von).TotalDays;        // Zeitspanne in Tagen

    DateTime gezogen = von.AddDays(zufall.Next(tage + 1));   // (1)
    ```

    1. Das `+ 1` sorgt dafür, dass der letzte Tag selbst noch gezogen werden kann – die obere Grenze von `Next` ist ja ausgeschlossen.

## Häufige Fehler

| Fehlerbild | Ursache |
| ---------- | ------- |
| Alle Werte sind gleich | In der Schleife wird jedes Mal ein neues `Random` angelegt. |
| Der größte Wert kommt nie vor | Die obere Grenze von `Next` ist ausgeschlossen; es fehlt das `+ 1`. |
| `ArgumentOutOfRangeException` | Die untere Grenze ist größer als die obere. |
| `IndexOutOfRangeException` beim Ziehen aus einer Liste | Es wurde `Next(liste.Count + 1)` gerechnet oder die Liste ist leer. |
| Jeder Programmstart liefert dasselbe | Es wurde ein fester Startwert gesetzt, der nur für Tests gedacht war. |
