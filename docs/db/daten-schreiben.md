# Schreiben von Daten in die Datenbank

Das Einfügen neuer Daten in eine MySQL-Datenbank ist ein grundlegender Bestandteil der Interaktion mit relationalen Datenbanken. In C# kann dies effizient mit einem `MySqlCommand`-Objekt erreicht werden, das einen SQL-Insert-Befehl ausführt. Hier ist ein Beispiel, wie Sie Daten in die Tabelle `person` schreiben können, die Felder wie `id`, `Vorname`, `Nachname`, `GebDatum` und `Gehalt` enthält:

```csharp
// Anlegen einer Beispielperson
Person p = new Person(...);

// Hier wird der SQL-Befehl zusammengesetzt
string insertQuery = @$"INSERT INTO person ({p.Id}, {p.Vorname}, {p.Nachname}, {p.GebDatum}, {p.Gehalt}) 
                       VALUES (id, Vorname, Nachname, GebDatum, Gehalt)";

// Zuerst den Command erstellen
using (MySqlCommand cmd = new MySqlCommand(insertQuery, con))
{
    // Führt den Insert-Befehl aus
    cmd.ExecuteNonQuery(); 
}
```

In diesem Beispiel wird der SQL-Insert-Befehl als String mit Werten vorbereitet.

Das Ausführen des Befehls erfolgt durch Aufrufen von `cmd.ExecuteNonQuery()`. Diese Methode ist ideal für SQL-Statements, die keine Daten zurückgeben, wie INSERT, UPDATE oder DELETE. Vor dem Ausführen des Insert-Befehls sollte sichergestellt werden, dass die Datenbankverbindung geöffnet ist (`con.Open()`).