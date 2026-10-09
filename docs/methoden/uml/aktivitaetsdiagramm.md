# Aktivitätsdiagramm

## Was ein Aktivitätsdiagramm ist

Ein Aktivitätsdiagramm zeigt einen Ablauf als Bild: Was passiert nacheinander, wo wird
entschieden, wo wird etwas wiederholt. Es gehört zur UML, der Standardsprache für die
Modellierung von Software, und ist damit unabhängig von jeder Programmiersprache.

Die Methodenabteilung benutzt Aktivitätsdiagramme immer dann, wenn ein Ablauf mit einem
Auftraggeber abgestimmt werden muss. Ein Bild lässt sich in einer Besprechung gemeinsam ansehen;
eine Textbeschreibung liest jeder für sich und meist anders.

## Notation { #notation }

![Elemente eines Aktivitätsdiagramms](notation_aktivitaetsdiagramm.png){ .diagramm }

| Element | Bedeutung |
|--------------------------------------------|-------------------------------------------------------------|
| Startknoten (ausgefüllter Kreis) | Hier beginnt der Ablauf. Genau einer je Diagramm. |
| Aktion (Rechteck mit runden Ecken) | Ein Schritt, der etwas tut. Formulierung als Tätigkeit: „Offene Positionen sammeln“. |
| Entscheidung (Raute, ein Eingang) | Der Ablauf teilt sich. Jede ausgehende Kante wird beschriftet, z. B. [ja] und [nein]. |
| Zusammenführung (Raute, mehrere Eingänge) | Die geteilten Wege laufen wieder zusammen. |
| Kante (Pfeil) | Die Reihenfolge. Pfeile kreuzen sich möglichst nicht. |
| Endknoten (Kreis mit Punkt) | Hier endet der Ablauf. |

## Vorgehen

1. Den Ablauf zuerst in Sätzen aufschreiben oder als [Pseudocode](../algorithmen/pseudocode_struktogramm.md) notieren.
2. Start und Ende festlegen und ganz oben bzw. ganz unten zeichnen.
3. Aktionen in der richtigen Reihenfolge untereinander setzen; eine Aktion, ein Rechteck.
4. Jede Stelle, an der es weitergeht „nur wenn …“, wird eine Raute. Beide Kanten beschriften; die Bedingungen müssen sich ausschließen und alles abdecken.
5. Geteilte Wege wieder zusammenführen, bevor es weitergeht.
6. Wiederholungen als Rücksprung auf eine Zusammenführung oberhalb zeichnen.
7. Zum Schluss mit dem Finger jeden Weg vom Start bis zum Ende abgehen.

## Beispiel aus dem Hausprojekt KALK

Werkzeug KALK, Menüpunkt „Angebot in Auftrag übernehmen“: Ein Angebot darf erst übernommen
werden, wenn jede seiner Positionen einen Stundensatz hat. Fehlt bei einer Position der Satz, wird
eine Liste der offenen Positionen ausgegeben.

![Angebot in Auftrag übernehmen im Werkzeug KALK](aktivitaetsdiagramm_auftragsuebernahme.png){ .diagramm }

## Kurzreferenz

| Prüfen Sie zum Schluss | Frage |
|--------------------|-------------------------------------------------------------|
| Start und Ende | Genau ein Startknoten, mindestens ein Endknoten vorhanden? |
| Rauten | Ist jede ausgehende Kante beschriftet? Schließen sich die Bedingungen aus? |
| Vollständigkeit | Gibt es für jeden möglichen Wert einen Weg? |
| Zusammenführung | Laufen geteilte Wege wieder zusammen, oder enden sie im Nichts? |
| Sackgassen | Führt jeder Weg irgendwann zum Endknoten? |
| Sprache | Ist jede Aktion als Tätigkeit formuliert, nicht als Hauptwort? |

Typische Fehler: Bedingung in ein Rechteck statt in eine Raute geschrieben; nur der ja-Zweig
gezeichnet; Rücksprung direkt in eine Raute statt auf eine Zusammenführung; Schleife ohne
Abbruchbedingung.

## Erweiterung: gleichzeitige Wege und Verantwortungsbereiche

Die Elemente aus dem Abschnitt [Notation](#notation) reichen für einen Ablauf, der eine Stelle allein abarbeitet. Sobald mehrere
Abteilungen beteiligt sind und Dinge nebeneinander laufen, kommen zwei Elemente dazu. Beide
gehören zum Prüfungsstoff.

| Element | Bedeutung |
|--------------------------------------------|-------------------------------------------------------------|
| Aufteilung (waagerechter Balken, ein Eingang, mehrere Ausgänge) | Ab hier laufen **alle** ausgehenden Wege gleichzeitig. Die Kanten werden **nicht** beschriftet. |
| Synchronisation (waagerechter Balken, mehrere Eingänge, ein Ausgang) | Es geht erst weiter, wenn **alle** eingehenden Wege angekommen sind. |
| Verantwortungsbereich (senkrechte oder waagerechte Spalte mit Überschrift) | Alles in dieser Spalte tut die genannte Stelle. Die Überschrift ist eine Rolle, kein Personenname. |

**Balken oder Raute?** Das ist die Frage, an der die meisten Punkte verloren gehen.

| Formulierung im Text | Element |
|----------------------------------------------|-----------------------------|
| „wenn … , sonst …", „falls", „je nachdem" | Entscheidung (Raute): genau **ein** Weg wird gegangen |
| „währenddessen", „in der Zwischenzeit", „gleichzeitig", „parallel dazu" | Aufteilung (Balken): **alle** Wege werden gegangen |
| „erst wenn beides fertig ist", „ich warte, bis alles da ist" | Synchronisation (Balken) |
| „die Wege laufen wieder zusammen" nach einer Entscheidung | Zusammenführung (Raute) |

Merksatz: **Raute heißt entweder-oder, Balken heißt sowohl-als-auch.**

Zusätzliche Regeln für diese Elemente:

1. Jede Aufteilung braucht eine Synchronisation. Ein Weg, der aus einem Balken herausläuft und
   nirgends wieder ankommt, ist ein Fehler.
2. Aus einem gleichzeitigen Bereich wird nicht per Entscheidung herausgesprungen. Erst
   zusammenführen, dann entscheiden.
3. Verantwortungsbereiche werden vor dem Zeichnen festgelegt und danach nicht mehr geändert.
   Jede Aktion steht in genau einem Bereich, nämlich dem der Stelle, die sie ausführt.
4. Eine Kante darf einen Bereich verlassen. Das ist der übliche Fall: Sie ist die Übergabe von
   einer Stelle an die nächste.
5. Läuft ein Schritt außerhalb des Systems (Anruf, Papier, Zuruf), wird er trotzdem gezeichnet.
   Gerade solche Schritte sind der Grund, warum ein Ablauf aufgeschrieben werden muss.

**Kurzreferenz zur Erweiterung:** Hat jede Aufteilung eine Synchronisation? Sind die Kanten am
Balken unbeschriftet? Steht jede Aktion im Bereich derjenigen Stelle, die sie tut? Ist jede
Bereichsüberschrift eine Rolle und kein Name?
