using System;
using UnityEngine;

public class HealthPoint : MonoBehaviour
{
    [SerializeField] protected byte MaxHp;
    protected bool isDead = false;

    public Action<byte> onHealth;
    public Action<byte> onDamage;

    public virtual void TakeDamage(byte damage)
    {
        int tempHp = Hp - damage;
        Hp = tempHp < 0 ? (byte)0 : (byte)tempHp;
        onDamage?.Invoke(Hp);
    }

    public virtual void Healing(byte healing)
    {
        int tempHp = Hp + healing;
        Hp = tempHp > MaxHp ? MaxHp : (byte)tempHp;
        onHealth?.Invoke(Hp);
    }

    private byte _hp;

    protected byte Hp
    {
        private set
        {
            _hp = value;
            if (_hp <= 0 && !isDead)
            {
                Die();
                isDead = true;
            }
        }
        get
        {
            return _hp;
        }
    }

    private void Start()
    {
        StartMonoBehaviour();
    }

    public virtual void StartMonoBehaviour()
    {
        Hp = MaxHp;
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
