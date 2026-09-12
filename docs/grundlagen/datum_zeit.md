# Datum und Zeit

Für die Arbeit mit Datum und Uhrzeit gibt es zwei verschiedene Dinge, die man nicht verwechseln darf:

| Was?                                                    | C#         | Java                         |
| ------------------------------------------------------- | ---------- | ---------------------------- |
| Ein **Zeitpunkt**: "10.09.2026 um 06:00 Uhr"           | `DateTime` | `LocalDateTime`, `LocalDate` |
| Eine **Zeitspanne**: "4 Stunden und 30 Minuten"        | `TimeSpan` | `Duration`                   |

!!! info "Merksatz"
	Zeitpunkt minus Zeitpunkt ergibt eine Zeitspanne. Zeitpunkt plus Zeitspanne ergibt wieder einen Zeitpunkt. Zwei Zeitpunkte kann man nicht addieren.

## Zeitpunkt erzeugen

=== "C#"

    ``` csharp
    // Aktueller Zeitpunkt mit Uhrzeit
    DateTime jetzt = DateTime.Now;

    // Heutiges Datum, Uhrzeit auf 00:00:00
    DateTime heute = DateTime.Today;

    // Ein bestimmter Zeitpunkt: Jahr, Monat, Tag, Stunde, Minute, Sekunde
    DateTime abfahrt = new DateTime(2026, 9, 10, 6, 0, 0);

    // Nur ein Datum ohne Uhrzeit
    DateTime termin = new DateTime(2026, 9, 30);
    ```

=== "Java"

    ``` java
    import java.time.LocalDate;
    import java.time.LocalDateTime;

    LocalDateTime jetzt = LocalDateTime.now();
    LocalDate heute = LocalDate.now();

    LocalDateTime abfahrt = LocalDateTime.of(2026, 9, 10, 6, 0);
    LocalDate termin = LocalDate.of(2026, 9, 30);
    ```

### Bestandteile eines Zeitpunkts

=== "C#"

    ``` csharp
    DateTime abfahrt = new DateTime(2026, 9, 10, 6, 30, 0);

    Console.WriteLine(abfahrt.Year);        // 2026
    Console.WriteLine(abfahrt.Month);       // 9
    Console.WriteLine(abfahrt.Day);         // 10
    Console.WriteLine(abfahrt.Hour);        // 6
    Console.WriteLine(abfahrt.Minute);      // 30
    Console.WriteLine(abfahrt.DayOfWeek);   // Thursday

    // Date liefert denselben Tag, aber mit Uhrzeit 00:00:00 (1)
    DateTime tag = abfahrt.Date;
    ```

    1. `Date` ist praktisch, wenn zwei Zeitpunkte auf denselben Kalendertag geprüft werden sollen: `a.Date == b.Date`.

=== "Java"

    ``` java
    LocalDateTime abfahrt = LocalDateTime.of(2026, 9, 10, 6, 30);

    System.out.println(abfahrt.getYear());        // 2026
    System.out.println(abfahrt.getMonthValue());  // 9
    System.out.println(abfahrt.getDayOfMonth());  // 10
    System.out.println(abfahrt.getHour());        // 6
    System.out.println(abfahrt.getMinute());      // 30
    System.out.println(abfahrt.getDayOfWeek());   // THURSDAY

    // Nur der Kalendertag
    LocalDate tag = abfahrt.toLocalDate();
    ```

## Zeitspanne erzeugen

=== "C#"

    ``` csharp
    // Stunden, Minuten, Sekunden
    TimeSpan pause = new TimeSpan(0, 45, 0);      // 45 Minuten
    TimeSpan schicht = new TimeSpan(8, 30, 0);    // 8 Stunden 30 Minuten

    // Über Hilfsmethoden, wenn nur eine Einheit gebraucht wird
    TimeSpan zwanzigMinuten = TimeSpan.FromMinutes(20);
    TimeSpan zweiStunden = TimeSpan.FromHours(2);

    // Die Zeitspanne der Länge null
    TimeSpan summe = TimeSpan.Zero;
    ```

