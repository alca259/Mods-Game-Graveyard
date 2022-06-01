using Harmony;
using System.Reflection;

namespace NoCorpseDecay
{
    public class MainPatcher
    {
        public static bool NoDecay = true;
        private const string BodyID = "body";

        public static void Patch()
        {
            var val = HarmonyInstance.Create($"Alca259.graveyardkeeper.NoCorpseDecay");
            val.PatchAll(Assembly.GetExecutingAssembly());
        }

        [HarmonyPatch(typeof(Item))]
        [HarmonyPatch(nameof(Item.UpdateDurability))]
        public class PatchNoSleep
        {
            [HarmonyPostfix]
            public static void Postfix(Item __instance, float delta_time, float parent_modificator = 1)
            {
                if (!NoDecay) return;

                if (__instance == null
                    || __instance.definition == null
                    || !__instance.definition.has_durability) return;

                if (!BodyID.Equals(__instance.definition.id, System.StringComparison.InvariantCultureIgnoreCase)) return;

                __instance.durability = 1f;
            }
        }
    }
}
