using Exiled.API.Features;
using Exiled.Events.EventArgs.Server;
using MEC;
using PlayerRoles;
using System;

namespace FFafterRound
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "FFafterRound";
        public override string Prefix => "FFafterRound";
        public override string Author => "[DDG] SIN KIPU";
        public override Version Version { get; } = new Version(1, 0, 0);
        public static Plugin Instance;

        public override void OnEnabled()
        {
            Instance = this;
            Exiled.Events.Handlers.Server.RoundEnded += OnRoundEnded;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Server.RoundEnded -= OnRoundEnded;
            base.OnDisabled();
        }

        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            Server.FriendlyFire = true;

            Timing.CallDelayed(5f, () =>
            {
                Server.FriendlyFire = false;

                foreach (Player Player in Player.List)
                {
                    Player.Role.Set(RoleTypeId.Scp939);
                }
            });
        }
    }
}