=== "Java"

    ``` java
    import java.time.Duration;

    Duration pause = Duration.ofMinutes(45);
    Duration schicht = Duration.ofHours(8).plusMinutes(30);
    Duration summe = Duration.ZERO;
    ```

### Die zwei Sorten von Eigenschaften

Das ist die häufigste Fehlerquelle bei `TimeSpan`:

| Eigenschaft   | Bedeutung                                       | Beispiel bei 8 h 30 min |
| ------------- | ----------------------------------------------- | ----------------------- |
| `Hours`       | Der **Stundenanteil** der Zeitspanne (0 bis 23) | `8`                     |
| `Minutes`     | Der **Minutenanteil** (0 bis 59)                | `30`                    |
| `TotalHours`  | Die **gesamte** Zeitspanne in Stunden           | `8.5`                   |
| `TotalMinutes`| Die **gesamte** Zeitspanne in Minuten           | `510`                   |

=== "C#"

    ``` csharp
    TimeSpan dauer = new TimeSpan(8, 30, 0);

    Console.WriteLine(dauer.Hours);         // 8
    Console.WriteLine(dauer.Minutes);       // 30
    Console.WriteLine(dauer.TotalHours);    // 8,5
    Console.WriteLine(dauer.TotalMinutes);  // 510

    // Ausgabe als "8:30 h": Gesamtstunden abschneiden, Minutenanteil anhängen
    int stunden = (int)dauer.TotalHours;
    string text = stunden + ":" + dauer.Minutes.ToString("00") + " h";
    ```

=== "Java"

    ``` java
    Duration dauer = Duration.ofHours(8).plusMinutes(30);

    System.out.println(dauer.toHours());          // 8   (abgeschnitten)
    System.out.println(dauer.toMinutesPart());    // 30
    System.out.println(dauer.toMinutes());        // 510
    ```

??? quote "Output (C#)"
	``` text
	8
	30
	8,5
	510
	8:30 h
	```

!!! warning "Achtung"
	`Hours` gibt bei einer Zeitspanne von 30 Stunden den Wert `6` zurück, nicht `30`. Wenn Sie mit der gesamten Dauer rechnen wollen, nehmen Sie immer `Total...`.

## Rechnen

=== "C#"

    ``` csharp
    DateTime abfahrt = new DateTime(2026, 9, 10, 6, 0, 0);
    DateTime ankunft = new DateTime(2026, 9, 10, 10, 15, 0);

    // Zeitpunkt minus Zeitpunkt ergibt eine Zeitspanne
    TimeSpan dauer = ankunft - abfahrt;              // 04:15:00

    // Zeitspannen lassen sich addieren
    TimeSpan gesamt = TimeSpan.Zero;
    gesamt = gesamt + dauer;

    // Zeitpunkt plus Zeitspanne ergibt einen neuen Zeitpunkt
    DateTime spaeter = abfahrt.AddHours(2);
    DateTime naechsterTag = abfahrt.AddDays(1);
    DateTime mitPause = ankunft.Add(new TimeSpan(0, 45, 0));
    ```

=== "Java"

    ``` java
    LocalDateTime abfahrt = LocalDateTime.of(2026, 9, 10, 6, 0);
    LocalDateTime ankunft = LocalDateTime.of(2026, 9, 10, 10, 15);

    Duration dauer = Duration.between(abfahrt, ankunft);   // PT4H15M

    Duration gesamt = Duration.ZERO;
    gesamt = gesamt.plus(dauer);

    LocalDateTime spaeter = abfahrt.plusHours(2);
    LocalDateTime naechsterTag = abfahrt.plusDays(1);
    ```

!!! info "Objekte sind unveränderlich"
	`AddHours` und `AddDays` ändern das Objekt **nicht**, sondern liefern ein neues zurück. `abfahrt.AddDays(1);` allein bewirkt nichts – das Ergebnis muss zugewiesen werden. Dasselbe gilt in Java für `plusHours` und `plusDays`.

## Vergleichen

Zeitpunkte und Zeitspannen lassen sich direkt mit den Vergleichsoperatoren prüfen:

