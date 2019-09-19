﻿using System;
using HeroSolutions.AI.Demo.Speech;


namespace CustomSpeech
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Speech recognition using customized model");
            SpeechRecognitionSamples.SpeechContinuousRecognitionUsingCustomizedModelAsync().Wait();
        }
    }
    }
