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
