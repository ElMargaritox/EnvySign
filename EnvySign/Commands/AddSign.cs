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
                Signs s = EnvySignPlugin.Instance.dataBase.Buscar(x => x.InstanceID == sign.GetInstanceID())[0];
                UnturnedChat.Say(player, EnvySignPlugin.Instance.Translate("already_exist_commands", s.Command, s.Permission));
                return;
            }
            catch (Exception) { }

            try
            {

                

                Signs s = new Signs
                {
                    InstanceID = sign.GetInstanceID(),
                    Command = command[0],
                    Permission = command[1]
                };

                EnvySign.EnvySignPlugin.Instance.dataBase.Insertar(s);
                R.Commands.Execute(new ConsolePlayer(), "/save");
                UnturnedChat.Say(player, EnvySignPlugin.Instance.Translate("added_sign"));
            }
            catch (Exception)
            {
                Logger.LogError("Error To Add Signs");
                
            }


        }
    }
}
