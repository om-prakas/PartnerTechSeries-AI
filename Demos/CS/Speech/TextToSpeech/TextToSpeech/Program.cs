using System;
using HeroSolutions.AI.Demo.Speech;

namespace TextToSpeech
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Language Translation");
            TextToSpeechSample.SynthesisToSpeakerAsync
                ().Wait();
        }
    }
}
