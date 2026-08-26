# build-versions

Permanente, login-frie endpoints til versionscheck i BUILD's software.

Hostes på GitHub Pages bag eget domæne: **https://versions.build.dk**

## Endpoints

| Fil | URL |
|---|---|
| Be26 – seneste version | `https://versions.build.dk/be/be26/latest-version.txt` |
| Be26 – versionshistorik | `https://versions.build.dk/be/be26/versionhistory.md` |

Struktur er `/<familie>/<produkt>/<fil>`, så Be18 senere kan lægges i
`/be/be18/` og BSIM i `/bsim/` uden at Be26's URL ændrer sig.

Det er `versions.build.dk`-adressen — ikke `*.github.io` — der skal hardkodes i
Be26. Så kan hostingen flyttes væk fra GitHub senere uden at udsende en ny klient.

## Regler for filerne

- `latest-version.txt` indeholder **kun** versionsnummeret på én linje, f.eks. `11.26.8.26`.
  Be26 bruger firetalsformen `major.år.måned.dag`, og klienten sammenligner med
  `Version.TryParse`, så formen skal matche assembly-versionen.
  Ingen BOM, ingen præfiks som "v". Afsluttende linjeskift er tilladt — klienten skal `trim()`.
- `versionhistory.md` er ren Markdown/tekst, nyeste version øverst.

## Opdatering ved release

```bash
git clone https://github.com/<ORG>/build-versions.git
cd build-versions
echo "11.26.8.27" > be/be26/latest-version.txt
$EDITOR be/be26/versionhistory.md
git commit -am "Be26 11.26.8.27"
git push
```

Live ca. 30–60 sekunder efter push. Kan også gøres helt uden git: rediger filen
direkte i GitHub-webfladen → Commit.

## Opsætning (engangs)

1. Opret repo `build-versions` i BUILD's GitHub-organisation, **public**.
   (GitHub Pages kræver public repo på Free-planen. Se "Om at det er offentligt" nedenfor.)
2. Push indholdet af denne mappe. Filen `CNAME` med `versions.build.dk` skal med.
3. Opret DNS-record hos den, der administrerer `build.dk`:

   | Type | Navn | Værdi | TTL |
   |---|---|---|---|
   | CNAME | `versions` | `<ORG>.github.io.` | 3600 |

   `<ORG>` er GitHub-organisationens navn i små bogstaver. Bemærk det afsluttende
   punktum — nogle DNS-udbydere kræver det, andre tilføjer det selv.

4. Settings → Pages → Source: `Deploy from a branch`, branch `main`, mappe `/ (root)`.
   Custom domain skulle allerede stå udfyldt fra `CNAME`-filen.
5. Vent til GitHub har udstedt certifikatet (typisk 5–20 min. efter DNS er slået
   igennem), sæt så flueben i **Enforce HTTPS**.
6. Verificér:

   ```bash
   curl -i https://versions.build.dk/be/be26/latest-version.txt
   ```

### Hvis DNS ikke er slået igennem endnu

Indtil da svarer `https://<ORG>.github.io/build-versions/be/be26/latest-version.txt`.
Peter kan bygge og teste mod den, men bør skifte til `versions.build.dk` inden release.

## Om at det er offentligt

Indholdet — et versionsnummer og en changelog — er ikke følsomt, og endpointet
skal netop kunne læses uden login. Tre ting at holde øje med alligevel:

- **Repoet er public**, så hele commit-historikken er det også. Læg intet andet i
  dette repo end versionsfilerne. Ingen nøgler, ingen interne noter.
- **Changeloggen læses af alle**, også før en version er annonceret. Undgå
  formuleringer, der beskriver en sikkerhedsfejl, som brugerne endnu ikke har
  fået patchet — beskriv rettelsen, ikke angrebet.
- **GitHub ser klienternes IP-adresser**, når Be26 kalder endpointet. Det er en
  almindelig HTTP-request uden telemetri fra jeres side, men det bør stå i
  Be26's privatlivstekst, at programmet kontakter en server ved opstart.

## Filen `.nojekyll`

Skal blive liggende. Uden den kører GitHub Pages filerne gennem Jekyll, som kan
omskrive eller udelade filer.

## Klient-side

`be26-versioncheck.cs` er et eksempel på C#-implementering med korrekt
cache-håndtering og timeout. GitHub Pages sætter `Cache-Control: max-age=600`,
så uden cache-busting kan klienten se op til 10 minutter gammelt indhold.
