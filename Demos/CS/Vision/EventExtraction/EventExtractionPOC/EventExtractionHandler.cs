﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
namespace HeroSolutions
{
    namespace AI
    {
        namespace Demo
        {
            public class EventExtractionHandler
            {
                private string ProjectId = ConfigurationManager.AppSettings["ProjectId"], Endpoint = ConfigurationManager.AppSettings["EndPoint"], TrainingKey = ConfigurationManager.AppSettings["TrainingKey"];
                public string error = "";
                public string TagName = "No Problem";
                public object JsonResponse = "";

                public void EventExtraction(string base64data)
                { 
                    try
                    {
                        var imagebytes = Convert.FromBase64String(base64data);
                        var client = new RestClient(Endpoint + "/customvision/v2.2/Training/projects/"+ ProjectId + "/quicktest/image");
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("Content-Type", "application/octet-stream");
                        request.AddHeader("training-key", TrainingKey);
                        request.AddParameter("data", imagebytes, ParameterType.RequestBody);
                        IRestResponse response = client.Execute(request);

                        var result = response.Content;
                        JsonResponse = result;

                        dynamic res_obj = JObject.Parse(result);
                        var res_pred = res_obj.predictions.ToString();
                        JArray res_array = JArray.Parse(res_pred);

                        for (int i = 0; i < res_array.Count; i++)
                        {

                            dynamic pred = JObject.Parse(res_array[i].ToString());
                            int prob = pred.probability * 100;

                            if (prob > 50)
                            {
                                TagName = pred.tagName;
                                break;
                            }

                        }

                    }
                    catch (Exception e)
                    {
                        error = e.Message;

                    }

                }
            }
        }
    }

}