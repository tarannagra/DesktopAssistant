/* 
    Desktop Assistant by Taran Nagra
    This project is protected by the MIT Licence
*/

// Is it just me, or do you like having the `using` statements in ascending order?

// Everyone got these
using System;
using System.Threading.Tasks;

// My own libraries
using static IBM;
using static Design;

namespace DesktopAssistant {
    class DesktopAssistant {
        async static Task Main(string[] args) {
            welcome_screen();
            await text_to_speech(content: "I hate how Americans say my name");
            // string response = await speech_to_text();
            // Console.WriteLine(response);
        }
    }
}