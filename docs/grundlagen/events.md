# Events

In C# werden Events verwendet, um die Ereignisbehandlung zu ermöglichen. Dies ist ein grundlegendes Konzept in der objektorientierten Programmierung und insbesondere in der Entwicklung von Benutzeroberflächen und ereignisgesteuerten Anwendungen. Hier sind einige Gründe, warum Events in C# verwendet werden:

1. **Entkopplung**: Events ermöglichen die Entkopplung von Objekten. Das bedeutet, dass ein Objekt (der Publisher) ein Ereignis auslösen kann, ohne zu wissen, welches Objekt (der Subscriber) auf das Ereignis reagiert. Dies fördert eine lose Kopplung und verbessert die Modularität und Wartbarkeit des Codes.

2. **Benachrichtigung über Zustandsänderungen**: Events werden häufig verwendet, um andere Teile eines Programms über Änderungen in einem Objekt zu informieren. Beispielsweise kann eine Benutzeroberflächenkomponente ein Ereignis auslösen, wenn sich ein Wert ändert, und andere Komponenten können darauf reagieren, um ihre Anzeige zu aktualisieren.

3. **Erweiterbarkeit**: Durch die Verwendung von Events können Klassen so gestaltet werden, dass sie erweiterbar sind, ohne dass der ursprüngliche Code geändert werden muss. Dies ermöglicht es Entwicklern, zusätzliche Funktionalitäten hinzuzufügen, indem sie einfach neue Event-Handler registrieren.

4. **Asynchronität**: Events können verwendet werden, um asynchrone Operationen zu handhaben. Beispielsweise kann eine Netzwerkoperation ein Event auslösen, wenn sie abgeschlossen ist, sodass der Hauptthread nicht blockiert wird.

5. **Erweiterte Ereignisbehandlung**: Durch Events kann die Ereignisbehandlung in einem klaren und strukturierten Ansatz implementiert werden. Der Code für die Ereignisbehandlung wird in speziellen Methoden (Event-Handlern) platziert, was die Lesbarkeit und Wartbarkeit des Codes verbessert.

6. **Benutzerdefinierte Ereignisse**: Entwickler können benutzerdefinierte Events erstellen, um spezifische Aktionen oder Zustandsänderungen in ihrer Anwendung zu signalisieren. Dies macht den Code flexibler und anpassbarer an spezielle Anforderungen.

## Beispiel ohne Events

Ein einfaches Beispiel zur Veranschaulichung:

```csharp
public class Sensor 
{
    private Alarm _alarm; // Referenz auf ein Alarm-Objekt
    private Logger _logger; // Referenz auf ein Logger-Objekt
    private int _threshold = 100; // Definierter Schwellenwert

    // Konstruktor, der Alarm- und Logger-Objekte akzeptiert und speichert
    public Sensor(Alarm a, Logger l) 
    {
        _alarm = a;
        _logger = l;
    }

    // Methode zur Überprüfung des tatsächlichen Wertes im Vergleich zum Schwellenwert
    public void CheckThreshold(int actualValue) 
    {
        // Wenn der tatsächliche Wert den Schwellenwert überschreitet
        if (actualValue > _threshold) 
        {
            _alarm.Ring(); // Alarm auslösen
            _logger.LogThresholdReached(); // Protokolleintrag erstellen
        }
    }
}

public class Alarm 
{
    // Methode zum Auslösen des Alarms
    public void Ring() 
    {
        Console.WriteLine("Alarm: Threshold reached!");
    }
}

public class Logger 
{
    // Methode zum Protokollieren, dass der Schwellenwert erreicht wurde
    public void LogThresholdReached() 
    {
        Console.WriteLine("Logger: Threshold reached! Logging data...");
    }
}

```

Die `Sensor`-Klasse enthält Referenzen auf `Alarm`- und `Logger`-Objekte und überprüft, ob ein gegebener Wert einen bestimmten Schwellenwert überschreitet. Wenn der Wert den Schwellenwert überschreitet, wird ein Alarm ausgelöst und ein Protokolleintrag erstellt.

### Enge Kopplung

Die Deklaration der beiden Klassen `Alarm` und `Logger` innerhalb der `Sensor`-Klasse ist problematisch, da sie zu einer engen Kopplung führt. Dies hat mehrere negative Auswirkungen auf die Flexibilität, Erweiterbarkeit und Wartbarkeit des Codes. Hier sind die Hauptgründe, warum dies problematisch ist:

