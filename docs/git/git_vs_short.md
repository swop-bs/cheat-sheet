# Git in Visual Studio Kurzübersicht

![alt text](git_vs_short.png)

## 1. Repository von Remote kopieren:

Visual Studio starten und "Clone a repository" wählen

![alt text](vs_clone_a_repo.png){ width="600" }

URL des Remote Repos kopieren:

![alt text](remote-repo-url.png){ width="800" }

Danach Remote Repository URL und Pfad zum Speichern auf lokalem Gerät angeben:

![alt text](vs_clone_url.png){ width="600" }

## 2. Neuen Branch erstellen und auschecken:

Im Menübereich unter `Git` im Unterpunkt `New Branch...` kann ein neuer Branch angelegt werden:

![alt text](git_new_branch_toolbar.png){ width="500" }

Namen für den Branch vergeben (klasse_vorname_nachname) und von welchem Branch es weg kopiert werden soll:

![alt text](git_create_new_branch.png){ width="500" }

## 3. Coden und Änderungen commiten:

Unter `Git Changes` Commit-Message eingegeben. Commit All fügt die angezeigten Änderungen automatisch zur Staging-Area hinzu und commitet im Anschluss automatisch mit der eingegebenen Commit-Message.

![alt text](git_commit_message.png){ width="500" }

## 4. Code in eigenen Branch pushen:

Um die Änderungen jetzt im Remote Repository hochzuladen, muss der Branch mit den Änderungen noch gepusht werden:

![alt text](git_push_branch.png){ width="500" }