using Harmony;
using System.Reflection;
using UnityEngine;

namespace PrayAllDays
{
    public class MainPatcher
    {
        public static bool PrayAllDay = true;

        public static void Patch()
        {
            try
            {
                //_cfg = Config.GetOptions();
                var val = HarmonyInstance.Create($"Alca259.graveyardkeeper.PrayAllDays");
                val.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (System.Exception ex)
            {
                Debug.LogError("[PrayAllDay] Cannot be loaded.");
                Debug.LogException(ex);
            }
        }

        [HarmonyPatch(typeof(WorldGameObject))]
        [HarmonyPatch(nameof(WorldGameObject.SetParam), typeof(string), typeof(float))]
        public class PatchPrayAllDay
        {
            [HarmonyPrefix]
            public static bool Prefix(string param_name, float value)
            {
                var result = !PrayAllDay || !(param_name == "prayed_this_week") || (double)value != 1.0;
                Debug.Log("[PrayAllDay] prayed_this_week: success!");
                return result;
            }
        }
    }
}
