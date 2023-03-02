/* 
    Desktop Assistant by Taran Nagra
    This project is protected by the MIT Licence
*/

// Everyone got these
using System;
using System.Threading.Tasks;

// My own libraries
using Design;
using Request;

namespace DesktopAssistant {
    class DesktopAssistant {
        async static Task Main(string[] args) {
            Design.Design.welcome_screen();
            // await Request.Request.text_to_speech(content: "");
            
        }
    }
}