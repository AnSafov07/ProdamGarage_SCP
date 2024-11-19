using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEC;

namespace EscapeFromSaratov 
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "EscapeFromSaratov";
        public override string Prefix => "EscapeFromSaratov";
        public override string Author => "SIN KIPU";
        public override Version Version { get; } = new Version(1, 0, 0);
        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Player.Escaping += OnEscaping;
            Exiled.Events.Handlers.Server.OnWaitingForPlayers += Start;
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            base.OnDisabled();
        }
        public void OnEscaping(EscapingEventArgs ev)
        {
            if (ev.Player.Role == RoleTypeId.ClassD || ev.Player.Role == RoleTypeId.Scientist) 
            {
                Log.Info($"Игрок {ev.Player.Nickname} сбежал из Иерусалима");
                float random = UnityEngine.Random.Range(0f, 100f);
                if (random >= Config.ChanceToEscape)
                {
                    ev.Player.Broadcast(5, "Никто не сбежит из Саратова!");
                }
                else
                {
                    ev.Player.Broadcast(5, "Тебе удалось сбежать из Саратова!");
                    IEnumerator<float> TimeForEsc(Exiled.API.Features.Player pl)
                    {
                        yield return Timing.WaitForSeconds(2);
                        pl.Teleport(RoomType.Hcz096);
                        pl.AddItem(ItemType.Jailbird);
                        pl.EnableEffect(EffectType.Flashed, 1, 5f);
                    }
                    Timing.RunCoroutine(TimeForEsc(ev.Player));
                }
            }
            else
            {
                return;
            }
        }
    }
}
