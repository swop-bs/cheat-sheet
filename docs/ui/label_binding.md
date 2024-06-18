# Daten an ein Element binden

In diesem Abschnitt erklären wir, wie Sie Daten an ein Element in Ihrer WPF-Anwendung binden können. Wir verwenden dazu das `Label` und den `Button`, die wir bereits in unserem Beispiel verwendet haben.

## Variable für den Inhalt im ViewModel anlegen

Zuerst müssen wir im `MainWindowViewModel` eine Variable für den Inhalt anlegen. Fügen Sie dazu den folgenden Code in `MainWindowViewModel.cs` ein:

```csharp
namespace WpfApp1
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        // Private Variable für den Label-Inhalt
        private string labelTest = "TestLabel";

        // Öffentliche Eigenschaft für den Label-Inhalt
        public string LabelTest
        {
            get => labelTest;
            set
            {
                labelTest = value;
            }
        }

        // Deklarieren des PropertyChanged-Ereignisses
        public event PropertyChangedEventHandler? PropertyChanged;

        // OnPropertyChanged löst das PropertyChanged-Ereignis aus und übergibt
        // die Quelleigenschaft, die aktualisiert wird.
        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
```

## DataContext im Grid des MainWindow setzen

Damit wir das ViewModel in der Benutzeroberfläche verwenden können, muss es im `Grid` des `MainWindow` deklariert werden. Ändern Sie den XAML-Code von `MainWindow.xaml` wie folgt:

```xml
<!-- App.xaml -->
<Window x:Class="WpfApp1.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:WpfApp1"
        mc:Ignorable="d"
        Title="MainWindow" Height="450" Width="800">
    <Grid DataContext="{StaticResource mwvm}">
        <Label Content="{Binding LabelTest}" FontSize="30" HorizontalAlignment="Center" Margin="0,67,0,0" VerticalAlignment="Top"/>
        <Button Content="Beispiel-Button" FontSize="30" HorizontalAlignment="Center" Margin="0,217,0,0" VerticalAlignment="Top" Height="110" Width="278"/>
    </Grid>
</Window>
```

## Erklärung
Wir haben das MainWindowViewModel "mwvm" [bereits instanziiert](databinding.md#instanziieren-der-viewmodel-klasse-in-appxaml). Dies können wir nun in unserer Anwendung verwenden.

- **DataContext setzen**: Durch das Setzen des `DataContext` im `Grid` auf `{StaticResource mwvm}` wird das `MainWindowViewModel` als Datenkontext für alle untergeordneten Elemente des `Grid` festgelegt. Dadurch können die UI-Elemente auf die Eigenschaften des ViewModels zugreifen.

## Inhalt des Labels an die Variable des ViewModels binden

Nun können wir den Inhalt des Labels auf die Variable des ViewModels binden. Ändern Sie den `Label`-Tag wie folgt:

```xml
<Label Content="{Binding LabelTest}" FontSize="30" HorizontalAlignment="Center" Margin="0,67,0,0" VerticalAlignment="Top"/>
```

### Erklärung

- **Binding**: Das `Content`-Attribut des `Label`-Elements ist an die Eigenschaft `LabelTest` des ViewModels gebunden. Dies bedeutet, dass der Text des Labels automatisch den Wert der `LabelTest`-Eigenschaft anzeigt.
  
!!! tip
    Eigentlich können alle Eigenschaften von XAML-Elementen an Eigenschaften des ViewModels gebunden werden. In diesem Beispiel binden wir den `Content`, der beschreibt, welchen Text das Label beinhaltet. Sie könnten jedoch auch andere Eigenschaften wie `FontSize`, `Visibility` usw. binden.

Durch die Verwendung von Datenbindung können Sie eine lose Kopplung zwischen der Benutzeroberfläche und der Geschäftslogik erreichen, was die Wartbarkeit und Flexibilität Ihrer Anwendung verbessert.