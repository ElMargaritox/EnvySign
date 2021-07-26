using EnvySign.Helper;
using EnvySign.Models;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace EnvySign.Commands
{
    class RemoveSign : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "removesign";

        public string Help => "Remove Commands Of Sign";

        public string Syntax => "/removesign [sign]";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>();

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;



            Transform trans = RaycastHelper.Raycast(player, 3f);
            if (trans == null) return;

            InteractableSign sign = trans.GetComponent<InteractableSign>();
            if (sign == null)
            {
                UnturnedChat.Say(player, EnvySignPlugin.Instance.Translate("sign_not_found")); return;
            }


            try
            {
                EnvySignPlugin.Instance.dataBase.Eliminar(x => x.InstanceID == sign.GetInstanceID());
            }
            catch (Exception)
            {

                UnturnedChat.Say(player, EnvySignPlugin.Instance.Translate("no_sign_command"));
            }

        }
    }
}
