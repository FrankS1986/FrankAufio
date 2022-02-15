using System;
using System.Globalization;
using System.IO;
using System.Speech.AudioFormat;
using System.Speech.Recognition;
using System.Threading;

namespace audiointext
{
    class Program
    {
       
        static void Main(string[] args)
        {
            string str = Paths.GetFilePath(@"test.txt");
            using (StreamWriter sw = new StreamWriter(str))
            {
                sw.WriteLine(DateTime.Now);
            }
            using (
                     SpeechRecognitionEngine recognizer =
                       new SpeechRecognitionEngine(
                         new System.Globalization.CultureInfo("de"))) // de-DE   en-GB geht nur wenn auf Computer Paket instaliert ist
            {

                // Create and load a dictation grammar.  
                recognizer.LoadGrammar(new DictationGrammar());
                
                // Add a handler for the speech recognized event.  
                recognizer.SpeechRecognized +=
                  new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);
                recognizer.SetInputToAudioStream( File.OpenRead (Paths.GetFilePath(@"1.wav")),new SpeechAudioFormatInfo (44100, AudioBitsPerSample.Sixteen, AudioChannel.Stereo) );

                //recognizer.SetInputToDefaultAudioDevice();

                // Start asynchronous, continuous speech recognition.  
                recognizer.RecognizeAsync(RecognizeMode.Multiple);

                // Keep the console window open.  
                while (true)
                {
                    
                    Console.ReadLine();
                }
            }
        }

            static void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
            {
                string str = Paths.GetFilePath(@"test.txt");

                Console.WriteLine("Neue zeile: " + e.Result.Text);
            

                using (StreamWriter sw = new StreamWriter(str,true))
                {

                
                    sw.WriteLine("Neue zeile: " + e.Result.Text);
                }

                
            }


        
        

       


    }
}
