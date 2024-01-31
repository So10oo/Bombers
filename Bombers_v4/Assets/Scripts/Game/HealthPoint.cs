using System;
using UnityEngine;

public class HealthPoint : MonoBehaviour
{
    [SerializeField] protected byte MaxHp;

    protected bool isDead = false;

    protected virtual void OnHpChandged(byte hp)
    {
        _hp = hp;
        if (_hp <= 0 && !isDead)
        {
            Die();
        }
    }

    protected byte _hp;
    public byte Hp
    { 
        set
        {
            OnHpChandged(value);
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
        _hp = MaxHp;
    }

    public virtual void Die()
    {
        Destroy(gameObject);
        isDead = true;
    }
}
