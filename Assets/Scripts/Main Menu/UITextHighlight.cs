using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Main_Menu
{
    public class UITextHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Material normalMaterial;
        [SerializeField] private Material highlightMaterial;
        [SerializeField] private TextMeshProUGUI textToHighlight;
        
        private void Awake()
        {
            textToHighlight.fontMaterial = normalMaterial;
        }

        private void SetHighlightMaterial() => textToHighlight.fontMaterial = highlightMaterial;
        private void SetNormalMaterial() => textToHighlight.fontMaterial = normalMaterial;

        public void OnPointerEnter(PointerEventData eventData) => SetHighlightMaterial();

        public void OnPointerExit(PointerEventData eventData) => SetNormalMaterial();
    }
}
