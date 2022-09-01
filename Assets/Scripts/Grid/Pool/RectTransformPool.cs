using UnityEngine;

namespace Grid
{
    public class RectTransformPool : GameObjectPool<RectTransform>
    {
        protected override void OnElementGet(RectTransform obj)
        {
            obj.gameObject.SetActive(true);
        }

        protected override void OnElementReturn(RectTransform obj)
        {
            obj.gameObject.SetActive(false);
        }
    }
}