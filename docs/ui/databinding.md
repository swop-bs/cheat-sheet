# Datenbindung

Datenbindung ist ein zentrales Konzept in WPF, das es ermöglicht, die Benutzeroberfläche (View) mit der Geschäftslogik (Model) zu verbinden. Dies wird oft im Rahmen des MVVM-Modells (Model-View-ViewModel) umgesetzt.

## MVVM-Modell

MVVM (Model-View-ViewModel) ist ein Architekturdesignmuster, das speziell für die Entwicklung von WPF-Anwendungen entwickelt wurde. Es trennt die Logik der Anwendung in drei Hauptkomponenten:

- **Model**: Repräsentiert die Daten und die Geschäftslogik der Anwendung.
- **View**: Repräsentiert die Benutzeroberfläche. Sie zeigt die Daten des Models an und sendet Benutzeraktionen an das ViewModel.
- **ViewModel**: Vermittelt zwischen Model und View. Es hält die Zustände der View, verarbeitet Benutzerinteraktionen und aktualisiert das Model.

## Erstellen einer Klasse "MainWindowViewModel.cs"

Um die Datenbindung in einer WPF-Anwendung zu implementieren, erstellen wir eine ViewModel-Klasse. Führen Sie die folgenden Schritte aus, um eine neue Klasse "MainWindowViewModel.cs" hinzuzufügen:

1. Rechtsklick auf das Projekt im Projektmappen-Explorer.
2. Wählen Sie **Hinzufügen** > **Klasse...**.
3. Benennen Sie die Klasse `MainWindowViewModel.cs`.

Fügen Sie den folgenden Code in die Datei `MainWindowViewModel.cs` ein:

```csharp
using System.ComponentModel;

namespace WpfApp1
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
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

### Erklärung des Codes

- **INotifyPropertyChanged**: Ein Interface, das eine Benachrichtigung bereitstellt, wenn sich der Wert einer Eigenschaft ändert. Dies ist wichtig für die Datenbindung.
- **PropertyChanged-Ereignis**: Ein Ereignis, das ausgelöst wird, wenn sich der Wert einer Eigenschaft ändert.
- **OnPropertyChanged-Methode**: Eine Methode, die das `PropertyChanged`-Ereignis auslöst und den Namen der geänderten Eigenschaft übergibt.

### Das PropertyChanged-Ereignis im Detail

Das PropertyChanged-Ereignis spielt eine entscheidende Rolle in der Kommunikation zwischen dem ViewModel und der View. Das ViewModel kennt die Funktionalität der View nicht direkt, und dies ermöglicht eine lose Kopplung zwischen beiden Komponenten. Wenn sich der Wert einer Eigenschaft im ViewModel ändert, wird das PropertyChanged-Ereignis ausgelöst. Die View, die an diese Eigenschaft gebunden ist, wird benachrichtigt und kann entsprechend reagieren, z.B. durch das Aktualisieren der Anzeige.

![Abbildung On Property Changed](on_property_changed.png) 

Diese lose Kopplung ist wichtig, weil:

- Trennung der Verantwortlichkeiten: Die View kennt die Funktionen des ViewModels und kann sie aufrufen, um Daten anzuzeigen oder Benutzerinteraktionen zu verarbeiten. Das ViewModel kennt jedoch die Details der View nicht und ist nicht dafür verantwortlich, wie die Daten angezeigt werden.
- Flexibilität und Wiederverwendbarkeit: Durch die Entkopplung der View und des ViewModels können Sie die Benutzeroberfläche ändern, ohne die Geschäftslogik zu beeinflussen, und umgekehrt.

## Instanziieren der ViewModel-Klasse in App.xaml

Damit die `MainWindowViewModel`-Klasse in der Anwendung verwendet werden kann, müssen wir sie in der `App.xaml`-Datei instanziieren. Hier ist der Code, der in `App.xaml` eingefügt werden muss:

```xml
<Application x:Class="WpfApp1.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:local="clr-namespace:WpfApp1"
             StartupUri="MainWindow.xaml">
    <Application.Resources>
        <local:MainWindowViewModel x:Key="mwvm"/>
    </Application.Resources>
</Application>
```

!!! bug
    Die hinzugefügte Zeile wird von der IDE beim ersten Hinzufügen in der Regel als fehlerhaft makiert. Starten Sie die Anwendung einmal, damit dieser Fehler nicht mehr auftritt.

### Erklärung des Inhalts von Application.Resources

- **Application.Resources**: Ein Bereich, in dem Ressourcen definiert werden, die in der gesamten Anwendung verwendet werden können.
- **local:MainWindowViewModel**: Instanziiert die `MainWindowViewModel`-Klasse.
- **x:Key**: Ein eindeutiger Schlüssel, mit dem die Ressource referenziert werden kann.

Durch das Hinzufügen des ViewModels zu den Anwendungsressourcen können wir es in der gesamten Anwendung verwenden und an die UI-Elemente binden.

!!! info
    Durch die Verwendung des MVVM-Musters und die Implementierung der Datenbindung in WPF können Sie die Benutzeroberfläche und die Geschäftslogik klar trennen. Dies erleichtert die Wartbarkeit und Testbarkeit Ihrer Anwendung erheblich.

