using UnityEngine;

namespace Assets.Scripts
{
    public class FireLengrhBonus : Bonus
    {
        public override void UpBonus(PlayerManager playerManager)
        {
            playerManager.CharacterTraits.FlameLength++;
        }

    }
}
