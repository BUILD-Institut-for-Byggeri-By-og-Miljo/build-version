# Be26 — Ændringslog

## Version 11.26.9.29

### Pakke-ændringer

- **Sommertemperaturberegningen afviser nu de samme filer som resten af kernen** — `Be06Temp` kontrollerer nu modellen for E001 (gammel kølingsandel fra Be18) og E002 (klimafil mangler eller er ugyldig) ligesom `Be06Calc`. Tidligere blev en sådan fil beregnet uden fejl, eller beregningen stoppede blot med returkode 3. Nu skrives en `_tmp.xml`, der kun indeholder fejlkoden og en forklaring (`<diagnostics>`), og funktionen returnerer 0.
- **XML-erklæring i alle outputfiler** — Fejlsvar (`<diagnostics>`), `_log.xml` og `_tmp.xml` starter nu med `<?xml version="1.0" encoding="ISO-8859-1"?>` ligesom resultatfilerne, så danske tegn altid læses korrekt.
- **Manualen `Be26EngV11.pdf` er fjernet fra pakken** — `README.txt` beskriver nu de eksporterede funktioner, kaldkonventionen og filerne.
- Ingen ændringer i beregningen, i API'et eller i strukturen af resultat-XML'en.

### Fejlrettelser i brugerfladen

- **Skyggeforhold: udhæng og sideskygge er vinkler i grader, ikke meter** — Felterne for udhæng og sideskygge (venstre og højre) var mærket i meter, men beregningen og Be18 bruger dem som vinkler i grader. Etiketter, grænser (0-90°) og hjælpetekst er rettet. Beregningen er uændret, og gemte modeller er upåvirkede.
- **Advarsel om ugemte ændringer ved lukning** — Lukkede man Be26 med ugemte ændringer, lukkede programmet uden at spørge, og ændringerne gik tabt. Nu vises "Gem / Kassér / Annullér" som ved åbning af en anden fil, når vinduet lukkes på Windows, og ved Afslut (Cmd+Q) på Mac.
- **Ugemte ændringer gendannes efter nedbrud** — Desktop-programmet gemmer nu en gendannelseskopi, så længe der er ugemte ændringer. Lukkes programmet uden at spørge (Macs røde lukkeknap, nedbrud, tvungen lukning), tilbydes ændringerne ved næste start, og modellen står som ugemt. Kopien fjernes, når modellen gemmes, eller når ændringerne kasseres ved lukning.
- **Setpunkt for rumopvarmning: standard er 20 °C** — Feltet viste "standard 21", mens en ny fil og beregningen bruger 20 °C.
- **Belastningsfaktor for mekanisk køling må være over 1** — Faktoren er forholdet mellem den interne belastning i kølede og ikke-kølede rum, og en ny fil har standardværdien 1,2. Grænsen 0-1 gav en fejlmeddelelse i en ny fil; grænsen er nu 0-10.
- **Belysning: tabeloverskrifterne overlappede** — De lange overskrifter ombrydes nu på 2-3 linjer, så de ikke løber ind i hinanden.
- **Programmets data flyttes til `%LOCALAPPDATA%\BUILD` (Windows)** — Indstillinger, licens og seneste filer lå i en mappe med navnet "User Name" (en rest fra projektskabelonen). De kopieres automatisk til den nye mappe ved første start, så intet går tabt.
- **Engelske menunavne** — "Boilers and district heating" hedder nu "Boilers", og "District heating exchanger" hedder "District heating".

### Be26-programmet

- **Nye ikoner i sidemenuen** — Menuen har fået et samlet sæt ikoner, hvor hvert punkt har et motiv, der svarer til indholdet (fx rør for varmefordeling, varmepumpe og gasbrænder under forsyning).

### Hjælp

- **Belysningsniveau** — Hjælpen angiver nu 500 lux for kontorarbejde efter DS/EN 12464-1 (tidligere 300 lux) og 300 lux for klasselokaler i skoler.

## Version 11.26.9.28

### Pakke-ændringer

