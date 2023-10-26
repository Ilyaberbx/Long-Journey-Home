using Extensions;
using Logic.Common;
using Logic.Enemy;
using Logic.Player;
using UnityEngine;

namespace Logic.Level
{
    public class MazeEnterPoint : MonoBehaviour
    {
        [SerializeField] private Light _lightToParent;
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private float _lightParentedHeight;

        private void Awake() 
            => _triggerObserver.OnTriggerEntered += Entered;

        private void Entered(Collider collider)
        {
            if (!collider.TryGetComponent(out HeroFreezable heroFreeze)) return;
            
            DisableComponents(heroFreeze);
            BlendLightToFeet(heroFreeze);
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