using UnityEngine;

namespace Assets.Scripts
{
    public class SpeedBonus : Bonus
    {
        public override void UpBonus(PlayerManager playerManager)
        {
            playerManager.CharacterTraits.Speed += 0.3f;
        }
    }
}