- **E001 afviser færre filer** — Beregningskernen afviser nu kun filer, hvor den gamle Be18-andel med mekanisk køling (`cooling_frac`) reelt ikke kan oversættes: andel mellem 0 og 1, mekanisk køling slået til og ingen zoner med `mech_cooling`/`incl_in_cooling_frac`. Fejlteksten for E001 er omskrevet og nævner nu andelen og antallet af zoner. Se `Aendringer_siden_11.26.9.14.md` og `DiagnosticsAndBreakingChanges.html`.
- **Varmepumper uden alle felter regnes med skemaets standardværdier** — Ændrer resultatet for filer, hvor en varmepumpe mangler felter (se nedenfor). Filer, der er gemt af Be18 eller af Be26 med alle felter, er upåvirkede.
- **Ingen ændringer i API'et eller i strukturen af resultat-XML'en.**

### Be26-programmet

- **Be26 til macOS** — Be26 findes nu også som signeret og notariseret program til Mac med Apple Silicon.
- **Licens i webudgaven** — Licensoplysningerne gemmes nu i den enkelte browser, og licensen kontrolleres, før der regnes. Sammenligning med regneark viser en besked, hvis licensen mangler.

### Flere varmepumper i samme zone

- **Varmepumpe tilføjet i Be26 blev regnet med forkert varmeafgiver** — En varmepumpe, der er oprettet med "Tilføj varmepumpe", fik kun gemt de felter, brugeren selv havde udfyldt. For de manglende felter viste skemaet "Udeluft/Varmeanlæg", mens beregningen stille regnede med "Rumluft" som varmeafgiver, og for den manglende relative COP ved 50 % viste skemaet 0,8, mens beregningen brugte 0. Med en udeluftvarmepumpe til gulvvarme gav det en for høj COP og dermed for lavt elforbrug til varmepumpen (776 mod regnearkets 1012 kWh i eksemplet Parcelhus med ventilations- og udeluftvarmepumpe). Beregningen bruger nu de samme standardværdier, som skemaet viser.
- **Nye varmepumper gemmes med alle felter** — "Tilføj varmepumpe" skriver nu alle felter for rumopvarmning og varmt brugsvand med standardværdier, som Be26 altid har gjort, så filen også kan læses af ældre C++-baserede beregningskerner, der afviste den sparsomme fil.
- **Afkrydsningsfelt i stedet for minus foran andelen** — At en anden varmepumpe dækker resten af samme zone blev angivet med et minus foran "Andel af etageareal". Det angives nu med afkrydsningsfeltet "Anden varmepumpe dækker resten af zonen", og andelen vises altid positiv. Filformatet er uændret (fortegnet gemmes stadig), så Be18-filer og beregningskernen forstår det samme. Hjælpen er opdateret.
- **Sammenligning med regneark** — Et parcelhus med ventilationsvarmepumpe, udeluftvarmepumpe og el til varmt brugsvand sammenlignes nu automatisk med Be05-regnearkets RESULTAT-ark.

### Fejlrettelser i brugerfladen

- **Rotation kan angives fra -360 til 360°** — Grænsen -180 til 180° var alene en indtastningsgrænse; beregningen normaliserer alle vinkler modulo 360, og Be18 tillod 0 til 360°. Ved fokusskift blev fx 270° klemt ned til 180° og modellen ændret.
- **Skyggeforhold: "Vindueshul" er i procent, ikke meter** — Feltet var mærket "Lysning, m" med grænsen 0-10 m, mens kolonneoverskriften, modeldokumentet, Be18 og selve beregningen (knæk ved 10, 20 og 30 %) alle bruger procent. En Be18-fil med fx 14 % blev vist som fejl og klemt ned til 10 ved fokusskift. Feltet hedder nu "Vindueshul, %" med grænsen 0-100, og hjælpeteksten er rettet.
- **Tabelvisning: kolonnebredderne hoppede ved scroll** — Kolonnerne fulgte indholdet i de synlige rækker og skiftede bredde, hver gang der blev scrollet. Felterne har nu faste bredder pr. type (navn, valgliste, tal), så kolonnerne står stille. Gælder alle tabelsider.
- **Tabel- og kortvisning hakkede ved scroll** — Lange lister blev genopbygget for hvert scroll-trin, hvilket især var tungt i webudgaven. Lister under 200 rækker (60 kortrækker) vises nu fuldt ud én gang, så browseren scroller selv, og større lister genopbygges sjældnere. Gælder alle syv tabelsider og deres kortvisninger.
- **Tabelvisning skifter ikke længere til stablet mobilvisning på smalle skærme** — Tabellen blev stablet række for række på smalle skærme, hvilket gjorde den ulæselig. Tabellen beholder nu sine kolonner og scroller vandret; kortvisningen er fortsat valget til små skærme.

