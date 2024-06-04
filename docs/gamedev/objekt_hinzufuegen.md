# Erstellen und Anzeigen eines neuen Spielobjekts

In diesem Abschnitt fügen wir ein neues Spielobjekt hinzu und lassen es in der Mitte der Anzeige anzeigen. Wir werden einen einfachen Kreis zeichnen, der sich in der Mitte des Fensters befindet.

## Code-Erklärung

Hier ist der Code, der ein neues Spielobjekt vom Typ `Node2D` erzeugt und anzeigt. Der Kreis wird in der Mitte des Fensters gezeichnet.

```csharp
using Godot;

public partial class EinKreis : Node2D
{
    // Der Radius des Kreises
    private float _radius = 50;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // Ermittelt die Größe des Anzeigefensters
        Vector2 windowSize = GetViewport().GetVisibleRect().Size;

        // Berechnet die Position des Kreises als die Mitte des Fensters
        Vector2 kreisPosition = windowSize / 2;

        // Setzt die Position des Kreises
        this.Position = kreisPosition;
        // Gibt dem Objekt die Farbe Grün
        this.Modulate = Colors.Green;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        // Bisher nichts zu tun
    }

    // Called to draw the node.
    public override void _Draw()
    {
        // Zeichnet den Kreis. Der Vektor (0, 0) gibt die Position relativ zur Node-Position an.
        // Wir übergeben beim manuellen Zeichnen immer Colors.White. Die eigentliche Farbe
        // legen wir für über Modulate fest.
        DrawCircle(Vector2.Zero, _radius, Colors.White);
    }
}
```

## Erklärung der Klasse `EinKreis`

### Eigenschaften

- **_radius**: Dies ist der Radius des Kreises, der gezeichnet wird. In diesem Beispiel beträgt der Radius 50 Einheiten.

### Methoden

- **_Ready()**: Diese Methode wird aufgerufen, wenn die Node zum ersten Mal in die Szene eingefügt wird. Hier wird die Größe des Anzeigefensters ermittelt und die Position des Kreises in die Mitte des Fensters gesetzt.

- **_Process(double delta)**: Diese Methode wird jeden Frame aufgerufen. Der Parameter `delta` gibt die vergangene Zeit seit dem vorherigen Frame an. In diesem Beispiel wird diese Methode nicht genutzt.

- **_Draw()**: Diese Methode wird aufgerufen, um die Node zu zeichnen. Hier wird der Kreis gezeichnet. Der Kreis wird an der Position `(0, 0)` relativ zur Position der Node gezeichnet, was bedeutet, dass er an der zuvor festgelegten Position in der Mitte des Fensters erscheint.

!!! info
    Der Vektor `(0, 0)` in der Methode `DrawCircle` gibt die Position relativ zur Node-Position an. Da die Node-Position bereits auf die Mitte des Fensters gesetzt wurde, wird der Kreis ebenfalls in der Mitte des Fensters gezeichnet.

## Hinzufügen der Klasse zur Szene

Um die Klasse `EinKreis` zur Szene hinzuzufügen und anzuzeigen, müssen wir sie instanziieren und zur Root-Node hinzufügen. Hier ist ein Beispiel, wie dies in der `Root`-Klasse gemacht werden kann:

Da wir bisher das Objekt nur erstellt, aber nicht initialisiert haben, müssen wir noch ein Objekt der Klasse `EinKreis` instanziieren. Das können wir in unserer `Root`-Klasse in der `Ready`-Funktion machen: `EinKreis kreis = new EinKreis()`. Das genügt aber noch nicht. Aktuell legen wir nur das Objekt an, sagen aber Godot (dem Framework) nicht, dass wir das Objekt auch in unserem Code angelegt haben. Um dies zu erreichen, fügen wir die Instanz von `EinKreis` dem `Root`-Objekt als "Child" hinzu: `AddChild(kreis)`. Nun "kennt" Godot den Kreis und wird die Funktionen der Klasse (`_Ready`, `_Process` und `_Draw`) aufrufen.

```csharp
using Godot;

public partial class Root : Node2D
{
    public override void _Ready()
    {
        // Erstellen einer Instanz der Klasse EinKreis
        EinKreis kreis = new EinKreis();

        // Hinzufuegen des Kreises als Kindknoten zur Root-Node
        AddChild(kreis);
    }

    public override void _Process(double delta)
    {
    }
}
```

In diesem Beispiel wird im `_Ready`-Methode der `Root`-Klasse eine Instanz der `EinKreis`-Klasse erstellt und zur Root-Node hinzugefügt. Dadurch wird der Kreis im Fenster angezeigt.

