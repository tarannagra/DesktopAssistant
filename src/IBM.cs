/* 
    MIT License

    Copyright (c) 2023 Taran Nagra

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
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


public class Format {
    public string? text {get; set;}
}

class IBM {
    // TOML is easier than JSON for C#
    private static string ibm_key(string? version) {
        // Loads the IBM api_key from the conf.toml file.
        using (StreamReader reader = File.OpenText("conf.toml")) {
            TomlTable tomlTable = TOML.Parse(reader);
            if (version == "tts") {
                return tomlTable["tts"]["api_key"];
            } else if (version == "stt") {
                return tomlTable["stt"]["api_key"];
            } else {
                return "Could not get key!";
            }
        }
    }
    private static string ibm_base_url(string? version) {
        using (StreamReader reader = File.OpenText("conf.toml")) {
            TomlTable tomlTable = TOML.Parse(reader);
            if (version == "tts") {
                return tomlTable["tts"]["base_url"];
            } else if (version == "stt") {
                return tomlTable["stt"]["base_url"];
            } else {
                return "Could not get base url!";
            }
        }
    }
    private static string tts_api_key = ibm_key("tts");
    private static string tts_base_url = ibm_base_url("tts");
    private static string stt_api_key = ibm_key("stt");
    private static string stt_base_url = ibm_base_url("stt");
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
            RequestUri = new Uri(uriString: $"{tts_base_url}/v1/synthesize?voice={voice}"),
            Method = HttpMethod.Post
        };
        request.Headers.Add(name: "Accept", value: "audio/wav");
        request.Headers.Add(name: "Authorization", "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"apikey:{tts_api_key}")));
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
            RequestUri = new Uri(uriString: $"{stt_base_url}/v1/recognize"),
            Method = HttpMethod.Post
        };
        request.Headers.Add(name: "Authorization", "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"apikey:{stt_api_key}")));
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