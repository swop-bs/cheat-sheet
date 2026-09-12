# Ereignisse in WPF

Ein Konsolenprogramm läuft von oben nach unten durch und fragt, wenn es etwas wissen will. Ein
Fensterprogramm dreht das um: Es baut das Fenster auf und **wartet**. Was danach geschieht,
entscheidet die Anwenderin. Jedes Steuerelement meldet, was ihm zustößt — angeklickt werden, den
Wert ändern, den Mauszeiger bekommen. Diese Meldungen sind die **Ereignisse**.

Ihre eigenen Methoden rufen Sie in einem Fensterprogramm fast nie selbst auf. Sie hängen sie an
ein Ereignis, und aufgerufen werden sie von WPF.

!!! info "Was ein Ereignis technisch ist"
	Dahinter steckt derselbe Mechanismus wie bei eigenen Events in C#
	(siehe [Events](../grundlagen/events.md)). Für die Arbeit an einer Oberfläche genügt: Das
	Steuerelement ruft die Methode auf, deren Namen im XAML steht.

## Eine Ereignismethode anlegen

Drei Wege führen zum Ziel:

1. **Doppelklick im Designer** auf das Element — Visual Studio trägt das Standardereignis ein und
   legt die Methode an. Schnell, aber immer nur das eine Standardereignis.
2. **Eigenschaftenfenster, Reiter mit dem Blitzsymbol** — alle Ereignisse des Elements; ein
   Doppelklick auf die gewünschte Zeile legt die Methode an. Das ist der Weg für alles außer
   `Click`.
3. **Von Hand:** Attribut ins XAML schreiben, Methode in der `.xaml.cs` anlegen. Beim Tippen des
   Attributwerts bietet Visual Studio `<Neuer Ereignishandler>` an.

``` xml
<Button x:Name="btSpeichern" Content="Speichern" Click="btSpeichern_Click"/>
```

``` csharp
private void btSpeichern_Click(object sender, RoutedEventArgs e)
{
    MessageBox.Show("Gespeichert.");
}
```

Der übliche Name ist `<Name des Elements>_<Ereignis>`.

!!! warning "Löschen im XAML löscht die Methode nicht"
	Wird ein Element im Designer entfernt, bleibt die Methode in der `.xaml.cs` stehen. Sie
	stört nicht, aber sie wird nie wieder aufgerufen. Umgekehrt gilt: Wird die **Methode**
	gelöscht oder umbenannt und das Attribut im XAML nicht, meldet der Übersetzer
	*„Der Name … ist im aktuellen Kontext nicht vorhanden"* — und zwar in einer erzeugten Datei,
	die Sie nie geschrieben haben.

## Welches Element meldet was

| Element | Ereignis | Wird ausgelöst |
| --- | --- | --- |
| `Button` | `Click` | beim Anklicken |
| `ListBox`, `ComboBox` | `SelectionChanged` | wenn sich die Auswahl ändert — **auch beim Leeren der Liste** |
| `Slider` | `ValueChanged` | bei jeder Bewegung des Reglers, auch bei winzigen |
| `CheckBox`, `RadioButton` | `Checked`, `Unchecked` | beim Setzen bzw. Entfernen. Zwei getrennte Ereignisse |
| `TextBox` | `TextChanged` | bei **jedem** Tastendruck, nicht erst beim Verlassen des Feldes |
| jedes Element | `MouseEnter`, `MouseLeave` | wenn der Mauszeiger die Fläche betritt bzw. verlässt |
| jedes Element | `MouseDown`, `MouseUp` | beim Drücken bzw. Loslassen einer Maustaste |
| `Window` | `Loaded` | wenn das Fenster fertig aufgebaut und sichtbar ist |
| `Window` | `Closing` | bevor das Fenster geschlossen wird — noch abbrechbar |

Die Signatur unterscheidet sich je Ereignis. Der zweite Parameter bringt die Einzelheiten mit:

