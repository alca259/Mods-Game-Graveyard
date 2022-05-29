using System.IO;
using UnityEngine;

namespace AlchemyResearch
{
    public class Logg
    {
        private static string path = "F:\\Dev\\Graveyard Keeper Logs\\Mod Output.txt";

        public static void Log(string message)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(path)))
                {
                    Debug.Log(message);
                }

                if (!File.Exists(Logg.path))
                {
                    using (StreamWriter text = File.CreateText(path))
                        text.WriteLine(message);
                }
                else
                {
                    using (StreamWriter streamWriter = File.AppendText(path))
                        streamWriter.WriteLine(message);
                }
            }
            catch (System.Exception)
            {
            }
        }
    }
}
