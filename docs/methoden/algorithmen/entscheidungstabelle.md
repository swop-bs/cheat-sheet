# Entscheidungstabelle

*Nach DIN 66241*

## Was eine Entscheidungstabelle ist

Eine Entscheidungstabelle stellt Regeln dar, bei denen mehrere Bedingungen zusammenwirken. Sie
zeigt auf einen Blick, welche Kombination von Bedingungen welche Aktion auslöst, und macht
sichtbar, ob ein Fall vergessen wurde. Aufbau und Schreibweise sind in der DIN 66241 festgelegt.

Die Methodenabteilung setzt sie überall dort ein, wo ein Text wie „wenn … und gleichzeitig …
dann …, sonst aber …“ mehrdeutig wird, und dort, wo Code über viele Verzweigungen gewachsen ist
und niemand mehr sagen kann, welcher Fall wo landet
([Vorhandenen Code prüfen und vereinfachen](#vorhandenen-code-pruefen-und-vereinfachen)). Aus der fertigen Tabelle lassen sich
unmittelbar Testfälle ableiten: je Regelspalte einer.

### Aufbau

| Bereich | Kennzeichnung (links) | Werte (rechts) |
|---------|------------------------------|--------------------------------------------------|
| oben | Bedingungen B1, B2, … | je Regel J (trifft zu), N (trifft nicht zu), – (ohne Bedeutung) |
| unten | Aktionen A1, A2, … | X, wenn die Aktion ausgeführt wird, sonst leer |

Jede Spalte rechts ist eine Regel. Bei n Bedingungen gibt es 2 hoch n mögliche Kombinationen: bei
2 Bedingungen 4 Regeln, bei 3 Bedingungen 8, bei 4 Bedingungen 16. Eine Tabelle ist vollständig,
wenn jede Kombination genau einmal vorkommt.

Bei drei Bedingungen sieht der Bedingungsteil so aus, gefüllt nach [Vorgehen](#vorgehen), Schritt 3:

| | R1 | R2 | R3 | R4 | R5 | R6 | R7 | R8 |
|----|----|----|----|----|----|----|----|----|
| B1 | J | J | J | J | N | N | N | N |
| B2 | J | J | N | N | J | J | N | N |
| B3 | J | N | J | N | J | N | J | N |

B1 wechselt nach vier Spalten, B2 nach zwei, B3 nach jeder. So fehlt keine Kombination, und keine
kommt doppelt vor.

## Vorgehen { #vorgehen }

1. Aus dem Text alle Bedingungen herausschreiben, die nur wahr oder falsch sein können. Aus „bei mehr als zwei Euro Erhöhung“ wird die Bedingung „Die Erhöhung beträgt mehr als 2,00 € je Stunde“.
2. Alle Aktionen herausschreiben. Auch der Fall „es passiert nichts Besonderes“ ist eine Aktion und braucht eine Zeile.
3. Die Tabelle mit 2 hoch n Spalten anlegen und die Bedingungsspalten systematisch füllen: die oberste Bedingung wechselt halb J halb N, die nächste in halb so großen Blöcken, und so fort.
4. Je Spalte eintragen, welche Aktionen ausgeführt werden.
5. Auf Widersprüche prüfen: Gibt es zwei gleiche Bedingungsspalten mit verschiedenen Aktionen? Dann ist die Vorgabe unklar und muss beim Auftraggeber geklärt werden.
6. Konsolidieren: Unterscheiden sich zwei Spalten nur in einer Bedingung und lösen dieselben Aktionen aus, werden sie zu einer Spalte zusammengefasst; die unterscheidende Bedingung bekommt ein „–“. Danach auf Redundanz prüfen: Deckt eine Bedingungskombination jetzt zwei Regeln ab, ist eine davon überflüssig.

## Beispiel aus dem Hausprojekt KALK { #beispiel-aus-dem-hausprojekt-kalk }

Altsystem KALK, Menüpunkt „Preisliste pflegen“. Wer wann einen Stundensatz ändern darf, steht
nirgends; auf Nachfrage formuliert die Technische Leitung es so: „Gibt es noch offene Angebote
mit dem alten Satz, will ich jede Änderung freigeben, und der Vertrieb bekommt eine Mitteilung.
Gibt es keine, dürfen kleine Änderungen sofort übernommen werden. Erhöhungen um mehr als 2,00 €
je Stunde brauchen aber auch dann meine Freigabe.“

### Schritt 1 bis 4: die vollständige Tabelle

Zwei Bedingungen, drei Aktionen, vier Regeln:

| | R1 | R2 | R3 | R4 |
|-------------------------------------------|----|----|----|----|
| **Bedingungen** | | | | |
| B1 Es gibt offene Angebote mit dem alten Satz | J | J | N | N |
| B2 Die Erhöhung beträgt mehr als 2,00 € je Stunde | J | N | J | N |
| **Aktionen** | | | | |
| A1 Änderung sofort übernehmen | | | | X |
| A2 Freigabe der Technischen Leitung einholen | X | X | X | |
| A3 Mitteilung an den Vertrieb erzeugen | X | X | | |

Die Tabelle ist vollständig: zwei Bedingungen ergeben vier Regeln, jede Kombination kommt genau
einmal vor. Sie ist widerspruchsfrei, weil keine Bedingungsspalte doppelt vorkommt.

### Schritt 6: konsolidieren

R1 und R2 lösen dieselben Aktionen aus (A2 und A3) und unterscheiden sich nur in B2. Ob die
Erhöhung groß oder klein ist, spielt also keine Rolle, sobald offene Angebote da sind. Genau das
hat die Technische Leitung auch gesagt: „will ich **jede** Änderung freigeben“. Beide Spalten
werden zu einer zusammengefasst; bei B2 steht dann ein Strich:

| | R1+2 | R3 | R4 |
|-------------------------------------------|------|----|----|
| **Bedingungen** | | | |
| B1 Es gibt offene Angebote mit dem alten Satz | J | N | N |
| B2 Die Erhöhung beträgt mehr als 2,00 € je Stunde | – | J | N |
| **Aktionen** | | | |
| A1 Änderung sofort übernehmen | | | X |
| A2 Freigabe der Technischen Leitung einholen | X | X | |
| A3 Mitteilung an den Vertrieb erzeugen | X | | |

Der Strich bedeutet: Bei dieser Regel ist es gleichgültig, ob die Bedingung zutrifft. Die
Tabelle hat jetzt drei Spalten und bleibt vollständig, weil R1+2 zwei der vier Kombinationen
abdeckt. Alle Aktionszeilen bleiben erhalten; es fallen nur Spalten weg, nie Zeilen.

R3 und R4 lassen sich nicht zusammenfassen: Sie unterscheiden sich zwar auch nur in B2, lösen
aber verschiedene Aktionen aus.

### Redundanz und Widerspruch nach der Konsolidierung

Eine vollständige Tabelle mit lauter verschiedenen Bedingungsspalten ist nie redundant.
Redundanz und Widerspruch entstehen erst durch Striche: Eine Regel mit Strich deckt mehrere
Kombinationen ab und kann sich dadurch mit einer anderen Regel überschneiden. Lösen beide dort
dieselben Aktionen aus, ist eine der Regeln überflüssig (Redundanz); lösen sie verschiedene aus,
ist die Vorgabe unklar (Widerspruch) und muss beim Auftraggeber geklärt werden.

Angenommen, jemand hat den letzten Satz der Technischen Leitung („Erhöhungen um mehr als 2,00 €
je Stunde brauchen aber auch dann meine Freigabe“) als eigene Regel R5 an die konsolidierte
Tabelle angehängt: Bei großer Erhöhung wird die Freigabe eingeholt, gleichgültig, ob es offene
Angebote gibt.

| | R1+2 | R3 | R4 | R5 |
|-------------------------------------------|------|----|----|----|
| **Bedingungen** | | | | |
| B1 Es gibt offene Angebote mit dem alten Satz | J | N | N | – |
| B2 Die Erhöhung beträgt mehr als 2,00 € je Stunde | – | J | N | J |
| **Aktionen** | | | | |
| A1 Änderung sofort übernehmen | | | X | |
| A2 Freigabe der Technischen Leitung einholen | X | X | | X |
| A3 Mitteilung an den Vertrieb erzeugen | X | | | |

Auf den ersten Blick sieht die Tabelle ordentlich aus. Ob sich Regeln überschneiden, zeigt sich
erst, wenn man jede Regel mit Strich in ihre einzelnen Kombinationen auflöst: R1+2 steht für J/J
und J/N, R5 für J/J und N/J.

| Kombination B1/B2 | abgedeckt von | Aktionen | Befund |
|---|---|---|---|
| J/J | R1+2 und R5 | A2, A3 bzw. nur A2 | **Widerspruch** |
| J/N | R1+2 | A2, A3 | in Ordnung |
| N/J | R3 und R5 | A2 bzw. A2 | **Redundanz** |
| N/N | R4 | A1 | in Ordnung |

Die Redundanz bei N/J ist harmlos: R3 und R5 verlangen dasselbe, eine von beiden ist überflüssig.
Der Widerspruch bei J/J ist es nicht: Bekommt der Vertrieb bei einer großen Erhöhung mit offenen
Angeboten eine Mitteilung oder nicht? Das entscheidet nicht, wer die Tabelle schreibt, sondern
die Technische Leitung. Hier ist ihre Aussage eindeutig („will ich jede Änderung freigeben, und
der Vertrieb bekommt eine Mitteilung“): R5 ist falsch eingetragen und entfällt.

### Von der Tabelle zu den Testfällen

Je Regelspalte entsteht ein Testfall: Man wählt Werte, die genau diese Bedingungskombination
erzeugen, und notiert als erwartetes Ergebnis die Aktionen der Spalte. Bei Bedingungen mit einer
Zahlengrenze nimmt man zusätzlich die Werte direkt an der Grenze ([Schreibtischtest](../testen/schreibtischtest.md)).

Aus der konsolidierten Tabelle werden so fünf Testfälle:

| Testfall | Regel | Offene Angebote mit dem alten Satz | Erhöhung je Stunde | Erwartet |
|----------|-------|------------------------------------|--------------------|----------|
| T1 | R1+2 | ja | 1,00 € | Freigabe einholen, Mitteilung an den Vertrieb |
| T2 | R3 | nein | 3,50 € | Freigabe einholen |
| T3 | R4 | nein | 1,00 € | sofort übernehmen |
| T4 | R4, Grenze | nein | 2,00 € | sofort übernehmen |
| T5 | R3, Grenze | nein | 2,01 € | Freigabe einholen |

Bei R1+2 spielt die Höhe der Erhöhung keine Rolle, jeder Wert ist recht. T4 und T5 liegen direkt
an der Grenze: 2,00 € sind nicht „mehr als 2,00 €“, 2,01 € schon.

## Vorhandenen Code prüfen und vereinfachen { #vorhandenen-code-pruefen-und-vereinfachen }

Eine Entscheidungstabelle lässt sich auch aus Code aufstellen, der schon da ist. Das lohnt sich,
wenn ein Ablauf über viele Verzweigungen gewachsen ist: Die Tabelle zeigt, ob jeder Fall richtig
behandelt wird, und sie zeigt, welche Prüfungen gar nichts entscheiden.

### Bedingungen und Aktionen aus dem Code

- Jede Abfrage im Code ist eine Bedingung: jedes `if` und `else if`, auch in Methoden, die
  aufgerufen werden. Prüft der Code denselben Wert an verschiedenen Stellen verschieden, etwa
  einmal „genau“ und einmal „mindestens“, wird jede dieser Prüfungen eine eigene Bedingung.
- Jede Stelle, an der das Ergebnis festgelegt oder verändert wird, gehört zu einer Aktion. Haben
  zwei Stellen dieselbe Wirkung, ist das dieselbe Aktion.
- Je Spalte verfolgt man den Weg durch den Code und kreuzt an, welche Aktionen er auslöst. Das ist
  ein kurzer [Schreibtischtest](../testen/schreibtischtest.md) je Spalte; die Zeilennummern schreibt man dazu.
- Danach vergleicht man jede Spalte mit der Vorgabe. Weicht eine ab, ist ein Fehler gefunden.
  Stimmen alle, ist belegt, dass der Code in jedem Fall das Richtige tut.

Ein Beispiel: So könnte KALK eine Änderung in der Preisliste behandeln.

```csharp linenums="1"
if (offeneAngebote > 0)
{
    FreigabeAnfordern(leistungsart, neuerSatz);
    if (neuerSatz - alterSatz > 2.00m)
    {
        MitteilungAnVertrieb(leistungsart);
    }
}
else if (neuerSatz - alterSatz > 2.00m)
{
    FreigabeAnfordern(leistungsart, neuerSatz);
}
else
{
    SatzUebernehmen(leistungsart, neuerSatz);
}
```

Zeile 4 und Zeile 9 prüfen dasselbe auf dieselbe Weise; das ist eine Bedingung. Zeile 3 und
Zeile 11 haben dieselbe Wirkung; das ist eine Aktion.

| | R1 | R2 | R3 | R4 |
|-------------------------------------------|----|----|----|----|
| **Bedingungen** | | | | |
| B1 Es gibt offene Angebote (Z. 1) | J | J | N | N |
| B2 Die Erhöhung beträgt mehr als 2,00 € (Z. 4, Z. 9) | J | N | J | N |
| **Aktionen** | | | | |
| A1 Änderung sofort übernehmen (Z. 15) | | | | X |
| A2 Freigabe einholen (Z. 3, Z. 11) | X | X | X | |
| A3 Mitteilung an den Vertrieb (Z. 6) | X | | | |
| **Weg durch den Code (Zeilen)** | 1, 3, 4, 6 | 1, 3, 4 | 1, 9, 11 | 1, 9, 15 |
| **Entspricht der Vorgabe** | ja | **nein** | ja | ja |

R2 weicht von der Vorgabe der Technischen Leitung ab ([Beispiel aus dem Hausprojekt KALK](#beispiel-aus-dem-hausprojekt-kalk)):
Bei offenen Angeboten und kleiner Erhöhung bekommt der Vertrieb keine Mitteilung. Der Fehler
steckt in Zeile 4, die die Mitteilung zusätzlich an die Höhe der Erhöhung bindet.

### Unmögliche Kombinationen

Beziehen sich zwei Bedingungen auf denselben Wert, schließen sie manche Kombination aus. Mit den
Bedingungen „Die Erhöhung beträgt mehr als 2,00 €“ und „Die Erhöhung beträgt mehr als 5,00 €“
gibt es keine Erhöhung, die mehr als 5,00 € beträgt, aber nicht mehr als 2,00 €. Solche Spalten
werden nicht stillschweigend weggelassen, sondern mit „unmöglich“ gekennzeichnet und danach
gestrichen. Sie brauchen keine Aktion.

Angenommen, eine Erhöhung um mehr als 2,00 € braucht die Freigabe der Technischen Leitung, eine
um mehr als 5,00 € zusätzlich die der Geschäftsführung:

| | R1 | R2 | R3 | R4 |
|-------------------------------------------|----|----|----|----|
| **Bedingungen** | | | | |
| B1 Die Erhöhung beträgt mehr als 2,00 € je Stunde | J | J | N | N |
| B2 Die Erhöhung beträgt mehr als 5,00 € je Stunde | J | N | J | N |
| **Aktionen** | | | unmöglich | |
| A1 Änderung sofort übernehmen | | | | X |
| A2 Freigabe der Technischen Leitung einholen | X | X | | |
| A3 Freigabe der Geschäftsführung einholen | X | | | |

R3 verlangt eine Erhöhung über 5,00 €, die nicht über 2,00 € liegt. Nach dem Streichen bleiben
drei Spalten, und die Tabelle ist trotzdem vollständig: Sie deckt jede Erhöhung ab, die es geben
kann.

### Von der zusammengefassten Tabelle zum einfacheren Ablauf

Nach dem Zusammenfassen ([Vorgehen](#vorgehen), Schritt 6) liest man die Tabelle **Zeile für Zeile**:

1. Hat eine Aktion genau dort ein X, wo eine bestimmte Bedingung J ist, hängt sie nur an dieser
   Bedingung. Sie bekommt eine eigene Abfrage, unabhängig von allen anderen.
2. Wird eine Bedingung für keine Aktion gebraucht, entfällt ihre Prüfung im Ablauf. Am
   deutlichsten sieht man das, wenn bei ihr nach dem Zusammenfassen überall ein Strich steht. Die
   Zeile bleibt trotzdem in der Tabelle stehen.
3. Schließen sich zwei Aktionen gegenseitig aus und steht in jeder Spalte genau eine von beiden,
   werden sie zu einem `WENN … SONST`.
4. Eine Aktion für den Fall „nichts trifft zu“ wird oft zum Startwert vor allen Abfragen.

Am [Beispiel aus dem Hausprojekt KALK](#beispiel-aus-dem-hausprojekt-kalk): A3 (Mitteilung) hat genau dort ein X, wo B1 zutrifft, und hängt damit nur an
B1. A2 (Freigabe) hat ein X, sobald B1 oder B2 zutrifft, A1 (sofort übernehmen) genau in der
übrigen Spalte. Daraus wird:

```
WENN es offene Angebote mit dem alten Satz gibt DANN
  Mitteilung an den Vertrieb erzeugen
ENDE WENN

WENN es offene Angebote mit dem alten Satz gibt ODER die Erhöhung mehr als 2,00 € beträgt DANN
  Freigabe der Technischen Leitung einholen
SONST
  Änderung sofort übernehmen
ENDE WENN
```

Zum Schluss die Probe: Jede Spalte der zusammengefassten Tabelle wird einmal durch den neuen
Ablauf geschickt; es müssen genau die angekreuzten Aktionen herauskommen. Der Ablauf hat zwei
Abfragen nebeneinander statt Zweigen in Zweigen, und jede Aktion steht an genau einer Stelle.

## Kurzreferenz

| Prüfen Sie zum Schluss | Frage |
|--------------------|-------------------------------------------------------------|
| Vollständigkeit | Sind es 2 hoch n Spalten und kommt jede Kombination genau einmal vor? |
| Bedingungen | Kann jede Bedingung nur wahr oder falsch sein? |
| Aktionen | Hat jede Spalte mindestens eine Aktion, auch die harmlose? |
| Widerspruch | Gibt es gleiche Bedingungsspalten mit verschiedenen Aktionen? |
| Konsolidierung | Unterscheiden sich zwei Spalten nur in einer Bedingung bei gleichen Aktionen? Dann zusammenfassen. |
| Redundanz | Deckt nach dem Zusammenfassen eine Kombination zwei Regeln mit gleichen Aktionen ab? |
| Unmöglich | Gibt es Kombinationen, die es nicht geben kann? Kennzeichnen, dann streichen. |
| Vereinfachen | Hängt eine Aktion nur an einer Bedingung? Wird eine Bedingung für keine Aktion gebraucht? |
| Schreibweise | J, N und – bei den Bedingungen, X bei den Aktionen? |

Typische Fehler: Spalten stillschweigend weggelassen, weil sie „nicht vorkommen können“, statt sie
als unmöglich zu kennzeichnen; beim Zusammenfassen Aktionszeilen gestrichen statt Spalten;
Bedingungen mit drei Ausprägungen in eine Zeile gepresst; die Aktion für den Normalfall vergessen;
J und X vertauscht.
