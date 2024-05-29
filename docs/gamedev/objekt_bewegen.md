# Bewegung eines Objekts mit Hilfe von Tasten

In diesem Kapitel erklären wir, wie man die Position eines Objekts mit Hilfe von Tasten verändern kann. Dazu verwenden wir den folgenden Code, der in der Methode `_Process` implementiert ist. Dieser Code ermöglicht es, ein Objekt auf dem Bildschirm zu bewegen, indem die Pfeiltasten auf der Tastatur gedrückt werden.

```csharp
public override void _Process(double delta)
{
    // Definiert die Geschwindigkeit der Bewegung
    float speed = 1000;
    
    // Speichert die aktuelle Position des Objekts
    var pos = this.Position;
    
    // Berechnet die Änderung der Position basierend auf der Zeit, die seit dem letzten Frame vergangen ist
    var deltaPos = (float)(delta * speed);

    // Überprüft, ob die linke Pfeiltaste gedrückt wird
    if (Input.IsKeyPressed(Key.Left))
    {
        // Bewegt das Objekt nach links
        pos.X -= deltaPos;
    }
    // Überprüft, ob die rechte Pfeiltaste gedrückt wird
    if (Input.IsKeyPressed(Key.Right))
    {
        // Bewegt das Objekt nach rechts
        pos.X += deltaPos;
    }

    // Aktualisiert die Position des Objekts
    this.Position = pos;
}
```

## Erklärung des Codes

### Geschwindigkeit der Bewegung

```csharp
float speed = 1000;
```

Hier definieren wir eine Variable `speed`, die die Geschwindigkeit der Bewegung des Objekts festlegt. Der Wert 1000 bedeutet, dass das Objekt sich schnell bewegen wird.

### Speichern und Aktualisieren der Position

```csharp
var pos = this.Position;
var deltaPos = (float)(delta * speed);
```

- `var pos = this.Position;` speichert die aktuelle Position des Objekts in der Variablen `pos`. Diese Variable ist vom Typ `Vector2`, einer Struktur, die die x- und y-Koordinaten des Objekts enthält.
- `var deltaPos = (float)(delta * speed);` berechnet die Änderung der Position basierend auf der Zeit (`delta`), die seit dem letzten Frame vergangen ist, und der festgelegten Geschwindigkeit (`speed`).

!!! info
    Da `Vector2` ein Werttyp (struct) und kein Referenztyp ist, speichern wir die aktuelle Position zuerst in einer Variablen, ändern sie und weisen sie dann wieder der `Position`-Eigenschaft zu. Dies ist notwendig, weil wir nicht direkt auf die Eigenschaften einer Struktur zugreifen und sie ändern können.

### Tastenüberprüfung und Positionsänderung

```csharp
if (Input.IsKeyPressed(Key.Left))
{
    pos.X -= deltaPos;
}
if (Input.IsKeyPressed(Key.Right))
{
    pos.X += deltaPos;
}
```

Hier überprüfen wir, ob eine der Pfeiltasten gedrückt wird, und ändern entsprechend die x- oder y-Koordinate der Position:

- `Input.IsKeyPressed(Key.Left)`: Überprüft, ob die linke Pfeiltaste gedrückt wird, und bewegt das Objekt nach links, indem `deltaPos` von der x-Koordinate abgezogen wird.

- `Input.IsKeyPressed(Key.Right)`: Überprüft, ob die rechte Pfeiltaste gedrückt wird, und bewegt das Objekt nach rechts, indem `deltaPos` zur x-Koordinate addiert wird.

### Aktualisieren der Position

```csharp
this.Position = pos;
```

Am Ende der Methode wird die aktualisierte Position dem Objekt zugewiesen. Dadurch wird die neue Position des Objekts auf dem Bildschirm reflektiert.

## Wichtige Funktionen und Konzepte

### Input.IsKeyPressed

Die Funktion `Input.IsKeyPressed(Key key)` überprüft, ob eine bestimmte Taste auf der Tastatur gedrückt wird. Sie gibt `true` zurück, wenn die Taste gedrückt wird, und `false`, wenn nicht. Dies ermöglicht es uns, auf Benutzereingaben zu reagieren und entsprechende Aktionen im Spiel auszuführen.

### Vector2 als Struktur

Die `Position`-Eigenschaft eines `Node2D`-Objekts ist vom Typ `Vector2`, einer Struktur, die zwei float-Werte (x und y) speichert. Da `Vector2` ein Werttyp ist, müssen wir die Position erst in einer Variablen speichern, ändern und dann zurückschreiben, anstatt direkt die Eigenschaften zu ändern.

## Erweiterung der Navigation

Aufgabe 1: Erweitern Sie den Code, damit der Kreis auch nach oben und unten (Y-Richtung) mit den entsprechenden Pfeiltasten navigiert werden kann.

??? quote "Lösung"

    ```csharp
    public override void _Process(double delta)
    {
        float speed = 1000;
        var pos = this.Position;
        var deltaPos = (float)(delta * speed);

        if (Input.IsKeyPressed(Key.Left))
        {
            pos.X -= deltaPos;
        }
        if (Input.IsKeyPressed(Key.Right))
        {
            pos.X += deltaPos;
        }
        if (Input.IsKeyPressed(Key.Up))  // Fügt Bewegung nach oben hinzu
        {
            pos.Y -= deltaPos;
        }
        if (Input.IsKeyPressed(Key.Down))  // Fügt Bewegung nach unten hinzu
        {
            pos.Y += deltaPos;
        }

        this.Position = pos;
    }
    ```

Aufgabe 2: Erweitern Sie den Code so, dass der Kreis nicht aus dem Fenster bewegt werden kann.

??? quote "Lösung"

    ```csharp
    public override void _Process(double delta)
    {
        // ...

        // Begrenzung der Bewegung innerhalb des Fensters
        var windowSize = GetViewport().GetVisibleRect().Size;
        if (pos.X < _radius)
        {
            pos.X = _radius;
        }
        if (pos.X > windowSize.x - _radius)
        {
            pos.X = windowSize.x - _radius;
        }
        if (pos.Y < _radius)
        {
            pos.Y = _radius;
        }
        if (pos.Y > windowSize.y - _radius)
        {
            pos.Y = windowSize.y - _radius;
        }

        // ...
    }
    ```