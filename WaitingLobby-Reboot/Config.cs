using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaitingLobby_Reboot
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        [Description("Координаты куда телепортируем игроков в начале раунда")]
        public float x { get; set; } = 30f;
        public float y { get; set; } = 991f;
        public float z { get; set; } = -26f;
        [Description("Переключатель зомби режима")]
        public bool ZombieMode { get; set; } = false;
        [Description("Задаем минимальное количество игроков для появления 1 зомби во время ожидания раунда(НЕ РАБОТАЕТ ЕСЛИ ВЫКЛЮЧИТЬ ПЕРЕКЛЮЧАТЕЛЬ ПОЯВЛЕНИЯ ЗОМБИ)")]
        public int MinPl { get; set; } = 5;
    }
}
