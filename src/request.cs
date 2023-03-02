using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;

namespace Request {

    public class Format {
        public string? text {get; set;}
    }

    class Request {
        private static string ibm_apikey = "ueTqlcYQ4DXz6H1WaXkk1Z7Os6ge8XDt3y8b2iiOsAZ0";
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
                RequestUri = new Uri(uriString: "https://api.eu-gb.text-to-speech.watson.cloud.ibm.com/instances/cd85e365-9c22-40f7-ab9f-ca89f47a6491/v1/synthesize?voice=en-US_MichaelV3Voice"),
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
            return response;
        }

        public static void test() {
            string content = "aaaaaa";
            var Format = new Format {
                text = content
            };
            Console.WriteLine(JsonSerializer.Serialize(Format));

        }

        private async static Task make_file(string? filename, byte[]? contents) {
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