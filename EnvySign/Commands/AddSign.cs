using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnvySign.Helper;
using EnvySign.Models;
using Rocket.API;
using Rocket.Core;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace EnvySign.Commands
{
    class AddSign : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "addsign";

        public string Help => "Add Commands To Sign";

        public string Syntax => "/addsign [command] [permission]";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>();

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;

            if(command.Length < 1)
            {
                UnturnedChat.Say(player, Syntax); return;
            }

            Transform trans = RaycastHelper.Raycast(player, 3f);
            if (trans == null) return;

            InteractableSign sign = trans.GetComponent<InteractableSign>();
            if (sign == null)
            {
                UnturnedChat.Say(player, EnvySignPlugin.Instance.Translate("sign_not_found")); return;         
            }





            try
            {


                EnvySignPlugin.Instance.SignDatabase.AddSign(sign.text, command[0], command[1], sign.GetInstanceID());
                UnturnedChat.Say(player, EnvySignPlugin.Instance.Translate("added_sign"));
            }
            catch
            {
                Logger.LogError("Error To Add Signs");
                
            }


        }
    }
}
