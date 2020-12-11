using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Services
{
    public interface IPytorchService
    {
        public Task<PredictionData> GetPredictionNewAPI(string audioPath, string path);
        public Task<PredictionData> GetPrediction(string audioPath);
    }
    public class PytorchService:IPytorchService
    {
        readonly HttpClient client;
        string url = "http://34.209.233.244:44743/predict";
        string newAPIurl = "http://34.214.237.5:8070/WeatherForecast/GetPrediction";
        public PytorchService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

        }


        public async Task<PredictionData> GetPrediction(string audioPath)
        {
            var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(audioPath));

            MultipartFormDataContent form = new MultipartFormDataContent
            {
                { fileContent, "audio_file", Path.GetFileName(audioPath) }
            };

            var response = await client.PostAsync(url, form);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();

                var d = JsonConvert.DeserializeObject<PredictionData>(data);

                return d;
            }
            else
            {
                return null;
            }
        }

        public async Task<PredictionData> GetPredictionNewAPI(string audioPath, string path)
        {
            var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(audioPath));

            MultipartFormDataContent form = new MultipartFormDataContent
            {
                { fileContent, "audioFile", Path.GetFileName(audioPath) },

                { new StringContent(path),"page" }
            };

            

            var response = await client.PostAsync(newAPIurl, form);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();

                var d = JsonConvert.DeserializeObject<PredictionData>(data);

                d.Probability *= 100;

                return d;
            }
            else
            {
                return null;
            }

        }
    }
    public class PredictionData
    {
        [JsonProperty("class_id")]
        public string ClassId { get; set; }
        [JsonProperty("probability")]
        public double Probability { get; set; }
    }
}
