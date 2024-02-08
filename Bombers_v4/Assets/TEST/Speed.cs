using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : MonoBehaviour
{
    public float speed;

    void Update()
    {
        var x = transform.position.x + speed * Time.deltaTime;
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
}
