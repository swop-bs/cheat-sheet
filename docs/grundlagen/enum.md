# Enum

Ein **Enum** (Aufzählung) ist ein eigener Datentyp mit einer festen, überschaubaren Menge erlaubter Werte. Man nimmt ihn überall dort, wo eine Angabe nur wenige Ausprägungen haben kann: eine Ampelfarbe, ein Wochentag, eine Priorität.

Der Gewinn gegenüber einem `string`: Es gibt genau diese Werte und keine anderen. `"Gruen"`, `"grün"`, `"GRÜN"` und `"grüen"` wären für den Rechner vier verschiedene Texte — beim Enum kann der Tippfehler gar nicht erst entstehen, weil der Übersetzer ihn anmeckert.

## Deklarieren

Ein Enum steht auf derselben Ebene wie eine Klasse und bekommt eine eigene Datei.

=== "C#"

    ``` csharp
    namespace Beispiel
    {
        public enum Ampelfarbe
        {
            Rot,
            Gelb,
            Gruen
        }
    }
    ```

=== "Java"

    ``` java
    public enum Ampelfarbe {
        ROT,
        GELB,
        GRUEN
    }
    ```

!!! info "Hinter den Kulissen sind es Zahlen"
	`Rot` ist 0, `Gelb` ist 1, `Gruen` ist 2 — die Reihenfolge der Deklaration. Man kann die Zahlen auch selbst vergeben (`Rot = 1`), braucht das aber selten. Wichtig ist: **Fügen Sie neue Werte am Ende an**, wenn die Zahlen irgendwo abgelegt wurden. Sonst bedeutet eine abgelegte 2 plötzlich etwas anderes.

## Verwenden

=== "C#"

    ``` csharp
    Ampelfarbe farbe = Ampelfarbe.Rot;

    if (farbe == Ampelfarbe.Rot)
    {
        Console.WriteLine("Stehen bleiben.");
    }
    ```

=== "Java"

    ``` java
    Ampelfarbe farbe = Ampelfarbe.ROT;

    if (farbe == Ampelfarbe.ROT) {
        System.out.println("Stehen bleiben.");
    }
    ```

## Auf jeden Wert reagieren: `switch`

`switch` ist der übliche Weg, wenn zu jedem Wert etwas anderes geschehen soll.

=== "C#"

    ``` csharp
    switch (farbe)
    {
        case Ampelfarbe.Rot:
            Console.WriteLine("Stehen bleiben.");
            break;

        case Ampelfarbe.Gelb:
            Console.WriteLine("Gleich geht es los.");
            break;

        case Ampelfarbe.Gruen:
            Console.WriteLine("Fahren.");
            break;

        default:                                  // (1)
            Console.WriteLine("Unbekannt.");
            break;
    }
    ```

    1. Der `default`-Zweig fängt alles ab, was nicht aufgezählt ist. Nehmen Sie ihn mit — sonst fällt es nicht auf, wenn später ein Wert dazukommt.

=== "Java"

    ``` java
    switch (farbe) {
        case ROT:
            System.out.println("Stehen bleiben.");
            break;
        case GELB:
            System.out.println("Gleich geht es los.");
            break;
        case GRUEN:
            System.out.println("Fahren.");
            break;
        default:
            System.out.println("Unbekannt.");
            break;
    }
    ```

### Eine Methode, die zu jedem Wert einen Text liefert

Im Enum stehen Bezeichner ohne Leerzeichen und ohne Umlaute. Für die Anzeige braucht man oft eine schönere Schreibweise. Dafür schreibt man eine kleine `static`-Methode:

``` csharp
public static string AlsText(Ampelfarbe farbe)
{
    switch (farbe)
    {
        case Ampelfarbe.Rot: return "Rot";
        case Ampelfarbe.Gelb: return "Gelb";
        case Ampelfarbe.Gruen: return "Grün";
        default: return farbe.ToString();
    }
}
```

## In Text umwandeln und zurück

Für die Ablage in einer Datei wird der Enum-Wert als **Name** geschrieben, nicht als Zahl. Der Name bleibt lesbar, auch wenn jemand die Datei in einem Editor öffnet, und er verschiebt sich nicht, wenn im Enum ein Wert dazukommt.

### Enum → Text

