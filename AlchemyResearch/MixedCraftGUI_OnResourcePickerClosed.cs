using Harmony;
using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AlchemyResearch
{
    [HarmonyPatch(typeof(MixedCraftGUI), "OnResourcePickerClosed", new Type[] { typeof(Item) })]
    public class MixedCraftGUI_OnResourcePickerClosed
    {
        [HarmonyPostfix]
        public static void Patch(MixedCraftGUI __instance, Item item)
        {
            string objId = ((BaseCraftGUI)__instance).GetCrafteryWGO().obj_id;
            Transform crafteryTransform = MixedCraftGUI_OpenAsAlchemy.GetCrafteryTransform(((Component)__instance).transform, objId);
            Transform ResultPreview = ((Component)__instance).transform.Find("ingredient container result");
            MixedCraftGUI_OpenAsAlchemy.ResultPreviewDrawUnknown(ResultPreview);
            if (!UnityExtensions.IsUnityObject((Object)crafteryTransform))
                return;
            Transform transform1 = ((Component)crafteryTransform).transform.Find("ingredients/ingredient container/Base Item Cell");
            Transform transform2 = ((Component)crafteryTransform).transform.Find("ingredients/ingredient container (1)/Base Item Cell");
            Transform transform3 = ((Component)crafteryTransform).transform.Find("ingredients/ingredient container (2)/Base Item Cell");
            if (!UnityExtensions.IsUnityObject((Object)transform1) || !UnityExtensions.IsUnityObject((Object)transform2))
                return;
            BaseItemCellGUI component1 = ((Component)transform1).GetComponent<BaseItemCellGUI>();
            BaseItemCellGUI component2 = ((Component)transform2).GetComponent<BaseItemCellGUI>();
            BaseItemCellGUI baseItemCellGui = (BaseItemCellGUI)null;
            if (UnityExtensions.IsUnityObject((Object)transform3))
                baseItemCellGui = ((Component)transform3).GetComponent<BaseItemCellGUI>();
            if (!UnityExtensions.IsUnityObject((Object)component1) || !UnityExtensions.IsUnityObject((Object)component2))
            {
                MixedCraftGUI_OpenAsAlchemy.ResultPreviewDrawUnknown(ResultPreview);
            }
            else
            {
                string id1 = component1.item.id;
                string id2 = component2.item.id;
                string Ingredient3 = "empty";
                if (UnityExtensions.IsUnityObject((Object)baseItemCellGui))
                    Ingredient3 = baseItemCellGui.item.id;
                if (id1 == "empty" || id2 == "empty" || Ingredient3 == "empty" && objId == "mf_alchemy_craft_03")
                {
                    MixedCraftGUI_OpenAsAlchemy.ResultPreviewDrawUnknown(ResultPreview);
                }
                else
                {
                    string ItemID = ResearchedAlchemyRecipes.IsRecipeKnown(id1, id2, Ingredient3);
                    MixedCraftGUI_OpenAsAlchemy.ResultPreviewDrawItem(ResultPreview, ItemID);
                }
            }
        }
    }
}