### Be18-filer med kølingsandel (E001)

- **Be18-feltet `cooling_frac` oversættes ved indlæsning** — Be18 gemte andelen med mekanisk køling som ét tal på bygningen; Be26 angiver mekanisk køling pr. ventilationszone. Programmet fjerner nu feltet, når en fil åbnes, og markerer zonerne: har bygningen ikke mekanisk køling (eller er andelen 0), fjernes feltet blot; er andelen 1, markeres alle zoner; er andelen en brøkdel, markeres alle zoner, og en meddelelse beder om at fjerne markeringen på de zoner, der ikke køles. Modellen markeres som ændret, så den gemmes i nyt format, og feltet skrives aldrig ved gem. Tidligere blev feltet bevaret ved gem, så en Be18-fil blev ved med at blive afvist af kernen (E001), selv efter zonerne var sat, og resultatsiden viste blot tomme faner.
- **Andelen med mekanisk køling udledes af zonerne** — Programmet læser ikke længere det gamle felt, men beregner andelen af zonernes arealer og flag som kernen gør. Feltet "Andel mek. køling" i regnearkssammenligningen er dermed skrivebeskyttet.
- **E001 udløses kun, når andelen reelt er tvetydig** — Kernen afviser nu kun filer med `0 < cooling_frac < 1`, hvor bygningen har mekanisk køling, og ingen zone bærer `mech_cooling`/`incl_in_cooling_frac`. Filer uden mekanisk køling, filer med andel 1 og filer, der allerede har zoneflag, beregnes. Tidligere blev fx en Be18-fil med 27 zoner og køling slået fra afvist, og det samme gjorde filer i nyt format, der stadig bar det gamle felt. Desktop-programmet, webudgaven og DLL'en bruger nu den samme kontrol.
- **Resultatsiden viser kernens afvisning** — Afviser kernen modellen (E001/E002), viser fanen Beregning nu koden og kernens forklaring i stedet for tomme faner, og for E001 en henvisning til Ventilation. Kernens tekst følger programmets sprog.
- **Webudgaven bruger samme kontrol** — Webudgavens beregning afviser nu de samme filer som kernen (E001) og melder E002, hvis modellen henviser til en klimadatafil, som webudgaven ikke kan læse; tidligere blev der stille regnet med referenceklimaet.

## Version 11.26.9.14

### Pakke-ændringer

- **Automatisk signering af DLL-pakken** — `Be26Eng.dll` (32-bit) og `Be26Engine.exe` signeres nu automatisk med Azure Artifact Signing som en del af pakningen, på samme måde som Be26-installationsprogrammet. Signaturnavnet er `Aalborg Universitet`; tidligere pakker var signeret som `Aalborg University`. Integrationer, der kontrollerer signaturens udsteder, skal opdateres til det nye navn (se `Aendringer_siden_11.26.9.14.md`).
- **Ingen ændringer i beregningskernen** siden 11.26.8.26. API, eksporterede funktioner og resultat-XML er uændrede.

### Be26-programmet

- **Nyt moderne temaudtryk** — Et nyt, roligere udtryk med nye ikoner kan vælges under temaindstillingerne; det gamle udtryk kan fortsat vælges.
- **Engelsk hjælp** — Hjælpen findes nu også på engelsk og følger programmets sprogvalg.
- **Hjælpetekster opdateret** — Hjælpen er gennemgået og udvidet, blandt andet for "Vis som tabel", import/eksport-knappen på tabelsiderne og fortryd-konflikter. Beregner-siden er fjernet fra hjælpen.
- **Sprogvalg** — Det valgte sprog markeres med en ramme, og der advares, hvis decimaltegnet i Windows ikke svarer til sproget.

## Version 11.26.8.26

### Fejlrettelser i brugerfladen

