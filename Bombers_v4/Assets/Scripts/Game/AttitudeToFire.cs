using UnityEngine;

public class AttitudeToFire : MonoBehaviour
{
    public AttitudeFire StatusAttitudeToFire;

    public enum AttitudeFire
    {
        //Skips,//���������� 
        Absorb,//���������
        Stops,//�������������
    }
}
