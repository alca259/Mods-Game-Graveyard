using Harmony;
using System;
using Object = UnityEngine.Object;

namespace AlchemyResearch
{
    [HarmonyPatch(typeof(CraftComponent), "ProcessFinishedCraft", new Type[] { })]
    public class CraftComponent_ProcessFinishedCraft
    {
        [HarmonyPrefix]
        public static void Patch(CraftComponent __instance)
        {
            string str = "empty";
            if (((WorldGameObjectComponentBase)__instance).wgo.data == null || !((WorldGameObjectComponentBase)__instance).wgo.data.id.StartsWith("mf_alchemy_craft_"))
                return;
            if (__instance.current_craft.output.Count > 0)
                str = __instance.current_craft.output[0].id;
            else if (__instance.current_craft.needs.Count > 0)
                str = __instance.current_craft.needs[0].id;
            if (str.Equals("empty") || !AlchemyRecipe.MatchesResult(((Object)((WorldGameObjectComponentBase)__instance).wgo).GetInstanceID(), ((WorldGameObjectComponentBase)__instance).wgo.obj_id, str) || !AlchemyRecipe.HasValidRecipe())
                return;
            ResearchedAlchemyRecipes.AddCurrentRecipe(str);
        }
    }
}
