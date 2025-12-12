# Lesen von Daten aus der Datenbank

Nachdem eine Verbindung zur MySQL-Datenbank erfolgreich hergestellt wurde, ist der nächste Schritt oft das Lesen von Daten. Dies kann durch Ausführen eines SQL-Befehls mit einem `MySqlCommand`-Objekt und dem Abrufen der Ergebnisse durch einen `MySqlDataReader` erfolgen. Hier ist ein einfaches Beispiel, das zeigt, wie Daten aus einer Tabelle namens `person` gelesen werden können:

```csharp
MySqlCommand cmd = new MySqlCommand("SELECT * FROM person", con);
MySqlDataReader rdr = cmd.ExecuteReader();

while (rdr.Read())
{
    int id = rdr.GetInt32("id");
    string vn = rdr.GetString("Vorname");
    string nn = rdr.GetString("Nachname");
    DateTime datum = rdr.GetDateTime("GebDatum");
    double gehalt = rdr.GetDouble("Gehalt");

    Console.WriteLine($"{id} {nn} {vn} {datum.ToShortDateString()} {gehalt}");
}

rdr.Close();
```

In diesem Beispiel führt `cmd.ExecuteReader()` den SQL-Befehl `SELECT * FROM person` aus und gibt einen `MySqlDataReader` (`rdr`) zurück, der die Daten enthält. Die `while`-Schleife mit `rdr.Read()` iteriert über alle Datensätze im Ergebnis. Innerhalb der Schleife können Sie auf die einzelnen Spaltenwerte des aktuellen Datensatzes zugreifen, indem Sie die entsprechenden `Get...`-Methoden des `MySqlDataReader` verwenden, z.B. `GetInt32` für Integer, `GetString` für String-Werte, `GetDateTime` für Datumswerte und `GetDouble` für Gleitkommazahlen. Diese Werte werden dann in der Konsole ausgegeben.

Es ist wichtig, den `MySqlDataReader` nach Gebrauch mit `rdr.Close()` zu schließen, um Ressourcen freizugeben. Alternativ können Sie auch das `using`-Statement verwenden, das den `MySqlDataReader` automatisch schließt, sobald der Block abgeschlossen ist:

```csharp
using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM person", con))
using (MySqlDataReader rdr = cmd.ExecuteReader())
{
    while (rdr.Read())
    {
        // Verarbeiten der Daten wie oben
    }
}
```

Dieser Ansatz stellt sicher, dass alle Ressourcen ordnungsgemäß bereinigt werden und ist besonders nützlich in Szenarien, wo mehrere Datenbankoperationen durchgeführt werden und die Übersichtlichkeit des Codes gewahrt bleiben soll.