- **Andet elforbrug: store værdier blev skåret ned til grænsen** — Felterne "Udebelysning (dagslysstyret)" og "Særligt apparatur, i brugstiden" er absolut el-effekt i W for hele bygningen, men grænserne var sat, som om værdierne var pr. m² (0-100 og 0-200). Overskridelser blev ikke blot markeret røde: ved fokusskift blev værdien rettet ned til grænsen, så inddata reelt blev ændret i modellen. Eksempelmodellen for en administrationsbygning har 180 W udebelysning og 600 W apparatur og blev dermed skåret ned til 100 og 200 W. Grænserne er nu 0-1.000.000 W. Samtidig rettet: valideringsteksten for apparatvarme skrev "W/m²", og den engelske label for udebelysning skrev "W/m²" - begge er W.
- **Fjernvarmeveksler: forkert enhed på varmetabet** — Feltet "Varmetab fra veksler" var angivet i kW. Værdien er W/K, som i Be18. Beregningen har hele tiden brugt den som W/K (varmetabet ganges med temperaturdifferensen), og både modeldokumentet og valideringsteksten skrev W/K - kun feltets label var forkert. Gemte modeller er upåvirkede.

### Fejlrettelser i resultater

- **Resultatark: "Samlet dimensionerende varmetab"** — Tabellen viser bygningens dimensionerende varmetab, altså varmetabet ved de dimensionerende temperaturer, men overskriften sagde blot "Samlet varmetab, W/m²". Ordet "dimensionerende" er nu med. Tabel- og række-ID'er er uændrede.

### Nyheder

- **Nyt hjælpesystem med semantisk søgning** — Be26 har fået en indbygget hjælp, der åbnes med F1 eller ?-knappen i værktøjslinjen: 31 emnesider og 263 felthjælp-ankre, der dækker alle skemaer. Søgningen er semantisk og finder emner på betydning frem for ordlyd, så et spørgsmål formuleret i egne ord rammer det rigtige afsnit, selv om det ikke bruger skemaets ord. Sprogmodellen (multilingual-e5-small) kører **lokalt på maskinen** - ingen API-nøgle, ingen konto, og hverken søgetekst eller modeldata forlader computeren. Indekset over hjælpeteksterne er bygget på forhånd og følger med programmet; ved et opslag omsættes kun selve søgestrengen. Er modellen ikke hentet ned, falder søgningen tilbage til almindelig tekstsøgning, og resten af hjælpen virker uændret.
- **"Vis mig hvor"** — Fra et hjælpeemne kan man springe direkte til det felt, emnet handler om. Ligger feltet på en anden side, navigerer programmet derhen; ligger det i et sammenklappet panel eller på en inaktiv fane, foldes det ud først, og feltet fremhæves med et blink.
- **Felthjælpen er flyttet ind i panelet** — De gamle ?-ikoner ved hvert felt og deres popovers er væk. Hjælpen til et felt vises nu i hjælpepanelet sammen med resten af sidens emner, så man kan læse videre i sammenhængen i stedet for at lukke en boble og åbne den næste.
- **Hjælpen åbner på den side, man står på** — Hjælpepanelet åbnede på indholdsfortegnelsen. Det slår nu den aktuelle side op og viser sidens egne emner. Findes der ikke et hjælpedokument for siden, vises indholdsfortegnelsen som før.
- **Versionscheck ved opstart** — Be26 spørger nu `https://versions.build.dk/be/be26/latest-version.txt`, om der er udgivet en nyere version, og giver besked én gang pr. udgivelse. Om-siden viser din version over for den seneste, med en knap til at søge manuelt og et link til versionshistorikken. Checket kan slås fra under **Indstillinger → Opstart**. Det kører løsrevet fra opstarten og kan hverken forsinke eller afbryde den; svarer den kanoniske adresse ikke, forsøges GitHub Pages-adressen i stedet. Der sendes ingen oplysninger om bruger eller beregning - serveren ser kun IP-adressen, som ved ethvert andet websideopslag.

## Version 11.26.7.8

### Fejlrettelser i beregningsmodel (varmepumper)

- **Aftræks-varmepumper (udsugningsluft) gav for lavt elforbrug og forkert ydelse** — For en varmepumpe med kold side "Aftræk" (VpA) brugte motoren indblæsningstemperaturen (tilluft efter varmegenvinding) som fordampertemperatur i stedet for afkasttemperaturen. Det gav en for høj COP. Fordamperen bruger nu afkasttemperaturen `th − NyVgv·(th − Tu)` (som regnearket). Retter varmepumpens elforbrug og ydelse for alle Aftræk-typer (Aftræk–Rum, Aftræk–Indblæsning, Aftræk–Varmeanlæg). Eksempel Aftræk–Varmeanlæg: HP-elforbrug (Q281) 847 → 956 kWh (regneark 955,8), HP-ydelse (Q263/Q265) 2,55 → 2,13 MWh (regneark 2,13). Udeluft- og jordslange-varmepumper er uændrede.

