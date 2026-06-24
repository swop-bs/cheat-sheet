# XML serialisieren & deserialisieren

Für XML wird die Bibliothek **`System.Xml.Serialization`** mit der Klasse `XmlSerializer` verwendet. Das Vorgehen unterscheidet sich von JSON: Ein `XmlSerializer` wird einmalig für einen bestimmten **Typ** erstellt und liest bzw. schreibt dann über einen `TextWriter` (Schreiben) bzw. `TextReader` (Lesen).

```csharp
using System.Xml.Serialization;   // XmlSerializer und die Xml-Attribute
using System.IO;                  // StringWriter, StringReader, StreamWriter, StreamReader
```

## Beispielklasse

Es wird dieselbe Klasse wie bei [JSON](json.md) verwendet:

```csharp
public class WeatherForecast
{
    public DateTimeOffset Date { get; set; }
    public int TemperatureCelsius { get; set; }
    public string? Summary { get; set; }
}
```

!!! warning "Voraussetzungen für `XmlSerializer`"
    Damit eine Klasse mit `XmlSerializer` funktioniert, muss sie zwei Bedingungen erfüllen:

    1. Ein **öffentlicher, parameterloser Konstruktor** muss vorhanden sein (hier implizit gegeben). Er wird beim Deserialisieren benötigt, um das Objekt zu erzeugen.
    2. Es werden nur **öffentliche** Felder und Properties serialisiert. Private Member, Methoden und Logik gehen verloren.

## Serialisieren (Objekt → XML)

Zuerst wird ein `XmlSerializer` für den Typ `WeatherForecast` erstellt. Das Objekt wird dann über einen `TextWriter` geschrieben. Mit einem `StringWriter` landet das Ergebnis in einem String.

```csharp
var weatherForecast = new WeatherForecast
{
    Date = DateTimeOffset.Parse("2019-08-01"),
    TemperatureCelsius = 25,
    Summary = "Hot"
};

var serializer = new XmlSerializer(typeof(WeatherForecast));

using var writer = new StringWriter();
serializer.Serialize(writer, weatherForecast);
Console.WriteLine(writer.ToString());
```

??? quote "Output"
    ``` xml
    <?xml version="1.0" encoding="utf-16"?>
    <WeatherForecast xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
      <Date>2019-08-01T00:00:00+02:00</Date>
      <TemperatureCelsius>25</TemperatureCelsius>
      <Summary>Hot</Summary>
    </WeatherForecast>
    ```

!!! info "Was fällt an der Ausgabe auf?"
    - Der Klassenname `WeatherForecast` wird zum **Wurzelelement**, jede Property zu einem **Kindelement**.
    - `XmlSerializer` ergänzt automatisch die beiden Namespaces `xsi` und `xsd`. Diese sind technisch korrekt und können meist ignoriert werden.
    - Die Kodierung steht auf `utf-16`, weil ein `StringWriter` intern mit UTF-16 arbeitet. Beim Schreiben in eine Datei über einen `StreamWriter` ist es standardmäßig `utf-8`.

### In eine Datei schreiben

Statt eines `StringWriter` wird ein `StreamWriter` verwendet, der direkt in eine Datei schreibt.

```csharp
var serializer = new XmlSerializer(typeof(WeatherForecast));

using var writer = new StreamWriter("WeatherForecast.xml");
serializer.Serialize(writer, weatherForecast);
```

