
using EnvySign.DataStorage;
using EnvySign.Models;
using Rocket.Core.Logging;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvySign.Database
{
    public class SignDatabase
    {
        private List<Signs> Data;

       

        private DataStorage<List<Signs>> Storage { get; set; }
        public SignDatabase()
        {
           

            this.Storage = new DataStorage<List<Signs>>(EnvySignPlugin.Instance.Directory, "Signs.json");
            Logger.Log("Conexion a la base de datos... OK");
        }

        public Signs GetSign(string text)
        {
            return this.Data.Find(x => x.Text == text);
        }

        public void AddSign(string text, string command, string permission, int instanceId)
        {
            Signs signs = new Signs
            {
                Command = command,
                InstanceID = instanceId,
                Permission = permission,
                Text = text
            };

            Data.Add(signs);
            this.Storage.Save(this.Data);
        }

        public void RemoveSign(string text)
        {
            var sign = GetSign(text);
            if(sign != null)
            {
                Data.Remove(sign);
                this.Storage.Save(this.Data);
            }
        }

        public void Reload()
        {
            Data = Storage.Read();
            if (Data == null)
            {
                Data = new List<Signs>();
                Storage.Save(Data);
            }
        }




    }
}
