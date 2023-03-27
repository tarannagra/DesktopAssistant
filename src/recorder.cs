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
using System.Threading.Tasks;
using System.Speech.Recognition;

public class Recordr {
    
    #pragma warning disable CS1998 // remove warning for "lacking await operators"
    public static async Task record() {
        
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
        sre.SetInputToDefaultAudioDevice(); // default microphone unless you wanna specify
        Choices choices = new Choices(new string[] {"Hello", "there"});
        GrammarBuilder grammarBuilder = new GrammarBuilder(choices);
        Grammar grammar = new Grammar(grammarBuilder);
        sre.LoadGrammar(grammar);
        
        #pragma warning disable CS8622
        sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recogniser_speechRecognised);
        sre.SpeechRecognitionRejected += new EventHandler<SpeechRecognitionRejectedEventArgs>(recogniser_speechRejected);

        sre.RecognizeAsync(RecognizeMode.Multiple);
        Console.WriteLine("Press any key to quit!");
        Console.ReadKey();

        sre.RecognizeAsyncStop();
        return; // nothing to return!
    }
    
    static void recogniser_speechRecognised(object sender, SpeechRecognizedEventArgs e) {
        Console.WriteLine("Recognised: {0}", e.Result.Text);
    }
    static void recogniser_speechRejected(object sender, SpeechRecognitionRejectedEventArgs e) {
        Console.WriteLine("Failed: {0}", e.Result.Text);
    }
}
