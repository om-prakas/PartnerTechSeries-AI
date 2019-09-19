using System;
using HeroSolutions.AI.Demo.Speech;

namespace TextBasedAssistant
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Text Based Assistant");
            IntentRecognitionSamples.TextBasedAssistant().Wait();
        }
    }
}
