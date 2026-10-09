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

1. Festlegen, was geprüft wird: welche Methode, welcher Fall, welche Eingabewerte.
2. Vorgabe bereitlegen: Was müsste bei diesen Werten herauskommen? Diese Antwort stammt aus der Anforderung, nicht aus dem Code.
3. Alle Größen bestimmen, die sich während des Ablaufs ändern. Für jede eine Spalte anlegen, dazu eine Spalte für die Ausgabe.
4. Zeile für Zeile durchgehen. Nach jeder Zeile, die einen Wert ändert, eine neue Protokollzeile schreiben. Nichts im Kopf rechnen und nichts überspringen.
5. Bei jeder Bedingung notieren, ob sie zutrifft, und welchen Weg der Ablauf nimmt.
6. Am Ende das Ergebnis des Codes und die Vorgabe nebeneinanderstellen.
7. Bei Abweichung die Zeilennummer angeben, die Stelle wörtlich zitieren und aufschreiben, wie sie richtig lauten müsste.

### Welche Werte prüfen?

Fehler sitzen selten in der Mitte eines Wertebereichs, sondern an seinen Rändern. Zu jeder Grenze
gehören deshalb drei Prüfwerte: der Grenzwert selbst, ein Wert knapp darunter und ein Wert knapp
darüber. Bei einer Regel „höchstens 2.000,00 €“ sind das 1.999,99 €, 2.000,00 € und 2.000,01 €.

## Beispiel aus dem Hausprojekt KALK

Aus dem Altsystem KALK, Menüpunkt „Preisliste pflegen“: Die folgende Funktion zählt, wie viele
Stundensätze der Preisliste über 100,00 € liegen, und meldet zusätzlich, wenn die Summe aller
Sätze über 400,00 € liegt. Kommentiert ist im Quelltext nichts; was die Funktion tut, muss man
ihr absehen.

```
FUNKTION ZaehleHoheSaetze(stundensaetze)
  // stundensaetze enthält die vier Sätze der Preisliste in Euro
  anzahl = 0
  summe = 0
  FÜR i VON 1 BIS 4
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

Geprüfter Fall: `stundensaetze = [95, 120, 75, 110]` – die Preisliste, Stand 01/2024. Vorgabe aus
der Kalkulationsrichtlinie: zwei Sätze über 100,00 €, Summe 400,00 €, also **keine** Meldung.

| Nr. | Zeile | i | stundensaetze[i] | summe | anzahl | Bedingung > 100 | Ausgabe |
|-----|----------------------|---|------|-------|--------|-----------------|------------------------------|
| 0 | anzahl = 0 / summe = 0 | – | – | 0 | 0 | – | |
| 1 | Schleifendurchlauf | 1 | 95 | 95 | 0 | nein | |
| 2 | Schleifendurchlauf | 2 | 120 | 215 | 1 | ja | |
| 3 | Schleifendurchlauf | 3 | 75 | 290 | 1 | nein | |
| 4 | Schleifendurchlauf | 4 | 110 | 400 | 2 | ja | |
| 5 | WENN summe > 400 | – | – | 400 | 2 | nein (400 > 400 ist falsch) | keine Ausgabe |
| 6 | RÜCKGABE anzahl | – | – | 400 | 2 | – | Rückgabe: 2 |

Ergebnis: Der Ablauf liefert 2 und keine Meldung. Die Vorgabe lautet 2 und keine Meldung. Beides
stimmt überein, an dieser Stelle ist kein Fehler.

Beachten Sie den Grenzwert: Die Summe beträgt **genau** 400,00 €, und trotzdem passiert nichts,
weil dort `> 400` und nicht `>= 400` steht. Ob das richtig ist, entscheidet nicht der Code,
sondern die Kalkulationsrichtlinie. Genau solche Stellen sucht der Schreibtischtest.

## Kurzreferenz

| Prüfen Sie zum Schluss | Frage |
|--------------------|-------------------------------------------------------------|
| Spalten | Hat jede veränderliche Größe eine eigene Spalte? |
| Startwerte | Steht in der ersten Zeile der Startwert jeder Größe? |
| Lückenlos | Ist jeder Schleifendurchlauf als eigene Zeile erfasst? |
| Bedingungen | Steht bei jeder Bedingung, ob sie zutrifft? |
| Möglichkeit | Kann jeder Zwischenwert in der Wirklichkeit überhaupt vorkommen? |
| Vergleich | Stehen Ergebnis des Codes und Vorgabe nebeneinander? |
| Fundstelle | Ist bei einer Abweichung die Zeilennummer genannt? |

Typische Fehler: Zwischenschritte im Kopf gerechnet; der letzte Schleifendurchlauf fehlt; das
erwartete Ergebnis wird aus dem Code abgelesen statt aus der Anforderung; die Abweichung wird
beschrieben, aber die Stelle nicht benannt.
