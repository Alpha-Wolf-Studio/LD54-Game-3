using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public abstract class ItemDisplayPanel : MonoBehaviour
    {
        [SerializeField] private Button backToTheRoomButton;
        [SerializeField] private Button storeButton;

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
