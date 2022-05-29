using Object = UnityEngine.Object;

namespace AlchemyResearch
{
    public static class UnityExtensions
    {
        public static bool IsUnityObject(Object obj1)
        {
            if (obj1 == null) return false;
            return obj1 is Object;
        }
    }
}
