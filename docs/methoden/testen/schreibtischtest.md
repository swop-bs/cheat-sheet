# Schreibtischtest

## Was ein Schreibtischtest ist

Beim Schreibtischtest spielt eine Person den Rechner: Sie geht einen Ablauf Zeile für Zeile durch
und schreibt nach jedem Schritt auf, welchen Wert jede veränderliche Größe gerade hat. Der Code
wird dabei nicht ausgeführt. Das Ergebnis ist ein Ablaufprotokoll, das man einer anderen Person
hinlegen kann.

Der Schreibtischtest gehört zu den statischen Prüfverfahren: Man braucht weder einen lauffähigen
Stand noch Testdaten im System. Die Methodenabteilung setzt ihn ein, wenn ein Stand nicht
startet, wenn ein Fehler nur bei bestimmten Werten auftritt, oder wenn schriftlich belegt werden
muss, warum ein Programm sich so verhält.

### Wozu, wenn es doch einen Debugger gibt?

| Der Debugger | Der Schreibtischtest |
|-------------------------------------------|--------------------------------------------------|
| Braucht ein Programm, das startet | Braucht nur den abgedruckten Code |
| Zeigt, was passiert | Zeigt zusätzlich, was passieren sollte |
| Hinterlässt nichts Schriftliches | Ergibt ein Protokoll, das man ablegen und vorlegen kann |
| Findet den Fehler dort, wo er auffällt | Findet auch Fälle, die im Test nie vorkommen |

## Vorgehen

1. Festlegen, was geprüft wird: welche Methode mit welchen Eingabewerten. Die Eingabewerte in eine kleine Tabelle schreiben.
2. Vorgabe bereitlegen: Was müsste bei diesen Werten herauskommen? Die Antwort stammt aus der Anforderung, nicht aus dem Code.
3. Spalten anlegen: je eine für jede Größe, die sich ändert, für jede Bedingung (WENN) und für die Ausgabe.
4. Zeile für Zeile durchgehen. Für jede Zeile, die einen Wert ändert oder etwas prüft, eine Protokollzeile mit ihrer Zeilennummer schreiben. Zeilen wie ENDE WENN ändern nichts und bekommen keine. Nichts im Kopf rechnen und nichts überspringen.
5. Bei einer geprüften Bedingung „ja“ oder „nein“ eintragen. Was in dieser Zeile nicht geprüft wird oder keine Rolle spielt, bekommt einen Strich „–“.
6. Am Ende das Ergebnis des Codes und die Vorgabe nebeneinanderstellen.
7. Bei Abweichung die Zeilennummer angeben, die Stelle wörtlich zitieren und aufschreiben, wie sie richtig lauten müsste.

### Welche Werte prüfen?

Fehler sitzen selten in der Mitte eines Wertebereichs, sondern an seinen Rändern. Zu jeder Grenze
gehören deshalb drei Prüfwerte: der Grenzwert selbst, ein Wert knapp darunter und ein Wert knapp
darüber. Bei einer Regel „höchstens 2.000,00 €“ sind das 1.999,99 €, 2.000,00 € und 2.000,01 €.

## Beispiel aus dem Hausprojekt KALK

Aus dem Altsystem KALK, Menüpunkt „Preisliste pflegen“: Die folgende Funktion zählt, wie viele
Stundensätze der Preisliste über 100,00 € liegen, und meldet zusätzlich, wenn die Summe aller
Sätze über 400,00 € liegt.

```text linenums="1"
FUNKTION ZaehleHoheSaetze(stundensaetze)
  // stundensaetze enthält die drei Sätze der Preisliste in Euro
  anzahl = 0
  summe = 0
  FÜR i VON 1 BIS 3
    summe = summe + stundensaetze[i]
    WENN stundensaetze[i] > 100 DANN
      anzahl = anzahl + 1
    ENDE WENN
  ENDE FÜR
  WENN summe > 400 DANN
    AUSGABE "Summe der Stundensätze über 400 Euro"
  ENDE WENN
  RÜCKGABE anzahl
ENDE FUNKTION
```

**Eingaben:** die Preisliste, Stand 01/2024.

