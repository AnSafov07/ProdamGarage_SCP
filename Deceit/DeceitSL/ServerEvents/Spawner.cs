using Exiled.API.Features;
using Exiled.API.Enums;
using UnityEngine;

namespace DeceitSL
{

    public static class Spawner
    {
        public static void SpawnNormal(Player player)
        {
            player.EnableEffect(EffectType.FogControl, 5);
            player.EnableEffect(EffectType.Burned, 3);
            player.Role.Set(PlayerRoles.RoleTypeId.ClassD);
            Vector3 Exit = new Vector3(136.4f, 995.691f, -26);
            player.Position = Exit;
        }

        public static void SpawnMonster(Player player)
        {
            Vector3 Exit = new Vector3(136.4f, 995.691f, -26);
            player.BadgeHidden = true;
            player.Role.Set(PlayerRoles.RoleTypeId.ClassD);
            player.Position = Exit;
        }
    }
}