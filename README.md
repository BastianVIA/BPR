# BPR - LINAK Actuator Tracking System

Contributors:

| Github navn  | Rigtige navn | VIA ID |
| ------------- | ------------- | ------------- |
| Solaiman1991 | Solaiman Jalili | 304870 |
| Bimmerlynge | Mathias Lynge-Jacobsen | 304888 |
| BastianVIA | Bastian Thomsen | 305294 |
| Nissen99 | Mikkel Jacobsen | 304077 |


Velkommen til LINAK Actuator Tracking System. Dette projekt har til formål at facilitere en alternativ løsning til den nuværende praksis, hvor TECHLINE opbevarer data i forskellige systemer og formater. Dette projekt indeholder et forslag til en centraliseret database med henblik på at reducere ressourceforbruget og forenkle vedligeholdelsen af disse systemer.

## Backend Setup
### Database
For at køre systemet lokalt skal en MSSQL server køre på localhost. 
#### LINAK-DB
Den udleverede LINAK-DB.bak skal restores.
Dette kan for eksempel gøres gennem SQL server management, med Restore Database.

#### EF Core
1. Åben terminalen og navigér til "BPR\BuildingBlocks\BuildingBlocks"
2. Kør følgende kommando
```
dotnet ef -s ../../Backend database update
```
Dette laver en tom "ActuatorDB" med korrekt struktur for systemet.

### CSV Logs
1. Lave en ny mappe under LINTEST ved navn "CSVLogs"
2. Placer udleverede CSV filer i mappen
3. Hvis der står noget i LINTests.lastTimeForProcessedData.json, så skal dette slettes

# Run
Der kræves ikke mere setup, nu kan både backenden og frontenden køres.

# Note
Dette projekt er et C# projekt, for optimal repræsentation af projektstrukturen anbefales det at projektet åbnes i en IDE, som understøtter Solution View. Teamet bruger og anbefaler derfor Rider 
