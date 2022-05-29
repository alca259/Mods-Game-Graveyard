using Harmony;
using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AlchemyResearch
{
    [HarmonyPatch(typeof(MixedCraftGUI), "Hide", new Type[] { typeof(bool) })]
    public class MixedCraftGUI_Hide
    {
        [HarmonyPostfix]
        public static void Postfix(MixedCraftGUI __instance)
        {
            Transform transform = ((Component)__instance).transform.Find("ingredient container result");
            if (!UnityExtensions.IsUnityObject((Object)transform) || !UnityExtensions.IsUnityObject((Object)((Component)transform).gameObject))
                return;
            Object.Destroy((Object)((Component)transform).gameObject);
        }
    }
}
