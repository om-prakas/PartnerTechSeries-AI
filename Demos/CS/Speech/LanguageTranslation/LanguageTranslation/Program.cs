using System;
using HeroSolutions.AI.Demo.Speech;

namespace LanguageTranslation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Language Translation");
            TranslationSamples.TranslationWithMicrophoneAsync().Wait();
        }
    }
}
