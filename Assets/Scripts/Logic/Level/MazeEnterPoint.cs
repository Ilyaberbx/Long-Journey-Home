using Extensions;
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

        private void DisableComponents(HeroFreezable heroFreezable)
        {
            heroFreezable.enabled = false;
            heroFreezable.GetComponent<HeroWindowOpener>().enabled = false;
            heroFreezable.GetComponent<HeroAttack>().enabled = false;
            heroFreezable.GetComponent<HeroLight>().enabled = false;
            heroFreezable.GetComponent<HeroPauseHandler>().enabled = false;
            heroFreezable.GetComponent<HeroHudWrapper>().Hide();
        }
    }
}