using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.MusicService;
using Logic.Enemy;
using UI.Services.Window;
using UnityEngine;

namespace Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string EnemyStaticDataGroupAddress = "EnemiesStaticData";
        private const string LevelStaticDataGroupAddress = "LevelStaticData";
        private const string WindowsStaticDataAddress = "WindowsData";
        private const string AchievementsStaticDataAddress = "AchievementsConfig";
        private const string MusicStaticDataAddress = "MusicStaticData";
        private const string AmbienceStaticDataAddress = "AmbienceStaticData";
        private Dictionary<EnemyType, EnemyData> _enemies;
        private Dictionary<string, LevelData> _levels;
        private Dictionary<WindowType, WindowConfig> _windows;
        private Dictionary<AchievementType, AchievementData> _achievements;
        private Dictionary<AmbienceType, AmbienceData> _ambience;
        private Dictionary<MusicType, MusicData> _music;
        private readonly IAssetProvider _assetProvider;


        public StaticDataService(IAssetProvider assetProvider)
            => _assetProvider = assetProvider;

        public async Task Load()
        {
            _enemies = await LoadEnemiesData();
            _levels = await LoadLevelsData();
            _windows = await LoadWindowsData();
            _achievements = await LoadAchievementsData();
            _ambience = await LoadAmbienceData();
            _music = await LoadMusicData();
        }

        private async Task<Dictionary<MusicType, MusicData>> LoadMusicData()
        {
            IList<MusicData> handle = await _assetProvider.LoadAll<MusicData>(MusicStaticDataAddress);
            return handle.ToDictionary(x => x.Type, x => x);
        }

        private async Task<Dictionary<AmbienceType, AmbienceData>> LoadAmbienceData()
        {
            IList<AmbienceData> handle = await _assetProvider.LoadAll<AmbienceData>(AmbienceStaticDataAddress);
            return handle.ToDictionary(x => x.Type, x => x);
        }

        private async Task<Dictionary<AchievementType, AchievementData>> LoadAchievementsData()
        {
            AchievementsConfig handle = await _assetProvider.Load<AchievementsConfig>(AchievementsStaticDataAddress);
            return handle.Config.ToDictionary(x => x.Type, x => x);
        }

        private async Task<Dictionary<WindowType, WindowConfig>> LoadWindowsData()
        {
            WindowsStaticData handle = await _assetProvider.Load<WindowsStaticData>(WindowsStaticDataAddress);
            return handle.Configs.ToDictionary(x => x.Type, x => x);
        }


        private async Task<Dictionary<string, LevelData>> LoadLevelsData()
        {
            IList<LevelData> handle = await _assetProvider.LoadAll<LevelData>(LevelStaticDataGroupAddress);
            return handle.ToDictionary(x => x.LevelKey, x => x);
        }

        private async Task<Dictionary<EnemyType, EnemyData>> LoadEnemiesData()
        {
            IList<EnemyData> handle = await _assetProvider.LoadAll<EnemyData>(EnemyStaticDataGroupAddress);
            return handle.ToDictionary(x => x.Type, x => x);
        }

        public EnemyData GetEnemyDataByType(EnemyType type)
            => _enemies.TryGetValue(type, out EnemyData data)
                ? data
                : null;


        public LevelData GetLevelData(string sceneKey)
        {
            foreach (KeyValuePair<string, LevelData> pair in _levels) 
                Debug.Log(pair.Key + " x " + pair.Value);
            
            return _levels.TryGetValue(sceneKey, out LevelData data)
                ? data
                : null;
        }

        public WindowConfig GetWindowData(WindowType windowType)
            => _windows.TryGetValue(windowType, out WindowConfig config)
                ? config
                : null;

        public AmbienceData GetAmbienceData(AmbienceType type) 
            => _ambience.TryGetValue(type, out AmbienceData data)
                ? data
                : null;

        public MusicData GetMusicData(MusicType type)
            => _music.TryGetValue(type, out MusicData data)
                ? data
                : null;


        public AchievementData GetAchievementData(AchievementType type) =>
            _achievements.TryGetValue(type, out AchievementData config)
                ? config
                : null;
    }
}