=== "C#"

    ``` csharp
    TimeSpan dauer = ankunft - abfahrt;
    TimeSpan grenze = new TimeSpan(4, 30, 0);

    if (dauer > grenze)
    {
        Console.WriteLine("Die Grenze ist überschritten.");
    }

    if (abfahrt < DateTime.Now)
    {
        Console.WriteLine("Liegt in der Vergangenheit.");
    }
    ```

=== "Java"

    ``` java
    Duration dauer = Duration.between(abfahrt, ankunft);
    Duration grenze = Duration.ofMinutes(270);

    if (dauer.compareTo(grenze) > 0) {
        System.out.println("Die Grenze ist überschritten.");
    }

    if (abfahrt.isBefore(LocalDateTime.now())) {
        System.out.println("Liegt in der Vergangenheit.");
    }
    ```

!!! danger "Nicht als Text vergleichen"
	`"9:30" > "10:15"` ist als Zeichenkettenvergleich **wahr**, weil `"9"` alphabetisch nach `"1"` kommt. Uhrzeiten und Datumsangaben werden erst in `DateTime` bzw. `TimeSpan` umgewandelt und dann verglichen, nie als `string`.

## Text in einen Zeitpunkt umwandeln

Datum und Uhrzeit kommen fast immer als Text aus einer Datei, einer Eingabe oder einer Datenbank. Für die Umwandlung gibt es drei Wege:

| Methode                | Verhalten bei ungültigem Text                | Wann sinnvoll                                  |
| ---------------------- | -------------------------------------------- | ---------------------------------------------- |
| `DateTime.Parse`       | wirft eine `FormatException`                | wenn der Text sicher gültig ist                |
| `DateTime.TryParse`    | liefert `false`, wirft nichts                | wenn mit falschen Eingaben zu rechnen ist      |
| `DateTime.TryParseExact` | liefert `false`, akzeptiert nur ein Muster | wenn das Format genau festgelegt ist           |

### Parse und TryParse

=== "C#"

    ``` csharp
    using System.Globalization;

    string eingabe = "10.09.2026 06:00";
    CultureInfo deutsch = new CultureInfo("de-DE");   // (1)

    // Variante 1: Parse – bei falschem Text fliegt eine Exception
    DateTime zeitpunkt = DateTime.Parse(eingabe, deutsch);

    // Variante 2: TryParse – kein Absturz, Rückgabewert prüfen
    DateTime gelesen;
    if (DateTime.TryParse(eingabe, deutsch, DateTimeStyles.None, out gelesen))
    {
        Console.WriteLine("Gelesen: " + gelesen);
    }
    else
    {
        Console.WriteLine("Der Text ist kein gültiger Zeitpunkt.");
    }
    ```

    1. Die Kultur legt fest, wie der Text zu lesen ist. `"10.09.2026"` bedeutet in `de-DE` der 10. September, in `en-US` würde `"09/10/2026"` denselben Tag bezeichnen. Ohne Angabe wird die Kultur des Rechners verwendet – und die ist auf einem anderen Rechner womöglich eine andere.

=== "Java"

    ``` java
    import java.time.LocalDateTime;
    import java.time.format.DateTimeFormatter;
    import java.time.format.DateTimeParseException;

    String eingabe = "10.09.2026 06:00";
    DateTimeFormatter muster = DateTimeFormatter.ofPattern("dd.MM.yyyy HH:mm");

    try {
        LocalDateTime gelesen = LocalDateTime.parse(eingabe, muster);
        System.out.println("Gelesen: " + gelesen);
    } catch (DateTimeParseException ex) {
        System.out.println("Der Text ist kein gültiger Zeitpunkt.");
    }
    ```

### TryParseExact mit festem Muster

`TryParseExact` akzeptiert genau die angegebene Schreibweise und sonst nichts. Das ist die richtige Wahl, wenn eine Datei ein festes Format hat und abweichende Zeilen auffallen sollen:

