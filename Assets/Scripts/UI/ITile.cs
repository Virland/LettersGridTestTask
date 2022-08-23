using UnityEngine;

namespace UI
{
    public interface ITile
    {
        void SetTarget(RectTransform targetTransform);
        void SetActive(bool enabled);
        void FitTarget(float progress);
    }
}