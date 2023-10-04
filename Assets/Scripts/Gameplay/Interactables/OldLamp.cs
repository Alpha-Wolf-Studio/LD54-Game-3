using Data;
using Gameplay.Components;
using Gameplay.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Gameplay.Interactables
{
    public class OldLamp : ItemDisplayPanel, IPointerClickHandler
    {
        [Header("Lamp Specific Configurations")]
        [SerializeField] private Image lampImage;
        [SerializeField] private Sprite[] sprites;
        private int _spriteIndex = 0;

        private ItemImageSwapComponent _itemImageSwapComponent;
        
        public override void SetPanel(StoreData data, ItemComponent itemComponent)
        {
            base.SetPanel(data, itemComponent);
            _itemImageSwapComponent = itemComponent.GetComponent<ItemImageSwapComponent>();
            SetLight(_itemImageSwapComponent.CurrentIndex);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            SwitchLight();
        }

        private void SetLight(int index)
        {
            _spriteIndex = index;
            lampImage.sprite = sprites[_spriteIndex];
        }
        
        private void SwitchLight()
        {
            _spriteIndex++;

            if (_spriteIndex >= sprites.Length)
                _spriteIndex = 0;
            
            lampImage.sprite = sprites[_spriteIndex];
            
            _itemImageSwapComponent.Swap(_spriteIndex);
        }
    }
}