### Fejlrettelser i resultater (elforbrug fordelt på brugsprofil)

- **Direkte elopvarmning indgik ikke i brugsprofilens rumopvarmning** — Rækken "Rumopvarmning" (`RESULTAT!Q343`) og dermed "Bygningsdrift"/"Samlet" (Q355/Q357/Q360/Q362) manglede den direkte elopvarmning, selvom den indgår i det samlede elbehov. Beregningen fulgte en ældre formel; det aktuelle regneark medregner den. Bidraget er nu tilføjet. Eksempel: rumopvarmning i profil (Q343) 1026 → 1149 kWh (regneark 1149,3).
- **Varmepumpens standby-el vises nu med 2 decimaler** — Rækkerne "Elbehov, stb. rumopvarmning"/"Elbehov, stb. VBV" (Q282/Q284) blev afrundet til hele kWh (17,52 → 18) og afveg dermed fra regnearket. De vises nu med 2 decimaler som regnearket. (Den beregnede værdi var korrekt; kun visningen ændres.)

### Nyheder

- **Ny energiramme: Renovering nulemissionsklasse** — Bygningsreglementet har en energiramme for eksisterende bygninger til nulemissionsniveau, som ligger mellem Renoveringsklasse 2 og Renoveringsklasse 1 (mere lempelig end klasse 1). SBST har gjort opmærksom på den i deres kommentarer til SBi-anvisning 213. Den er nu tilføjet under **Forudsætninger** (Konstanter/energirammer) og i **Nøgletal** på resultatsiden. Basis/areal: Bolig 63,0 / 2000, Andet 85,5 / 2000 (kWh pr. m²·år). For referencebygningen "Speciel bygning til Metodebeskrivelse" giver det 96,6 kWh/m²·år (svarer til regnearkets RESULTAT!R15).

## Version 11.26.6.9

### Fejlrettelser i beregningsmodel (erhverv / delvist benyttede bygninger)

- **Driftstid (fo) på ventilation indgik ikke** — Ventilationszoner med en driftstidsfaktor (`fo` < 1, fx en kantine der kun bruges en del af tiden) blev regnet med fuldt areal i ventilatorel, ventilationsvarmetab og kølebehov. Faktoren anvendes nu, så zonens effektive areal = areal × fo. I et kontoreksempel faldt ventilatorel fra 2347 → 1740 kWh (regneark 1748), og varmebehov/-tab matcher nu regnearket.
- **Køling fra overtemperatur var for høj (op til +71 % på erhvervsbygninger)** — Den natlige frikøling via ventilation indgik ikke i kølebehovsberegningen: nattens varmeledningsevne brugte dagluftstrømmen i stedet for natstrømmen, så natteventilationens køleeffekt reelt blev sat til nul. Beregningen bruger nu natstrømmen (`qvm_night`), hvilket svarer til regnearkets højere natledningsevne. Kontoreksempel: køling fra overtemperatur 680 → 393 kWh (regneark 396,5).
- **Specialbygning med udluftet zone: overtemperatur-køling faldt fejlagtigt til 0** — For en zone med dagudluftning men uden reel natventilation (`qvm_night` = 0) blev dagudluftningen fejlagtigt anvendt om natten, så rummet blev overkølet og kølebehovet forsvandt. Natventilationen bruger nu kun reel natstrøm; en zone uden natventilation står på grundventilation om natten. (Eksempel: overtemperatur-køling 0 → 62,3 kWh.)

### Fejlrettelser i resultater (sommerkomfort)

- **Solindfald rapporteres nu med bevægelig solafskærmning** — "Solindfald" og "Solindfald – maks." under sommerkomfort rapporteres nu med den bevægelige solafskærmning indregnet (samme værdi som selve temperatursimuleringen og regnearkets `Som_Temp`), i stedet for den geometriske værdi uden bevægelig afskærmning.

### Værktøjer

- **Sammenlign model: nyt konsistens-tjek for VBV-beholderens ladestyring** — En varmtvandsbeholder med en ladeeffekt angivet (> 0) men uden "Pumpe styret"/ladestyring markeres nu som en mulig inddata-fejl. Denne uoverensstemmelse fik kombi-pumpen til at køre hele året og overvurdere pumpe-el, men kunne ikke fanges af den almindelige celle-for-celle-sammenligning (regnearket har ingen separat "reguleret"-celle — reguleringen er underforstået af, at ladeeffekten er udfyldt).