``` csharp
Ampelfarbe farbe = Ampelfarbe.Gruen;

string abgelegt = farbe.ToString();      // "Gruen"
```

### Text → Enum

``` csharp
string gelesen = "Gruen";

Ampelfarbe farbe;

if (Enum.TryParse(gelesen, out farbe)
    && Enum.IsDefined(typeof(Ampelfarbe), farbe))   // (1)
{
    Console.WriteLine("Gelesen: " + farbe);
}
else
{
    throw new FormatException("Unbekannter Wert in der Datei: " + gelesen);
}
```

1. Die zweite Bedingung ist wichtig: `TryParse` nimmt auch **Zahlen** an. Aus `"7"` würde ein Ampelfarbe-Wert 7, den es gar nicht gibt — `IsDefined` fängt das ab.

!!! warning "`Enum.Parse` wirft, `Enum.TryParse` nicht"
	`Enum.Parse` bricht mit einer Ausnahme ab, wenn der Text nicht passt. Beim Einlesen einer Datei, die jemand von Hand bearbeitet haben könnte, ist `TryParse` die bessere Wahl: Sie können selbst entscheiden, welche Meldung die Anwenderin bekommt — am besten eine mit der Zeilennummer.

### Groß- und Kleinschreibung

`TryParse` unterscheidet standardmäßig zwischen groß und klein. Wer das nicht will, übergibt `true` als zweiten Wert:

``` csharp
Enum.TryParse("gruen", true, out farbe);   // ignoriert die Schreibweise
```

## Alle Werte durchlaufen

``` csharp
foreach (Ampelfarbe farbe in Enum.GetValues(typeof(Ampelfarbe)))
{
    Console.WriteLine(farbe);
}
```

Meist braucht man das gar nicht: Wenn nur bestimmte Werte in Frage kommen, ist es klarer, sie in eine `List<Ampelfarbe>` zu legen und diese zurückzugeben.

``` csharp
public List<Ampelfarbe> WasAlsNaechstesKommt(Ampelfarbe jetzt)
{
    List<Ampelfarbe> moeglich = new List<Ampelfarbe>();

    switch (jetzt)
    {
        case Ampelfarbe.Rot:
            moeglich.Add(Ampelfarbe.Gelb);
            break;
        case Ampelfarbe.Gelb:
            moeglich.Add(Ampelfarbe.Gruen);
            break;
        case Ampelfarbe.Gruen:
            moeglich.Add(Ampelfarbe.Gelb);
            break;
    }

    return moeglich;
}
```

!!! info "Eine Regel, eine Stelle"
	Wenn festgelegt ist, welcher Wert auf welchen folgen darf, gehört diese Regel **genau einmal** ins Programm — dorthin, wo der Wert zu Hause ist. Alle anderen Stellen fragen dort nach. Wer die Regel in der Konsolenausgabe *und* im Fenster *und* in der Prüfung noch einmal hinschreibt, hat sie dreimal zu pflegen und irgendwann dreimal verschieden.

## Enum oder Konstanten oder Text?

| | `string` | `const int` | `enum` |
| --- | --- | --- | --- |
| Tippfehler fällt auf | nein, erst zur Laufzeit | nein | **ja, beim Übersetzen** |
| Nur erlaubte Werte möglich | nein | nein | **ja** |
| Lesbar in einer Datei | ja | nein | ja, über `ToString()` |
| `switch` sinnvoll | mühsam | ja | **ja** |

## Häufige Fehler

| Fehlerbild | Ursache |
| --- | --- |
| Nach dem Einlesen steht ein Wert da, den es nicht gibt | `Enum.TryParse` hat eine Zahl angenommen. `Enum.IsDefined` fehlt. |
| `ArgumentException` beim Einlesen | `Enum.Parse` statt `TryParse` bei einem Text, der nicht passt. |
| Nach dem Ergänzen eines Wertes stimmen alte Dateien nicht mehr | Es wurden die Zahlen abgelegt statt der Namen, und der neue Wert wurde nicht ans Ende gestellt. |
| Der `switch` behandelt einen neuen Wert nicht | Es fehlt der `default`-Zweig, der auffällt. |
| Umlaute im Enum-Bezeichner | Erlaubt, aber unüblich und in Dateinamen und Ablagen lästig. Im Bezeichner `Gruen` schreiben, für die Anzeige eine Methode `AlsText` verwenden. |
