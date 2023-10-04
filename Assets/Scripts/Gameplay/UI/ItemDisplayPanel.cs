using Audio;
using Data;
using Gameplay.Components;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public abstract class ItemDisplayPanel : MonoBehaviour
    {
        [SerializeField] private Button backToTheRoomButton;
        [SerializeField] private Button storeButton;
        
        [Header("Store Tooltip Configuration")]
        [SerializeField] private TextMeshProUGUI storeTooltipText;
        [SerializeField] private string canStoreKey;
        [SerializeField] private string cantStoreKey;

        [SerializeField] private EventTrigger trigger;
        [SerializeField] private AudioClip clip;

        protected ItemComponent ItemComponent;
        
        public virtual void Awake()
        {
            if(clip != null && trigger != null)
            {
                var pointerEvent = new EventTrigger.Entry();
                pointerEvent.eventID = EventTriggerType.PointerDown;
                pointerEvent.callback.AddListener((e) => AudioManager.Instance.PlaySfx(clip));
                trigger.triggers.Add(pointerEvent);
            }
        }

        public virtual void SetPanel(StoreData data, ItemComponent itemComponent)
        {
            storeButton.onClick.AddListener(() => data.StoreAction?.Invoke());
            backToTheRoomButton.onClick.AddListener(() => data.CancelAction?.Invoke());

            storeButton.interactable = data.CanBeStored;
            storeTooltipText.color = data.CanBeStored ? Color.white : Color.red;
            storeTooltipText.text = data.CanBeStored ? Loc.ReplaceKey(canStoreKey) : Loc.ReplaceKey(cantStoreKey);
            
            ItemComponent = itemComponent;
        }

        private void OnDestroy()
        {
            backToTheRoomButton.onClick.RemoveAllListeners();
            storeButton.onClick.RemoveAllListeners();
        }
    }
}
