namespace ImageCaption.Services
{
    using System.IO;
    using System.Threading.Tasks;
    using System.Web.Configuration;
    using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
    using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
    using System.Collections.Generic;
   

    
    public class MicrosoftCognitiveCaptionService : ICaptionService
    {
        /// <summary>
        /// Microsoft Computer Vision API key.
        /// </summary>
        private static readonly string subscriptionKey = WebConfigurationManager.AppSettings["MicrosoftVisionApikey"];

        /// <summary>
        /// Microsoft Computer Vision API Endpoint.
        /// </summary>
                        private static readonly List<VisualFeatureTypes> features =
                     new List<VisualFeatureTypes>()
                 {
                            VisualFeatureTypes.Categories, VisualFeatureTypes.Description,
                            VisualFeatureTypes.Faces, VisualFeatureTypes.ImageType,
                            VisualFeatureTypes.Tags
                 };

        
        public async Task<string> GetCaptionAsync(string url)
        {
            ComputerVisionClient computerVision = new ComputerVisionClient(
                new ApiKeyServiceClientCredentials(subscriptionKey),
                new System.Net.Http.DelegatingHandler[] { });
            computerVision.Endpoint = "https://westus2.api.cognitive.microsoft.com/";

            ImageAnalysis analysis =
                await computerVision.AnalyzeImageAsync(url, features);

           
            return ProcessAnalysisResult(analysis);
        }

   
        public async Task<string> GetCaptionAsync(Stream stream)
        {
            ComputerVisionClient computerVision = new ComputerVisionClient(
                new ApiKeyServiceClientCredentials(subscriptionKey),
                new System.Net.Http.DelegatingHandler[] { });
            computerVision.Endpoint = WebConfigurationManager.AppSettings["MicrosoftVisionApiEndpoint"];

            ImageAnalysis analysis = await computerVision.AnalyzeImageInStreamAsync(
                    stream, features);
          
            return ProcessAnalysisResult(analysis);
        }

       
        private static string ProcessAnalysisResult(ImageAnalysis analysis)
        {

            string tags="";
            string describe="";
            string categories="";
            string comma = ",";

            if (analysis.Description.Captions.Count != 0) {
                describe = analysis.Description.Captions[0].Text;
            }

            for (int i = 0; i < analysis.Tags.Count; i++)
            {
                tags = tags + analysis.Tags[i].Name+comma;
                if (i == analysis.Tags.Count - 2) {
                    comma = "";
                }
            }
            if (analysis.Categories.Count != 0) {
                categories = analysis.Categories[0].Name;
            }
            


            string message = " "+describe + "\n" +"Tags : "+ tags + "\n" +"Categories : "+ categories; 
          

            return string.IsNullOrEmpty(message) ?
                        "Couldn't find a Details for this one" :
                        "I think it's " + message;
        }
    }
}