!!! info
    Durch das [`using`](../grundlagen/dateien.md#using) wird der `StreamWriter` am Ende automatisch geschlossen und die Datei freigegeben.

## Deserialisieren (XML → Objekt)

Beim Lesen liest der `XmlSerializer` aus einem `TextReader`. Mit einem `StringReader` wird ein vorhandener XML-String eingelesen.

!!! warning "Cast erforderlich"
    `Deserialize` gibt ein allgemeines `object?` zurück. Das Ergebnis muss deshalb explizit auf den Zieltyp **gecastet** werden.

```csharp
string xmlString = """
<?xml version="1.0" encoding="utf-16"?>
<WeatherForecast>
  <Date>2019-08-01T00:00:00+02:00</Date>
  <TemperatureCelsius>25</TemperatureCelsius>
  <Summary>Hot</Summary>
</WeatherForecast>
""";

var serializer = new XmlSerializer(typeof(WeatherForecast));

using var reader = new StringReader(xmlString);
var weatherForecast = (WeatherForecast?)serializer.Deserialize(reader);

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

!!! info "Hinweise zu Eingabe und Ausgabe"
    - Die beim Serialisieren erzeugten Namespaces `xmlns:xsi` und `xmlns:xsd` sind beim **Einlesen optional** – das XML oben funktioniert auch ohne sie.
    - `Console.WriteLine` gibt das Datum im Format der **System-Spracheinstellung** aus (hier `de-DE`); auf einem englisch eingestellten System z. B. `8/1/2019 12:00:00 AM +02:00`. Der Versatz `+02:00` stammt direkt aus dem eingelesenen Text.

### Aus einer Datei lesen

Statt eines `StringReader` wird ein `StreamReader` (oder `FileStream`) verwendet, der die Datei einliest.

```csharp
var serializer = new XmlSerializer(typeof(WeatherForecast));

using var reader = new StreamReader("WeatherForecast.xml");
var weatherForecast = (WeatherForecast?)serializer.Deserialize(reader);
```

## Steuerung über Attribute

Standardmäßig richtet sich die XML-Struktur nach den Klassen- und Property-Namen. Mit Attributen aus `System.Xml.Serialization` lässt sich die Ausgabe gezielt anpassen.

| Attribut | Wirkung |
| -------- | ------- |
| `[XmlRoot("...")]` | Legt den Namen des **Wurzelelements** fest (nur auf einer Klasse). |
| `[XmlElement("...")]` | Benennt das **Kindelement** einer Property um. |
| `[XmlAttribute("...")]` | Schreibt die Property als **XML-Attribut** statt als Kindelement. |
| `[XmlArray("...")]` | Benennt das **umschließende** Element einer Liste/eines Arrays. |
| `[XmlArrayItem("...")]` | Benennt die **einzelnen Einträge** einer Liste/eines Arrays. |
| `[XmlIgnore]` | Schließt eine Property von der Serialisierung **aus**. |
| `[XmlText]` | Schreibt die Property als reinen **Elementtext** (ohne eigenes Tag). |

### Beispiel mit Attributen

Für dieses Beispiel wird die `WeatherForecast`-Klasse erweitert: um die Property `Einheit` (die als XML-**Attribut** geschrieben wird) und eine `InterneNotiz` (die mit `[XmlIgnore]` von der Serialisierung ausgeschlossen wird).

```csharp
[XmlRoot("Wettervorhersage")]
public class WeatherForecast
{
    [XmlElement("Datum")]
    public DateTimeOffset Date { get; set; }

    [XmlAttribute("einheit")]
    public string Einheit { get; set; } = "Celsius";

    [XmlElement("Temperatur")]
    public int TemperatureCelsius { get; set; }

    public string? Summary { get; set; }

    [XmlIgnore]
    public string? InterneNotiz { get; set; }   // taucht im XML nicht auf
}
```

??? quote "Output"
    ``` xml
    <?xml version="1.0" encoding="utf-16"?>
    <Wettervorhersage xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" einheit="Celsius">
      <Datum>2019-08-01T00:00:00+02:00</Datum>
      <Temperatur>25</Temperatur>
      <Summary>Hot</Summary>
    </Wettervorhersage>
    ```

### Listen mit `[XmlArray]` und `[XmlArrayItem]`

Enthält ein Objekt eine Liste, steuern `[XmlArray]` (das umschließende Element) und `[XmlArrayItem]` (die einzelnen Einträge) die Struktur.

```csharp
[XmlRoot("Wettervorhersagen")]
public class WeatherReport
{
    [XmlArray("Tage")]
    [XmlArrayItem("Tag")]
    public List<WeatherForecast> Forecasts { get; set; } = new();
}
```

??? quote "Output"
    ``` xml
    <?xml version="1.0" encoding="utf-16"?>
    <Wettervorhersagen xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
      <Tage>
        <Tag>
          <Date>2019-08-01T00:00:00+02:00</Date>
          <TemperatureCelsius>25</TemperatureCelsius>
          <Summary>Hot</Summary>
        </Tag>
        <Tag>
          <Date>2019-08-02T00:00:00+02:00</Date>
          <TemperatureCelsius>28</TemperatureCelsius>
          <Summary>Sunny</Summary>
        </Tag>
      </Tage>
    </Wettervorhersagen>
    ```

## Typische Stolperfallen

!!! danger "Worauf zu achten ist"
    - **Kein parameterloser Konstruktor** → beim Deserialisieren tritt zur Laufzeit ein Fehler auf. Sobald ein Konstruktor mit Parametern definiert wird, muss der parameterlose Konstruktor wieder **manuell** ergänzt werden.
    - **Cast vergessen** → `Deserialize` liefert `object?`; ohne Cast auf den Zieltyp lässt sich nicht auf die Properties zugreifen.
    - **Private Member / Methoden** werden nicht serialisiert – nur öffentliche Felder und Properties.
    - **`DateTimeOffset`** wird von `XmlSerializer` unterstützt; ältere .NET-Framework-Versionen (vor 4.5) kannten den Typ jedoch noch nicht.

## Methodenübersicht

| Element | Erklärung |
| ------- | --------- |
| [`new XmlSerializer(typeof(T))`](https://learn.microsoft.com/de-de/dotnet/api/system.xml.serialization.xmlserializer.-ctor) | Erstellt einen Serialisierer für den Typ `T`. |
| [`Serialize(TextWriter, object)`](https://learn.microsoft.com/de-de/dotnet/api/system.xml.serialization.xmlserializer.serialize) | Schreibt ein Objekt als XML in den `TextWriter`. |
| [`Deserialize(TextReader)`](https://learn.microsoft.com/de-de/dotnet/api/system.xml.serialization.xmlserializer.deserialize) | Liest ein Objekt (`object?`) aus dem `TextReader`; Ergebnis muss gecastet werden. |

!!! note "Weiterführend"
    Eine Übersicht aller Attribute zur Steuerung der XML-Serialisierung findet sich in der Microsoft-Dokumentation: [Steuern der XML-Serialisierung mit Attributen](https://learn.microsoft.com/de-de/dotnet/standard/serialization/controlling-xml-serialization-using-attributes){:target="_blank"}.
