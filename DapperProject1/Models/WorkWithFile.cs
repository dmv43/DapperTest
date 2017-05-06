using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
namespace DapperProject1.Models
{
    public class WorkWithFile
    {
        public static RootObject deserialize()
        {
            var fromFIle = File.ReadAllLines(Directory.GetCurrentDirectory() + @"" + Path.DirectorySeparatorChar + "Queries" + Path.DirectorySeparatorChar + "Full_Json_Data4.txt");
                
            return Deserialize.deserializeBulk(new List<string>(fromFIle));
        }
    }
}
