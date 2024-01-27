using UnityEngine;

public class BombQuantityBonus : Bonus
{
    public override void UpBonus(CharacterTraits characterTraits)
    {
        characterTraits.bombQuantity++;
    }


}
