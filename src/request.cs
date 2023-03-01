using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace Request {
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

        public async static Task<string> text_to_speech() {
            var client = new HttpClient();
            var request = new HttpRequestMessage() {
                RequestUri = new Uri(uriString: "https://api.eu-gb.text-to-speech.watson.cloud.ibm.com/instances/cd85e365-9c22-40f7-ab9f-ca89f47a6491/v1/synthesize?voice=en-US_MichaelV3Voice"),
                Method = HttpMethod.Post
            };
            request.Headers.Add(name: "Accept", value: "audio/wav");
            request.Headers.Add(name: "Authorization", "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"apikey:{ibm_apikey}")));
            request.Content = new StringContent("{\"text\":\"hello world\"}");
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var task = await client.SendAsync(request);
            task.EnsureSuccessStatusCode();
            string response = await task.Content.ReadAsStringAsync();
            // write response to a wav file and return result.
            return response;
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