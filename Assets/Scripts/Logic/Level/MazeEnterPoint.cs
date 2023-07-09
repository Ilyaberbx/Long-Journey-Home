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
            if (!collider.TryGetComponent(out HeroFreeze heroFreeze)) return;
            
            DisableComponents(heroFreeze);
            BlendLightToFeet(heroFreeze);
        }

        private void BlendLightToFeet(HeroFreeze heroFreeze)
        {
            _lightToParent.transform.SetParent(heroFreeze.transform);
            _lightToParent.transform.localPosition = Vector3.zero.AddY(_lightParentedHeight);
        }

        private void DisableComponents(HeroFreeze heroFreeze)
        {
            heroFreeze.enabled = false;
            heroFreeze.GetComponent<HeroWindowOpener>().enabled = false;
            heroFreeze.GetComponent<HeroAttack>().enabled = false;
            heroFreeze.GetComponent<HeroLight>().enabled = false;
            heroFreeze.GetComponent<HeroPause>().enabled = false;
            heroFreeze.GetComponent<HeroHudWrapper>().Hide();
        }
    }
}