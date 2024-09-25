# Git in Visual Studio

<figure markdown="span">
  ![Image title](git-workflow-1.png)
  <figcaption>Quelle: https://www.getdbt.com/ui/img/guides/analytics-engineering/git-workflow-1.png</figcaption>
</figure>

## 1. Repository von Remote kopieren:

Visual Studio starten und "Clone a repository" wählen

![alt text](vs_clone_a_repo.png){ width="600" }

URL des Remote Repos kopieren:

![alt text](remote-repo-url.png){ width="800" }

Danach Remote Repository URL und Pfad zum Speichern auf lokalem Gerät angeben:

![alt text](vs_clone_url.png){ width="600" }

## Übersicht

Über `View --> Git Repository` oder `Crtl+0, Ctrl+R` kann die Git Übersicht von Visual Studio geöffnet werden.

![alt text](git-overview_view.png){ width="500" }

In diesem Fenser finden Sie alle Branches (grün schraffiert) und die Historie (gelb schraffiert):

![alt text](overview_detailed.png)

## 2. Neuen Branch erstellen und auschecken:

Im Menübereich unter `Git` im Unterpunkt `New Branch...` kann ein neuer Branch angelegt werden:

![alt text](git_new_branch_toolbar.png){ width="500" }

Namen für den Branch vergeben (klasse_vorname_nachname) und von welchem Branch es weg kopiert werden soll:

![alt text](git_create_new_branch.png){ width="500" }

Dieser wird dann in der Übersicht angezeigt:

![alt text](git_new_branch_in_overview.png)

## 3. Coden und Änderungen commiten:

Änderungen werden im `Solution Explorer` durch einen kleinen roten Haken angezeigt (hier ist in Program.cs eine Änderung enthalten):

![alt text](git_change_solution_explorer.png){ width="400" }

Eine Übersicht über die Änderungen kann unter `Git Changes` im gleichen Abschnitt des Solution Explorers angezeigt werden:

![alt text](git_in_solution_explorer.png){ width="300" }

Mit Doppelklick auf die angezeigt Datei werden die Änderung in der Datei angezeigt. Links das Original, rechts mit Änderungen, grün markiert.

![alt text](git_changed_file.png)

Unter `Git Changes` kann eine Commit-Message eingegeben werden. Commit All fügt die angezeigten Änderungen automatisch zur Staging-Area hinzu und commitet im Anschluss automatisch mit der eingegebenen Commit-Message.

![alt text](git_commit_message.png)

Die Änderung wird nach dem Commit auch in der Übersicht dargestellt:

![alt text](git_change_in_overview.png)

## 4. Code in eigenen Branch pushen:

Um die Änderungen jetzt im Remote Repository hochzuladen, muss der Branch mit den Änderungen noch gepusht werden:

![alt text](git_push_branch.png){ width="500" }

Nun wird der Branch auch in der Übersicht unter `remotes` angezeigt:

![alt text](git_remotes.png){ width="500" }