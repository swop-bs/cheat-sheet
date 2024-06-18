# Steuerelemente und Layout

In WPF können verschiedene Steuerelemente und Layout-Container verwendet werden, um die Benutzeroberfläche einer Anwendung zu erstellen. In Visual Studio können Sie diese Elemente einfach aus der Toolbox in das Fenster ziehen. Diese Elemente erscheinen dann sowohl in der visuellen Oberfläche als auch im XAML-Code.

## Verwendung der Toolbox

Die Toolbox in Visual Studio enthält eine Vielzahl von Steuerelementen, die Sie in Ihre WPF-Anwendung einfügen können. Zu den häufig verwendeten Steuerelementen gehören Labels, Buttons, TextBoxen und viele mehr. Um ein Steuerelement hinzuzufügen, ziehen Sie es einfach aus der Toolbox in das Fenster im Designer.

![Toolbox](wpf_project_toolbox.png){ width="400" }

## Beispiel

Der folgende XAML-Code zeigt ein einfaches Beispiel, bei dem ein `Label` und ein `Button` in einem `Grid`-Layout-Container verwendet werden. Diese Elemente wurden aus der Toolbox in das Fenster gezogen.

```xml
<Window x:Class="WpfApp1.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:WpfApp1"
        mc:Ignorable="d"
        Title="MainWindow" Height="450" Width="800">
    <Grid>
        <Label Content="Beispieltext im Label" FontSize="30" HorizontalAlignment="Center" Margin="0,67,0,0" VerticalAlignment="Top"/>
        <Button Content="Beispiel-Button" FontSize="30" HorizontalAlignment="Center" Margin="0,217,0,0" VerticalAlignment="Top" Height="110" Width="278"/>
    </Grid>
</Window>
```

## Erklärung des Codes

- **Window**: Das grundlegende Container-Element für die Benutzeroberfläche der Anwendung. Es definiert das Hauptfenster.
- **Grid**: Ein Layout-Container, der eine flexible Anordnung der UI-Elemente ermöglicht. Elemente können in Zeilen und Spalten angeordnet werden.
- **Label**: Ein einfaches Steuerelement zur Anzeige von Text.
    - `Content`: Der anzuzeigende Text.
    - `FontSize`: Die Schriftgröße des Textes.
    - `HorizontalAlignment` und `VerticalAlignment`: Die Ausrichtung des Labels innerhalb des Grid.
    - `Margin`: Der Abstand um das Label.
- **Button**: Ein Steuerelement, das eine Aktion auslöst, wenn es angeklickt wird.
    - `Content`: Der Text, der auf dem Button angezeigt wird.
    - `FontSize`: Die Schriftgröße des Textes.
    - `HorizontalAlignment` und `VerticalAlignment`: Die Ausrichtung des Buttons innerhalb des Grid.
    - `Height` und `Width`: Die Höhe und Breite des Buttons.

!!! info
    Durch das Ziehen von Steuerelementen aus der Toolbox in das Fenster im Designer werden diese Elemente automatisch im XAML-Code der entsprechenden Datei eingefügt. Dies ermöglicht eine visuelle Bearbeitung der Benutzeroberfläche und eine einfache Anpassung des Layouts und der Steuerelemente.