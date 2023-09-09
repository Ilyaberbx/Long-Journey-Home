using UnityEngine;

namespace Logic.Player
{

    public class HeroCutSceneHandler : MonoBehaviour, ICutSceneHandler
    {
        [SerializeField] private HeroToggle _heroToggle;
        
        public bool IsInCutScene { get; private set; }
        
        public void HandleCutScene(bool isInCutScene)
        {
            IsInCutScene = isInCutScene;
            _heroToggle.Toggle(!isInCutScene);
        }
    }
}