using Harmony;
using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AlchemyResearch
{
    [HarmonyPatch(typeof(MixedCraftGUI), "OpenAsAlchemy", new Type[] { typeof(WorldGameObject), typeof(string) })]
    public class MixedCraftGUI_OpenAsAlchemy
    {
        public const string AlchemyWorkbench1ObjID = "mf_alchemy_craft_02";
        public const string AlchemyWorkbench2ObjID = "mf_alchemy_craft_03";

        [HarmonyPostfix]
        public static void Patch(
          MixedCraftGUI __instance,
          WorldGameObject craftery_wgo,
          string preset_name)
        {
            AlchemyRecipe.Initialize();
            Transform crafteryTransform = MixedCraftGUI_OpenAsAlchemy.GetCrafteryTransform(((Component)__instance).transform, craftery_wgo.obj_id);
            if ((Object)((Component)__instance).transform.Find("ingredient container result") != (Object)null)
                return;
            Transform transform1 = ((Component)crafteryTransform).transform.Find("ingredients/ingredient container (1)");
            GameObject gameObject1 = Object.Instantiate<GameObject>(((Component)transform1).gameObject);
            ((Object)gameObject1).name = "ingredient container result";
            Transform transform2 = gameObject1.transform;
            ((Component)transform2).transform.SetParent(((Component)__instance).transform, false);
            ((Component)transform2).transform.position = Vector3.zero;
            ((Component)transform2).transform.localPosition = new Vector3(0.0f, -40f, 0.0f);
            if (craftery_wgo.obj_id == "mf_alchemy_craft_03")
                ((Component)transform2).transform.localPosition = new Vector3(transform1.localPosition.x, -40f, 0.0f);
            MixedCraftGUI_OpenAsAlchemy.ResultPreviewDrawUnknown(gameObject1.transform);
            GameObject gameObject2 = Object.Instantiate<GameObject>(((Component)((Component)crafteryTransform).transform.Find("ingredients/ingredient container/Base Item Cell/x2 container/counter")).gameObject);
            ((Object)gameObject2).name = "label result";
            gameObject2.transform.SetParent(gameObject1.transform, false);
            UILabel component = gameObject2.GetComponent<UILabel>();
            component.text = MainPatcher.ResultPreviewText;
            ((UIWidget)component).pivot = (UIWidget.Pivot)4;
            ((UIWidget)component).color = new Color(0.937f, 0.87f, 0.733f);
            component.overflowWidth = 0;
            component.overflowMethod = (UILabel.Overflow)0;
            ((UIRect)component).topAnchor.target = gameObject1.transform;
            ((UIRect)component).bottomAnchor.target = gameObject1.transform;
            ((UIRect)component).rightAnchor.target = gameObject1.transform;
            ((UIRect)component).leftAnchor.target = gameObject1.transform;
            ((UIRect)component).leftAnchor.relative = -10f;
            ((UIRect)component).rightAnchor.relative = 10f;
            ((UIRect)component).topAnchor.relative = -9f;
            ((UIRect)component).bottomAnchor.relative = -10f;
        }

        public static Transform GetCrafteryTransform(
          Transform CraftingStation,
          string CrafteryWGOObjectID)
        {
            if (CrafteryWGOObjectID == "mf_alchemy_craft_02")
                return CraftingStation.Find("alchemy_craft_02");
            return CrafteryWGOObjectID == "mf_alchemy_craft_03" ? CraftingStation.Find("alchemy_craft_03") : (Transform)null;
        }

        public static void ResultPreviewDrawUnknown(Transform ResultPreview)
        {
            if (ResultPreview == null || !(ResultPreview is Object))
                return;
            BaseItemCellGUI componentInChildren = ((Component)ResultPreview).GetComponentInChildren<BaseItemCellGUI>();
            if (componentInChildren == null || !(componentInChildren is Object))
                return;
            componentInChildren.DrawEmpty();
            componentInChildren.DrawUnknown();
        }

        public static void ResultPreviewDrawItem(Transform ResultPreview, string ItemID)
        {
            BaseItemCellGUI componentInChildren = ((Component)ResultPreview).GetComponentInChildren<BaseItemCellGUI>();
            componentInChildren.DrawEmpty();
            componentInChildren.DrawItem(ItemID, 1, true, false, false);
        }
    }
}
