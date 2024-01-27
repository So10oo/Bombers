using UnityEngine;

public class AttitudeToFire : MonoBehaviour
{
    public AttitudeFire StatusAttitudeToFire;

    public enum AttitudeFire
    {
        //Skips,//пропускает 
        Absorb,//впитывает
        Stops,//останавливает
    }
}
