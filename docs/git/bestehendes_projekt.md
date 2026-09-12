# Bestehendes Projekt unter Git stellen

Die Kapitel *Git Kommandozeile* und *Git Visual Studio* gehen davon aus, dass es das Repository schon gibt und du es klonst. Hier geht es um den umgekehrten Fall: Ein Projekt liegt bereits als Ordner auf deiner Festplatte und soll ab jetzt unter Versionsverwaltung stehen.

!!! info "Reihenfolge"
    Erst `.gitignore` anlegen, **dann** das Repository. Wer zuerst committet und danach an die `.gitignore` denkt, hat `bin` und `obj` schon in der Historie – und bekommt sie nur mühsam wieder heraus (siehe ganz unten).

## Schritt 1 – `.gitignore` anlegen

In ein Repository gehört alles, was man zum Bauen braucht, und nichts, was daraus wieder entsteht. Bei einem .NET-Projekt entstehen beim Bauen die Ordner `bin` und `obj` neu; Visual Studio legt zusätzlich `.vs` und `*.user` an. Diese Dateien sind größer als das Projekt selbst und tauchen bei jedem Bauen als Änderung auf.

Lege im **Wurzelverzeichnis** des Projekts (dort, wo die `.csproj` liegt) eine Datei namens `.gitignore` an:

```
# Build-Ergebnisse von Visual Studio / .NET
bin/
obj/

# Benutzerbezogene Visual-Studio-Dateien
.vs/
*.user
*.suo

# Betriebssystem
Thumbs.db
.DS_Store
```

Die `.gitignore` selbst gehört ins Repository – sonst muss sie jede Person neu anlegen.

## Schritt 2 – Repository im vorhandenen Ordner anlegen

In der Kommandozeile in den Projektordner wechseln und dort:

```
cd C:\Projekte\MeinProjekt
git init -b main
```

`git init` legt im Ordner ein verstecktes Verzeichnis `.git` an. Darin liegt ab jetzt die gesamte Historie. Dein Projekt selbst ändert sich dadurch nicht. `-b main` legt fest, dass der erste Branch `main` heißt.

Beim ersten Mal auf einem Rechner musst du Git außerdem sagen, wer du bist. Der Name erscheint später in der Historie:

```
git config --global user.name "Max Mustermann"
git config --global user.email "max.mustermann@bszw.de"
```

## Schritt 3 – Erster Commit

Zuerst anschauen, was Git überhaupt sieht:

```
git status
```

Tauchen hier `bin/` oder `obj/` auf, stimmt die `.gitignore` noch nicht. Erst weitermachen, wenn die Liste sauber ist.

```
git add .
git commit -m "Projekt unter Versionsverwaltung gestellt"
```

`git add .` nimmt alle Dateien des Ordners auf – außer denen, die in der `.gitignore` stehen.

Mit `git log --oneline` kannst du dir die Historie ansehen.

## Schritt 4 – Leeres Repository auf dem Server anlegen

Auf dem Gitea unter `https://bszw-git.ddns.net:3000` anmelden und ein **neues, leeres** Repository anlegen:

`+` oben rechts → *New Repository* → Name eintragen → **keine** Häkchen bei `README`, `.gitignore` oder `License` setzen.

!!! warning "Wirklich leer"
    Wird beim Anlegen eine `README` erzeugt, hat das Repository auf dem Server schon einen eigenen ersten Commit. Dein lokales Repository hat einen anderen. Git weigert sich dann zu pushen, weil die beiden Historien nichts miteinander zu tun haben.

Nach dem Anlegen zeigt Gitea die URL des Repositories an. Sie sieht so aus:

```
https://bszw-git.ddns.net:3000/swopperer/MeinProjekt.git
```

## Schritt 5 – Server eintragen und pushen

```
git remote add origin https://bszw-git.ddns.net:3000/swopperer/MeinProjekt.git
git push -u origin main
```

`origin` ist der übliche Name für das Remote-Repository. Das `-u` merkt sich beim ersten Mal die Zuordnung; ab dem zweiten Mal genügt `git push`.

Mit `git remote -v` kannst du prüfen, welcher Server eingetragen ist.

Danach im Browser nachsehen: Wenn deine Dateien und dein Commit dort stehen, ist alles angekommen.

## Dasselbe in Visual Studio

1. Projekt öffnen. `.gitignore` wie in Schritt 1 im Projektordner anlegen (Visual Studio legt auf Wunsch selbst eine an, sie ist deutlich länger und funktioniert genauso).
2. Menü `Git` → `Create Git Repository...`.
3. Im Dialog links **Existing Remote** wählen, wenn du das leere Repository auf dem Server schon angelegt hast, und dessen URL eintragen. Sonst **Local only** wählen und den Server später über `Git` → `Manage Remotes...` nachtragen.
4. Auf `Create and Push` klicken.

Ab hier gilt das Kapitel [Git Visual Studio](git_vs.md): Änderungen erscheinen unter `Git Changes`, Commit-Nachricht eingeben, `Commit All`, danach `Push`.

Die Übersicht über Branches und Historie öffnest du mit `View` → `Git Repository`.

## Kontrolle

| Prüfung | So geht sie |
|---|---|
| Sind `bin` und `obj` draußen? | `git status` zeigt sie nicht an; im Browser sind sie nicht zu sehen. |
| Ist alles committet? | `git status` meldet *nothing to commit, working tree clean*. |
| Ist alles auf dem Server? | `git status` meldet *Your branch is up to date with 'origin/main'*. |
| Ist das Projekt vollständig? | In einen **anderen** Ordner klonen und dort bauen. Fehlt eine Datei, fällt es hier auf. |

## Wenn `bin` und `obj` schon eingecheckt sind

Passiert oft, wenn zuerst committet und danach an die `.gitignore` gedacht wurde. Die `.gitignore` gilt nämlich nur für Dateien, die Git noch nicht kennt.

```
git rm -r --cached bin obj
git commit -m "bin und obj aus der Versionsverwaltung entfernt"
```

`--cached` entfernt die Ordner nur aus dem Repository, nicht von der Festplatte. Aus den **älteren** Commits verschwinden sie damit nicht – dafür müsste die Historie umgeschrieben werden. Deshalb: `.gitignore` zuerst.
