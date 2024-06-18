# Oberflächenprogrammierung Einführung

## Was ist WPF?

Windows Presentation Foundation (WPF) ist ein grafisches Subsystem von Microsoft, das für die Entwicklung von Benutzeroberflächen für Windows-Anwendungen verwendet wird. Es wurde erstmals mit .NET Framework 3.0 eingeführt und bietet eine moderne Alternative zur klassischen Windows Forms-Technologie. WPF verwendet XAML (Extensible Application Markup Language) zur Beschreibung der Benutzeroberfläche und ermöglicht die Trennung von UI-Design und Geschäftslogik.

!!! note
    WPF bietet zahlreiche Vorteile wie Datenbindung, Vorlagen, Animationen und Medienintegration, die es Entwicklern erleichtern, reichhaltige und interaktive Benutzeroberflächen zu erstellen.

## Was ist MVVM?

MVVM (Model-View-ViewModel) ist ein Architekturdesignmuster, das speziell für die Trennung der Entwicklungsaufgaben bei der Erstellung von Anwendungen mit grafischen Benutzeroberflächen entwickelt wurde. MVVM trennt die Benutzeroberfläche (View), die Geschäftslogik (Model) und die Vermittlung zwischen beiden (ViewModel).

![alt text](mvvm.png)

- **Model**: Repräsentiert die Daten und Geschäftslogik der Anwendung. Es enthält die Geschäftsregeln und die Datenvalidierung.
- **View**: Repräsentiert die Benutzeroberfläche. Sie zeigt die Daten des Models an und sendet Benutzeraktionen an das ViewModel.
- **ViewModel**: Vermittelt zwischen Model und View. Es hält die Zustände der View, verarbeitet Benutzerinteraktionen und aktualisiert das Model.

### Warum MVVM in WPF verwenden?

MVVM ist besonders gut für WPF-Anwendungen geeignet, da es die Vorteile der WPF-Datenbindung voll ausschöpft und eine klare Trennung der Verantwortlichkeiten ermöglicht. Durch die Verwendung von MVVM können Entwickler:

- **Wiederverwendbare Komponenten** erstellen: Da die Logik im ViewModel und nicht in der View enthalten ist, können Komponenten leichter wiederverwendet werden.
- **Bessere Testbarkeit** erreichen: Geschäftslogik und Benutzerschnittstelle sind getrennt, was Unit-Tests erleichtert.
- **Verbesserte Wartbarkeit** erzielen: Der Code ist modularer und daher einfacher zu verstehen und zu warten.

!!! info
    Die Trennung von Geschäftslogik und UI-Code führt zu einer saubereren Architektur und fördert die Single Responsibility Principle (SRP).

