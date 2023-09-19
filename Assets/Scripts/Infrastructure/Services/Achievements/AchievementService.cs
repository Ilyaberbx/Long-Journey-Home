using System.Collections.Generic;
using System.Threading.Tasks;
using Data;
using Infrastructure.Services.GlobalProgress;
using Infrastructure.Services.SaveLoad;
using UI.Elements;
using UI.Services.Factory;

namespace Infrastructure.Services.Achievements
{
    public class AchievementService : IAchievementService
    {
        private readonly IGlobalProgressService _globalProgressService;
        private readonly IUIFactory _uiFactory;
        private readonly ISaveLoadService _saveLoadService;

        public AchievementService(IGlobalProgressService globalProgressService,IUIFactory uiFactory,ISaveLoadService saveLoadService)
        {
            _globalProgressService = globalProgressService;
            _uiFactory = uiFactory;
            _saveLoadService = saveLoadService;
        }


        public async Task Achieve(AchievementType type)
        {
            if (PassedAchievements().Contains(type))
                return;
            
            PassedAchievements().Add(type);
            
            AchievementView view = await _uiFactory.CreateAchievementView(type);
            view.ShowAchieve();
            
            Save();
        }

        private void Save() 
            => _saveLoadService.SaveGlobalProgress(_globalProgressService.GlobalPlayerProgress);

        private List<AchievementType> PassedAchievements()
            => _globalProgressService.GlobalPlayerProgress.Achievements.PassedAchievements;
    }
}