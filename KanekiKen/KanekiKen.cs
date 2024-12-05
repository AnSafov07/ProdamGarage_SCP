using CustomPlayerEffects;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using PlayerRoles;
using PluginAPI.Core;
using PluginAPI.Core.Zones.Heavy.Rooms;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekiKen
{
    [CustomRole(RoleTypeId.Tutorial)]
    public class KanekiKen : CustomRole
    {
        public override uint Id { get; set; } = 10007;
        public override RoleTypeId Role { get; set; } = RoleTypeId.Tutorial;
        public override int MaxHealth { get; set; } = 2000;
        public override string Name { get; set; } = "Канеки Кен";
        public override string Description { get; set; } = "Специальная роль с уникальными способностями.";
        public override string CustomInfo { get; set; } = "Канеки Кен";
        public override List<string> Inventory { get; set; } = new()
        {
            $"{ItemType.Lantern}",
            $"{ItemType.KeycardO5}",
            $"{ItemType.Jailbird}",
        };
        public override SpawnProperties SpawnProperties { get; set; } = new()
        {
            Limit = 1,
            RoleSpawnPoints = new List<RoleSpawnPoint>
            {
                new()
                {
                    Role = RoleTypeId.ClassD,
                    Chance = 100,
                },
            },
        };
        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Hurting += OnGettingDamage;
            Exiled.Events.Handlers.Player.Dying += OnDying;
            Exiled.Events.Handlers.Player.DroppingItem += OnDroppingItem;
            Exiled.Events.Handlers.Player.PickingUpItem += OnPickingUpItem;
            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.DroppingItem -= OnDroppingItem;
            Exiled.Events.Handlers.Player.PickingUpItem -= OnPickingUpItem;
            base.UnsubscribeEvents();
        }
        private void OnDying(DyingEventArgs ev)
        {
            if (Check(ev.Attacker))
            {
                ev.Attacker.Heal(500);
                ev.Attacker.Broadcast(3, "Ты полечился, убив другого игрока!");
            }
        }
        private void OnGettingDamage(HurtingEventArgs ev)
        {
            if (Check(ev.Attacker))
            {
                ev.Attacker.EnableEffect(Exiled.API.Enums.EffectType.MovementBoost, 5, true);
                Timing.CallDelayed(3f, () => { ev.Attacker.DisableEffect(Exiled.API.Enums.EffectType.MovementBoost); });
            }
        }
        private void OnPickingUpItem(PickingUpItemEventArgs ev)
        {
            if (Check(ev.Player))
                ev.IsAllowed = false;
        }

        private void OnDroppingItem(DroppingItemEventArgs ev)
        {
            if (Check(ev.Player))
                ev.IsAllowed = false;
        }
    }
}
