using Audio;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public abstract class ItemDisplayPanel : MonoBehaviour
    {
        [SerializeField] private Button backToTheRoomButton;
        [SerializeField] private Button storeButton;

        [SerializeField] private EventTrigger trigger;
        [SerializeField] private AudioClip clip;

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

        public void SetPanel(Action onStoreAction, Action onBackRoomAction)
        {
            storeButton.onClick.AddListener(() => onStoreAction?.Invoke());
            backToTheRoomButton.onClick.AddListener(() => onBackRoomAction?.Invoke());
        }

        private void OnDestroy()
        {
            backToTheRoomButton.onClick.RemoveAllListeners();
            storeButton.onClick.RemoveAllListeners();
        }
    }
}
