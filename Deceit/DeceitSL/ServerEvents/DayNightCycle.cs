using Exiled.API.Features;
using MEC;
using System.Collections.Generic;

namespace DeceitSL.ServerEvents
{
    public static class DayNightCycle
    {
        private static CoroutineHandle _cycleCoroutine;
        private static bool _isDay = true;

        public static void StartCycle(float dayDuration, float nightDuration)
        {
            if (_cycleCoroutine.IsRunning)
                Timing.KillCoroutines(_cycleCoroutine); // Останавливаем прошлый цикл, если он есть

            _cycleCoroutine = Timing.RunCoroutine(Cycle(dayDuration, nightDuration));
        }

        public static void StopCycle()
        {
            if (_cycleCoroutine.IsRunning)
                Timing.KillCoroutines(_cycleCoroutine);
        }

        private static IEnumerator<float> Cycle(float dayDuration, float nightDuration)
        {
            while (true) // Бесконечный цикл
            {
                if (_isDay)
                {
                    Day.Event();
                    Map.Broadcast(5, "Наступил день!");
                    yield return Timing.WaitForSeconds(dayDuration);
                }
                else
                {
                    Night.Event();
                    Map.Broadcast(5, "Наступила ночь!");
                    yield return Timing.WaitForSeconds(nightDuration);
                }

                _isDay = !_isDay;
            }
        }
    }
}
