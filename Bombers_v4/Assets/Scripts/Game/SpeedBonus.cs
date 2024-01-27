using UnityEngine;

namespace Assets.Scripts
{
    public class SpeedBonus : Bonus
    {
        public override void UpBonus(CharacterTraits characterTraits)
        {
            characterTraits.speed += 0.3f;
        }
    }
}