## Version 11.26.5.28

### Be26-programmet

- Tilretning af desktop-versionen. Ingen ændringer i beregningskernen.

## Version 11.26.5.24

### Fejlrettelser i resultater

- **Elbehov pr. m² var ~13 % for høj på bygninger med opvarmet kælder** — Tabellen "Elbehov fordelt på brugsprofil (kWh/m²)" (rækkerne *Bygningsdrift*, *Andet elbehov* og *Samlet elbehov*) blev divideret med det opvarmede etageareal uden kælderbidrag. Regnearket (`RESULTAT!Q360-Q362`) deler med `Aeg = opvarmet areal + 0,5 × opvarmet kælder`. I et etagehuseksempel (Aeg = 1222,5 m²) gav det en konsekvent overskydning på ~13 %; tallene matcher nu regnearket.
- **"Energibehov, Varme" var den rene ydelse i stedet for forbrug minus udnyttet tab** — Rækken viste kedlens ydelse i stedet for kedlens forbrug minus det udnyttede tab, som regnearket bruger. Det undervurderede energibehovet i overgangsmånederne (april og oktober).

### Fejlrettelser i sommerkomfort-beregning

- **Forkert nævner i time-simuleringen af rumtemperaturen** — Den interne simulering af rumtemperaturen brugte en forkert formel, hvor rummets areal indgik i nævneren. Nævneren følger nu det aktuelle regneark. Påvirker køling fra overtemperatur og sommerkomfort-resultaterne.
- **Ventilationssetpunkt for sommerkomfort hævet fra 23 til 24 °C** — Den faste værdi for, hvornår sommerkomfort-simuleringen udlufter, er hævet fra 23 til 24 °C som i det aktuelle regneark. Den er ikke det samme som feltet "Udluftning / dagventilation".
- **Klimatabeller for skyggevirkning regenereret med fuld præcision** — Tabellerne var afrundet til 2 decimaler, hvilket gav små afvigelser i solindfald og rumtemperatur. Sommerkomfort-resultaterne matcher nu regnearket.

### Fejlrettelser i beregningsmodel

- **Pumper kørte hele året — også ved behovsstyret drift** — En kombi-pumpe (typen "Pumper behovsstyret (fx gulvvarme)") kørte konstant 8760 timer/år, så snart der var en cirkulationspumpe på en varmtvandsbeholder, uanset om beholderen var "Pumpe styret"/reguleret. Driftstiden følger nu varmebehovet og varmtvandsbeholderens regulerede pumpetimer som tilsigtet.
- **Sommer-aktiv solafskærmning gav forkert effekt i april og september** — For vinduer med negativ Fc (sommer-aktiv solafskærmning) anvendte overgangsmånederne april og september en forkert formel. Nu bruges en lineær overgang mellem vinter og sommer som i regnearket.
- **Skyggetabel for syd-sidefin var forskudt** — Tabellen for sideskygge mod syd (90°) blev læst forskudt én måned, så januar-værdien blev 0. Sydvendte vinduer med sideskygge fik derved for lavt solindfald om vinteren.

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

- **Supplerende rumvarme indgik ikke i beregningen** — Når der var sat flueben i "Suppl. el-opvarmning" eller "Brændeovne / gasstrålevarmere", blev disse bidrag slet ikke trukket fra varmebehovet før hovedforsyningen. Det er nu korrigeret, så supplementets dækning regnes med, før kedel/varmepumpe/solvarme aktiveres.
- **Arealandel for brændeovne** — Feltet for arealandel (`a_frac`) til brændeovne/gasstrålevarmere blev parset forkert; dækningsgraden virker nu som tilsigtet.
- **Kedel og fjernvarmeveksler — fuld EN 15316-model** — Kedlens og vekslerens virkningsgrad og tab beregnes nu efter den fulde CEN-model (EN 15316) med driftsperioder, standbytab og fuld-/dellast i stedet for en forenklet virkningsgrad.
- **Bygningens rotation anvendes nu på vinduer, solceller og solfangere** — Rotationen blev læst, men ikke lagt til orienteringen. Påvirker bygninger med en rotation forskellig fra 0.

## Version 11.26.5.9

