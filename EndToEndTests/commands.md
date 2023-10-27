# Commands for E2E tests

#### Installer drivers:
Installerer de nødvendige browser drivers, som **Playwright** bruger til at køre tests.
```bash
bin/Debug/net7.0/playwright.ps1 install
```

#### Kør alle test:
```bash
dotnet test
```

Hvis `Headless` er sat til **false** i `.runsettings` vises de kørende tests live i en browser.



#### Åben codegen session:
*Note: <u>Frontend</u> skal være kørende for, at der kan startes en codegen session*


Erstat <u>**urlPath**</u> med den side du vil åbne en codegen session for.<br>

Eks:``
bin/Debug/net7.0/playwright.ps1 codegen localhost:5002/PCBAInfo
``
```bash
bin/Debug/net7.0/playwright.ps1 codegen urlPath
```



