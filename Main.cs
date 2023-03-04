/* 
    Desktop Assistant by Taran Nagra
    This project is protected by the MIT Licence
*/

// Is it just me, or do you like having the `using` statements in ascending order?

// Everyone got these
using System;
using System.Threading.Tasks;

// My own libraries
using IBM;
using Design;

namespace DesktopAssistant {
    class DesktopAssistant {
        async static Task Main(string[] args) {
            Design.Design.welcome_screen();
            await IBM.IBM.text_to_speech(content: "Testing from IBM once again.");
            // await Request.Request.text_to_speech(content: "This is an awesome tool made by the awesome person!");
            
        }
    }
}