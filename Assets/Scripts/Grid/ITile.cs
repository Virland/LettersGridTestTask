using UnityEngine;

namespace Grid
{
    public interface ITile
    {
        void SetTarget(RectTransform targetTransform);
        void SetActive(bool enabled);
        void FitTarget(float progress);
    }
}