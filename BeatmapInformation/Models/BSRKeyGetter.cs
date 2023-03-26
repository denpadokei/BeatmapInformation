using BeatmapInformation.SimpleJsons;
using BeatmapInformation.WebClients;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BeatmapInformation.Models
{
    public static class BSRKeyGetter
    {
        public static readonly ConcurrentDictionary<string, string> s_hashKeyPairs = new ConcurrentDictionary<string, string>();
        public static readonly SemaphoreSlim s_semaphoreSlim = new SemaphoreSlim(1, 1);
        public static async Task<string> GetBSRKey(string hash, CancellationToken token)
        {
            await s_semaphoreSlim.WaitAsync();
            try {
                if (s_hashKeyPairs.TryGetValue(hash, out var bsrKey)) {
                    return bsrKey;
                }
                var beatmap = await WebClient.GetAsync($"https://api.beatsaver.com/maps/hash/{hash.ToLower()}", token);
                var jsonText = beatmap?.ContentToString();
                if (!string.IsNullOrEmpty(jsonText)) {
                    var json = JSON.Parse(jsonText);
                    bsrKey = json["id"].Value;
                    s_hashKeyPairs.TryAdd(hash, bsrKey);
                    return bsrKey;
                }
            }
            catch {
            }
            finally {
                s_semaphoreSlim.Release();
            }
            return "";
        }
    }
}
