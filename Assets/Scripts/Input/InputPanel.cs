using System;
using UnityEngine;
using UnityEngine.UI;
using Validation;
using Extensions;

namespace Input
{
    public class InputPanel : MonoBehaviour
    {
        [SerializeField] private ValidatableInputField widthInputField;
        [SerializeField] private ValidatableInputField heightInputField;
        [SerializeField] private Button m_GenerateButton;
        [SerializeField] private Button m_ShuffleButton;

        public event Action<int, int> OnGenerateClick = delegate { };
        public event Action OnShuffleClick = delegate { };

        private void GenerateBttnHandler()
        {
            int w, h;
            if (!widthInputField.TryGetValidValue(GetPositiveInteger, out w)) return;
            if (!heightInputField.TryGetValidValue(GetPositiveInteger, out h)) return;
            OnGenerateClick(w, h);
        }

        private void ShuffleBttnHandler()
        {
            OnShuffleClick();
        }

        private void OnEnable()
        {
            m_GenerateButton.onClick.AddListener(GenerateBttnHandler);
            m_ShuffleButton.onClick.AddListener(ShuffleBttnHandler);
        }

        private void OnDisable()
        {
            m_GenerateButton.onClick.RemoveListener(GenerateBttnHandler);
            m_ShuffleButton.onClick.RemoveListener(ShuffleBttnHandler);
        }

        private bool GetPositiveInteger(string input, out int validValue)
        {
            bool result = int.TryParse(input, out validValue);
            result &= validValue > 0;
            return result;
        }
    }
}