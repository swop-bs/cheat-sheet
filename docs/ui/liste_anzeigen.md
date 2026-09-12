# Liste von Objekten anzeigen

Eine `ListBox` oder `ListView` zeigt, was in ihrer Sammlung `Items` liegt. Sie können dort Objekte hineinlegen und nach einer Änderung wieder neu füllen — ganz ohne Datenbindung.

!!! info "Ohne Datenbindung"
	Auf dieser Seite wird die Liste **von Hand** gefüllt und nach jeder Änderung neu aufgebaut. Das ist umständlich, aber es ist an jeder Stelle sichtbar, was passiert. Wie man sich das später spart, steht unter [Datenbindung](databinding.md).

## Ausgangslage

Angenommen, es gibt eine Klasse `Fahrzeug` und eine Klasse, die den Fuhrpark verwaltet:

``` csharp
public class Fahrzeug
{
    public int Nummer { get => _nummer; }
    public string Kennzeichen { get => _kennzeichen; }
    public string Standort { get => _standort; }

    public override string ToString()
    {
        return _nummer + "  " + _kennzeichen + "  " + _standort;
    }
}
```

## Weg 1: Objekte hineinlegen

Legt man die Objekte selbst in `Items`, zeigt die ListBox das an, was `ToString()` liefert. Beim Auslesen bekommt man das Objekt zurück.

``` csharp
private void ListeFuellen()
{
    lbFahrzeuge.Items.Clear();                 // (1)

    foreach (Fahrzeug fahrzeug in _verwaltung.AlleFahrzeuge())
    {
        lbFahrzeuge.Items.Add(fahrzeug);       // (2)
    }
}
```

1. Immer zuerst leeren. Sonst hängen die alten Einträge unten dran.
2. Angezeigt wird `fahrzeug.ToString()`.

Auslesen der Auswahl:

``` csharp
Fahrzeug? ausgewaehlt = lbFahrzeuge.SelectedItem as Fahrzeug;

if (ausgewaehlt != null)
{
    Console.WriteLine(ausgewaehlt.Kennzeichen);
}
```

!!! warning "`as` liefert `null`, wenn nichts ausgewählt ist"
	`SelectedItem` ist `null`, solange niemand etwas angeklickt hat. Prüfen Sie das immer, bevor Sie auf das Objekt zugreifen.

## Weg 2: Zeilen bauen und die Objekte daneben halten

Weg 1 hat eine Grenze: `ToString()` kennt nur das, was im Objekt selbst steht. Soll in der Zeile etwas stehen, das erst aus einer anderen Klasse dazukommt, muss die Zeile im Fenster zusammengesetzt werden.

Dann legt man **Text** in die ListBox und führt daneben eine Liste der Objekte in **derselben Reihenfolge**. Über `SelectedIndex` kommt man von der angeklickten Zeile zurück zum Objekt.

``` csharp
public partial class MainWindow : Window
{
    private Fuhrparkverwaltung _verwaltung;

    // Zu jeder Zeile der ListBox gehoert das Fahrzeug an derselben Stelle.
    private List<Fahrzeug> _angezeigteFahrzeuge = new List<Fahrzeug>();

    private void ListeFuellen()
    {
        lbFahrzeuge.Items.Clear();
        _angezeigteFahrzeuge.Clear();          // (1)

        foreach (Fahrzeug fahrzeug in _verwaltung.AlleFahrzeuge())
        {
            _angezeigteFahrzeuge.Add(fahrzeug);
            lbFahrzeuge.Items.Add(ZeileVon(fahrzeug));
        }
    }

    private string ZeileVon(Fahrzeug fahrzeug)
    {
        string fahrerin = _verwaltung.FahrerinVon(fahrzeug.Nummer);

        return fahrzeug.Nummer.ToString().PadLeft(4) + " "
               + fahrzeug.Kennzeichen.PadRight(12) + " "
               + fahrerin.PadRight(20) + " "
               + fahrzeug.Standort;
    }
}
```

1. **Beide Sammlungen immer zusammen leeren und zusammen füllen.** Läuft eine der beiden aus dem Takt, zeigt die Auswahl auf das falsche Objekt.

Und der Weg zurück:

``` csharp
private bool IstEtwasAusgewaehlt()
{
    return lbFahrzeuge.SelectedIndex >= 0
           && lbFahrzeuge.SelectedIndex < _angezeigteFahrzeuge.Count;
}

private Fahrzeug AusgewaehltesFahrzeug()
{
    if (!IstEtwasAusgewaehlt())
    {
        throw new InvalidOperationException("Es ist kein Fahrzeug ausgewählt.");
    }

    return _angezeigteFahrzeuge[lbFahrzeuge.SelectedIndex];
}
```

