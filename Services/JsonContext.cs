using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Services
{
    public class JsonContext<T>
    {
        public JsonContext(string r)
        {
            rute = r;
        }
        public List<T> values = new List<T>();
        public string rute;
        public void Save()
        {
            string jsonText = JsonConvert.SerializeObject(values);
            File.WriteAllText(rute, jsonText);
        }

        public void Load()
        {
            string file = File.ReadAllText(rute);
            values = JsonConvert.DeserializeObject<List<T>>(file);
        }
        
        public void Insert(T data)
        {
            values.Add(data);
            Save();
        }        

        public void Delete(Func<T, bool> criterio)
        {
            values = values.Where(x => !criterio(x)).ToList();
        }

        public void Update(Func<T, bool> criterio, T data)
        {
            values = values.Select(x =>
            {
                if (criterio(x)) x = data;
                return x;
            }).ToList();
        }

    }
}
