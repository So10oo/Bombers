using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventsMonoBeheviour : MonoBehaviour
{
    [SerializeField] UnityEvent _startsUnityEvent;

    [SerializeField] List<EventUpdateMonoBeheviour> _updateUnityEvent;

    void Start()
    {
        _startsUnityEvent?.Invoke();
    }

}

[SerializeField]
public class EventUpdateMonoBeheviour
{
    [SerializeField] UnityEvent unityEvent;
    [SerializeField] float timeUpdate;
}

//[SerializeField]
//public class EventStartMonoBeheviour
//{
//    [SerializeField] UnityEvent unityEvent;
//}

