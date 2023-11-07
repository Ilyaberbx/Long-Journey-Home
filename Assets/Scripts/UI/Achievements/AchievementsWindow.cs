using System.Text;
using Infrastructure.Services.GlobalProgress;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.StaticData;
using TMPro;
using UI.Elements;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Achievements
{
    public class AchievementsWindow : WindowBase
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private TextMeshProUGUI _achievementsCount;
        [SerializeField] private VerticalLayoutGroup _layoutGroup;

        [Inject]
        private void Initialize(IGlobalProgressService globalService, IStaticDataService staticDataService)
        {
            UpdateAchievementsCountView(globalService, staticDataService);
        }

        private void UpdateAchievementsCountView(IGlobalProgressService globalService, IStaticDataService staticDataService)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(PassedAchievementsCount(globalService));
            builder.Append(" / ");
            builder.Append(staticDataService.GetAchievementsCount());
            _achievementsCount.text = builder.ToString();
        }

        private static int PassedAchievementsCount(IGlobalProgressService globalService)
            => globalService.GlobalPlayerProgress.Achievements.PassedAchievements.Count;

        protected override void SubscribeUpdates()
            => _closeButton.onClick.AddListener(Close);

        protected override void CleanUp()
        {
            base.CleanUp();
            _closeButton.onClick.RemoveListener(Close);
        }
    }
}