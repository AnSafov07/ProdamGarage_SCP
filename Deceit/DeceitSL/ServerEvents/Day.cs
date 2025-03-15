using Exiled.API.Features;
using UnityEngine;

namespace DeceitSL.ServerEvents
{
    public static class Day
    {
        public static void Event()
        {
            Map.ChangeLightsColor(Color.blue);
        }
    }
}
