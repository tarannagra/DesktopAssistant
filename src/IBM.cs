/*
    IBM.cs - Made by Taran Nagra
    ---
    This is the IBM library I made for
    handling requests to and from IBM.

    
*/

using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;

// TOML language support
using Tommy;

namespace IBM {

    public class Format {
        public string? text {get; set;}
    }

    class IBM {
        // TOML is easier than JSON for C#
        private static string ibm_key() {
            // Loads the IBM api_key from the conf.toml file.
            using (StreamReader reader = File.OpenText("conf.toml")) {
                TomlTable tomlTable = TOML.Parse(reader);
                return tomlTable["ibm"]["api_key"];
            }
        }
        private static string ibm_base_url() {
            using (StreamReader reader = File.OpenText("conf.toml")) {
                TomlTable tomlTable = TOML.Parse(reader);
                return tomlTable["ibm"]["base_url"];
            }
        }
        private static string ibm_apikey = ibm_key();
        private static string base_url = ibm_base_url();
        private static string voice = "en-US_MichaelV3Voice"; // change voice here if needed instead of changing in the URL 
        // a() -> a test function to see if a successful request is made.
        public async static Task<string> a() {
            var client = new HttpClient();
            var request = new HttpRequestMessage() {
                RequestUri = new Uri(uriString: "https://httpbin.org/headers"),
                Method = HttpMethod.Get
            };
            request.Headers.Add(name: "Awesome", value:"Header");
            var task = await client.SendAsync(request);
            string result = await task.Content.ReadAsStringAsync();
            return result;
        }

        public async static Task<byte[]> text_to_speech(string content) {
            var Format = new Format {
                text = content
            };
            var client = new HttpClient();
            var request = new HttpRequestMessage() {
                RequestUri = new Uri(uriString: $"{base_url}/v1/synthesize?voice={voice}"),
                Method = HttpMethod.Post
            };
            request.Headers.Add(name: "Accept", value: "audio/wav");
            request.Headers.Add(name: "Authorization", "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"apikey:{ibm_apikey}")));
            request.Content = new StringContent(JsonSerializer.Serialize(Format));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var task = await client.SendAsync(request);
            task.EnsureSuccessStatusCode();
            byte[] response = await task.Content.ReadAsByteArrayAsync();
            await make_file(filename: "audio.wav", contents: response);
            return response; // optional response if you would like it
        }

        public async static Task<string> speech_to_text() {
            var client = new HttpClient();
            var request = new HttpRequestMessage() {
                RequestUri = new Uri(uriString: $"{base_url}/v1/recognize"),
                Method = HttpMethod.Post
            };
            request.Headers.Add(name: "Authorization", "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"apikey:{ibm_apikey}")));
            request.Content = new StringContent(File.ReadAllText("recording.flac").Replace("\n", string.Empty).Replace("\r", string.Empty));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("audio/flac");

            HttpResponseMessage response = await client.SendAsync(request: request);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }

        private async static Task make_file(string filename, byte[] contents) {
            #pragma warning disable CS8604
            // potential nullable value and want to remove that 
            await File.WriteAllBytesAsync(filename, contents);
        }

        // public async static Task<string> speech_to_text() {
        //     var client = new HttpClient();
        //     var request = new HttpRequestMessage() {
        //         RequestUri = new Uri(uriString: "https://api.eu-gb.speech-to-text.watson.cloud.ibm.com/instances/88951a8e-dee9-4fc4-8106-89839f091273/v1/recognize"),
        //         Method = HttpMethod.Get
        //     };
        //     request.Headers.Add(name: "Content-Type", value: "audio/flac");
        //     return ";a";
        // }
    }
}