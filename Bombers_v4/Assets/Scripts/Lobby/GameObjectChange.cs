using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectChange : MonoBehaviour
{

    public void ChangeActive(GameObject go)
    {
        go.SetActive(!go.activeSelf);
    }
}
