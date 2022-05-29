using Harmony;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace AlchemyResearch
{
    public class MainPatcher
    {
        public static string ResultPreviewText = "Result";
        public static readonly string configFilePathAndName = Application.dataPath + "/../QMods/AlchemyResearch/config.txt";
        public static readonly string KnownRecipesFilePathAndName = Application.dataPath + "/../QMods/AlchemyResearch/Known Recipes.txt";
        public static readonly char ParameterSeparator = '|';
        public static readonly string ParameterComment = "#";
        public static readonly string ParameterSectionBegin = "[";
        public static readonly string ParameterSectionEnd = "]";
        public const string ParameterResultPreviewText = "ResultPreviewText";

        public static void Patch()
        {
            HarmonyInstance.Create("com.graveyardkeeper.urbanvibes.alchemyresearch").PatchAll(Assembly.GetExecutingAssembly());
            AlchemyResearch.Reflection.Initialization();
            MainPatcher.ReadParametersFromFile();
        }

        public static void ReadParametersFromFile()
        {
            string empty = string.Empty;
            string[] strArray1;
            try
            {
                strArray1 = File.ReadAllLines(MainPatcher.configFilePathAndName);
            }
            catch (Exception ex)
            {
                return;
            }
            if (strArray1 == null || strArray1.Length == 0)
                return;
            foreach (string str1 in strArray1)
            {
                string str2 = str1.Trim();
                if (!string.IsNullOrEmpty(str2) && !str2.StartsWith(MainPatcher.ParameterComment))
                {
                    string[] strArray2 = str2.Split(MainPatcher.ParameterSeparator);
                    if (strArray2.Length >= 2 && strArray2[0].Trim() == "ResultPreviewText" && !string.IsNullOrEmpty(strArray2[1].Trim()))
                    {
                        MainPatcher.ResultPreviewText = strArray2[1].Trim();
                        break;
                    }
                }
            }
        }
    }
}
