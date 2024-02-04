public class BombQuantityBonus : Bonus
{
    public override void UpBonus(PlayerManager playerManager)
    {
        playerManager.CharacterTraits.BombQuantity++;
    }


}
