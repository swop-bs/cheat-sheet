# JSON serialisieren & deserialisieren

Für die Arbeit mit JSON bringt .NET die Bibliothek **`System.Text.Json`** mit. Sie ist modern, schnell und ohne zusätzliches NuGet-Paket direkt verfügbar. Die zentrale Klasse ist `JsonSerializer` mit den beiden Methoden `Serialize` (Objekt → JSON) und `Deserialize` (JSON → Objekt).

```csharp
using System.Text.Json;                 // JsonSerializer, JsonSerializerOptions
using System.Text.Json.Serialization;   // Attribute wie [JsonPropertyName], [JsonIgnore]
```

## Beispielklasse

Alle Beispiele auf dieser Seite verwenden die Klasse `WeatherForecast`:

```csharp
public class WeatherForecast
{
    public DateTimeOffset Date { get; set; }
    public int TemperatureCelsius { get; set; }
    public string? Summary { get; set; }
}
```

!!! info
    Standardmäßig werden nur **öffentliche Properties** verarbeitet. Öffentliche **Felder** (`public string Summary;`) werden ohne zusätzliche Konfiguration **ignoriert**.

## Serialisieren (Objekt → JSON)

Zum Schreiben von JSON wird `JsonSerializer.Serialize` aufgerufen.

```csharp
var weatherForecast = new WeatherForecast
{
    Date = DateTimeOffset.Parse("2019-08-01"),
    TemperatureCelsius = 25,
    Summary = "Hot"
};

string jsonString = JsonSerializer.Serialize(weatherForecast);
Console.WriteLine(jsonString);
```

??? quote "Output"
    ``` json
    {"Date":"2019-08-01T00:00:00+02:00","TemperatureCelsius":25,"Summary":"Hot"}
    ```

!!! info "Zeitzone"
    `DateTimeOffset` enthält die Zeitzonenverschiebung. Der Wert `+02:00` entspricht der mitteleuropäischen Sommerzeit – auf einem Rechner mit anderer Zeitzone kann hier ein anderer Versatz stehen.

### Lesbar formatieren (Pretty-Print)

Die Ausgabe ist standardmäßig **minimiert** (alles in einer Zeile). Mit `JsonSerializerOptions` und `WriteIndented = true` wird sie eingerückt und damit besser lesbar.

```csharp
var options = new JsonSerializerOptions { WriteIndented = true };
string jsonString = JsonSerializer.Serialize(weatherForecast, options);
Console.WriteLine(jsonString);
```

??? quote "Output"
    ``` json
    {
      "Date": "2019-08-01T00:00:00+02:00",
      "TemperatureCelsius": 25,
      "Summary": "Hot"
    }
    ```

### In eine Datei schreiben

Der einfachste Weg: zuerst in einen String serialisieren, dann mit `File.WriteAllText` speichern.

```csharp
string jsonString = JsonSerializer.Serialize(weatherForecast, options);
File.WriteAllText("WeatherForecast.json", jsonString);
```

??? tip "Variante: direkt in einen Stream (asynchron)"
    Bei großen Datenmengen kann ohne Umweg über einen String direkt in einen `FileStream` geschrieben werden. Dafür muss der Code in einem `async`-Kontext stehen – in .NET 8 ist das mit den standardmäßigen Top-Level-Statements ohne weiteres möglich, bei einer klassischen `Main`-Methode muss diese als `static async Task Main(...)` deklariert werden.

    ```csharp
    using FileStream createStream = File.Create("WeatherForecast.json");
    await JsonSerializer.SerializeAsync(createStream, weatherForecast);
    ```

## Deserialisieren (JSON → Objekt)

Zum Einlesen wird `JsonSerializer.Deserialize<T>` mit der Zielklasse als Typparameter aufgerufen. Der Rückgabetyp ist **nullable** (`WeatherForecast?`), da das Ergebnis bei leerer Eingabe `null` sein kann.

```csharp
string jsonString = """
{
  "Date": "2019-08-01T00:00:00+02:00",
  "TemperatureCelsius": 25,
  "Summary": "Hot"
}
""";

WeatherForecast? weatherForecast =
    JsonSerializer.Deserialize<WeatherForecast>(jsonString);

Console.WriteLine($"Datum: {weatherForecast?.Date}");
Console.WriteLine($"Temperatur: {weatherForecast?.TemperatureCelsius} °C");
Console.WriteLine($"Zusammenfassung: {weatherForecast?.Summary}");
```

??? quote "Output"
    ``` text
    Datum: 01.08.2019 00:00:00 +02:00
    Temperatur: 25 °C
    Zusammenfassung: Hot
    ```

!!! info "Datumsformat abhängig von der Spracheinstellung"
    `Console.WriteLine` gibt das Datum im Format der **System-Spracheinstellung** aus (hier `de-DE`). Auf einem englisch eingestellten System sähe es z. B. `8/1/2019 12:00:00 AM +02:00` aus. Der Versatz `+02:00` stammt dagegen direkt aus dem eingelesenen Text.

### Aus einer Datei lesen

Datei mit `File.ReadAllText` einlesen und anschließend deserialisieren.

