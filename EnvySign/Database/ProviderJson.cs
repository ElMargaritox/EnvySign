using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvySign.Database
{
    public class ProviderJson<T>
    {
        public List<T> data = new List<T>();
        public string ruta;
        public ProviderJson(string r)
        {
            ruta = r;
        }

        public void Guardar()
        {
            string texto = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(ruta, texto);
        }

        public void Cargar()
        {
            try
            {
                string archivo = File.ReadAllText(ruta);
                data = JsonConvert.DeserializeObject<List<T>>(archivo);
            }
            catch (Exception) { }
        }

        public void Insertar(T Nuevo)
        {
            data.Add(Nuevo);
            Guardar();
        }

        public List<T> Buscar(Func<T, bool> criterio)
        {
            return data.Where(criterio).ToList();
        }

        public void Eliminar(Func<T, bool> criterio)
        {
            data = data.Where(x => !criterio(x)).ToList();
            Guardar();
        }
    }
}
