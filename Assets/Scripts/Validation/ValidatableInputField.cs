using TMPro;
using UnityEngine;
using System.Collections.Generic;

namespace Validation
{
    [RequireComponent(typeof(TMP_InputField))]
    public class ValidatableInputField : MonoBehaviour
    {
        public delegate bool TryGetValidValueDelegate<T>(string input, out T validValue);
        private TMP_InputField inputField;
        private List<IValidationFX> fx = new List<IValidationFX>();

        private void Awake()
        {
            inputField = GetComponent<TMP_InputField>();
            GetComponents(fx);
            inputField.onValueChanged.AddListener(OnValueChange);
        }

        public bool TryGetValidValue<T>(TryGetValidValueDelegate<T> isValid, out T validValue)
        {
            bool result = isValid(inputField.text, out validValue);

            if (!result) ShowError();

            return result;
        }

        protected virtual void ShowError()
        {
            fx.ForEach(x => x.ShowError());
        }

        protected virtual void ShowDefault()
        {
            fx.ForEach(x => x.ShowDefault());
        }

        private void OnValueChange(string value)
        {
            ShowDefault();
        }
    }
}