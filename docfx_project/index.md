# Dokumentationsseite der *EMS*.

Die EMS (Experimentkonfigurationsverwaltungssoftware) dient dem Anwender Experimentkonfigurationen zu verwalten.

Zu den Funktionen der EMS gehören:
- Das **einlesen** von Faktorbaumkonfigurationen im XML-Format.
- Das **schreiben** von Faktorbaumkonfigurationen im XML-Format.
- Das **erzeugen** von Experimentkonfigurationen durch weiterschalten eines Faktorbaums.
- Das **setzen** eines Faktorbaumes in seinen Initialzustand.
- Das **ausgeben** altueller Faktorbaumkonfigurationen auf der Benutzeroberfläche.


## Funktionsweise
Das Programm liest einen sog. Faktorbaum ein und iteriert über diesen um verschiedene Faktorwertkombinationen zu erzeugen. Mehr Informationen finden Sie dazu in der [EMS-Klassendokumentation](api/index.md).