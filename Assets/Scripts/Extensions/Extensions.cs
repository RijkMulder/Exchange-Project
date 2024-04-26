using UnityEngine;

namespace Extensions.Transforms
{
    public static class Extensions
    {
        public static void ResetScale(this Transform trans)
        {
            trans.localScale = new Vector3(1f, 1f, 1f);
        }
        public static void SetPosition(this Transform trans, Vector3 pos)
        {
            trans.position = pos;
        }
    }
}

