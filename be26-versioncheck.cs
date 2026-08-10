// Eksempel på klient-side versionscheck til Be26.
// Afhænger kun af System.Net.Http.
//
// Bemærk: GitHub Pages sætter Cache-Control: max-age=600, og .NET/WinINET kan
// cache oveni. Derfor både no-cache-header og cache-busting query-parameter.

using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Be26.Updates
{
    public sealed class VersionCheckResult
    {
        public bool Succeeded { get; set; }
        public Version LatestVersion { get; set; }
        public bool UpdateAvailable { get; set; }
        public string Error { get; set; }
    }

    public static class VersionCheck
    {
        // Den permanente adresse. Peg altid på versions.build.dk — aldrig på
        // *.github.io — så hostingen kan flyttes uden at udsende en ny klient.
        private const string BaseUrl = "https://versions.build.dk/be/be26/";
        private const string LatestVersionFile = "latest-version.txt";
        public const string VersionHistoryUrl = BaseUrl + "versionhistory.md";

        private static readonly HttpClient Client = CreateClient();

        private static HttpClient CreateClient()
        {
            var handler = new HttpClientHandler
            {
                AllowAutoRedirect = true
            };

            var client = new HttpClient(handler)
            {
                Timeout = TimeSpan.FromSeconds(8)
            };

            client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
            {
                NoCache = true,
                NoStore = true
            };
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Be26-VersionCheck");

            return client;
        }

        /// <summary>
        /// Henter seneste version. Fejler aldrig med exception — returnerer et resultat.
        /// Versionschecket må ikke kunne blokere opstart af programmet.
        /// </summary>
        public static async Task<VersionCheckResult> CheckAsync(
            Version currentVersion,
            CancellationToken cancellationToken = default)
        {
            var result = new VersionCheckResult();

            try
            {
                // Cache-busting: uden dette kan et proxy- eller CDN-lag levere
                // op til 10 minutter gammelt indhold efter en release.
                var url = BaseUrl + LatestVersionFile
                          + "?t=" + DateTime.UtcNow.Ticks.ToString();

                using (var response = await Client.GetAsync(url, cancellationToken)
                                                 .ConfigureAwait(false))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        result.Error = "HTTP " + (int)response.StatusCode;
                        return result;
                    }

                    var raw = await response.Content.ReadAsStringAsync()
                                            .ConfigureAwait(false);

                    // Fjern BOM, whitespace og et evt. "v"-præfiks.
                    var text = raw.Trim().TrimStart('﻿').TrimStart('v', 'V').Trim();

                    Version latest;
                    if (!Version.TryParse(text, out latest))
                    {
                        result.Error = "Kunne ikke fortolke versionsnummer: '" + text + "'";
                        return result;
                    }

                    result.Succeeded = true;
                    result.LatestVersion = latest;
                    result.UpdateAvailable = latest > currentVersion;
                    return result;
                }
            }
            catch (TaskCanceledException)
            {
                result.Error = "Timeout";
                return result;
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
                return result;
            }
        }
    }
}