=== "C#"

    ``` csharp
    using System.Globalization;

    CultureInfo deutsch = new CultureInfo("de-DE");
    DateTime zeitpunkt;

    bool erfolg = DateTime.TryParseExact(
        "10.09.2026 06:00",      // der Text
        "dd.MM.yyyy HH:mm",      // das erlaubte Muster (1)
        deutsch,                 // die Kultur
        DateTimeStyles.None,
        out zeitpunkt);          // hier steht das Ergebnis
    ```

    1. `dd` = Tag zweistellig, `MM` = Monat zweistellig, `yyyy` = Jahr vierstellig, `HH` = Stunde 0–23, `mm` = Minute. Achtung: `MM` ist der Monat, `mm` die Minute.

??? quote "Was TryParseExact mit diesem Muster akzeptiert"
	``` text
	"10.09.2026 06:00"   ->  true
	"1.9.2026 6:00"      ->  false   (nicht zweistellig)
	"2026-09-10 06:00"   ->  false   (andere Schreibweise)
	"10.09.2026"         ->  false   (Uhrzeit fehlt)
	```

## Einen Zeitpunkt als Text ausgeben

=== "C#"

    ``` csharp
    using System.Globalization;

    DateTime zeitpunkt = new DateTime(2026, 9, 10, 6, 0, 0);
    CultureInfo deutsch = new CultureInfo("de-DE");

    Console.WriteLine(zeitpunkt.ToString("dd.MM.yyyy", deutsch));        // 10.09.2026
    Console.WriteLine(zeitpunkt.ToString("HH:mm", deutsch));             // 06:00
    Console.WriteLine(zeitpunkt.ToString("dd.MM.yyyy HH:mm", deutsch));  // 10.09.2026 06:00
    Console.WriteLine(zeitpunkt.ToString("dddd", deutsch));              // Donnerstag

    // Auch in der Zeichenkettenformatierung möglich
    Console.WriteLine($"Abfahrt am {zeitpunkt:dd.MM.yyyy} um {zeitpunkt:HH:mm} Uhr.");
    ```

=== "Java"

    ``` java
    import java.time.format.DateTimeFormatter;
    import java.util.Locale;

    DateTimeFormatter muster = DateTimeFormatter.ofPattern("dd.MM.yyyy HH:mm", Locale.GERMANY);
    System.out.println(zeitpunkt.format(muster));    // 10.09.2026 06:00
    ```

### Die wichtigsten Musterzeichen

| Zeichen | Bedeutung             | Beispiel      |
| ------- | --------------------- | ------------- |
| `dd`    | Tag, zweistellig      | `10`          |
| `MM`    | Monat, zweistellig    | `09`          |
| `yyyy`  | Jahr, vierstellig     | `2026`        |
| `HH`    | Stunde, 0 bis 23      | `06`          |
| `hh`    | Stunde, 1 bis 12      | `06`          |
| `mm`    | Minute                | `00`          |
| `ss`    | Sekunde               | `00`          |
| `dddd`  | Wochentag ausgeschrieben | `Donnerstag` |

## Zeitspannen über Mitternacht

Wenn nur die Uhrzeiten bekannt sind und der Vorgang über Mitternacht geht, wird die Differenz negativ:

=== "C#"

    ``` csharp
    DateTime beginn = new DateTime(2026, 9, 10, 22, 30, 0);
    DateTime ende   = new DateTime(2026, 9, 10, 3, 0, 0);   // gemeint ist der Folgetag!

    TimeSpan dauer = ende - beginn;
    Console.WriteLine(dauer.TotalHours);    // -19,5  – offensichtlich falsch

    // Lösung: Liegt das Ende vor dem Beginn, gehört es zum nächsten Tag.
    if (ende <= beginn)
    {
        ende = ende.AddDays(1);
    }

    dauer = ende - beginn;
    Console.WriteLine(dauer.TotalHours);    // 4,5
    ```

=== "Java"

    ``` java
    LocalDateTime beginn = LocalDateTime.of(2026, 9, 10, 22, 30);
    LocalDateTime ende   = LocalDateTime.of(2026, 9, 10, 3, 0);

    if (!ende.isAfter(beginn)) {
        ende = ende.plusDays(1);
    }

    Duration dauer = Duration.between(beginn, ende);
    System.out.println(dauer.toHours());    // 4
    ```

