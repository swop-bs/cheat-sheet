# Beispiel

Hier ist ein vollständiges C#-Beispiel, das den Aufbau einer Verbindung zu einer MySQL-Datenbank, das Schreiben von Daten in eine Tabelle und das anschließende Lesen dieser Daten demonstriert. Dieses Beispiel setzt voraus, dass Sie bereits das `MySql.Data`-Paket installiert haben und eine Tabelle namens `person` in Ihrer Datenbank existiert, die Felder wie `id`, `Vorname`, `Nachname`, `GebDatum` und `Gehalt` enthält.

```csharp
using System;
using MySql.Data.MySqlClient;

namespace MySQLConnectionExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=bszw.ddns.net;Database=xxx;Uid=yyy;Pwd=zzz;";
            
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                try
                {
                    // Schreiben von Daten in die Datenbank
                    string insertQuery = "INSERT INTO person (id, Vorname, Nachname, GebDatum, Gehalt) VALUES (@id, @vorname, @nachname, @gebDatum, @gehalt)";

                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@id", 1);
                        cmd.Parameters.AddWithValue("@vorname", "Max");
                        cmd.Parameters.AddWithValue("@nachname", "Mustermann");
                        cmd.Parameters.AddWithValue("@gebDatum", new DateTime(2000, 1, 1));
                        cmd.Parameters.AddWithValue("@gehalt", 3500.50);

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Datensatz erfolgreich hinzugefügt.");
                    }

                    // Lesen von Daten aus der Datenbank
                    string selectQuery = "SELECT * FROM person";
                    using (MySqlCommand cmd = new MySqlCommand(selectQuery, con))
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            int id = rdr.GetInt32("id");
                            string vorname = rdr.GetString("Vorname");
                            string nachname = rdr.GetString("Nachname");
                            DateTime gebDatum = rdr.GetDateTime("GebDatum");
                            double gehalt = rdr.GetDouble("Gehalt");

                            Console.WriteLine($"{id} {nachname} {vorname} {gebDatum.ToShortDateString()} {gehalt}");
                        }
                    }
                }
                catch (MySqlException e)
                {
                    Console.WriteLine($"Fehler: {e.Message}");
                }
            }
        }
    }
}
```

Dieses Beispiel umfasst:

1. **Verbindungsaufbau**: Zu Beginn wird eine Verbindung zur MySQL-Datenbank hergestellt.
2. **Schreiben von Daten**: Es werden Daten in die `person`-Tabelle eingefügt. Beachten Sie die Verwendung von Parametern im SQL-Befehl zur Vermeidung von SQL-Injection.
3. **Lesen von Daten**: Anschließend werden die Daten mit einem SELECT-Befehl abgerufen und ausgegeben.
4. **Verbindungsmanagement**: Die Verwendung des `using`-Statements sorgt dafür, dass die Verbindung und andere Ressourcen korrekt freigegeben werden, unabhängig davon, ob der Vorgang erfolgreich war oder ein Fehler aufgetreten ist.

Stellen Sie sicher, dass Sie die Verbindungszeichenfolge entsprechend Ihrer Datenbankkonfiguration anpassen und die richtigen Werte für Server, Datenbank, Benutzer-ID und Passwort verwenden.