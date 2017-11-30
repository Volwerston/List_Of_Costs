using ListOfCosts.db_client;
using ListOfCosts.Models.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ListOfCosts.Models.Helpers
{
    public static class LogHelper
    {
        public static int OffsetInDays { get; set; }

        static LogHelper()
        {
            OffsetInDays = 30;
        }

        public static Log GenerateNextLogItem()
        {
            return new Log()
            {
                LastUpdate = DateTime.Now,
                NextUpdate = DateTime.Now.AddDays(OffsetInDays)
            };
        }

        public static void CheckLog(string login)
        {
            string relativePath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\storage\\" + login;

            DirectoryInfo di = new DirectoryInfo(relativePath);

            if (!di.Exists)
            {
                di.Create();
           
                relativePath += "\\logs.txt";

                List<Log> log = new List<Log>();
                log.Add(GenerateNextLogItem());

                using (FileStream fs = File.Create(relativePath))
                {
                    using (StreamWriter w = new StreamWriter(fs))
                    {
                        w.WriteLine(JsonConvert.SerializeObject(log, new JsonSerializerSettings() { TypeNameHandling=TypeNameHandling.All }));
                    }
                }
            }
            else
            {
                relativePath += "\\logs.txt";
                string data = "";
                List<Log> log = new List<Log>();

                using (FileStream fs = new FileStream(relativePath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader r = new StreamReader(fs))
                    {
                        data = r.ReadToEnd().Trim();
                    }

                    log = (List<Log>)JsonConvert.DeserializeObject(data, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });

                    if (DateTime.Now >= log[log.Count - 1].NextUpdate)
                    {
                        log.Add(GenerateNextLogItem());
                        UpdateDbCostEntries();
                    }
                }

                using (FileStream fs = new FileStream(relativePath, FileMode.Open, FileAccess.Write))
                {
                    using (StreamWriter w = new StreamWriter(fs))
                    {
                        w.WriteLine(JsonConvert.SerializeObject(log, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All }));
                    }
                }

                  
            }
        }

        private static void UpdateDbCostEntries()
        {
            CostDbStrategy cds = new CostDbStrategy();

            foreach(var cost in cds.ReadAll())
            {
                if (cost.OwnerId == DbContext.Identity.Id)
                {
                    cost.CurrentWaste = 0;

                    cds.Update(cost);
                }
            }
        }
    }
}
