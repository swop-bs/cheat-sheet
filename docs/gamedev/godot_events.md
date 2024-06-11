# Verwendung von Signalen in Godot

Godot verwendet ein Signalsystem ([Events](../grundlagen/events.md)), um Ereignisse zu handhaben und Kommunikation zwischen Nodes zu ermöglichen. Signale sind ein wichtiges Konzept in Godot, da sie eine flexible und entkoppelte Art der Interaktion zwischen verschiedenen Teilen des Spiels bieten.

## Grundlagen von Signalen

Signale sind benutzerdefinierte oder eingebaute Ereignisse, die von Nodes gesendet werden und von anderen Nodes empfangen werden können. Sie ermöglichen es, auf bestimmte Ereignisse zu reagieren, ohne dass die beteiligten Nodes direkt miteinander kommunizieren müssen.

### Beispiel: Verwendung von Signalen

In diesem Beispiel erstellen wir eine einfache Anwendung, in der ein Signal ausgelöst wird, wenn die Leertaste gedrückt wird. Das Signal wird in der `Root`-Klasse verknüpft, und eine andere Node reagiert auf dieses Signal.

### Schritt 1: Signal deklarieren und auslösen

Zuerst deklarieren wir ein Signal in einer benutzerdefinierten Node-Klasse und lösen es aus, wenn eine Bedingung erfüllt ist.

```csharp
using Godot;
using System;

public partial class SpaceShip : Node2D
{
    // Deklariert ein Signal
    [Signal]
    public delegate void WeaponFiredEventHandler(Vector2 SpaceShipNosePosition);

    public override void _Process(double delta)
    {
        // Bedingung: Löst das Signal aus, wenn die Leertaste gedrückt wird
        if (Input.IsKeyPressed(Key.Space))
        {
            Vector2 weaponPos = new Vector2(0, 0); // Platzhalterwert, richtige Position muss berechnet werden
            
            // Hier muss die richtige Position noch berechnet werden 
            // Zum Beispiel: weaponPos = this.GlobalPosition + new Vector2(0, -10);

            EmitSignal(SignalName.WeaponFired, weaponPos);
        }
    }
}
```

### Erklärung des Codes

- **[Signal] public delegate void WeaponFiredEventHandler(Vector2 SpaceShipNosePosition)**: Deklariert ein Signal namens `WeaponFiredEventHandler`, das die Position der Nasenspitze des Raumschiffs als Argument übergibt.
- **_Process(double delta)**: Diese Methode wird jeden Frame aufgerufen. Sie überprüft, ob die Leertaste gedrückt wird. Wenn dies der Fall ist, wird das Signal `WeaponFiredEventHandler` ausgelöst und die Position der Waffe als Argument übergeben.

### Schritt 2: Signal in der Root-Klasse verknüpfen

In der `Root`-Klasse verknüpfen wir das Signal, damit eine andere Methode darauf reagieren kann.

```csharp
using Godot;

public partial class Root : Node2D
{
    public override void _Ready()
    {
        // Erstellen und Hinzufügen einer Instanz der SpaceShip-Klasse
        SpaceShip s = new SpaceShip();
        
        // Verknüpfen des Signals mit einer Methode
        s.WeaponFiredEventHandler += OnWeaponFired;
        
        AddChild(s);
    }

    // Methode, die auf das Signal reagiert
    private void OnWeaponFired(Vector2 SpaceShipNosePosition)
    {
        // Erstellen einer neuen Waffe an der Position der Nasenspitze des Raumschiffs
        Weapon w = new Weapon(SpaceShipNosePosition);
        AddChild(w);
    }

    public override void _Process(double delta)
    {
    }
}
```

### Erklärung des Codes

- **_Ready()**: Diese Methode wird aufgerufen, wenn die Root-Node in die Szene eingefügt wird. Hier wird eine Instanz der `SpaceShip`-Klasse erstellt, das Signal `WeaponFiredEventHandler` mit der Methode `OnWeaponFired` verknüpft und die Instanz als Kindknoten hinzugefügt.
- **OnWeaponFired(Vector2 SpaceShipNosePosition)**: Diese Methode wird aufgerufen, wenn das Signal `WeaponFiredEventHandler` ausgelöst wird. Sie erstellt eine neue Instanz der `Weapon`-Klasse an der angegebenen Position und fügt sie als Kindknoten hinzu.

## Zusammenfassung

Signale bieten eine leistungsstarke und flexible Möglichkeit, Ereignisse in Godot zu handhaben. Durch die Deklaration und Verknüpfung von Signalen können verschiedene Teile eines Spiels auf eine entkoppelte Weise miteinander interagieren. In diesem Beispiel haben wir gezeigt, wie ein Signal in einer Node-Klasse deklariert und in der `Root`-Klasse verknüpft wird, um auf eine bestimmte Bedingung zu reagieren und darauf basierend eine Aktion auszuführen.