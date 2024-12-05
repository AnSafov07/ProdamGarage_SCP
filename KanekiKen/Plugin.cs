using Exiled.API.Features;
using Exiled.CustomRoles.API;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using System;
using Exiled.CustomRoles;

namespace KanekiKen
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "KanekiKen Plugin";
        public override string Prefix => "KanekiKen Plugin";
        public override string Author => "[PG] SIN KIPU";
        public override Version Version { get; } = new Version(1, 0, 0);
        public static Plugin meow;
        public KanekiKen KanekiKen { get; set; } = new KanekiKen();

        public override void OnEnabled()
        {
            meow = this;
            Exiled.Events.Handlers.Player.Verified += OnVerified;
            CustomRole.RegisterRoles(false, null);
            base.OnEnabled();
            KanekiKen.Register();
        }
        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.Verified -= OnVerified;
            meow = null;

            CustomRole.UnregisterRoles();

            base.OnDisabled();
        }

        private void OnVerified(VerifiedEventArgs ev)
        {
            if (ev.Player.UserId == "76561199132431203@steam")
            {
                KanekiKen.AddRole(ev.Player);
            }
        }
    }
}