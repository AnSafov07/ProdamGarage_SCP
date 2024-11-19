using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Interfaces;

namespace MutatedSCP
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        public int Chanse { get; set; } = 1;

        public bool IsHealingOnHurting { get; set; } = true;
        public bool IsHealingOnHurt { get; set; } = true;
        public bool IsHealingOnKillingPlayer { get; set; } = true;
        public bool IsGainingStaminaOnKillingPlayer { get; set; } = true;

        public int RegenerationOnHurting { get; set; } = 3;
        public int RegenerationOnHurt { get; set; } = 1;
        public int RegenerationOnKillingPlayer { get; set; } = 1;
        public int GainingStaminaOnKillingPlayer { get; set; } = 5;
    }
}