!!! info "Spaltenüberschriften"
	Setzen Sie für die ListBox eine feste Schriftbreite (`FontFamily="Consolas"`) und legen Sie einen `TextBlock` mit derselben Schrift darüber. Dann stehen Überschriften und Werte untereinander.

	``` xml
	<TextBlock FontFamily="Consolas" Text="Nr.  Kennzeichen  Fahrerin             Standort"/>
	<ListBox x:Name="lbFahrzeuge" FontFamily="Consolas"
	         SelectionChanged="lbFahrzeuge_SelectionChanged"/>
	```

## Nach einer Änderung neu füllen

Die ListBox merkt von sich aus **nichts** davon, dass sich ein Objekt geändert hat. Nach jeder Änderung wird sie neu gefüllt:

``` csharp
private void btUebernehmen_Click(object sender, RoutedEventArgs e)
{
    Fahrzeug fahrzeug = AusgewaehltesFahrzeug();
    int nummer = fahrzeug.Nummer;                     // (1)

    _verwaltung.StandortAendern(nummer, tbStandort.Text);

    ListeFuellen();
    AuswahlSetzen(nummer);                            // (2)
}

// Setzt die Auswahl wieder auf das Objekt mit dieser Nummer.
private void AuswahlSetzen(int nummer)
{
    for (int i = 0; i < _angezeigteFahrzeuge.Count; i++)
    {
        if (_angezeigteFahrzeuge[i].Nummer == nummer)
        {
            lbFahrzeuge.SelectedIndex = i;
            return;
        }
    }
}
```

1. Die Nummer **vorher** merken: Nach `ListeFuellen` ist die Auswahl weg.
2. Ohne das springt die Auswahl nach jeder Änderung an den Anfang — für die Bedienung sehr lästig.

## Auf die Auswahl reagieren

Das Ereignis `SelectionChanged` wird immer ausgelöst, wenn sich die Auswahl ändert — auch beim Leeren der Liste.

``` xml
<ListBox x:Name="lbFahrzeuge" SelectionChanged="lbFahrzeuge_SelectionChanged"/>
```

``` csharp
private void lbFahrzeuge_SelectionChanged(object sender, SelectionChangedEventArgs e)
{
    AuswahlAuswerten();
}
```

## Schaltflächen aktivieren und sperren

Was gerade nicht möglich ist, wird nicht angeboten. Das ist freundlicher als eine Fehlermeldung hinterher.

``` csharp
private void AuswahlAuswerten()
{
    btBearbeiten.IsEnabled = IstEtwasAusgewaehlt();
    btEntfernen.IsEnabled = IstEtwasAusgewaehlt();
}
```

Im XAML gibt man den Ausgangszustand vor:

``` xml
<Button x:Name="btBearbeiten" Content="Bearbeiten" IsEnabled="False"
        Click="btBearbeiten_Click"/>
```

!!! warning "`IsEnabled`, nicht `Visibility`"
	Ein gesperrter Knopf ist noch da, nur grau — die Anwenderin sieht, dass es die Möglichkeit gibt. Ein Knopf, der verschwindet, verunsichert und lässt die übrigen springen.

## Eine ComboBox füllen

Für eine `ComboBox` gilt dasselbe. Auch hier hilft die Liste daneben, wenn die Anzeige nicht direkt aus `ToString()` kommt:

``` csharp
cbStandort.Items.Clear();
_angeboteneStandorte.Clear();

foreach (Standort standort in _verwaltung.AlleStandorte())
{
    _angeboteneStandorte.Add(standort);
    cbStandort.Items.Add(standort.Bezeichnung);
}

if (_angeboteneStandorte.Count > 0)
{
    cbStandort.SelectedIndex = 0;      // (1)
}
```

1. Ohne Vorauswahl steht die ComboBox leer da und `SelectedIndex` ist `-1`.

## Häufige Fehler

| Fehlerbild | Ursache |
| --- | --- |
| Die Einträge stehen doppelt (und dreifach) in der Liste | `Items.Clear()` vor dem Füllen vergessen. |
| In der Liste steht `Beispiel.Fahrzeug` statt der Angaben | `ToString()` wurde in der Klasse nicht überschrieben. |
| Nach einer Änderung zeigt die Liste den alten Stand | Sie wurde nicht neu gefüllt. Die ListBox merkt Änderungen an den Objekten nicht. |
| `NullReferenceException` beim Klick auf eine Schaltfläche | `SelectedItem` war `null`, weil nichts ausgewählt war. |
| Die Auswahl zeigt auf das falsche Objekt | Die Liste daneben ist aus dem Takt geraten. Beide immer zusammen leeren und füllen. |
| Die Auswahl springt nach jeder Änderung nach oben | Nach dem Neufüllen wurde sie nicht wieder gesetzt. |
| `SelectionChanged` läuft beim Programmstart ins Leere | Das Ereignis wird auch beim Leeren ausgelöst. Immer prüfen, ob überhaupt etwas ausgewählt ist. |
| Die Spalten sind nicht bündig | Die ListBox hat keine feste Schriftbreite. `FontFamily="Consolas"` setzen. |
