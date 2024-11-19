using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Loader;
using PlayerRoles;
using System;
using UnityEngine;

namespace DetonationCoin
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "LuckyCoin";
        public override string Prefix => "LuckyCoin";
        public override string Author => "SIN KIPU";
        public override Version Version { get; } = new Version(1, 0, 1);
        public static Plugin plugin;

        private Vector3 surfaceNukePosition = new Vector3(30, 992, -26);
        private string originalNickname;
        private int idLocal;

        public override void OnEnabled()
        {
            plugin = this;
            Exiled.Events.Handlers.Player.FlippingCoin += OnFlippingCoin;
            Exiled.Events.Handlers.Player.Dying += OnPlayerDying;
            Exiled.Events.Handlers.Player.PickingUpItem += OnPickingUpItem;
            Exiled.Events.Handlers.Player.DroppingItem += OnThrowingItem;
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.FlippingCoin -= OnFlippingCoin;
            Exiled.Events.Handlers.Player.Dying -= OnPlayerDying;
            Exiled.Events.Handlers.Player.PickingUpItem -= OnPickingUpItem;
            Exiled.Events.Handlers.Player.DroppingItem -= OnThrowingItem;
            base.OnDisabled();
        }

        public void OnFlippingCoin(FlippingCoinEventArgs ev)
        {
            switch (Loader.Random.Next(9))
            {
                case 0:
                    ev.Player.Role.Set(RoleTypeId.Scp0492);
                    Log.Info($"Player {ev.Player.Nickname} has made a zombie:)");
                    ev.Player.Broadcast(3, "Ты стал зомби :)");
                    break;
                case 1:
                    ev.Player.Teleport(RoomType.Pocket);
                    Log.Info($"Player {ev.Player.Nickname} has teleported to a pocket");
                    ev.Player.Broadcast(3, ":)");
                    break;
                case 2:
                    ev.Player.Broadcast(3, "Лутайся!");
                    ev.Player.AddItem(ItemType.KeycardO5);
                    break;
                case 3:
                    ev.Player.Broadcast(3, "Подкрепись!");
                    ev.Player.AddItem(ItemType.SCP207);
                    ev.Player.AddItem(ItemType.Painkillers);
                    break;
                case 4:
                    ev.Player.Broadcast(3, "С новым годом!!!");
                    ev.Player.Role.Set(RoleTypeId.ClassD);
                    ev.Player.AddItem(ItemType.KeycardScientist);
                    break;
                case 5:
                    idLocal = ev.Player.Id;
                    ev.Player.Broadcast(5, "<color=red>1000-7</color>");
                    Exiled.API.Features.Server.ExecuteCommand($"ball {idLocal}");
                    break;
                case 6:
                    Exiled.API.Features.Server.ExecuteCommand("mp load garage");
                    Map.Broadcast(5, "На карте появился гараж!");
                    break;
                case 7:
                    ev.Player.Broadcast(3, "Удача улыбнулась тебе!");
                    ev.Player.AddItem(ItemType.SCP500);
                    ev.Player.AddItem(ItemType.Coin);
                    ev.Player.AddItem(ItemType.ArmorLight);
                    ev.Player.AddItem(ItemType.GunCOM15);
                    ev.Player.AddAmmo(Exiled.API.Enums.AmmoType.Nato9, 20);
                    ev.Player.ThrowGrenade(Exiled.API.Enums.ProjectileType.Scp2176);
                    break;
                case 8:
                    originalNickname = ev.Player.Nickname;

                    ev.Player.EnableEffect(Exiled.API.Enums.EffectType.Flashed);
                    Log.Info($"Warning! Player {ev.Player.Nickname} has made an SCP-001!");

                    ev.Player.Broadcast(5, "Что произошло???");
                    ev.Player.Role.Set(RoleTypeId.Tutorial);
                    ev.Player.ClearInventory();
                    ev.Player.AddItem(ItemType.GunRevolver);
                    ev.Player.AddItem(ItemType.KeycardO5);
                    ev.Player.AddAmmo(Exiled.API.Enums.AmmoType.Ammo44Cal, 1000);
                    ev.Player.AddItem(ItemType.ArmorHeavy);
                    ev.Player.Position = surfaceNukePosition;
                    ev.Player.CustomName = "SCP 001";
                    ev.Player.Heal(500);
                    ev.Player.Health = 500;
                    ev.Player.Scale = new Vector3(0.7f, 0.7f, 0.7f);
                    break;
            }
        }

        public void OnPlayerDying(DyingEventArgs ev)
        {
            if (ev.Player.CustomName == "SCP 001")
            {

                ev.Player.CustomName = originalNickname;
                ev.Player.Scale = new Vector3(1f, 1f, 1f);
            }
        }

        private void OnPickingUpItem(PickingUpItemEventArgs ev)
        {
            if (ev.Player.CustomName == "SCP 001")
            {
                ev.IsAllowed = false;
                ev.Player.Broadcast(3, "Вы не можете поднимать предметы, пока вы SCP 001.");
            }
        }

        private void OnThrowingItem(DroppingItemEventArgs ev)
        {
            if (ev.Player.CustomName == "SCP 001")
            {
                ev.IsAllowed = false;
            }
        }
    }
}