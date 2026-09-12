# Dialoge

Ein **Dialog** ist ein eigenes Fenster, das für eine einzelne Aufgabe geöffnet wird: etwas eingeben, etwas auswählen, etwas bestätigen. Danach ist es wieder weg.

**Modal** heißt: Solange der Dialog offen ist, lässt sich das Fenster darunter nicht bedienen. Das ist der Normalfall — die Anwenderin soll die Sache zu Ende bringen, bevor daneben etwas geändert wird.

## Ein Fenster als Dialog anlegen

Ein Dialog ist ein ganz normales `Window`. In Visual Studio: Rechtsklick auf das Projekt → *Hinzufügen* → *Fenster (WPF)*.

Ein paar Eigenschaften machen daraus einen Dialog:

``` xml
<Window x:Class="Beispiel.StandortDialog"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="Standort ändern" Height="240" Width="420"
        WindowStartupLocation="CenterOwner"    
        ResizeMode="NoResize"                  
        ShowInTaskbar="False">                 
    <Grid Margin="16">
        <!-- Eingabefelder -->
        <Button x:Name="btOk" Content="Übernehmen" IsDefault="True" Click="btOk_Click"/>
        <Button x:Name="btAbbrechen" Content="Abbrechen" IsCancel="True" Click="btAbbrechen_Click"/>
    </Grid>
</Window>
```

| Eigenschaft | Wirkung |
| --- | --- |
| `WindowStartupLocation="CenterOwner"` | Der Dialog erscheint mittig über dem aufrufenden Fenster. |
| `ResizeMode="NoResize"` | Die Größe lässt sich nicht ziehen. Bei festen Formularen sinnvoll. |
| `ShowInTaskbar="False"` | Der Dialog erscheint nicht als eigener Eintrag in der Taskleiste. |
| `IsDefault="True"` am OK-Knopf | Die Eingabetaste löst ihn aus. |
| `IsCancel="True"` am Abbrechen-Knopf | Die Esc-Taste löst ihn aus. |

## Öffnen mit `ShowDialog`

``` csharp
private void btBearbeiten_Click(object sender, RoutedEventArgs e)
{
    StandortDialog dialog = new StandortDialog();
    dialog.Owner = this;                        // (1)

    bool? ergebnis = dialog.ShowDialog();       // (2)

    if (ergebnis == true)                       // (3)
    {
        ListeFuellen();
    }
}
```

1. `Owner` sagt, über welchem Fenster der Dialog liegt. Ohne das steht er irgendwo auf dem Bildschirm.
2. `ShowDialog` hält den Aufruf an dieser Stelle an, bis der Dialog geschlossen wird. `Show()` täte das nicht — dann liefe die nächste Zeile sofort.
3. Der Rückgabewert ist das `DialogResult`, das der Dialog selbst gesetzt hat.

## `DialogResult`: OK oder Abbrechen

`DialogResult` ist ein `bool?` und hat drei mögliche Werte:

| Wert | Bedeutung |
| --- | --- |
| `true` | Der Dialog wurde mit OK verlassen. |
| `false` | Der Dialog wurde abgebrochen. |
| `null` | Der Dialog wurde über das Kreuz oder mit Esc geschlossen, ohne dass etwas gesetzt wurde. |

Im Dialog:

``` csharp
private void btOk_Click(object sender, RoutedEventArgs e)
{
    // ... pruefen und uebernehmen ...

    DialogResult = true;        // (1)
}

private void btAbbrechen_Click(object sender, RoutedEventArgs e)
{
    DialogResult = false;
}
```

1. Das **Setzen** von `DialogResult` schließt das Fenster von selbst. Ein zusätzliches `Close()` braucht es nicht.

!!! warning "Auf `== true` prüfen, nicht auf `!= false`"
	Weil `null` möglich ist, prüft man immer auf `ergebnis == true`. Wer `if (ergebnis != false)` schreibt, behandelt das Schließen über das Kreuz wie ein OK.

## Werte hinein- und herausgeben

### Hinein: über den Konstruktor

``` csharp
public partial class StandortDialog : Window
{
    private Fuhrparkverwaltung _verwaltung;
    private int _nummer;

    // Neuen Eintrag anlegen
    public StandortDialog(Fuhrparkverwaltung verwaltung)
    {
        InitializeComponent();

        _verwaltung = verwaltung;
        _nummer = 0;

        Title = "Standort anlegen";
        btOk.Content = "Anlegen";
    }

    // Ueberladung: vorhandenen Eintrag aendern
    public StandortDialog(Fuhrparkverwaltung verwaltung, Standort standort)
    {
        InitializeComponent();

        _verwaltung = verwaltung;
        _nummer = standort.Nummer;

        Title = "Standort ändern";
        btOk.Content = "Übernehmen";

        tbBezeichnung.Text = standort.Bezeichnung;   // (1)
    }
}
```

1. Die vorhandenen Angaben werden in die Felder geschrieben. So sieht die Anwenderin, was bisher dasteht.

