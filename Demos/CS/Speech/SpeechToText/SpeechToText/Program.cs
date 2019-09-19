using System;
using HeroSolutions.AI.Demo.Speech;

namespace SpeechToText
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Speech To Text");
            SpeechRecognitionSamples.SpeechContinuousRecognitionUsingCustomizedModelAsync().Wait();
        }
    }
}
