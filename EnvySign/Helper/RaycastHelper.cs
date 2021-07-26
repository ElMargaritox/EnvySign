using Rocket.API;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EnvySign.Helper
{
    public class RaycastHelper
    {
        public static Transform Raycast(UnturnedPlayer player, float distance)
        {
            if(Physics.Raycast(player.Player.look.aim.position, player.Player.look.aim.forward, out RaycastHit hit, distance,
                RayMasks.BARRICADE_INTERACT | RayMasks.BARRICADE))
            {
                Transform transform = hit.transform;
                return transform;
            }
            return null;
        }
    }
}
