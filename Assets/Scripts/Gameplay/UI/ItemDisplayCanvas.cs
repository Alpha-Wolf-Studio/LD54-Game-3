using Data;

namespace Gameplay.UI
{
    public class ItemDisplayCanvas : MonoBehaviourSingletonInScene<ItemDisplayCanvas>
    {
        private ItemDisplayPanel _currentItemDisplayPanel;
    
        public void SetCanvas(ItemDisplayPanel go, StoreData storeData)
        {
            _currentItemDisplayPanel = Instantiate(go, transform);

            if (storeData.CancelAction != null) storeData.CancelAction += CleanCanvas;
            else storeData.CancelAction = CleanCanvas;
            
            if (storeData.StoreAction != null) storeData.StoreAction += CleanCanvas;
            else storeData.StoreAction = CleanCanvas;
            
            _currentItemDisplayPanel.SetPanel(storeData);
        }

        private void CleanCanvas()
        {
            Destroy(_currentItemDisplayPanel.gameObject);
        }
    }
}