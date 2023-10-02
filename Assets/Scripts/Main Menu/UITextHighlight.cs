using Audio;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Main_Menu
{
    public class UITextHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [FormerlySerializedAs("fontData")] [SerializeField] private UIData uiData;
        [SerializeField] private TextMeshProUGUI textToHighlight;
        
        private void Awake()
        {
            textToHighlight.font = uiData.fontAsset;
            textToHighlight.fontMaterial = uiData.normalMaterial;
        }

        private void SetHighlightMaterial() => textToHighlight.fontMaterial = uiData.highlightMaterial;
        private void SetNormalMaterial() => textToHighlight.fontMaterial = uiData.normalMaterial;

        private void PlayHighlightAudio() => AudioManager.Instance.PlaySfx(uiData.highlightAudio);
        private void PlayClickAudio() => AudioManager.Instance.PlaySfx(uiData.selectAudio);
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            PlayHighlightAudio();
            SetHighlightMaterial();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SetNormalMaterial();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            PlayClickAudio();
        }
    }
}
