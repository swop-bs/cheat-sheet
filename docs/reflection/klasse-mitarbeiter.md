# Klasse Mitarbeiter

Dieses Kapitel verwendet die Beispielklasse `Mitarbeiter` mit privaten Attributen, die durch Properties zugänglich gemacht werden, sowie mit Konstruktoren und Methoden, um die Nutzung von Reflection zu demonstrieren.

```mermaid
classDiagram
    class Mitarbeiter {
        - string _vorname
        - string _nachname
        - double _gehalt
        - string _arbeitsort
        «property» + string Vorname
        «property» + string Nachname
        «property» + double Gehalt
        «property» + string Arbeitsort
        + Mitarbeiter()
        + Mitarbeiter(string vorname, string nachname, double gehalt, string arbeitsort)
        + void AnzeigenInformationen()
        + void ErhoeheGehalt(double betrag)
        + double BerechneJahresgehalt()
    }
```