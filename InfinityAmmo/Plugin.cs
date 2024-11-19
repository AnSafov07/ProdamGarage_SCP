using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Interfaces;
using Exiled.Events.Commands.Reload;
using Exiled.Events.EventArgs.Player;
using InventorySystem;
using System;
using InventorySystem.Items;

namespace InfinityAmmo
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "InfinityAmmo";
        public override string Prefix => "InfinityAmmo";
        public override string Author => "[DDG] SIN KIPU";
        public override Version Version { get; } = new Version(1, 1, 0);
        public static Plugin plugin;

        public const int AmmoToAdd = 50; // Количество патронов, которое будет выдано
        public int totalAmmo = 100;

        public override void OnEnabled()
        {
            plugin = this;
            Exiled.Events.Handlers.Player.ReloadingWeapon += OnReloading;
            Exiled.Events.Handlers.Player.DroppingAmmo += OnThrowingAmmo;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.ReloadingWeapon -= OnReloading;
            base.OnDisabled();
        }

        public void OnReloading(ReloadingWeaponEventArgs ev)
        {
            ev.Player.ClearAmmo();
            ev.Player.AddAmmo(Exiled.API.Enums.AmmoType.Ammo12Gauge, AmmoToAdd);
            ev.Player.AddAmmo(Exiled.API.Enums.AmmoType.Ammo44Cal, AmmoToAdd);
            ev.Player.AddAmmo(Exiled.API.Enums.AmmoType.Nato762, AmmoToAdd);
            ev.Player.AddAmmo(Exiled.API.Enums.AmmoType.Nato9, AmmoToAdd);
            ev.Player.AddAmmo(Exiled.API.Enums.AmmoType.Nato556, AmmoToAdd);
        }
        public void OnThrowingAmmo(DroppingAmmoEventArgs ev)
        {
            ev.IsAllowed = false;
            ev.Player.Broadcast(3, "Вы не можете выбрасывать патроны");
        }
    }
}