using UnityEngine;

public class Stone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<HealthPoint>() is HealthPoint healthPoint)
        {
            healthPoint.Die();
        }
    }
}
