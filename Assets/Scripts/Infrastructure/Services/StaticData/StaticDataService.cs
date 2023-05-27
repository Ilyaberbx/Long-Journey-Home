using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Infrastructure.Services.AssetManagement;
using Logic.Enemy;
using UI.Services.Window;

namespace Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string EnemyStaticDataGroupAddress = "EnemiesStaticData";
        private const string LevelStaticDataGroupAddress = "LevelStaticData";
        private const string WindowsStaticDataAddress = "WindowsData";
        private Dictionary<EnemyType, EnemyData> _enemies;
        private Dictionary<string, LevelData> _levels;
        private Dictionary<WindowType, WindowConfig> _windows;
        private readonly IAssetProvider _assetProvider;

        public StaticDataService(IAssetProvider assetProvider)
            => _assetProvider = assetProvider;

        public async Task Load()
        {
            _enemies = await LoadEnemiesData();
            _levels = await LoadLevelsData();
            _windows = await LoadWindowData();
        }


        private async Task<Dictionary<WindowType, WindowConfig>> LoadWindowData()
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
            => _levels.TryGetValue(sceneKey, out LevelData data)
                ? data
                : null;

        public WindowConfig GetWindowData(WindowType windowType)
            => _windows.TryGetValue(windowType, out WindowConfig config)
                ? config
                : null;
    }
}