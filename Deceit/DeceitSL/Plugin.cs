using DeceitSL.ServerEvents;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using PluginAPI.Core;
using UnityEngine;
using Map = Exiled.API.Features.Map;

namespace DeceitSL
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "DeceitSCP";
        public override string Prefix => "DeceitSCP";
        public override string Author => "Kunica";
        public override PluginPriority Priority => PluginPriority.Last;
        public override System.Version Version { get; } = new System.Version(0, 1, 0);


        public static Plugin sex { get; private set; }

        public override void OnEnabled()
        {
            sex = this;
            Exiled.Events.Handlers.Server.WaitingForPlayers += OnWait;
            Exiled.Events.Handlers.Server.RoundStarted += OnStart;
            Exiled.Events.Handlers.Player.Verified += OnVerified;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Server.WaitingForPlayers -= OnWait;
            Exiled.Events.Handlers.Server.RoundStarted -= OnStart;
            Exiled.Events.Handlers.Player.Verified -= OnVerified;
            base.OnDisabled();
        }

        public void OnWait()
        {
            foreach (Exiled.API.Features.Player player in Exiled.API.Features.Player.List)
            {
                player.Broadcast(10, "Подготовьтесь к игре! \n Для информации напишите .S-Info в консоли.");
            }
            Exiled.API.Features.Round.IsLocked = true;
        }

        public void OnStart()
        {
            foreach (Exiled.API.Features.Player player in Exiled.API.Features.Player.List)
            {
                Spawner.SpawnNormal(player);
            }

            Exiled.API.Features.Round.IsLocked = true;

            DayNightCycle.StartCycle(Config.DayDuration, Config.NigtDuation);
        }

        public void OnVerified(VerifiedEventArgs ev)
        {
            if(PluginAPI.Core.Round.IsRoundStarted == true)
            {
                Spawner.SpawnNormal(ev.Player);
            }
        }
    }
}