!!! info "Ein Fenster für Anlegen und Ändern"
	Zwei Konstruktoren statt zwei Fenster: Der Aufbau ist identisch, nur Titel, Beschriftung der Schaltfläche und Vorbelegung unterscheiden sich. Das erspart zwei Fassungen desselben Formulars, die auseinanderlaufen.

### Heraus: über eine Eigenschaft

Das aufrufende Fenster liest nach `ShowDialog` aus dem Dialog-Objekt aus. Das Objekt lebt noch, auch wenn das Fenster geschlossen ist.

``` csharp
public int Nummer { get => _nummer; }
```

``` csharp
StandortDialog dialog = new StandortDialog(_verwaltung);
dialog.Owner = this;

if (dialog.ShowDialog() == true)
{
    ListeFuellen();
    AuswahlSetzen(dialog.Nummer);
}
```

## Eingaben prüfen, bevor übernommen wird

**Erst prüfen, dann übernehmen, dann schließen.** Wer einen Hinweis bekommt, bleibt im Dialog und verliert nichts von dem, was er getippt hat.

``` csharp
private void btOk_Click(object sender, RoutedEventArgs e)
{
    if (cbOrt.SelectedIndex < 0)
    {
        Hinweis("Bitte wählen Sie einen Ort aus.", cbOrt);
        return;                                   // (1)
    }

    if (tbBezeichnung.Text.Trim() == "")
    {
        Hinweis("Bitte geben Sie eine Bezeichnung ein.", tbBezeichnung);
        return;
    }

    try
    {
        _verwaltung.StandortAnlegen(tbBezeichnung.Text);
    }
    catch (ArgumentException fehler)              // (2)
    {
        Hinweis(fehler.Message, tbBezeichnung);
        return;
    }

    DialogResult = true;                          // (3)
}

private void Hinweis(string text, Control weiterMachenBei)
{
    MessageBox.Show(text, Title, MessageBoxButton.OK, MessageBoxImage.Information);
    weiterMachenBei.Focus();                      // (4)
}
```

1. `return` — nicht schließen. Der Dialog bleibt offen.
2. Was die darunterliegenden Klassen abweisen, wird hier zu einem Hinweis. Der Dialog prüft nur, ob überhaupt etwas eingegeben wurde; die fachliche Prüfung bleibt, wo sie hingehört.
3. Erst wenn alles gut gegangen ist.
4. Der Textcursor springt dorthin, wo etwas fehlt. Kleine Sache, große Wirkung.

!!! warning "Abbrechen darf nichts verändern"
	Übernehmen Sie Eingaben **erst** im OK-Zweig in das Objekt, nie schon beim Tippen. Sonst hat ein Abbruch die Hälfte schon geändert.

## MessageBox

``` csharp
MessageBox.Show("Die Bezeichnung darf nicht leer sein.");
```

Mit Titel, Schaltflächen und Symbol:

``` csharp
MessageBoxResult antwort = MessageBox.Show(
    "Fahrzeug N-XY 123 wirklich entfernen?",
    "Fahrzeug entfernen",
    MessageBoxButton.YesNo,
    MessageBoxImage.Question);

if (antwort == MessageBoxResult.Yes)
{
    // entfernen
}
```

| `MessageBoxButton` | `MessageBoxImage` | `MessageBoxResult` |
| --- | --- | --- |
| `OK` | `Information` | `OK` |
| `OKCancel` | `Question` | `Cancel` |
| `YesNo` | `Warning` | `Yes` |
| `YesNoCancel` | `Error` | `No` |

!!! info "Was in einen Hinweis gehört"
	Ein guter Hinweis sagt, **welche** Angabe fehlt oder falsch ist, und ist in der Sprache der Anwenderin geschrieben.

	- Gut: „Bitte geben Sie eine Bezeichnung ein."
	- Schlecht: „Eingabefehler."
	- Schlecht: „System.ArgumentException: Value cannot be null. (Parameter 'bezeichnung')"

	Die technische Meldung gehört ins Protokoll, nicht auf den Bildschirm. Vor dem Entfernen von Daten wird gefragt, nicht hinterher gemeldet.

## Häufige Fehler

| Fehlerbild | Ursache |
| --- | --- |
| Der Dialog erscheint und die nächste Zeile läuft sofort weiter | `Show()` statt `ShowDialog()`. |
| `InvalidOperationException: DialogResult can be set only after Window is created and shown as dialog` | `DialogResult` in einem Fenster gesetzt, das mit `Show()` geöffnet wurde. |
| Der Dialog schließt sich trotz fehlerhafter Eingabe | Nach dem Hinweis fehlt das `return`, oder `DialogResult` wird vor der Prüfung gesetzt. |
| Nach dem Abbrechen ist trotzdem etwas geändert | Eingaben wurden schon beim Tippen in das Objekt übernommen statt erst im OK-Zweig. |
| Das Schließen über das Kreuz wirkt wie OK | Es wurde auf `!= false` statt auf `== true` geprüft. |
| Der Dialog erscheint an einer merkwürdigen Stelle | `Owner` nicht gesetzt oder `WindowStartupLocation` nicht auf `CenterOwner`. |
| Die Eingabetaste tut nichts | `IsDefault="True"` am OK-Knopf fehlt. |