1. **Direkte Abhängigkeiten**:
    - Die `Sensor`-Klasse hat direkte Abhängigkeiten zu den `Alarm`- und `Logger`-Klassen. Dies bedeutet, dass Änderungen an den `Alarm`- oder `Logger`-Klassen möglicherweise auch Änderungen in der `Sensor`-Klasse erfordern.
    - Diese Abhängigkeiten erschweren es, die `Sensor`-Klasse unabhängig zu testen oder zu verwenden, da sie nicht ohne die `Alarm`- und `Logger`-Objekte funktioniert.

2. **Erweiterbarkeit**:
    - Wenn ein neues Objekt auf den Sensor reagieren soll (z.B. eine neue `Notifier`-Klasse), muss die `Sensor`-Klasse geändert werden, um dieses neue Objekt zu unterstützen. Dies erfordert das Hinzufügen neuer Felder, das Anpassen des Konstruktors und das Ändern der Methode `CheckThreshold`.
    - Dies widerspricht dem Prinzip der offenen/geschlossenen Klasse (Open/Closed Principle), wonach eine Klasse für Erweiterungen offen, aber für Änderungen geschlossen sein sollte.

3. **Wartbarkeit**:
    - Der Code wird schwieriger zu warten, da Änderungen in einer der abhängigen Klassen (z.B. `Alarm` oder `Logger`) möglicherweise auch Änderungen in der `Sensor`-Klasse erfordern.
    - Das Hinzufügen neuer Funktionalitäten oder das Refaktorisieren des Codes wird komplexer und fehleranfälliger.

## Alternative: Verwendung von Events

Eine bessere Lösung zur Vermeidung dieser Probleme ist die Verwendung von Events. Mit Events kann die `Sensor`-Klasse lose gekoppelt werden, indem sie Ereignisse auslöst, auf die andere Klassen reagieren können. Dadurch wird die `Sensor`-Klasse unabhängiger von den konkreten Implementierungen der Listener und bleibt einfacher erweiterbar.

Hier ist ein Beispiel, wie dies mit Events aussehen könnte:

```csharp
using System;

public class Sensor 
{
    private int _threshold = 100;

    // Event declaration
    public event EventHandler ThresholdReached;

    public void CheckThreshold(int actualValue) 
    {
        if (actualValue > _threshold) 
        {
            OnThresholdReached(EventArgs.Empty);
        }
    }

    protected virtual void OnThresholdReached(EventArgs e)
    {
        ThresholdReached?.Invoke(this, e);
    }
}

public class Alarm 
{
    public void OnThresholdReached(object sender, EventArgs e) 
    {
        Console.WriteLine("Alarm: Threshold reached!");
    }
}

public class Logger 
{
    public void OnThresholdReached(object sender, EventArgs e) 
    {
        Console.WriteLine("Logger: Threshold reached! Logging data...");
    }
}

public class Program 
{
    static void Main(string[] args) 
    {
        Sensor sensor = new Sensor();

        Alarm alarm = new Alarm();
        Logger logger = new Logger();

        // Subscribe to the event
        sensor.ThresholdReached += alarm.OnThresholdReached;
        sensor.ThresholdReached += logger.OnThresholdReached;

        // Simulate adding values to the sensor
        sensor.CheckThreshold(105); // This should trigger the event
    }
}
```

### Vorteile der Verwendung von Events:

1. **Lose Kopplung**:
    - Die `Sensor`-Klasse kennt die spezifischen Implementierungen der Listener-Klassen (`Alarm` und `Logger`) nicht. Sie löst lediglich das `ThresholdReached`-Event aus.
    - Dies fördert die Wiederverwendbarkeit und Modularität, da neue Listener hinzugefügt werden können, ohne die `Sensor`-Klasse zu ändern.

2. **Erweiterbarkeit**:
    - Neue Listener können einfach hinzugefügt werden, indem sie sich auf das `ThresholdReached`-Event der `Sensor`-Klasse abonnieren. Es sind keine Änderungen an der `Sensor`-Klasse erforderlich.
    - Die `Sensor`-Klasse bleibt offen für Erweiterungen (neue Listener), ohne dass sie selbst geändert werden muss.

3. **Wartbarkeit**:
    - Der Code wird wartbarer, da Änderungen in den Listener-Klassen keine Änderungen an der `Sensor`-Klasse erfordern.
    - Die Logik zur Schwellenwertüberprüfung bleibt in der `Sensor`-Klasse, während die Reaktionen auf das Erreichen des Schwellenwerts in den jeweiligen Listener-Klassen kapselt sind.

Durch die Verwendung von Events wird der Code flexibler, modularer und leichter zu warten, was die langfristige Entwicklung und Pflege der Software erheblich erleichtert.