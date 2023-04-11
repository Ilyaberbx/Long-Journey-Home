using Logic.Player;
using UnityEngine;

namespace Logic.Inventory.Modifiers
{
    [CreateAssetMenu(fileName = "StatModifier", menuName = "Modifiers/Light", order = 0)]
    public class CharacterLightModifier : CharacterStatModifier
    {
        public override void AffectCharacter(GameObject character, float value) 
            => character.GetComponent<IHeroLight>().CurrentIntensity += value;
    }
}