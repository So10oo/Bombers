using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] byte _damage;

    public byte Damage => _damage;
}
