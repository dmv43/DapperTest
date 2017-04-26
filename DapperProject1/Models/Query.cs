using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Http;
namespace DapperProject1.Models
{
    public class Query
    {
        public static RootObject startQuery()
        {

            var values = new Dictionary<string, string>
        {
            { "LOGIN_PA_USERNAME", "dmv4343@gmail.com" },
                { "LOGIN_PA_PASSWORD", "335435" }

        };
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri("https://www.italki.com");

                var content = new FormUrlEncodedContent(values);
                HttpResponseMessage response2 = client.PostAsync("https://www.italki.com/api/login", content).Result;


                bool check = false;
                int i = 1;
                Random rand = new Random();
                List<string> resultString = new List<string>();
                do
                {
                    string resulter = getQuery(client, i).Content.ReadAsStringAsync().Result;
                    resultString.Add(resulter);
                    RootObject tempObj = Deserialize.deserialize(resulter);


                    i++;

                    int zz = rand.Next(10, 200);
                    System.Threading.Thread.Sleep(zz);
                 
                   

                    if (tempObj.meta.has_next == true)
                    {

                        check = true;
                    }
                    else { check = false; }
                   


                } while (check == true);
                File.WriteAllLines(@"D:\Full_Json_Data1.txt", resultString);
               RootObject myObject = Deserialize.deserializeBulk(resultString);
                return myObject;


            }

        }
        static HttpResponseMessage getQuery(HttpClient cl, int count)
        {
            HttpResponseMessage response = cl.GetAsync("https://www.italki.com/api/teachers?_r=1493105167375&country=&hl=uk-UA&i_token=TkRBNE5EQTNNZz09fDE0OTI1OTEwMzB8OTliY2E5MTQzMjM3MWMxM2ViMmFlMTY5ZWI2Zjc3YjAxMzZmMjJjMw%3D%3D&is_instant=&page=" + count + "&price_usd=&speak=&teach=english&teacher_type=1").Result;
            response.EnsureSuccessStatusCode();
            
            return response;
        }
    } 
}
