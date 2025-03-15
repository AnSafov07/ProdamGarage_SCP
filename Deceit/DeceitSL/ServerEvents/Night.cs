using Exiled.API.Features;
using UnityEngine;

namespace DeceitSL.ServerEvents
{
    public static class Night
    {
        public static void Event()
        {
            Map.ChangeLightsColor(Color.red);
        }
    }
}
