using UnityEngine;

public class AttitudeToFire : MonoBehaviour
{
    public AttitudeFire StatusAttitudeToFire;

    public enum AttitudeFire
    {
        Absorb,//���������
        Stops,//�������������
        Skips,//���������� 
    }
}
