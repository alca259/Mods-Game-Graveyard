using Harmony;
using System.Reflection;
using UnityEngine;

namespace Insomnia
{
    public class MainPatcher
    {
        public static bool NoSleep = true;

        public static void Patch()
        {
            //_cfg = Config.GetOptions();
            var val = HarmonyInstance.Create($"Alca259.graveyardkeeper.Insomnia");
            val.PatchAll(Assembly.GetExecutingAssembly());
        }

        [HarmonyPatch(typeof(BuffsLogics))]
        [HarmonyPatch(nameof(BuffsLogics.AddBuff))]
        public class PatchNoSleep
        {
            [HarmonyPrefix]
            public static bool Prefix(string buff_id)
            {
                if (!NoSleep || !buff_id.Contains("buff_tired"))
                    return true;

                Debug.Log("[Insomnia] buff_tired: buff blocked : success!");
                MainGame.me.player.ClearTiredness();
                return false;
            }
        }
    }
}
