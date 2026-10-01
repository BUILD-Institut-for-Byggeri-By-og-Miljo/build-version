# BSim — Ændringslog

## Version 8.26.10.1

Første udgivelse siden videreudviklingen af BSim blev genoptaget i 2026. Ændringerne er i forhold til version 7.23.5.31. Punkter markeret **(resultater)** kan give andre simuleringsresultater end tidligere versioner for de samme modeller.

### Brugerflade

- **Nyt programikon og ny About-dialog** — BSim- og BUILD-logo og opdaterede kontaktoplysninger: support bsim-support@build.aau.dk, debat sbi-bsim@lists.aau.dk.
- **Nye ikoner** — Værktøjslinje, modeltræ, menuer og titellinjen på dialoger og egenskabsvinduer bruger et ensartet ikonsæt. Brugerfladen tegnes skarpt på skærme med høj opløsning, også når skærmene har forskellig skalering.
- **Værktøjslinjen** — *Clean model* og *Export to Radiance* er tilføjet. *Print*, *Print preview*, *Model documentation*, *ModelList*, *Export to Be10* og *Beat Tool* er taget af værktøjslinjen og findes fortsat i menuerne.
- **Udgåede menupunkter** — *Beat*, *BSimBatch* og *Edit / Register* er fjernet fra menuerne. Beat og BSimBatch ligger fortsat i installationsmappen.
- **Help-menuen** — *BUILD Home Page* åbner build.aau.dk, *BSim Home Page* åbner www.bsim.dk, *Mail to BSim* skriver til support, og *Subscribe to BSim* åbner tilmeldingssiden for mailinglisten. *Check for new update* henter versionsoplysningerne herfra.
- **Ny hjælp** — Windows-hjælpen (CHM) er erstattet af et nyt hjælpevindue, der viser hjælpebogen lokalt. F1 virker som før. Er hjælpen ikke installeret, åbnes onlinehjælpen på help.bsim.dk. Nyt menupunkt *Online Help*.
- **Opstart** — Splash-skærmen vises ikke længere. Nye modeller gemmes som standard i mappen Dokumenter.

### Licens og installation

- **Én licens** — Enhver gyldig BSim-licens giver adgang til alle funktioner, også fugtmodel, PV, naturlig ventilation og Glazing-ex. Allerede udstedte licenskoder virker uændret.
- **Licensdialog** — Licensen registreres og fjernes i programmet under *Help / License...*, som også viser de installerede licenser og deres udløbsdato. Udløbsdatoen fornyes automatisk online.
- **Uden licens** — BSim starter, men kun Help-menuen kan bruges. Menuerne låses op, så snart en licens registreres, og databasevinduet lukkes, når licensen fjernes.
- **Nyt installationsprogram** — BSim installeres i `Program Files (x86)\BUILD\BSim`, og programfilen hedder nu BSim.exe.

### Database

- **Ingen gæt på databasen** — Findes projektets database ikke på den registrerede sti, bliver brugeren bedt om at vælge en. Tidligere tog BSim uden videre en database med samme navn ved siden af modellen.
- **Rettet databaseopslag** — Opslaget afhang af, hvordan modelfilen blev åbnet.

### Model og beregning

- **Personbelastning** — *PeopleLoad* har fået felter for tør og latent varme og for CO₂, og standardværdierne for personer er opdateret.
- **Solabsorptans på finish-materialer (resultater)** — En brugerdefineret solabsorptans på finish-materialer blev ignoreret og bruges nu.
- **Langbølget stråling (resultater)** — Med *Longwave* slået til tager view factors nu højde for flader, der skygger for hinanden, også i konkave rum og mellem rum forbundet af åbninger.
- **Kortbølget fordeling i zonen (resultater)** — Solen fordeles i to trin. Første træf er solpletten fra XSun (uden XSun faste andele pr. fladetype), og den diffuse sol fordeles efter view factors. Derefter fordeles refleksionerne mellem alle flader. Sol gennem indvendige vinduer og huller går videre til zonen på den anden side.
- **Tab gennem vinduer (resultater)** — Det solindfald, der tabes ud gennem vinduerne (*Lost*), beregnes nu i stedet for at være en fast andel på 10 %. Feltet *Lost* er fjernet, *ToAir* er 0 i nye zoner, og feltet *Fraction* (diffus sol gennem indvendige vinduer) er fjernet.
- **A-kurver** — A-kurver for ruder angivet i procent omregnes automatisk. En ugyldig A-kurve regnes som manglende med en advarsel på vinduet.
- **XSun** — Ændring af *Floor* i den termiske zone opdaterer nu solfordelingen, og fordelingen *ToFloor* gemmes korrekt.
- **SimLight (resultater)** — Beregningen af reflekteret lys mellem flader er rettet, og dagslysfaktorer overføres til vinduer med fuld præcision.

### Vejrdata

- **Decimaler** — Konvertering fra EPW/ASHRAE og tekst mister ikke længere decimaler afhængigt af Windows' sprogindstillinger.
- **Skudår og manglende dage** — Skudår sættes automatisk ved konvertering, og manglende vejrdage rapporteres.

### Nyt

- **BSimCLI** — Kommandolinjeprogram, der kører simuleringer af .disxml-modeller uden brugerflade.
