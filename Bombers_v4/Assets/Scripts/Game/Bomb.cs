using Assets.Scripts;
using System;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject _fire;
    [SerializeField] private float _delay;
    [SerializeField] private protected LayerMask StopLayer;

    [HideInInspector] public int FlameLength;
    [HideInInspector] public event Action OnBlow;

    void Start() => Invoke(nameof(Blow), _delay);

    private void Blow()
    {
        Destroy(gameObject);
        var pos = transform.position;
        Instantiate(_fire, pos, Quaternion.identity);
        var (left, right, down, up) = (true, true, true, true);
        for (int i = 1; i <= FlameLength; i++)
        {
            if (left)
                left = InstantiateFire(pos, Vector3.left, i);
            if (right)
                right = InstantiateFire(pos, Vector3.right, i);
            if (down)
                down = InstantiateFire(pos, Vector3.down, i);
            if (up)
                up = InstantiateFire(pos, Vector3.up, i);
        }

        OnBlow?.Invoke();
    }

    private bool InstantiateFire(Vector3 pos, Vector3 direction, int i)
    {
        Vector3 _pos = pos + direction * i;
        var block = Physics2D.OverlapPoint(_pos, StopLayer);
        if (!block)
        {
            Instantiate(_fire, _pos, Quaternion.identity);
            return true;
        }
        else
        {
            var attitudeToFire = block.GetComponent<AttitudeToFire>();
            if (attitudeToFire != null)
            {
                switch (attitudeToFire.StatusAttitudeToFire)
                {
                    case AttitudeToFire.AttitudeFire.Absorb:
                        Instantiate(_fire, _pos, Quaternion.identity);
                        break;
                    case AttitudeToFire.AttitudeFire.Stops:
                        break;
                }
                return false;
            }
            else { return true; }
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, 0.4f);
        foreach (var collider in colliders)
        {
            if (collider.gameObject.GetComponent<MoveController>() is MoveController)
                return;
        }
        gameObject.layer = LayerMask.NameToLayer("Barriers");
    }
}