``` csharp
private void slBreite_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
private void lbFahrzeuge_SelectionChanged(object sender, SelectionChangedEventArgs e)
private void tbKennzeichen_TextChanged(object sender, TextChangedEventArgs e)
private void grKarte_MouseDown(object sender, MouseButtonEventArgs e)
```

Diese Zeilen tippt man nicht ab — Visual Studio erzeugt sie. Wichtig ist zu wissen, dass in `e`
etwas steht, das man brauchen kann.

## Ein Element hat kein `Click` — was dann?

`Click` gibt es nur bei Schaltflächen. Auf ein `Label`, ein `Image` oder ein `Grid` lässt sich
trotzdem klicken: über die Maus-Ereignisse.

``` xml
<Label x:Name="laHinweis" Content="Hinweis"
       Background="Transparent"
       MouseDown="laHinweis_MouseDown"/>
```

``` csharp
private void laHinweis_MouseDown(object sender, MouseButtonEventArgs e)
{
    if (e.ChangedButton == MouseButton.Left)      // (1)
    {
        MessageBox.Show("Der Hinweis wurde angeklickt.");
    }
}
```

1. `MouseDown` meldet **jede** Maustaste. Ohne diese Abfrage reagiert auch die rechte.
   Alternativ gibt es `MouseLeftButtonDown`, das nur die linke Taste meldet.

!!! warning "Ohne `Background` kommt kein Maus-Ereignis"
	Eine Fläche, die nichts malt, fängt keine Maus. Ein `Label` reagiert dann nur dort, wo
	Buchstaben stehen, ein leeres `Grid` überhaupt nicht. `Background="Transparent"` malt
	durchsichtig — man sieht nichts, aber die Fläche ist da. Das ist der häufigste Grund dafür,
	dass `MouseEnter` „manchmal" funktioniert.

## Das Schließen eines Fensters abfangen

Ein Fenster lässt sich auf mehreren Wegen schließen: über eine eigene Schaltfläche, über das
Kreuz in der Titelleiste, über Alt+F4. **Alle Wege kommen bei `Window.Closing` vorbei.** Dort
gehört deshalb die Rückfrage hin — und nur dorthin.

``` xml
<Window ... Closing="Window_Closing">
```

``` csharp
private void Window_Closing(object sender, CancelEventArgs e)      // (1)
{
    MessageBoxResult antwort = MessageBox.Show(
        "Es gibt ungesicherte Änderungen. Wirklich schließen?",
        "Fuhrpark",
        MessageBoxButton.YesNo,
        MessageBoxImage.Question);

    if (antwort == MessageBoxResult.No)
    {
        e.Cancel = true;                                           // (2)
    }
}

private void btEnde_Click(object sender, RoutedEventArgs e)
{
    Close();                                                       // (3)
}
```

1. Braucht `using System.ComponentModel;`.
2. `e.Cancel = true` nimmt das Schließen zurück. Das Fenster bleibt mit allem offen, was
   eingestellt war.
3. Die Schaltfläche schließt nur. Gefragt wird trotzdem — `Closing` läuft ja auch hier.

!!! warning "Nicht zweimal fragen"
	Wer die Rückfrage **zusätzlich** in `btEnde_Click` einbaut, wird beim Klick auf die
	Schaltfläche zweimal gefragt. Wer sie **nur** dort einbaut, hat das Kreuz vergessen. Die
	Frage gehört an die eine Stelle, durch die alles muss.

!!! warning "Kein `Close()` innerhalb von `Closing`"
	Das Fenster schließt sich gerade schon. Ein weiteres `Close()` an dieser Stelle führt zu
	merkwürdigem Verhalten. Wollen Sie das Schließen zulassen, tun Sie einfach nichts.

Zum Aufbau der `MessageBox` und zu ihren Rückgabewerten siehe [Dialoge](dialoge.md), Abschnitt
*MessageBox*.

## Ereignisse laufen, bevor das Fenster fertig ist

