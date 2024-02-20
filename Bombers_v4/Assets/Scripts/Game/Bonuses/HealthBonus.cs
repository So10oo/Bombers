public class HealthBonus : Bonus
{
    public override void UpBonus(PlayerManager playerManager)
    {
        var healthPoint = playerManager.gameObject.GetComponent<HealthPoint>();
        healthPoint.Healing(1);
    }
}
                   