### Fejlrettelser

- **Skyggetabeller** — Rettet fejl i opslag i skyggetabeller, der forhindrede beregning.

## Version 11.26.5.7

### Nyheder & Ændringer

- **Hurtigere beregning** — Beregningskernen er optimeret og kører nu hurtigere.
- **Versionsstempel på modelfiler** — Rodelementet `<BE05>` har nu en `version`-attribut, der angiver den Be26-version, der senest har gemt filen. Programmet afviser filer, der er gemt i en *nyere* version end den installerede, så ældre versioner ikke kan overskrive nyere modelfiler og dermed risikere at miste felter. Ældre filer (inklusive filer uden attributten, fx fra Be18) åbnes som hidtil.

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
- **`lang`-parameteren** — I den C#-baserede kerne styrer `lang` kun sproget i diagnostikmeddelelser (bit 0: 0 = dansk, 1 = engelsk). Resultat-XML skrives altid på dansk med komma som decimaltegn.

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
  - **E001** — Modelfil indeholder det forældede felt `cooling_frac` (bygningsniveaufraktion). Filen skal migreres; auto-migrering sker automatisk for de simple tilfælde (`cooling_frac = 0` eller `= 1` med én zone). Reglen er lempet i 11.26.9.28. Se `DiagnosticsAndBreakingChanges.html` for de gældende regler.
  - **E002** — Klimafil (`.dip`) mangler ved kørsel. `Be06Keys` og `Be06Res` blokerede tidligere ikke beregningen og returnerede stille forkerte resultater; dette er nu rettet.
- **Diagnostik** — `Be06Keys` og `Be06Res` returnerer nu en `<diagnostics>`-blok i output-XML, når kernen afviser modellen. Ved fejl returneres ingen resultattabeller — kun `<diagnostics>`-blokken. Se `DiagnosticsAndBreakingChanges.html` for XML-struktur og C#-eksempel på parsing.
- **Ændring af resultattabeller** — Tabellen `electricfactors` er omdøbt til `electricwithoutfactors`; ny tabel `electricwithfactors` er tilføjet. Rækkefølgen af outputtabeller er justeret.
- **Tabeller udskrives altid** — Alle 35 resultattabeller skrives nu i hver beregning, også når relevante data mangler (VE/batteri, sommertemperatur, brændsel, Ae=0). Fraværende data giver nul-værdier i rækkerne i stedet for udeladte tabeller, så strukturen er identisk på tværs af modeller. Dette gendanner adfærden fra ældre versioner.
- **Tilretning af nøgletalsberegning** — Fejl i beregnede nøgletalsværdier er rettet; tabellernes rækkefølge og layout er opdateret.
- **VE-allokering** — VE-produktion allokeres nu korrekt: først direkte til bygningens forbrug, derefter til øvrige forbrug, til sidst eksport til nettet.
- **Udvidet `lang`-parameter** — `lang`-parameteren i `Be06Keys` og `Be06Res` er nu et bit-flag, der tvinger outputsprog uanset hvad modelfilen angiver (gælder kun den tidligere C++-kerne; se 11.26.4.29):
  - `lang = 0` — Dansk (standard)
  - `lang = 1` — Engelsk
  - `lang = 2` — Dansk + tving dansk talformat (komma som decimaltegn)
  - `lang = 3` — Engelsk + tving engelsk talformat (punktum som decimaltegn)

  Bit 0 styrer sprog; bit 1 tvinger talformatet. Fejlmeddelelser i diagnostik-XML følger ligeledes det valgte sprog.

### Nye features

- **ID på tabeller og rækker** — Alle resultat- og nøgletalstabeller samt deres rækker har nu unikke ID'er i XML-outputtet, hvilket gør det nemmere at slå specifikke værdier op programmatisk.
- **ID-oversigter** — To nye HTML-referencefiler er inkluderet:
  - `Be26_key-id_oversigt.html` — oversigt over alle nøgletals-ID'er
  - `Be26_resultat-id_oversigt.html` — oversigt over alle resultat-ID'er
- **ID ved mouseover i HTML** — I HTML-rapportoutputtet vises tabel- og række-ID'er som tooltip ved mouseover, så det er let at finde det rigtige ID til brug i integrationer.
- **C# eksempel** — `RunCoreExample.cs` er inkluderet som eksempel på kald af `Be26Eng.dll` fra C# (.NET).
