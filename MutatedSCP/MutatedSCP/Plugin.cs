using System;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;

namespace MutatedSCP
{
    internal class Plugin : Plugin<Config>
    {
        public override string Prefix => "MSCP";
        public override string Name => "MutatedSCP";
        public override string Author => "[DDG] Wuegue4ik";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion => new Version(8, 9, 11);
        private int PlayerID = -1;
        private bool isMSCP = true;

        public override void OnEnabled()
        {
            Log.Info($"{Name} has been enabled! Plugin version - {Version}, required Exiled version - {RequiredExiledVersion}");
            base.OnEnabled();

            if (!isMSCP)
                Exiled.Events.Handlers.Player.Spawned += OnPlayerSpawned;

            Exiled.Events.Handlers.Player.Hurting += OnHurting;
            Exiled.Events.Handlers.Player.Hurt += OnHurt;
            Exiled.Events.Handlers.Player.KillingPlayer += OnKill;
            Exiled.Events.Handlers.Player.Died += OnDied;
        }

        public override void OnDisabled()
        {
            Log.Info($"{Name} has been disabled!");
            base.OnDisabled();

            Exiled.Events.Handlers.Player.Spawned -= OnPlayerSpawned;
            Exiled.Events.Handlers.Player.Hurting -= OnHurting;
            Exiled.Events.Handlers.Player.Hurt -= OnHurt;
            Exiled.Events.Handlers.Player.KillingPlayer -= OnKill;
            Exiled.Events.Handlers.Player.Died -= OnDied;
        }

        private void OnPlayerSpawned(SpawnedEventArgs ev)
        {
            if (ev.Player.IsScp)
            {
                if (UnityEngine.Random.Range(0, 100) <= Config.Chanse)
                {
                    ev.Player.Broadcast(7, "You are a <b>MUTATED SCP</b>!");
                    PlayerID = ev.Player.Id;
                    isMSCP = !isMSCP;
                    Log.Info($"Player {ev.Player.Nickname} became a MUTATED SCP as {ev.Player.Role}");
                }
            }
        }

        private void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Player.Id == PlayerID && Config.IsHealingOnHurting)
                ev.Player.Health += Config.RegenerationOnHurting;
        }

        private void OnHurt(HurtEventArgs ev)
        {
            if (ev.Player.Id == PlayerID && Config.IsHealingOnHurt)
                ev.Player.Health += Config.RegenerationOnHurt;
        }

        private void OnKill(KillingPlayerEventArgs ev)
        {
            if (ev.Player.Id == PlayerID && Config.IsHealingOnKillingPlayer)
                ev.Player.Health += Config.RegenerationOnKillingPlayer;

            if (ev.Player.Id == PlayerID && Config.IsGainingStaminaOnKillingPlayer)
                ev.Player.Stamina += Config.GainingStaminaOnKillingPlayer;
        }

        private void OnDied(DiedEventArgs ev)
        {
            if (ev.Player.Id == PlayerID)
            {
                PlayerID = -1;
                isMSCP = !isMSCP;
                Log.Info($"Player {ev.Player.Nickname} was killed as MUTATED SCP");
            }
        }
    }
}
