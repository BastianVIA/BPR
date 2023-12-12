# Kommandoer til E2E tests

#### Kør alle test:
```bash
dotnet test
```

Hvis `Headless` er sat til **false** i `.runsettings` vises de kørende tests live i en browser.



#### Åben codegen session:
*Note: <u>Frontend</u> skal være kørende for, at der kan startes en codegen session*


Erstat <u>**urlPath**</u> med den side du vil åbne en codegen session for.<br>

Eks:``
bin/Debug/net7.0/playwright.ps1 codegen localhost:5002/ActuatorSearch
``
```bash
bin/Debug/net7.0/playwright.ps1 codegen localhost:5002/urlPath
```



