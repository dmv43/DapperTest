using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperProject1.Models
{
    public class Deserialize
    {
        public static RootObject deserializeBulk(List<string> result)
        {
            RootObject mainObject = new RootObject();
            for (int i = 0; i < result.Count; i++)
            {
                if (i == 0)
                {
                    mainObject = JsonConvert.DeserializeObject<RootObject>(result[i]);
                }
                else
                {
                    mainObject.data.AddRange(JsonConvert.DeserializeObject<RootObject>(result[i]).data);
                }
            }
            return mainObject;

        }
        public static RootObject deserialize(string result)
        {
            RootObject mainObject = JsonConvert.DeserializeObject<RootObject>(result);

            return mainObject;
        }
    }
}
