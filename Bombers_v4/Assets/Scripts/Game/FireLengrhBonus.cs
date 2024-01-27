using UnityEngine;

namespace Assets.Scripts
{
    public class FireLengrhBonus : Bonus
    {
        public override void UpBonus(CharacterTraits characterTraits)
        {
            characterTraits.flameLength++;
        }

    }
}
