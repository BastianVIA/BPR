Task state definitioner
============================


**<span>Ikke startet</span>:**

- Taskens scope er defineret.
- Der er ingen ansvarlige for tasken.
- Ethvert teammember kan starte tasken og derimed sætter den [<span style="color:yellow">I gang</span>]

**<span style="color:yellow">I gang</span>:**

- En task der aktivt arbejdes på.
- En/flere teammembers er ansvarlige for tasken
- Ansvarlige for tasken forventes ikke at starte en ny task
- Ansvarlige opdaterer taskens status på burndown chart
- Kan tasken ikke fortsættes, sætter den ansvarlige tasken i [<span style="color:red">Blokeret</span>]

**<span style="color:red">Blokeret</span>:**

- Den ansvarlige angiver, hvilken task man er blokeret af

**<span style="color:teal">Klar</span>:**

- De ansvarlige mener, der ikke er mere arbejde i tasken
- Ansvarlige informerer andre teammembers
- Andre teammembers informerer, at de reviewer tasken

**<span style="color:green">Færdig</span>:**

- Mindst ét andet teammember end den ansvarlige har godkendt tasken
- Enhver kan rykke en task til [<span style="color:green">Færdig</span>]