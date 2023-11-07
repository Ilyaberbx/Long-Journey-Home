using Extensions;
using Infrastructure.Services.Hint;
using Logic.Common;
using Logic.Enemy;
using Logic.Player;
using UnityEngine;
using Zenject;

namespace Logic.Level
{
    public class MazeEnterPoint : MonoBehaviour
    {
        [SerializeField] private Light _lightToParent;
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private float _lightParentedHeight;
        private IHintService _hintService;

        [Inject]
        public void Construct(IHintService hintService) 
            => _hintService = hintService;

        private void Awake() 
            => _triggerObserver.OnTriggerEntered += Entered;

        private void Entered(Collider collider)
        {
            if (!collider.TryGetComponent(out HeroFreezable heroFreeze)) return;
            
            DisableComponents(heroFreeze);
            BlendLightToFeet(heroFreeze);
            _hintService.ShowHint("Interact: E");
        }

        private void BlendLightToFeet(HeroFreezable heroFreezable)
        {
            _lightToParent.transform.SetParent(heroFreezable.transform);
            _lightToParent.transform.localPosition = Vector3.zero.AddY(_lightParentedHeight);
        }

        private void DisableComponents(HeroFreezable hero)
        {
            hero.enabled = false;
            hero.GetComponent<HeroWindowOpener>().enabled = false;
            hero.GetComponent<HeroAttack>().enabled = false;
            hero.GetComponent<HeroLight>().enabled = false;
            hero.GetComponent<HeroPauseHandler>().IsHandlePause = false;
            hero.GetComponent<HeroHudWrapper>().Hide();
            hero.GetComponent<HeroCutsSceneProcessor>().IsCutSceneActive = true;
        }
    }
}