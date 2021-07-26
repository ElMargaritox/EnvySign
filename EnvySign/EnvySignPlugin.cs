using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnvySign.Database;
using EnvySign.Helper;
using EnvySign.Models;
using Rocket.API;
using Rocket.API.Collections;
using Rocket.Core;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;

namespace EnvySign
{
    public class EnvySignPlugin : RocketPlugin<EnvySignConfiguration>
    {
        public static EnvySignPlugin Instance { get; set; }
        public ProviderJson<Signs> dataBase { get; set; }
        public override TranslationList DefaultTranslations
        {
            get
            {
                TranslationList translationlist = new TranslationList();
                translationlist.Add("command_added", "The Command '{0}' Added To The Sign");
                translationlist.Add("command_removed", "The Command '{0}' Removed Of Sign");
                translationlist.Add("no_permission", "¡Incorrect Permissions!");
                translationlist.Add("already_exist_commands", "This Signs Have '{0}' and '{1}'");
                translationlist.Add("sign_not_found", "You Need To Look The Sign");
                translationlist.Add("no_sign_command", "This Sign Haven't Commands");
                return translationlist;
            }
        }

        protected override void Load()
        {
            Instance = this;
            dataBase = new ProviderJson<Signs>($"{System.Environment.CurrentDirectory}/Plugins/EnvySign/signs.json"); dataBase.Cargar();
            UnturnedPlayerEvents.OnPlayerUpdateGesture += Gesto;

        }

        private void Gesto(UnturnedPlayer player, UnturnedPlayerEvents.PlayerGesture gesture)
        {
            if (gesture == UnturnedPlayerEvents.PlayerGesture.PunchLeft & Configuration.Instance.Use_Left_Hand || gesture == UnturnedPlayerEvents.PlayerGesture.PunchRight & Configuration.Instance.Use_Right_Hand)
            {
                Transform trans = RaycastHelper.Raycast(player, 3f);
                if (trans == null) return;

                InteractableSign sign = trans.GetComponent<InteractableSign>();
                if (sign == null) return;

                try
                {
                    Signs datos = dataBase.Buscar(x => x.InstanceID == sign.GetInstanceID())[0];

                    if (player.HasPermission(datos.Permission))
                    {
                        R.Commands.Execute(player, datos.Command);
                    }
                    else
                    {
                        UnturnedChat.Say(player, Translate("no_permission"));
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                
                

            }
        }

        protected override void Unload()
        {
            UnturnedPlayerEvents.OnPlayerUpdateGesture -= Gesto;
            Instance = null;
            Database = null;
        }
    }
}
