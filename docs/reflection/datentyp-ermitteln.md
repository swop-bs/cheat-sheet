# Datentyp dynamisch zur Laufzeit ermitteln

## Type-Objekt

Um den Typ eines Objekts zur Laufzeit zu ermitteln, können verschiedene Methoden verwendet werden. Diese Methoden geben ein `Type`-Objekt zurück, das alle relevanten Metainformationen über die Klasse bzw. das Objekt enthält.

- **`m.GetType()`**: Ermittelt den Typ einer Instanz `m`.
- **`typeof(Mitarbeiter)`**: Ermittelt den Typ der Klasse `Mitarbeiter`.
- **`Type.GetType("Mitarbeiter")`**: Ermittelt den Typ basierend auf dem qualifizierten Namen der Klasse.

!!! warning
	Im ersten Schritt muss immer das Typen-Objekt erstellt werden. Dieses Objekt bildet die Grundlage zur Ermittlung der Ermittlung der Metadaten.

### Typ eines Objekts ermitteln

Nachdem wir die Klasse `Mitarbeiter` definiert haben, verwenden wir die Reflection-Methoden, um Metainformationen zur Laufzeit zu extrahieren.

```csharp
Mitarbeiter mitarbeiter = new Mitarbeiter("Max", "Mustermann", 50000, "Berlin");

// Typinformationen ermitteln
Type type = mitarbeiter.GetType();

Console.WriteLine(type.ToString());
```

??? quote "Output"
	``` text
	Mitarbeiter
	```




## `GetFields()`

Liefert die Attribute (Felder) der Klasse zurück.

``` csharp
FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
foreach (var field in fields)
{
    Console.WriteLine($"{field.Name}: {field.FieldType}");
}
```

??? quote "Output"
	``` text
	_vorname: System.String
    _nachname: System.String
    _gehalt: System.Double
    _arbeitsort: System.String
	```

!!! warning
    Beachte, dass private Felder nur mit entsprechenden `BindingFlags` sichtbar sind.

## `GetProperties()`

Liefert die Properties (Eigenschaften) einer Klasse zurück.

```csharp
PropertyInfo[] properties = type.GetProperties();
foreach (var property in properties)
{
    Console.WriteLine($"{property.Name}: {property.PropertyType}");
}
```

??? quote "Output"
	``` text
	Vorname: System.String
    Nachname: System.String
    Gehalt: System.Double
    Arbeitsort: System.String
	```

## `GetMethods()`

Liefert die Methoden einer Klasse zurück.

```csharp
MethodInfo[] methods = type.GetMethods();
foreach (var method in methods)
{
    Console.WriteLine($"{method.Name}");
}
```

??? quote "Output"
	``` text
    AnzeigenInformationen
    ErhoeheGehalt
    BerechneJahresgehalt
	```

!!! info
    Diese Liste enthält auch geerbte Methoden von `Object`, wie `ToString`, `Equals` usw.

## `GetConstructors()`

Liefert die Konstruktoren einer Klasse zurück

```csharp
ConstructorInfo[] constructors = type.GetConstructors();
foreach (var constructor in constructors)
{
    Console.WriteLine($"Konstruktor: {constructor.ToString()}");
}
```

??? quote "Output"
	``` text
    Konstruktor: Void .ctor()
    Konstruktor: Void .ctor(System.String, System.String, System.Double, System.String)
	```

## `GetParameters()`

Liefert die Paramter von Konstruktoren bzw. Methoden zurück.

```csharp
foreach (var constructor in constructors)
{
    ParameterInfo[] parameters = constructor.GetParameters();
    foreach (var parameter in parameters)
    {
        Console.WriteLine($"{parameter.Name}: {parameter.ParameterType}");
    }
}
```

??? quote "Output"
	``` text
    vorname: System.String
    nachname: System.String
    gehalt: System.Double
    arbeitsort: System.String
	```

!!! tip
    Analog können die Parameter auch für Methoden ausgegeben werden!