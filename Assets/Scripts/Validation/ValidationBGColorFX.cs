using TMPro;
using UnityEngine;

namespace Validation
{
    [RequireComponent(typeof(TMP_InputField))]
    public class ValidationBGColorFX : MonoBehaviour, IValidationFX
    {
        [SerializeField] private Color32 invalidImageColor = Color.red;
        private Color32 defaultImageColor = Color.red;
        private TMP_InputField inputField;
        void Awake()
        {
            inputField = GetComponent<TMP_InputField>();
            defaultImageColor = inputField.image.color;
        }

        public void ShowError()
        {
            inputField.image.color = invalidImageColor;
        }

        public void ShowDefault()
        {
            inputField.image.color = defaultImageColor;
        }
    }
}