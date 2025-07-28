using Exiled.API.Features;
using ProjectMER.Features.Objects;
using UnityEngine;
using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomizationSL.Handlers
{
    public static class SchematicHandler
    {
        private static readonly Dictionary<string, List<SchematicObject>> playerSchematics = new Dictionary<string, List<SchematicObject>>();

        public static bool ApplySchematic(Player player, string schematicName, SchematicData data, int playerLevel, out string response)
        {
            if (playerLevel < data.rank)
            {
                response = $"Недостаточный уровень для использования '{schematicName}' (нужен {data.rank}).";
                return false;
            }

            try
            {
                Vector3 offset = data.GetOffset();
                Quaternion rotation = data.GetRotation();

                SchematicObject schematic = ProjectMER.Features.ObjectSpawner.SpawnSchematic(schematicName, player.Position + Vector3.up * 1.5f);
                schematic.transform.parent = player.GameObject.transform;
                schematic.transform.localPosition = offset;
                schematic.transform.localRotation = rotation;

                if (!playerSchematics.ContainsKey(player.UserId))
                    playerSchematics[player.UserId] = new List<SchematicObject>();

                playerSchematics[player.UserId].Add(schematic);

                if (data.visible_schem == false)
                {
                    foreach (var net in schematic.NetworkIdentities)
                        player.Connection.Send(new ObjectDestroyMessage { netId = net.netId });
                }

                response = $"Схематик '{schematicName}' применён.";
                return true;
            }
            catch (Exception ex)
            {
                if (Plugin.Instance.Config.Debug)
                    Log.Error(ex.ToString());

                response = $"Ошибка при применении схематика: {ex.Message}";
                return false;
            }
        }

        public static string DeleteSchematics(Player player)
        {
            if (playerSchematics.TryGetValue(player.UserId, out var list))
            {
                foreach (var sch in list)
                {
                    if (sch != null && sch.gameObject != null)
                        UnityEngine.Object.Destroy(sch.gameObject);
                }

                list.Clear();
                return "Ваши схематики удалены.";
            }

            return "У вас нет активных схем.";
        }

        public static List<string> GetRegisteredSchematics(Player player)
        {
            if (playerSchematics.TryGetValue(player.UserId, out var list))
            {
                return list
                    .Where(s => s != null && s.name != null)
                    .Select(s => s.name)
                    .ToList();
            }

            return new List<string>();
        }
    }
}
