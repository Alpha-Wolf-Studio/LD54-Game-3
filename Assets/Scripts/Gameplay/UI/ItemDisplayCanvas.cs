using System;

namespace Gameplay.UI
{
    public class ItemDisplayCanvas : MonoBehaviourSingletonInScene<ItemDisplayCanvas>
    {
        private ItemDisplayPanel _currentItemDisplayPanel;
    
        public void SetCanvas(ItemDisplayPanel go, Action onStoreAction = null, Action onBackToRoomAction = null)
        {
            _currentItemDisplayPanel = Instantiate(go, transform);

            if (onBackToRoomAction != null) onBackToRoomAction += CleanCanvas;
            else onBackToRoomAction = CleanCanvas;
            
            if (onStoreAction != null) onStoreAction += CleanCanvas;
            else onStoreAction = CleanCanvas;
            
            _currentItemDisplayPanel.SetPanel(onStoreAction, onBackToRoomAction);
        }

        private void CleanCanvas()
        {
            Destroy(_currentItemDisplayPanel.gameObject);
        }
    }
}