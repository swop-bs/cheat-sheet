# Dictionary

Eine **Liste** kennt ihre Einträge über die Position: `liste[0]`, `liste[1]`, … Wer einen bestimmten Eintrag sucht, muss die Liste durchlaufen und vergleichen.

Ein **Dictionary** kennt seine Einträge über einen **Schlüssel**, den Sie selbst festlegen: eine Nummer, ein Kürzel, ein Name. Der Zugriff geht damit in einem Schritt, ohne Suche.

| Was? | C# | Java |
| --- | --- | --- |
| Zuordnung Schlüssel → Wert | `Dictionary<TKey, TValue>` | `HashMap<K, V>` |

## Anlegen und füllen

In den spitzen Klammern stehen zwei Typen: zuerst der des **Schlüssels**, dann der des **Wertes**.

=== "C#"

    ``` csharp
    Dictionary<int, string> telefonliste = new Dictionary<int, string>();

    telefonliste.Add(101, "Frau Berger");     // (1)
    telefonliste.Add(102, "Herr Yilmaz");
    telefonliste[103] = "Frau Vogel";         // (2)
    ```

    1. `Add` legt einen neuen Eintrag an. Ist der Schlüssel schon vergeben, gibt es eine Ausnahme.
    2. Die Schreibweise mit eckigen Klammern legt neu an **oder überschreibt**, ohne sich zu beschweren.

=== "Java"

    ``` java
    import java.util.HashMap;

    HashMap<Integer, String> telefonliste = new HashMap<>();

    telefonliste.put(101, "Frau Berger");
    telefonliste.put(102, "Herr Yilmaz");
    telefonliste.put(103, "Frau Vogel");
    ```

!!! warning "`Add` oder eckige Klammern?"
	`Add` bei einem vorhandenen Schlüssel wirft eine `ArgumentException`. Das ist manchmal genau richtig — wenn ein Doppeleintrag ein Fehler wäre, soll er auffallen. Wo ein Überschreiben in Ordnung ist, nimmt man die eckigen Klammern.

## Auf einen Wert zugreifen

=== "C#"

    ``` csharp
    string name = telefonliste[102];      // "Herr Yilmaz"
    ```

=== "Java"

    ``` java
    String name = telefonliste.get(102);  // "Herr Yilmaz"
    ```

!!! danger "Ein Schlüssel, den es nicht gibt"
	In C# wirft `telefonliste[999]` eine `KeyNotFoundException`. In Java liefert `get(999)` einfach `null` — der Fehler fällt dann erst später auf. Beides ist unangenehm; prüfen Sie deshalb vorher.

## Prüfen, ob ein Schlüssel vorhanden ist

=== "C#"

    ``` csharp
    if (telefonliste.ContainsKey(102))
    {
        Console.WriteLine(telefonliste[102]);
    }
    else
    {
        Console.WriteLine("Diese Nummer ist nicht vergeben.");
    }
    ```

=== "Java"

    ``` java
    if (telefonliste.containsKey(102)) {
        System.out.println(telefonliste.get(102));
    } else {
        System.out.println("Diese Nummer ist nicht vergeben.");
    }
    ```

### `TryGetValue`: prüfen und holen in einem Schritt

`TryGetValue` liefert `true`, wenn der Schlüssel vorhanden war, und legt den Wert in der Variablen ab, die mit `out` übergeben wurde. So wird nur einmal nachgeschlagen statt zweimal.

``` csharp
string name;

if (telefonliste.TryGetValue(102, out name))
{
    Console.WriteLine(name);
}
else
{
    Console.WriteLine("Diese Nummer ist nicht vergeben.");
}
```

## Entfernen und zählen

=== "C#"

    ``` csharp
    telefonliste.Remove(101);              // (1)
    int anzahl = telefonliste.Count;
    telefonliste.Clear();                  // alles weg
    ```

    1. `Remove` liefert `true`, wenn es den Schlüssel gab, und `false` sonst. Es wirft keine Ausnahme.

=== "Java"

    ``` java
    telefonliste.remove(101);
    int anzahl = telefonliste.size();
    telefonliste.clear();
    ```

## Durchlaufen mit `foreach`

Es gibt drei Wege: nur die Schlüssel, nur die Werte, oder beides als Paar.

=== "C#"

    ``` csharp
    // nur die Schluessel
    foreach (int nummer in telefonliste.Keys)
    {
        Console.WriteLine(nummer);
    }

    // nur die Werte
    foreach (string name in telefonliste.Values)
    {
        Console.WriteLine(name);
    }

    // beides zusammen
    foreach (KeyValuePair<int, string> eintrag in telefonliste)
    {
        Console.WriteLine(eintrag.Key + ": " + eintrag.Value);
    }
    ```

