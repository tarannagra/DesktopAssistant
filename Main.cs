/* 
    Desktop Assistant by Taran Nagra
    This project is protected by the MIT Licence
*/

// Everyone got these
using System;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

// My own libraries
using Request;

namespace DesktopAssistant {
    class DesktopAssistant {
        async static Task Main(string[] args) {
            await Request.Request.text_to_speech(content: "lets go to bowling");
            
        }
    }
}