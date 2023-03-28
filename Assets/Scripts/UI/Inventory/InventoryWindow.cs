using UI.Elements;

namespace UI.Inventory
{
    public class InventoryWindow : WindowBase
    {
        protected override void SubscribeUpdates()
        {
            _progressService.PlayerProgress.InventoryData.OnStateChanged += UpdateUI;
        }

        protected override void CleanUp()
        {
            base.CleanUp();
            _progressService.PlayerProgress.InventoryData.OnStateChanged -= UpdateUI;
        }

        private void UpdateUI()
        {
            
        }
    }
}