=== "Java"

    ``` java
    for (Integer nummer : telefonliste.keySet()) {
        System.out.println(nummer);
    }

    for (String name : telefonliste.values()) {
        System.out.println(name);
    }

    for (Map.Entry<Integer, String> eintrag : telefonliste.entrySet()) {
        System.out.println(eintrag.getKey() + ": " + eintrag.getValue());
    }
    ```

!!! info "Die Reihenfolge liegt nicht fest"
	Ein Dictionary ist ein Haufen, keine Reihe. In welcher Reihenfolge `foreach` die Einträge liefert, ist nicht zugesichert und kann sich ändern.

	Wer eine geordnete Ausgabe braucht, sammelt die Schlüssel in einer Liste, sortiert diese und geht dann darüber:

	``` csharp
	List<int> nummern = new List<int>();

	foreach (int nummer in telefonliste.Keys)
	{
	    nummern.Add(nummer);
	}

	nummern.Sort();

	foreach (int nummer in nummern)
	{
	    Console.WriteLine(nummer + ": " + telefonliste[nummer]);
	}
	```

## Objekte als Wert

Der Wert muss kein Text sein — meist ist es ein ganzes Objekt. Der Schlüssel ist dann die Kennung, unter der man das Objekt sucht.

``` csharp
Dictionary<int, Fahrzeug> fuhrpark = new Dictionary<int, Fahrzeug>();

Fahrzeug neuesFahrzeug = new Fahrzeug("N-XY 123", 18);
fuhrpark[neuesFahrzeug.Nummer] = neuesFahrzeug;

// spaeter, ohne die Sammlung zu durchlaufen:
if (fuhrpark.ContainsKey(7))
{
    Console.WriteLine(fuhrpark[7].Kennzeichen);
}
```

!!! warning "Der Schlüssel darf sich nicht ändern"
	Legen Sie ein Objekt unter seiner Nummer ab und ändern danach diese Nummer im Objekt, dann liegt es immer noch unter dem alten Schlüssel. Das Dictionary merkt davon nichts. Deshalb eignen sich nur Angaben als Schlüssel, die sich nie ändern.

## Zählen mit einem Dictionary

Ein häufiges Muster: nachsehen, ob der Schlüssel schon da ist, sonst mit 0 anlegen, dann um eins hochzählen.

``` csharp
Dictionary<string, int> haeufigkeit = new Dictionary<string, int>();

foreach (string wort in woerter)
{
    if (!haeufigkeit.ContainsKey(wort))
    {
        haeufigkeit[wort] = 0;
    }

    haeufigkeit[wort] = haeufigkeit[wort] + 1;
}
```

## Liste oder Dictionary?

| | Liste | Dictionary |
| --- | --- | --- |
| Zugriff über | Position (0, 1, 2, …) | selbst gewählten Schlüssel |
| Einen bestimmten Eintrag finden | durchlaufen und vergleichen | in einem Schritt |
| Reihenfolge | bleibt, wie eingefügt | nicht zugesichert |
| Doppelte | erlaubt | Schlüssel nur einmal |
| Passt für | „alle der Reihe nach" | „genau den mit der Nummer 4711" |

**Faustregel:** Sobald es eine natürliche Kennung gibt, unter der etwas gesucht wird — eine Nummer, ein Kürzel, ein Kennzeichen —, ist das Dictionary die richtige Wahl. Geht es nur darum, alles der Reihe nach zu haben, genügt die Liste.

## Häufige Fehler

| Fehlerbild | Ursache |
| --- | --- |
| `KeyNotFoundException` | Zugriff auf einen Schlüssel, den es nicht gibt. Vorher mit `ContainsKey` oder `TryGetValue` prüfen. |
| `ArgumentException: An item with the same key has already been added` | `Add` mit einem Schlüssel, der schon vergeben ist. Entweder ist das ein echter Fehler in den Daten, oder es sollten die eckigen Klammern sein. |
| Die Ausgabe ist jedes Mal anders sortiert | Die Reihenfolge im Dictionary ist nicht zugesichert. Schlüssel sammeln und sortieren. |
| Ein Eintrag ist plötzlich „weg" | Der Wert, der als Schlüssel dient, wurde im Objekt nachträglich geändert. |
| `foreach` über das Dictionary und dabei `Remove` | Während des Durchlaufens darf die Sammlung nicht verändert werden. Erst die betroffenen Schlüssel sammeln, danach entfernen. |
