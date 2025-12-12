# Schreiben von Daten in die Datenbank

Das Einfügen neuer Daten in eine MySQL-Datenbank ist ein grundlegender Bestandteil der Interaktion mit relationalen Datenbanken. In C# kann dies effizient mit einem `MySqlCommand`-Objekt erreicht werden, das einen SQL-Insert-Befehl ausführt. Hier ist ein Beispiel, wie Sie Daten in die Tabelle `person` schreiben können, die Felder wie `id`, `Vorname`, `Nachname`, `GebDatum` und `Gehalt` enthält:

```csharp
// Werte definieren
int id = 1;
string vorname = "Max";
string nachname = "Mustermann";
DateTime gebDatum = new DateTime(1990, 1, 1);
double gehalt = 3000.00;

// Hier wird der SQL-Befehl mit Platzhaltern erstellt
string insertQuery = "INSERT INTO person (id, Vorname, Nachname, GebDatum, Gehalt) VALUES (@id, @vorname, @nachname, @gebDatum, @gehalt)";

// Zuerst den Command erstellen
using (MySqlCommand cmd = new MySqlCommand(insertQuery, con))
{
    // Parameter hinzufügen
    cmd.Parameters.AddWithValue("@id", id);
    cmd.Parameters.AddWithValue("@vorname", vorname);
    cmd.Parameters.AddWithValue("@nachname", nachname);
    cmd.Parameters.AddWithValue("@gebDatum", gebDatum);
    cmd.Parameters.AddWithValue("@gehalt", gehalt);

    // Führt den Insert-Befehl aus
    cmd.ExecuteNonQuery(); 
}
```

In diesem Beispiel wird der SQL-Insert-Befehl als String mit Werten vorbereitet.

Das Ausführen des Befehls erfolgt durch Aufrufen von `cmd.ExecuteNonQuery()`. Diese Methode ist ideal für SQL-Statements, die keine Daten zurückgeben, wie INSERT, UPDATE oder DELETE. Vor dem Ausführen des Insert-Befehls sollte sichergestellt werden, dass die Datenbankverbindung geöffnet ist (`con.Open()`).