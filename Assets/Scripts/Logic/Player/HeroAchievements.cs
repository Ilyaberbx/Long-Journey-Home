using Data;
using Infrastructure.Services.Achievements;
using Logic.Enemy;
using Logic.Triggers;
using UnityEngine;
using Zenject;

namespace Logic.Player
{
    public class HeroAchievements : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        private IAchievementService _achievementService;
        
        [Inject]
        public void Construct(IAchievementService achievementService) 
            => _achievementService = achievementService;

        private void Awake() 
            => _triggerObserver.OnTriggerEntered += ProcessTriggered;

        private void OnDestroy()
            => _triggerObserver.OnTriggerEntered -= ProcessTriggered;

        private void ProcessTriggered(Collider collider)
        {
            if(!collider.TryGetComponent(out AchievementTrigger achievement))
                return;

            Achieve(achievement.Type);
        }

        private void Achieve(AchievementType type) 
            => _achievementService.Achieve(type);
    }
}