Das ist die Falle, in die fast jede erste WPF-Anwendung tappt.

`InitializeComponent()` baut das Fenster aus dem XAML auf — **von oben nach unten**, ein Element
nach dem anderen. Bekommt ein Element dabei einen Anfangswert, meldet es das sofort, obwohl die
weiter unten stehenden Elemente noch gar nicht existieren.

``` xml
<Slider x:Name="slBreite" Minimum="10" Maximum="200" ValueChanged="slBreite_ValueChanged"/>
...
<Label x:Name="laAnzeige" Content="—"/>     <!-- entsteht erst danach -->
```

Der Regler steht anfangs auf 0 und wird durch `Minimum="10"` auf 10 gezogen — das ist eine
Änderung, also läuft `slBreite_ValueChanged`. Dort greift der Code auf `laAnzeige` zu, und das
gibt es in diesem Moment noch nicht: **`NullReferenceException` beim Start**, in einer Zeile, die
richtig aussieht. Dasselbe passiert mit `IsChecked="True"` an einem `RadioButton` und mit einem
`Text="…"` an einer `TextBox` mit `TextChanged`.

Zwei saubere Auswege:

**1. Ein Merker, den die Ereignisse abfragen**

``` csharp
public partial class MainWindow : Window
{
    // Solange das Fenster aufgebaut wird, darf noch nichts angezeigt werden.
    private bool _fensterBereit = false;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        _fensterBereit = true;
        // ... hier die Anfangswerte setzen ...
    }

    private void slBreite_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (!_fensterBereit)
        {
            return;
        }

        laAnzeige.Content = slBreite.Value;
    }
}
```

**2. Anfangswerte gar nicht ins XAML schreiben**

Alles, was einen Anfangszustand herstellt, kommt nach `Loaded`. Dann läuft jedes Ereignis zu
einem Zeitpunkt, an dem es alle Elemente gibt:

``` csharp
private void Window_Loaded(object sender, RoutedEventArgs e)
{
    ListeFuellen();
    lbFahrzeuge.SelectedIndex = 0;   // löst SelectionChanged aus - jetzt gefahrlos
    rbDiesel.IsChecked = true;       // löst Checked aus
    slBreite.Value = 60;             // löst ValueChanged aus
}
```

Der zweite Weg ist der aufgeräumtere: Der Ausgangszustand steht dann an **einer** Stelle im Code
statt verstreut über das XAML. `Minimum` und `Maximum` müssen trotzdem ins XAML — und genau
deshalb braucht der Regler zusätzlich den Merker aus Weg 1.

## Häufige Fehler

| Fehlerbild | Ursache |
| --- | --- |
| `NullReferenceException` sofort beim Start | Ein Ereignis lief während `InitializeComponent`, siehe oben. |
| *„Der Name … ist im aktuellen Kontext nicht vorhanden"* in einer fremden Datei | Methode umbenannt, Attribut im XAML nicht (oder umgekehrt). |
| Beim Klick passiert nichts | Das Attribut fehlt im XAML. Die Methode allein reicht nicht — sie muss angehängt sein. |
| Das Fenster lässt sich nicht mehr schließen | `e.Cancel = true` ohne Bedingung, oder auf `Yes` geprüft statt auf `No`. Notausgang: in Visual Studio das Debuggen beenden. |
| Beim Beenden wird zweimal gefragt | Die Rückfrage steht in `Closing` **und** im `Click` der Schaltfläche. |
| `SelectionChanged` läuft beim Füllen der Liste ins Leere | Das Ereignis kommt auch bei `Items.Clear()`. Immer prüfen, ob überhaupt etwas ausgewählt ist. |
| `MouseEnter` reagiert nur auf dem Text, nicht auf der Fläche | Kein `Background` gesetzt. |
| Beim Umschalten zweier Auswahlknöpfe läuft der Code zweimal | Beim Wechsel wird der eine `Unchecked` und der andere `Checked`. Reagieren Sie nur auf `Checked`. |
