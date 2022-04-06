using BeatmapInformation.SimpleJsons;
using IPA.Loader;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BeatmapInformation.WebClients
{
    internal class WebResponse
    {
        public readonly HttpStatusCode StatusCode;
        public readonly string ReasonPhrase;
        public readonly HttpResponseHeaders Headers;
        public readonly HttpRequestMessage RequestMessage;
        public readonly bool IsSuccessStatusCode;

        private readonly byte[] _content;

        internal WebResponse(HttpResponseMessage resp, byte[] body)
        {
            this.StatusCode = resp.StatusCode;
            this.ReasonPhrase = resp.ReasonPhrase;
            this.Headers = resp.Headers;
            this.RequestMessage = resp.RequestMessage;
            this.IsSuccessStatusCode = resp.IsSuccessStatusCode;

            this._content = body;
        }

        public byte[] ContentToBytes()
        {
            return this._content;
        }

        public string ContentToString()
        {
            return Encoding.UTF8.GetString(this._content);
        }

        public JSONNode ConvertToJsonNode()
        {
            return JSONNode.Parse(this.ContentToString());
        }
    }

    internal static class WebClient
    {
        private static HttpClient _client;
        private static HttpClient Client
        {
            get
            {
                if (_client == null) {
                    Connect();
                }

                return _client;
            }
        }

        private static readonly int RETRY_COUNT = 5;

        private static void Connect()
        {
            try {
                _client?.Dispose();
            }
            catch (Exception e) {
                Logger.Debug($"{e}");
            }
            var meta = PluginManager.GetPlugin("BeatmapInformation");
            _client = new HttpClient()
            {
                Timeout = new TimeSpan(0, 0, 15)
            };
            _client.DefaultRequestHeaders.UserAgent.TryParseAdd($"{meta.Name}/{meta.HVersion}");
        }

        internal static async Task<WebResponse> GetAsync(string url, CancellationToken token)
        {
            try {
                return await SendAsync(HttpMethod.Get, url, token);
            }
            catch (Exception e) {
                Logger.Debug($"{e}");
                return null;
            }
        }

        internal static async Task<byte[]> DownloadImage(string url, CancellationToken token)
        {
            try {
                var response = await SendAsync(HttpMethod.Get, url, token);
                if (response?.IsSuccessStatusCode == true) {
                    return response.ContentToBytes();
                }
                return null;
            }
            catch (Exception e) {
                Logger.Debug($"{e}");
                return null;
            }
        }

        internal static async Task<byte[]> DownloadSong(string url, CancellationToken token, IProgress<double> progress = null)
        {
            // check if beatsaver url needs to be pre-pended
            try {
                var response = await SendAsync(HttpMethod.Get, url, token, progress: progress);

                if (response?.IsSuccessStatusCode == true) {
                    return response.ContentToBytes();
                }
                return null;
            }
            catch (Exception e) {
                Logger.Debug($"{e}");
                return null;
            }
        }

        internal static async Task<WebResponse> SendAsync(HttpMethod methodType, string url, CancellationToken token, IProgress<double> progress = null)
        {
            Logger.Debug($"{methodType.ToString()}: {url}");

            // send request
            try {
                HttpResponseMessage resp = null;
                var retryCount = 0;
                do {
                    try {
                        // create new request messsage
                        var req = new HttpRequestMessage(methodType, url);
                        if (retryCount != 0) {
                            await Task.Delay(1000);
                        }
                        retryCount++;
                        resp = await Client.SendAsync(req, HttpCompletionOption.ResponseHeadersRead, token).ConfigureAwait(false);
                        Logger.Debug($"resp code : {resp.StatusCode}");
                    }
                    catch (Exception e) {
                        Logger.Debug($"Error : {e}");
                        Logger.Debug($"{resp?.StatusCode}");
                    }
                } while (resp?.StatusCode != HttpStatusCode.NotFound && resp?.IsSuccessStatusCode != true && retryCount <= RETRY_COUNT);


                if (token.IsCancellationRequested) {
                    throw new TaskCanceledException();
                }

                using (var memoryStream = new MemoryStream())
                using (var stream = await resp.Content.ReadAsStreamAsync().ConfigureAwait(false)) {
                    var buffer = new byte[8192];
                    var bytesRead = 0; ;

                    var contentLength = resp?.Content.Headers.ContentLength;
                    var totalRead = 0;

                    // send report
                    progress?.Report(0);

                    while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false)) > 0) {
                        if (token.IsCancellationRequested) {
                            throw new TaskCanceledException();
                        }

                        if (contentLength != null) {
                            progress?.Report(totalRead / (double)contentLength);
                        }

                        await memoryStream.WriteAsync(buffer, 0, bytesRead).ConfigureAwait(false);
                        totalRead += bytesRead;
                    }

                    progress?.Report(1);
                    var bytes = memoryStream.ToArray();

                    return new WebResponse(resp, bytes);
                }
            }
            catch (Exception e) {
                Logger.Debug($"{e}");
                throw;
            }
        }
    }
}
