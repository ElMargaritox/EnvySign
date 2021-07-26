using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.API;

namespace EnvySign
{
    public class EnvySignConfiguration : IRocketPluginConfiguration
    {
        public bool Use_Right_Hand { get; set; }
        public bool Use_Left_Hand { get; set; }
        public void LoadDefaults()
        {
            Use_Right_Hand = true;
            Use_Left_Hand = true;
        }
    }
}
