using System;
using UnityEngine;

public class HealthPoint : MonoBehaviour
{
    [SerializeField] float MaxHp;
    
    protected event Action<float> OnDamage;

    bool isDead = false;
    protected bool isInvulnerable = false;

    private float _hp;
    public float Hp
    { 
        set
        {
            if (_hp > value && !isInvulnerable)
            {       
                _hp = value; 
                if (_hp <= 0 && !isDead)
                {
                    Die();
                    isDead = true;
                }
                OnDamage?.Invoke(_hp / MaxHp);
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
        _hp = MaxHp;
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
