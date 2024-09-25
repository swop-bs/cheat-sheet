# Git in Kommandozeile

<figure markdown="span">
  ![Image title](git-workflow-1.png)
  <figcaption>Quelle: https://www.getdbt.com/ui/img/guides/analytics-engineering/git-workflow-1.png</figcaption>
</figure>

##Schritt 1 - Klone das Original-Repository

Bevor du mit der lokalen Entwicklung beginnen kannst, solltest du eine Kopie des Repositories klonen, an dem du arbeitest.

In der Befehlszeile verwendest du den Befehl `git clone`.

Beispiel:

Remote Repository URL kopieren:

![alt text](remote-repo-url.png){ width="800" }

In Kommandozeile:

```
git clone https://bszw-git.ddns.net:3000/swopperer/Zufallsgenerator.git
```

##Schritt 2 - Erstelle deinen Entwicklungs-Branch

Anstatt auf dem Haupt-Produktions-Branch zu arbeiten, solltest du einen eigenen sicheren Bereich für deine Entwicklungsarbeit erstellen.

Nichts, was du hier tust, ist in Stein gemeißelt, also experimentiere so viel du möchtest mit dem Code.

In der Befehlszeile verwendest du den Befehl `git branch`, um deinen lokalen Klon des Repositories auf einen neuen Branch zu verschieben.

Für die Schule verwendest du bitte immer folgendes Schema für den Branchnamen:

```
klasse_vorname_nachname
```

Konkret:

```
git branch bfi11a_max_mustermann
```

Vergiss nicht, den neu erstellten Branch dann zu aktivieren (auszuchecken):

```
git checkout bfi11a_max_mustermann
```

### Alternative Brancherstellung:

Da man eigentlich fast immer den neu erstellten Branch direkt danach auschecken möchte, gibt es einen besseren Befehl, der den Branch erstellt und diesen automatisch auscheckt. Dadurch muss der Branch vorher nicht via `git branch` erstellt werden.

```
git checkout -b bfi11a_max_mustermann
```

##Schritt 3 - Änderungen ins lokale Repository übernehmen

Mit dem Befehl `git add *` fügst du alle Änderungen in die Staging Area hinzu.

Nachfolgend fügst du deine Änderungen mit dem Befehl `git commit -m "Commit Nachricht"` hinzu (du Commitest deine Änderungen).

Beispiel in Kommandozeile:

```
git add *
git commit -m "Ich habe viele tolle Änderungen gemacht"
```

Wiederhole das Hinzufügen von Änderungen so oft du möchtest. Commite lieber zu oft als zu wenig!

##Schritt 5 - Pushe deine Änderungen ins Remote-Repository

Um deine Änderungen in das Remote Repository hochzuladen, musst du diese `Pushen`.

Da es deinen Branch aktuell nur lokal gibt, musst du den Namen beim Pushen nochmals angeben:

```
git push -o branchname
```

Beispiel:

```
git push -o bfi11a_max_mustermann
```
