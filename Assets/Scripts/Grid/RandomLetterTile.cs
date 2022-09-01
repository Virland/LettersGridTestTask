using TMPro;
using UnityEngine;
using Extensions;

namespace Grid
{
    public class RandomLetterTile : MonoBehaviour, ITile
    {
        [SerializeField] private TMP_Text m_Label;
        [SerializeField] private RectTransform m_Self;

        private RectTransform m_Target;
        private Vector3 m_StartPosition;
        private Vector3 m_StartScale;
        private float m_ProgressCache = 1f;
        private Vector3 m_TargetPosCache = Vector3.zero;

        public void Awake()
        {
            m_Label.text = GetRandomLetter();
        }

        public void SetTarget(RectTransform targetTransform)
        {
            var firstCall = m_Target == null;
            m_Target = targetTransform;
            m_Self.SetParent(m_Target);

            m_StartPosition = m_Self.localPosition;
            m_StartScale = m_Self.localScale;
        }

        public void SetActive(bool enabled) => gameObject.SetActive(enabled);

        public void FitTarget(float progress)
        {
            m_ProgressCache = progress;
            m_Self.localPosition = Vector3.Lerp(m_StartPosition, m_TargetPosCache, progress);
            m_Self.localScale = Vector2.Lerp(m_StartScale, m_Target.sizeDelta / m_Self.sizeDelta, progress);
        }

        private string GetRandomLetter()
        {
            return ((char)Random.Range('A', 'Z')).ToString();
        }

        private void OnRectTransformDimensionsChange()
        {
            FitTarget(m_ProgressCache);
        }
    }
}