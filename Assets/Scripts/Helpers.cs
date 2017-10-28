using UnityEngine;

namespace Assets.Scripts
{
    public static class Helpers
    {
        public static T GetObjectWithTag<T>(string objectTag)
        {
            return GameObject.FindGameObjectWithTag(objectTag).GetComponent<T>();
        }
    }
}
