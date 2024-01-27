using UnityEngine;

public class Fire : Damager
{
    [SerializeField] private float _delay;

    private void Start()
    {
        Invoke(nameof(DestroyGameObject), _delay);
    }

    void DestroyGameObject() => Destroy(gameObject);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var gameobject = collision.gameObject.GetComponent<HealthPoint>();
        if (gameobject != null)
            gameobject.Hp -= Damage;
    }
    

}