| Eingabe | Wert in € |
|---------|----------:|
| `stundensaetze[1]` | 95 |
| `stundensaetze[2]` | 180 |
| `stundensaetze[3]` | 125 |

**Vorgabe** aus der Kalkulationsrichtlinie: zwei Sätze über 100,00 €, Summe 400,00 €, also
**keine** Meldung.

**Protokoll:** „–“ heißt, in dieser Zeile nicht geprüft oder ohne Bedeutung.

<div class="protokoll" markdown>

| Nr. | Zeile | i | stundensaetze[i] | summe | anzahl | stundensaetze[i] > 100 | summe > 400 | Ausgabe |
|----:|------:|---|------:|------:|-------:|------|------|------|
| 1 | 3 | – | – | – | 0 | – | – | – |
| 2 | 4 | – | – | 0 | 0 | – | – | – |
| 3 | 5 | 1 | 95 | 0 | 0 | – | – | – |
| 4 | 6 | 1 | 95 | 95 | 0 | – | – | – |
| 5 | 7 | 1 | 95 | 95 | 0 | nein | – | – |
| 6 | 5 | 2 | 180 | 95 | 0 | – | – | – |
| 7 | 6 | 2 | 180 | 275 | 0 | – | – | – |
| 8 | 7 | 2 | 180 | 275 | 0 | ja | – | – |
| 9 | 8 | 2 | 180 | 275 | 1 | – | – | – |
| 10 | 5 | 3 | 125 | 275 | 1 | – | – | – |
| 11 | 6 | 3 | 125 | 400 | 1 | – | – | – |
| 12 | 7 | 3 | 125 | 400 | 1 | ja | – | – |
| 13 | 8 | 3 | 125 | 400 | 2 | – | – | – |
| 14 | 11 | – | – | 400 | 2 | – | nein (400 > 400 ist falsch) | – |
| 15 | 14 | – | – | 400 | 2 | – | – | Rückgabe: 2 |

</div>

**Ergebnis:** Der Ablauf liefert 2 und keine Meldung, genau wie die Vorgabe. Kein Fehler.

Beachten Sie den Grenzwert: Die Summe ist **genau** 400,00 €. Weil im Code `> 400` steht und nicht
`>= 400`, kommt keine Meldung. Ob das richtig ist, entscheidet die Kalkulationsrichtlinie, nicht
der Code. Genau solche Stellen sucht der Schreibtischtest.

!!! info "Andere Eingabewerte, neue Tabelle"
    Ein Protokoll gilt nur für die Eingabewerte darüber. Für andere Werte, etwa eine Summe knapp
    über 400,00 €, legen Sie eine neue Eingabetabelle und ein neues Protokoll an. Welche Werte Sie
    brauchen, zeigt die
    [Entscheidungstabelle](../algorithmen/entscheidungstabelle.md#von-der-tabelle-zu-den-testfallen).

## Kurzreferenz

| Prüfen Sie zum Schluss | Frage |
|--------------------|-------------------------------------------------------------|
| Eingaben | Stehen die Eingabewerte in einer Tabelle über dem Protokoll? |
| Spalten | Hat jede Größe, die sich ändert, und jede Bedingung eine eigene Spalte? |
| Startwerte | Steht am Anfang der Startwert jeder Größe? |
| Lückenlos | Hat jede Zeile, die etwas ändert oder prüft, eine Protokollzeile mit ihrer Zeilennummer? |
| Bedingungen | Steht bei jeder geprüften Bedingung „ja“ oder „nein“, sonst „–“? |
| Möglichkeit | Kann jeder Zwischenwert in der Wirklichkeit überhaupt vorkommen? |
| Vergleich | Stehen Ergebnis des Codes und Vorgabe nebeneinander? |
| Fundstelle | Ist bei einer Abweichung die Zeilennummer genannt? |

Typische Fehler: Zwischenschritte im Kopf gerechnet; der letzte Schleifendurchlauf fehlt; in der
Spalte „Zeile“ steht Code statt der Zeilennummer; das erwartete Ergebnis wird aus dem Code
abgelesen statt aus der Anforderung; die Abweichung wird beschrieben, aber die Stelle nicht
benannt.
