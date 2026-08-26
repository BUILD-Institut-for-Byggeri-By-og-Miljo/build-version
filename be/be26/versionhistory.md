# Be26Eng — Ændringslog

## Version 11.26.8.26

### Fejlrettelser i brugerfladen

- **Andet elforbrug: store værdier blev skåret ned til grænsen** — Felterne "Udebelysning (dagslysstyret)" og "Særligt apparatur, i brugstiden" er absolut el-effekt i W for hele bygningen, men grænserne var sat, som om værdierne var pr. m² (0-100 og 0-200). Overskridelser blev ikke blot markeret røde: ved fokusskift blev værdien rettet ned til grænsen, så inddata reelt blev ændret i modellen. Eksempelmodellen for en administrationsbygning har 180 W udebelysning og 600 W apparatur og blev dermed skåret ned til 100 og 200 W. Grænserne er nu 0-1.000.000 W. Samtidig rettet: valideringsteksten for apparatvarme skrev "W/m²", og den engelske label for udebelysning skrev "W/m²" - begge er W.

- **Fjernvarmeveksler: forkert enhed på varmetabet** — Feltet "Varmetab fra veksler" var angivet i kW. Værdien er W/K, som i Be18. Beregningen har hele tiden brugt den som W/K (varmetabet ganges med temperaturdifferensen), og både modeldokumentet og valideringsteksten skrev W/K - kun feltets label var forkert. Gemte modeller er upåvirkede. (issue #68)

### Fejlrettelser i resultater

- **Resultatark: "Samlet dimensionerende varmetab"** — Tabellen viser bygningens dimensionerende varmetab, altså varmetabet ved de dimensionerende temperaturer, men overskriften sagde blot "Samlet varmetab, W/m²". Ordet "dimensionerende" er nu med. (issue #69)

### Nyheder

- **Nyt hjælpesystem med semantisk søgning** — Be26 har fået en indbygget hjælp, der åbnes med F1 eller ?-knappen i værktøjslinjen: 31 emnesider og 263 felthjælp-ankre, der dækker alle skemaer. Søgningen er semantisk og finder emner på betydning frem for ordlyd, så et spørgsmål formuleret i egne ord rammer det rigtige afsnit, selv om det ikke bruger skemaets ord. Sprogmodellen (multilingual-e5-small) kører **lokalt på maskinen** - ingen API-nøgle, ingen konto, og hverken søgetekst eller modeldata forlader computeren. Indekset over hjælpeteksterne er bygget på forhånd og følger med programmet; ved et opslag omsættes kun selve søgestrengen. Er modellen ikke hentet ned, falder søgningen tilbage til almindelig tekstsøgning, og resten af hjælpen virker uændret.

- **"Vis mig hvor"** — Fra et hjælpeemne kan man springe direkte til det felt, emnet handler om. Ligger feltet på en anden side, navigerer programmet derhen; ligger det i et sammenklappet panel eller på en inaktiv fane, foldes det ud først, og feltet fremhæves med et blink.

- **Felthjælpen er flyttet ind i panelet** — De gamle ?-ikoner ved hvert felt og deres popovers er væk. Hjælpen til et felt vises nu i hjælpepanelet sammen med resten af sidens emner, så man kan læse videre i sammenhængen i stedet for at lukke en boble og åbne den næste.

- **Hjælpen åbner på den side, man står på** — Hjælpepanelet åbnede på indholdsfortegnelsen. Det slår nu den aktuelle rute op og viser sidens egne emner. Findes der ikke et hjælpedokument for siden, vises indholdsfortegnelsen som før.

- **Versionscheck ved opstart** — Be26 spørger nu `https://versions.build.dk/be/be26/latest-version.txt`, om der er udgivet en nyere version, og giver besked én gang pr. udgivelse. Om-siden viser din version over for den seneste, med en knap til at søge manuelt og et link til versionshistorikken. Checket kan slås fra under **Indstillinger → Opstart**. Det kører løsrevet fra opstarten og kan hverken forsinke eller afbryde den; svarer den kanoniske adresse ikke, forsøges GitHub Pages-adressen i stedet. Der sendes ingen oplysninger om bruger eller beregning - serveren ser kun IP-adressen, som ved ethvert andet websideopslag.

## Version 11.26.7.8

### Fejlrettelser i beregningsmodel (varmepumper)

- **Aftræks-varmepumper (udsugningsluft) gav for lavt elforbrug og forkert ydelse** — For en varmepumpe med kold side "Aftræk" (VpA) brugte motoren indblæsnings­temperaturen (tilluft efter varmegenvinding) som fordamper­temperatur i stedet for afkast­temperaturen. Det gav en for høj COP. Fordamperen bruger nu afkast­temperaturen `th − NyVgv·(th − Tu)` (som C++-referencen og regnearket). Retter varmepumpens elforbrug og ydelse for alle Aftræk-typer (Aftræk–Rum, Aftræk–Indblæsning, Aftræk–Varmeanlæg). Testmodel Aftræk–Varmeanlæg: HP-elforbrug (Q281) 847 → 956 kWh (Excel 955,8), HP-ydelse (Q263/Q265) 2,55 → 2,13 MWh (Excel 2,13). Udeluft- og jordslange-varmepumper er uændrede.

### Fejlrettelser i resultater (elforbrug fordelt på brugsprofil)

- **Direkte elopvarmning indgik ikke i brugsprofilens rumopvarmning** — Rækken "Rumopvarmning" (`RESULTAT!Q343`) og dermed "Bygningsdrift"/"Samlet" (Q355/Q357/Q360/Q362) manglede den direkte elopvarmning (`QHdEl`), selvom den indgår i det samlede elbehov. Beregningen svarede til den ældre C++-formel; det aktuelle regneark medregner den. Bidraget er nu tilføjet. Testmodel VPrum: rumopvarmning i profil (Q343) 1026 → 1149 kWh (Excel 1149,3).

- **Varmepumpens standby-el vises nu med 2 decimaler** — Rækkerne "Elbehov, stb. rumopvarmning"/"Elbehov, stb. VBV" (Q282/Q284) blev afrundet til hele kWh (17,52 → 18) og afveg dermed fra regnearket. De vises nu med 2 decimaler som regnearket. (Rå-værdien var korrekt; kun visningen ændres.)

### Nyheder

- **Ny energiramme: Renovering nulemissionsklasse** — Bygningsreglementet har en energiramme for eksisterende bygninger til nulemissions­niveau, som ligger mellem Renoveringsklasse 2 og Renoveringsklasse 1 (mere lempelig end klasse 1). SBST har gjort opmærksom på den i deres kommentarer til SBi-anvisning 213. Den er nu tilføjet under **Forudsætninger** (Konstanter/energirammer) og i **Nøgletal** på resultatsiden. Basis/areal: Bolig 63,0 / 2000, Andet 85,5 / 2000 (kWh pr. m²·år). For referencebygningen "Speciel bygning til Metodebeskrivelse" giver det 96,6 kWh/m²·år (svarer til regnearkets RESULTAT!R15). (issue #65)

## Version 11.26.6.9

### Fejlrettelser i beregningsmodel (erhverv / delvist benyttede bygninger)

- **Driftstid (fo) på ventilation indgik ikke** — Ventilationszoner med en driftstidsfaktor (`fo` < 1, fx en kantine der kun bruges en del af tiden) blev regnet med fuldt areal i ventilatorel, ventilations­varmetab og kølebehov. Faktoren anvendes nu, så zonens effektive areal = areal × fo. På kontor-testmodellen faldt ventilatorel fra 2347 → 1740 kWh (Excel 1748), og varmebehov/-tab matcher nu referencen.

- **Køling fra overtemperatur var for høj (op til +71 % på erhvervsbygninger)** — Den natlige frikøling via ventilation indgik ikke i kølebehovs­beregningen: nattens varmeledningsevne brugte dag­luftstrømmen i stedet for nat­strømmen, så natteventilationens køleeffekt reelt blev sat til nul. Beregningen bruger nu nat­strømmen (`qvm_night`), hvilket svarer til Excel-referencens højere natledningsevne. Kontor-test: køling fra overtemperatur 680 → 393 kWh (Excel 396,5).

- **Specialbygning med udluftet zone: overtemperatur-køling faldt fejlagtigt til 0** — For en zone med dag­udluftning men uden reel natventilation (`qvm_night` = 0) blev dag­udluftningen fejlagtigt anvendt om natten, så rummet blev overkølet og kølebehovet forsvandt. Natventilationen bruger nu kun reel natstrøm; en zone uden natventilation står på grund­ventilation om natten. (Specialbygning-test: overtemperatur-køling 0 → 62,3 kWh.)

### Fejlrettelser i resultater (sommerkomfort)

- **Solindfald rapporteres nu med bevægelig solafskærmning** — "Solindfald" og "Solindfald – maks." under sommerkomfort rapporteres nu med den bevægelige solafskærmning indregnet (samme værdi som selve temperatur­simuleringen og Excel `Som_Temp`), i stedet for den geometriske værdi uden bevægelig afskærmning.

### Værktøjer

- **Sammenlign model: nyt konsistens-tjek for VBV-beholderens ladestyring** — En varmtvands­beholder med en ladeeffekt angivet (> 0) men uden "Pumpe styret"/ladestyring markeres nu som en mulig inddata-fejl. Denne uoverensstemmelse fik kombi-pumpen til at køre hele året og overvurdere pumpe­el, men kunne ikke fanges af den almindelige celle-for-celle-sammenligning (Excel har ingen separat "reguleret"-celle — reguleringen er underforstået af, at ladeeffekten er udfyldt).

## Version 11.26.5.28
Tilretning af Desktop-versionen, inge ændringer i dll

## Version 11.26.5.24

### Fejlrettelser i resultater

- **Elbehov pr. m² var ~13 % for høj på bygninger med opvarmet kælder** — Tabellen "Elbehov fordelt på brugsprofil (kWh/m²)" (rækkerne *Bygningsdrift*, *Andet elbehov* og *Samlet elbehov*) blev divideret med det opvarmede etage­areal uden kælderbidrag. Excel `RESULTAT!Q360-Q362` deler med `Aeg = opvarmet areal + 0,5 × opvarmet kælder`. På etagehus­testen (Aeg = 1222,5 m²) gav det en konsekvent overskydning på ~13 %; tallene matcher nu Excel-referencen.

### Fejlrettelser i sommerkomfort-beregning

- **Ukorrekt nævner i hverves time-simulering** — Den interne simulering af rum­temperatur brugte forkerte formel  
- **Ventilations­setpunkt opdateret 23 → 24 °C** — Den hardkodede konstant for sommer­komfort­ventilation  blev hævet fra 23 til 24 
- **Klimatabeller for skyggevirkning regenereret med fuld præcision**  

### Fejlrettelser i resultater

- **"Energibehov, Varme" var den rene ydelse i stedet for forbrug minus udnyttet tab**  
### Fejlrettelser i beregningsmodel

- **Pumper kørte hele året — også ved behovsstyret drift** — En kombi-pumpe (typen "Pumper behovsstyret (fx gulvvarme)") kørte konstant 8760 timer/år så snart der var en cirkulationspumpe på en varmtvandsbeholder, uanset om beholderen var "Pumpe styret"/reguleret. Drifts­tiden følger nu varmebehovet og varmtvands­beholderens regulerede pumpe­timer som tilsigtet. 
- **Sommer-aktiv solafskærmning gav forkert effekt i april og september** — For vinduer med negativ Fc (sommer-aktiv solafskærmning) anvendte overgangsmånederne april og september en forkert formel 
- **Skyggetabel for syd-sidefin var forskudt** 

## Version 11.26.5.22

### Fejlrettelser i resultater

- **Virkningsgrad altid 0** — "Virkningsgrad" under "Kedel/fjernvarmeveksler, Varme" viste 0 i stedet for den korrekte værdi. En direkte fjernvarmeveksler med varmetab 0 viser nu korrekt.
- **Brændselsandel altid gas** — "Brændsel til opvarmning, andel" viste altid Gas = 1,00 uanset forsyningstype. Værdien følger nu kedlens brændselstype (gas/olie/biobrændsel) og er 0 for fjernvarme og elforsyning.
- **Manglende rækker under "Varmebehov (MWh)"** — Følgende rækker var altid 0 og afspejlede ikke modellens reelle indhold:
  - Gasstrålevarmere
  - Køling
  - I alt (manglede bidrag fra ovenstående)
- **Manglende rækker under "Rumopvarmning, Dækning af varmebehov"** — Følgende rækker var altid 0:
  - Brændeovne mm.
  - I alt (manglede bidrag fra brændeovne)
- **Korrekt opdeling af kedel/fjernvarme i rumopvarmning og varmtvand** — Rækken "Kedel/fjernvarme" under rumopvarmning beregnes nu mere præcist.

### Fejlrettelser i beregningsmodel

- **Supplerende rumvarme indgik ikke i beregningen** — Når der var sat flueben i "Suppl. el-opvarmning" eller "Brændeovne / gasstrålevarmere", blev disse bidrag slet ikke trukket fra varmebehovet før hovedforsyningen. Det er nu korrigeret, så supplementets dækning regnes med før kedel/varmepumpe/solvarme aktiveres.
- **Arealandel for brændeovne** — Feltet for arealandel (`a_frac`) til brændeovne/gasstrålevarmere blev parset forkert; dækningsgraden virker nu som tilsigtet.
- **Kedel og fjernvarmeveksler — fuld EN 15316-model** — Kedlens og vekslerens virkningsgrad og tab beregnes nu efter den fulde CEN model II (EN 15316)  
- **Bygningens rotation anvendes nu på vinduer, solceller og solfangere**  

 

## Version 11.26.5.9

### Fejlrettelser

- **Skyggetabeller** — Rettet fejl i opslag i skyggetabeller der forhindrede beregning.

## Version 11.26.5.7

### Nyheder & Ændringer

- **Hurtigere beregning** — Beregningskernen er optimeret og kører nu hurtigere.
- **Versionsstempel på modelfiler** — Rodelementet `<BE05>` har nu en `version`-attribut der angiver den Be26-version der senest har gemt filen. Appen afviser filer der er gemt i en *nyere* version end den installerede, så ældre versioner ikke kan overskrive nyere modelfiler og dermed risikere at miste felter. Ældre filer (inklusive filer uden attributten, fx fra Be18) åbnes som hidtil.

### Fejlrettelser

- **Trådsikkerhed** — Rettet trådsikkerhed i beregningskernen.
- **Manglende resultater** — Rettet manglende resultater for rumtemperatur og varmt brugsvand.

## Version 11.26.4.29

### Pakke-ændringer

- **Selvstændig DLL** — `Be26Eng.dll` er nu en C# NativeAOT-bygget DLL og indeholder vejrdata indlejret. Følgende filer er fjernet fra distributionspakken:
  - `mfc140.dll`, `msvcp140.dll`, `vcruntime140.dll`, `vccorlib140.dll`, `concrt140.dll` — MSVC runtime kræves ikke længere
  - `StepXml7.dll` — XML-håndtering er nu indbygget
  - `DRY_2014-2023.dip` — referenceklimadata er indlejret i DLL'en

  `Be26Engine.exe` (kommandolinjeværktøjet) medfølger fortsat.

### Klimadata

- **Indlejret reference-klima som standard** — Når modelfilens `BE06_CLIM/<dry>` er tom (eller mangler), bruger beregningskernen indlejret 2014–2023 referenceklima. Ingen `.dip`-fil kræves ved siden af DLL'en.
- **Brugervalgt klimafil** — Hvis `BE06_CLIM/<dry>` indeholder et filnavn (kun bart filnavn, fx `MyClimate.dip`), åbner kernen den fil i mappen ved siden af `Be26Eng.dll`.
- **Validering** — `.dry` (Be18-arv) er ikke understøttet og giver E002. Stier (`/`, `\`, `..`, absolutte stier) afvises.

## Version 11.26.3.20

### Fejlrettelser

- **ISO charset** — Fejl i tegnsæt (ISO-8859-1) i XML/HTML output er rettet; danske tegn (æ, ø, å, Å) vises nu korrekt.
- **DIP-fil finding** — Fejl ved åbning af vejrdata-fil (.dip) er rettet; filen findes nu korrekt uanset arbejdsmappe.
- **Manglende VE uden batteri** — Beregning af vedvarende energi uden batteri viste ingen resultater i visse konfigurationer; dette er nu rettet.
- **Negativ udladning for batteri** — Fejl hvor batteriets udladning kunne blive negativ er rettet.

### Ændringer

- **Breaking changes håndhævet** — To tilfælde medfører nu en beregningsfejl i stedet for at give stille forkerte resultater:
  - **E001** — Modelfil indeholder det forældede felt `cooling_frac` (bygningsniveaufraktion). Filen skal migreres; auto-migrering sker automatisk for de simple tilfælde (`cooling_frac = 0` eller `= 1` med én zone). Se `DiagnosticsAndBreakingChanges.html` for migreringsregler.
  - **E002** — Klimafil (`.dip`/`.zip`) mangler ved kørsel. `Be06Keys` og `Be06Res` blokerede tidligere ikke beregningen og returnerede stille forkerte resultater; dette er nu rettet.
- **Diagnostik** — `Be06Keys` og `Be06Res` returnerer nu en `<diagnostics>`-blok i output-XML ved fejl eller advarsler. Ved fejl returneres ingen resultattabeller — kun `<diagnostics>`-blokken. Advarsler returneres sammen med normale resultater. Se `DiagnosticsAndBreakingChanges.html` for XML-struktur og C#-eksempel på parsing.
- **Ændring af resultattabeller** — Tabellen `electricfactors` er omdøbt til `electricwithoutfactors`; ny tabel `electricwithfactors` er tilføjet. Rækkefølgen af outputtabeller er justeret.
- **Tabeller udskrives altid** — Alle 35 result-tabeller skrives nu i hver beregning, også når relevante data mangler (VE/batteri, sommertemperatur, brændsel, Ae=0). Fraværende data giver nul-værdier i rækkerne i stedet for udeladte tabeller, så strukturen er identisk på tværs af modeller. Dette gendanner adfærden fra ældre versioner.
- **Tilretning af nøgletalsberegning** — Fejl i beregnede nøgletalsværdier er rettet; tabellernes rækkefølge og layout er opdateret.
- **VE-allokering** — VE-produktion allokeres nu korrekt: først direkte til bygningens forbrug, derefter til øvrige forbrug, til sidst eksport til nettet.
- **Udvidet `lang`-parameter** — `lang`-parameteren i `Be06Keys` og `Be06Res` er nu et bit-flag der tvinger outputsprog uanset hvad modelfilen angiver:
  - `lang = 0` — Dansk (standard)
  - `lang = 1` — Engelsk
  - `lang = 2` — Dansk + tving dansk talformat (komma som decimaltegn)
  - `lang = 3` — Engelsk + tving engelsk talformat (punktum som decimaltegn)
  Bit 0 styrer sprog; bit 1 tvinger `LC_NUMERIC`-locale. Fejlmeddelelser i diagnostik-XML følger ligeledes det valgte sprog.

### Nye features

- **ID på tabeller og rækker** — Alle result- og nøgletaltabeller samt deres rækker har nu unikke ID'er i XML-outputtet, hvilket gør det nemmere at slå specifikke værdier op programmatisk.
- **ID-oversigter** — To nye HTML-referencefiler er inkluderet:
  - `Be26_key-id_oversigt.html` — oversigt over alle nøgletals-ID'er
  - `Be26_resultat-id_oversigt.html` — oversigt over alle resultat-ID'er
- **ID ved mouseover i HTML** — I HTML-rapportoutputtet vises tabel- og række-ID'er som tooltip ved mouseover, så det er let at finde det rigtige ID til brug i integrationer.
- **C# eksempel** — `RunCoreExample.cs` er inkluderet som eksempel på kald af `Be26Eng.dll` fra C# (.NET).
