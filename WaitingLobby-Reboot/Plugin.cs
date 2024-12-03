using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using MEC;
using PlayerRoles;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace WaitingLobby_Reboot
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "WaitingLobby-Reboot";
        public override string Prefix => "WaitingLobby-Reboot";
        public override string Author => "SIN KIPU";
        public override Version Version { get; } = new Version(1, 2, 0);

        public static Plugin plugin;
        public Vector3 TeleportCoordinates;

        private readonly HashSet<Player> teleportedPlayers = new HashSet<Player>();
        public List<Player> ZombieList = new List<Player>();
        private bool isActive = true;
        private bool hasBecomeSpectators = false;

        public override void OnEnabled()
        {
            TeleportCoordinates = new Vector3(Config.x, Config.y, Config.z);
            plugin = this;

            Exiled.Events.Handlers.Server.WaitingForPlayers += onWaiting;
            Exiled.Events.Handlers.Player.Verified += onVerif;
            Exiled.Events.Handlers.Player.Died += onPlayerDied;
            Exiled.Events.Handlers.Server.RoundStarted += onRoundStarted;
            Exiled.Events.Handlers.Server.RestartingRound += onServerRestart;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Server.WaitingForPlayers -= onWaiting;
            Exiled.Events.Handlers.Player.Verified -= onVerif;
            Exiled.Events.Handlers.Player.Died -= onPlayerDied;
            Exiled.Events.Handlers.Server.RoundStarted -= onRoundStarted;
            Exiled.Events.Handlers.Server.RestartingRound -= onServerRestart;
            base.OnDisabled();
        }

        public void onWaiting()
        {
            GameObject.Find("StartRound").transform.localScale = Vector3.zero;
        }

        public void onVerif(VerifiedEventArgs ev)
        {
            if (isActive && !Round.IsStarted)
            {
                if (!teleportedPlayers.Contains(ev.Player))
                {
                    ev.Player.Role.Set(RoleTypeId.ClassD);
                    ev.Player.Teleport(TeleportCoordinates);
                    teleportedPlayers.Add(ev.Player);
                    ev.Player.AddItem(ItemType.Lantern);
                    ev.Player.AddItem(ItemType.Jailbird);
                }

                UpdateWaitingStats(ev.Player);
            }
        }

        public void UpdateWaitingStats(Player player)
        {
            if (isActive && !Round.IsStarted)
            {
                if (Round.LobbyWaitingTime == -2)
                {
                    string waitingStats = $"Игроков: {Player.List.Count} | Статус : Приостановлено";
                    player.Broadcast(1, waitingStats);
                }
                else if (Round.LobbyWaitingTime == 1 && !hasBecomeSpectators)
                {
                    foreach (Player pl in Player.List)
                    {
                        pl.Role.Set(RoleTypeId.Spectator);
                    }
                    hasBecomeSpectators = true;
                    return; // Прекращаем дальнейшие действия
                }
                else
                {
                    string waitingStats = $"Игроков: {Player.List.Count} | Статус : {Round.LobbyWaitingTime}";
                    player.Broadcast(1, waitingStats);
                }

                if (player.Position.y <= TeleportCoordinates.y - 5 && player.Role == RoleTypeId.Scp0492 && !Round.IsStarted)
                {
                    player.Teleport(TeleportCoordinates);
                    teleportedPlayers.Add(player);
                }

                ZombieList.Clear();
                foreach (Player pl in Player.List)
                {
                    if (pl.Role == RoleTypeId.Scp0492 && !ZombieList.Contains(pl))
                    {
                        ZombieList.Add(pl);
                    }
                }

                if (Player.List.Count >= Config.MinPl && !Round.IsStarted && ZombieList.Count == 0 && Config.ZombieMode)
                {
                    Player randomPlayer = Player.List.GetRandomValue();
                    if (randomPlayer != null)
                    {
                        randomPlayer.Role.Set(RoleTypeId.Scp0492);
                    }
                }

                Timing.CallDelayed(1f, () => UpdateWaitingStats(player));
            }
        }

        private void onPlayerDied(DiedEventArgs ev)
        {
            if (isActive && !Round.IsStarted)
            {
                if (teleportedPlayers.Contains(ev.Player))
                {
                    Timing.CallDelayed(2f, () =>
                    {
                        ev.Player.Role.Set(RoleTypeId.Scp0492);
                        ev.Player.Teleport(TeleportCoordinates);
                    });
                }
            }
        }

        private void onRoundStarted()
        {
            isActive = false;
        }

        private void onServerRestart()
        {
            isActive = true;
            teleportedPlayers.Clear();
            ZombieList.Clear();
            hasBecomeSpectators = false;
        }
    }
}