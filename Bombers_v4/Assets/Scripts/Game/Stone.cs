using UnityEngine;

public class Stone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var go = collision.gameObject;
        if (go.GetComponent<HealthPoint>() is HealthPoint healthPoint)
        {
            healthPoint.Die();
        }
        else if (go.GetComponent<Bonus>() is Bonus || go.GetComponent<Bomb>() is Bomb) 
        {
            Destroy(go);
        }
    }
}