```csharp
string jsonString = File.ReadAllText("WeatherForecast.json");
WeatherForecast? weatherForecast =
    JsonSerializer.Deserialize<WeatherForecast>(jsonString);
```

??? tip "Variante: direkt aus einem Stream (asynchron)"
    ```csharp
    using FileStream openStream = File.OpenRead("WeatherForecast.json");
    WeatherForecast? weatherForecast =
        await JsonSerializer.DeserializeAsync<WeatherForecast>(openStream);
    ```

## Deserialisierungsverhalten

Beim Deserialisieren gelten einige Standardregeln, die man kennen sollte:

- **Groß-/Kleinschreibung wird beachtet** (case-sensitive): Der JSON-Schlüssel muss exakt wie die Property heißen. Bei `"summary"` statt `"Summary"` bleibt die Property leer – **ohne** Fehlermeldung (siehe [Optionen](#optionen-jsonserializeroptions)).
- **Felder werden ignoriert**, nur Properties werden befüllt.
- **Zusätzliche JSON-Eigenschaften**, für die es keine Property gibt, werden stillschweigend übersprungen.
- **Enums** werden standardmäßig als **Zahl** erwartet, nicht als Text.
- **Kommentare** und **abschließende Kommas** (trailing commas) im JSON lösen standardmäßig eine Ausnahme aus.

!!! warning "Häufige Fehlerquelle: Schreibweise der Schlüssel"
    Wenn nach dem Deserialisieren unerwartet Werte fehlen (`null` oder `0`), stimmt fast immer die **Schreibweise** zwischen JSON-Schlüssel und C#-Property nicht überein. Es wird dabei **keine** Exception geworfen.

## Optionen (`JsonSerializerOptions`)

Über ein `JsonSerializerOptions`-Objekt lässt sich das Verhalten anpassen. Es wird als zweiter Parameter an `Serialize`/`Deserialize` übergeben.

| Option | Wirkung |
| ------ | ------- |
| `WriteIndented = true` | Lesbare, eingerückte Ausgabe (Standard = minimiert). |
| `PropertyNameCaseInsensitive = true` | Beim **Deserialisieren** wird die Groß-/Kleinschreibung der Schlüssel ignoriert. |
| `PropertyNamingPolicy = JsonNamingPolicy.CamelCase` | Alle Namen als `camelCase` (beim Serialisieren **und** Deserialisieren). |

```csharp
// Groß-/Kleinschreibung beim Einlesen ignorieren
var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

string jsonString = """{ "summary": "Hot", "temperatureCelsius": 25 }""";
WeatherForecast? wf = JsonSerializer.Deserialize<WeatherForecast>(jsonString, options);
```

!!! tip "Performance"
    Wird mit denselben Optionen mehrfach (de)serialisiert, sollte **dieselbe** `JsonSerializerOptions`-Instanz wiederverwendet und nicht bei jedem Aufruf neu erzeugt werden.

## Eigenschaften anpassen (Attribute)

Einzelne Properties lassen sich mit Attributen aus `System.Text.Json.Serialization` steuern.

| Attribut | Wirkung |
| -------- | ------- |
| `[JsonPropertyName("...")]` | Legt den JSON-Namen einer **einzelnen** Property fest (überschreibt die `PropertyNamingPolicy`). |
| `[JsonIgnore]` | Schließt die Property von der (De-)Serialisierung aus. |

```csharp
public class WeatherForecast
{
    [JsonPropertyName("datum")]
    public DateTimeOffset Date { get; set; }

    [JsonPropertyName("temperatur")]
    public int TemperatureCelsius { get; set; }

    public string? Summary { get; set; }

    [JsonIgnore]
    public string? InterneNotiz { get; set; }   // taucht im JSON nicht auf
}
```

??? quote "Output"
    ``` json
    {
      "datum": "2019-08-01T00:00:00+02:00",
      "temperatur": 25,
      "Summary": "Hot"
    }
    ```

## Methodenübersicht

| Methode | Erklärung |
| ------- | --------- |
| [`JsonSerializer.Serialize(obj)`](https://learn.microsoft.com/de-de/dotnet/api/system.text.json.jsonserializer.serialize) | Serialisiert ein Objekt in einen JSON-String. |
| [`JsonSerializer.SerializeAsync(stream, obj)`](https://learn.microsoft.com/de-de/dotnet/api/system.text.json.jsonserializer.serializeasync) | Schreibt ein Objekt asynchron als JSON in einen Stream. |
| [`JsonSerializer.Deserialize<T>(json)`](https://learn.microsoft.com/de-de/dotnet/api/system.text.json.jsonserializer.deserialize) | Erzeugt aus einem JSON-String ein Objekt vom Typ `T`. |
| [`JsonSerializer.DeserializeAsync<T>(stream)`](https://learn.microsoft.com/de-de/dotnet/api/system.text.json.jsonserializer.deserializeasync) | Liest ein Objekt vom Typ `T` asynchron aus einem Stream. |

!!! note "Weiterführend"
    Eine ausführliche Beschreibung des Deserialisierens findet sich in der offiziellen Microsoft-Dokumentation: [Deserialisieren von JSON in C#](https://learn.microsoft.com/de-de/dotnet/standard/serialization/system-text-json/deserialization){:target="_blank"}.
