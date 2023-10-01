using Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Main_Menu
{
    public class UITextHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private FontData fontData;
        [SerializeField] private TextMeshProUGUI textToHighlight;
        
        private void Awake()
        {
            textToHighlight.font = fontData.fontAsset;
            textToHighlight.fontMaterial = fontData.normalMaterial;
        }

        private void SetHighlightMaterial() => textToHighlight.fontMaterial = fontData.highlightMaterial;
        private void SetNormalMaterial() => textToHighlight.fontMaterial = fontData.normalMaterial;

        public void OnPointerEnter(PointerEventData eventData) => SetHighlightMaterial();

        public void OnPointerExit(PointerEventData eventData) => SetNormalMaterial();
    }
}
