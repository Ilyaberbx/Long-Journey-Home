using UnityEngine;

namespace Logic.Inventory.Modifiers
{
    public abstract class CharacterStatModifier : ScriptableObject
    {
        public abstract void AffectCharacter(GameObject character, float value);
    }
}