## Beispiel Rechteck

In diesem Absatz erklären wir anhand des folgenden Beispielcodes, wie anstatt eines Kreises (`EinKreis`) ein Rechteck hinzugefügt wird.

```csharp
public partial class EinRechteck : Node2D
{
    // Die Breite und Höhe des Rechtecks
    private float _width = 100;
    private float _height = 50;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // Ermittelt die Größe des Anzeigefensters
        Vector2 windowSize = GetViewport().GetVisibleRect().Size;

        // Position des Rechtecks: Mitte horizontal und im oberen Viertel vertikal
        float rechteckPosX = windowSize.X / 2;
        float rechteckPosY = windowSize.Y / 4;

        // Setzt die Position des Rechtecks
        this.Position = new Vector2(rechteckPosX, rechteckPosY);
        
        // Gibt dem Objekt die Farbe Blau
        this.Modulate = Colors.Blue;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        // Bisher nichts zu tun
    }

    // Called to draw the node.
    public override void _Draw()
    {
        // Zeichnet das Rechteck. Der Vektor (0, 0) gibt die Position relativ zur Node-Position an.
        // Die Position hier ist immer (0, 0), wir ändern die Position über this.Position.
        // Der zweite Vektor (_width, _height) gibt die Breite und Höhe des Rechtecks an.
        // Als Farbe verwenden wir hier immer Colors.White, die Farbe des Objekts geben wir
        // über this.Modulate an.
        DrawRect(new Rect2(new Vector2(0, 0), new Vector2(_width, _height)), Colors.White);
    }
}
```

### Erklärung der Klasse `EinRechteck`

#### Eigenschaften

- **_width**: Die Breite des Rechtecks, die auf 100 Einheiten festgelegt ist.
- **_height**: Die Höhe des Rechtecks, die auf 50 Einheiten festgelegt ist.

#### Methoden

- **_Ready()**: Diese Methode wird aufgerufen, wenn die Node zum ersten Mal in die Szene eingefügt wird. Hier wird die Größe des Anzeigefensters ermittelt und die Position des Rechtecks auf die Mitte horizontal und das obere Viertel vertikal gesetzt. Außerdem wird die Farbe des Rechtecks auf Blau gesetzt.
  
- **_Process(double delta)**: Diese Methode wird jeden Frame aufgerufen. Der Parameter `delta` gibt die vergangene Zeit seit dem vorherigen Frame an. In diesem Beispiel wird diese Methode nicht genutzt.
  
- **_Draw()**: Diese Methode wird aufgerufen, um die Node zu zeichnen. Hier wird das Rechteck gezeichnet. Der Vektor `(0, 0)` gibt die Position relativ zur Node-Position an, und der zweite Vektor gibt die Breite und Höhe des Rechtecks an. Die Farbe des Rechtecks wird über `this.Modulate` auf Blau gesetzt, während die Zeichenfarbe `Colors.White` ist.

## Erweiterung

### Aufgabe 1

!!! question "Aufgabe"

    Wenn Sie den Code für das Rechteck in Ihr Projekt übernehmen und ausführen, werden Sie merken, dass das Rechteck nicht gezeichnet wird. Fixen Sie diesen Bug!

??? tip

    Wie beim Kreis muss auch eine Instanz von EinRecheck in der Klasse Root erstellt werden.

??? success "Lösung"

    In der Funktion _Ready der Root-Klasse:

    ```csharp
    public partial class Root : Node2D
    {
        // ...
        
        public override void _Ready()
        {
            //... 

            // Erstellen einer Instanz der Klasse EinRechteck
            EinRechteck rechteck = new EinRechteck();

            // Hinzufuegen des Rechtecks als Kindknoten zur Root-Node
            AddChild(rechteck);
        }

        // ...
    }

    ```

### Aufgabe 2

!!! question "Aufgabe"
    Das Rechteck wird nicht wie der Kreis in der Mitte gezeichnet. Das liegt daran, dass der Nullpunkt des Kreises sein Mittelpunkt ist, der Nullpunkt des Rechtecks aber Oben links. Erweitern Sie den Code, damit das Rechteck seinen Ursprung auch immer (unabhängig von seiner Größe und Position) in seinem Mittplpunkt hat.

??? success "Lösung"

    Durch die Anpassung des Poisition-Parameters der DrawRect-Methode wird das Rechteck immer an die richtige Position gezeichnet (Vorzeichen beachten!):

    ```csharp
    // In _Draw() von EinRechteck
    DrawRect(new Rect2(new Vector2(-(_width / 2), -(_height / 2)), new Vector2(_width, _height)), Colors.White);
    ```