!!! info
	Eine negative Zeitspanne ist fast immer ein Hinweis darauf, dass ein Datum fehlt oder falsch zugeordnet wurde. Prüfen Sie das Ergebnis, bevor Sie damit weiterrechnen.

## Häufige Fehler

| Fehler                                                   | Auswirkung                                                                 |
| -------------------------------------------------------- | -------------------------------------------------------------------------- |
| `Hours` statt `TotalHours` verwendet                     | Zeitspannen über 24 Stunden werden zu klein ausgegeben                     |
| `mm` statt `MM` im Muster                                | Der Monat wird als Minute gelesen                                          |
| Uhrzeiten als `string` verglichen                        | `"9:30"` gilt als größer als `"10:15"`                                     |
| Kultur nicht angegeben                                   | Der Code läuft auf dem eigenen Rechner und schlägt auf einem anderen fehl  |
| `Parse` statt `TryParse` bei Daten aus einer Datei       | Eine einzige kaputte Zeile beendet das ganze Programm                      |
| Rückgabe von `AddDays` nicht zugewiesen                  | Der Zeitpunkt bleibt unverändert, ohne dass eine Fehlermeldung erscheint   |
| Ende vor Beginn bei Vorgängen über Mitternacht           | Die Zeitspanne wird negativ                                                |

## Methodenübersicht

### DateTime

| Methode / Eigenschaft                                                                                                                  | Erklärung                                                                    |
| --------------------------------------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------- |
| [Now](https://learn.microsoft.com/de-de/dotnet/api/system.datetime.now)                                                                 | Aktuelles Datum mit Uhrzeit.                                                 |
| [Today](https://learn.microsoft.com/de-de/dotnet/api/system.datetime.today)                                                             | Aktuelles Datum, Uhrzeit auf 00:00:00.                                       |
| [Date](https://learn.microsoft.com/de-de/dotnet/api/system.datetime.date)                                                               | Derselbe Tag mit Uhrzeit 00:00:00.                                           |
| [AddDays / AddHours / AddMinutes](https://learn.microsoft.com/de-de/dotnet/api/system.datetime.adddays)                                 | Liefert einen neuen Zeitpunkt; das Original bleibt unverändert.              |
| [Parse](https://learn.microsoft.com/de-de/dotnet/api/system.datetime.parse)                                                             | Wandelt Text in einen Zeitpunkt um, wirft bei Fehler eine Exception.         |
| [TryParse](https://learn.microsoft.com/de-de/dotnet/api/system.datetime.tryparse)                                                       | Wie Parse, liefert aber `false` statt einer Exception.                       |
| [TryParseExact](https://learn.microsoft.com/de-de/dotnet/api/system.datetime.tryparseexact)                                             | Wie TryParse, akzeptiert aber nur ein angegebenes Muster.                    |
| [ToString(Muster, Kultur)](https://learn.microsoft.com/de-de/dotnet/standard/base-types/custom-date-and-time-format-strings)            | Gibt den Zeitpunkt in der angegebenen Schreibweise aus.                      |

### TimeSpan

| Methode / Eigenschaft                                                                              | Erklärung                                                        |
| ---------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------ |
| [Zero](https://learn.microsoft.com/de-de/dotnet/api/system.timespan.zero)                           | Zeitspanne der Länge null, gut als Startwert einer Summe.        |
| [FromMinutes / FromHours](https://learn.microsoft.com/de-de/dotnet/api/system.timespan.fromminutes) | Erzeugt eine Zeitspanne aus einer einzelnen Einheit.             |
| [Hours / Minutes](https://learn.microsoft.com/de-de/dotnet/api/system.timespan.hours)              | Stunden- bzw. Minutenanteil der Zeitspanne.                      |
| [TotalHours / TotalMinutes](https://learn.microsoft.com/de-de/dotnet/api/system.timespan.totalhours)| Die gesamte Zeitspanne in dieser Einheit, als Kommazahl.         |
| [Add / Subtract](https://learn.microsoft.com/de-de/dotnet/api/system.timespan.add)                  | Zeitspannen addieren bzw. subtrahieren (auch mit `+` und `-`).   |
