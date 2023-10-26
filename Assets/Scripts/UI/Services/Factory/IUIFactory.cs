using System.Threading.Tasks;
using Data;
using Infrastructure.Services;
using UI.Elements;
using UI.Ending;
using UI.Envelope;
using UI.GameOver;
using UI.Inventory;
using UI.Menu;
using UI.Pause;
using UI.Settings;
using UnityEngine;

namespace UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        Task<InventoryWindow> CreateInventory();
        Task CreateUIRoot();
        Task<MenuWindow> CreateMainMenu();
        Task<SettingsWindow> CreateSettingsWindow();
        Task<PauseWindow> CreatePauseMenu();
        Task<GameObject> CreateCurtain();
        Task<EnvelopeWindow> CreateEnvelopeWindow();
        Task<GameOverWindow> CreateGameOverMenu();
        Task<EndingWindow> CreateEndingWindow();
        
        Task<AchievementView> CreateAchievementView(AchievementType type);
    }
}