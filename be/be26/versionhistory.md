# Be26 – versionshistorik

Nyeste version står altid i `latest-version.txt`.

Versionsnumre skrives i Be26's firetalsform `major.år.måned.dag` — `11.26.8.10`
er udgivelsen 10. august 2026. Det er samme form som i `latest-version.txt`, så
overskrifterne her kan slås direkte op mod det, klienten sammenligner.

---

## 11.26.8.26 - 2026-08-26

### Fejlrettelser i brugerfladen

- **Andet elforbrug: store værdier blev skåret ned til grænsen** — Felterne "Udebelysning (dagslysstyret)" og "Særligt apparatur, i brugstiden" er absolut el-effekt i W for hele bygningen, men grænserne var sat, som om værdierne var pr. m² (0-100 og 0-200). Overskridelser blev ikke blot markeret røde: ved fokusskift blev værdien rettet ned til grænsen, så inddata reelt blev ændret i modellen. Eksempelmodellen for en administrationsbygning har 180 W udebelysning og 600 W apparatur og blev dermed skåret ned til 100 og 200 W. Grænserne er nu 0-1.000.000 W. Samtidig rettet: valideringsteksten for apparatvarme skrev "W/m²", og den engelske label for udebelysning skrev "W/m²" - begge er W.

- **Fjernvarmeveksler: forkert enhed på varmetabet** — Feltet "Varmetab fra veksler" var angivet i kW. Værdien er W/K, som i Be18. Beregningen har hele tiden brugt den som W/K (varmetabet ganges med temperaturdifferensen), og både modeldokumentet og valideringsteksten skrev W/K - kun feltets label var forkert. Gemte modeller er upåvirkede. (issue #68)

### Fejlrettelser i resultater

- **Resultatark: "Samlet dimensionerende varmetab"** — Tabellen viser bygningens dimensionerende varmetab, altså varmetabet ved de dimensionerende temperaturer, men overskriften sagde blot "Samlet varmetab, W/m²". Ordet "dimensionerende" er nu med. (issue #69)

### Nyheder

- **Versionscheck ved opstart** — Be26 spørger nu `https://versions.build.dk/be/be26/latest-version.txt`, om der er udgivet en nyere version, og giver besked én gang pr. udgivelse. Om-siden viser din version over for den seneste, med en knap til at søge manuelt og et link til versionshistorikken. Checket kan slås fra under **Indstillinger → Opstart**. Det kører løsrevet fra opstarten og kan hverken forsinke eller afbryde den; svarer den kanoniske adresse ikke, forsøges GitHub Pages-adressen i stedet. Der sendes ingen oplysninger om bruger eller beregning - serveren ser kun IP-adressen, som ved ethvert andet websideopslag.

- **Hjælpen åbner på den side, man står på** — Hjælpepanelet (F1 eller ?-knappen i værktøjslinjen) åbnede på indholdsfortegnelsen. Det slår nu den aktuelle rute op og viser sidens egne emner. Findes der ikke et hjælpedokument for siden, vises indholdsfortegnelsen som før.

## 11.26.8.10 – 2026-08-10

- Første offentlige release af Be26.
- Versionscheck mod dette endpoint aktiveret.

<!--
Skabelon for nye punkter (nyeste øverst):

## 11.ÅÅ.M.D – ÅÅÅÅ-MM-DD

- Ændring 1
- Ændring 2
-->
