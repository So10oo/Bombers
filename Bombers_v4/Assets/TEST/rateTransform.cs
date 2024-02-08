using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rateTransform : MonoBehaviour
{
    public Transform target;
    float rateTime = 0.1f;
    float currentTime = 0;

    private void Start()
    {
        _currenPpositionNetwork = transform.position;
        _lastPositionNetwork = transform.position;
    }

    void Update()
    {
        if (currentTime >= rateTime)
        {
            currentTime = 0;
            acceptance();
        }
        else
        {
            currentTime += Time.deltaTime;
        }


        timeSerialization += Time.deltaTime;
        transform.position = Vector3.Lerp(new Vector3(_lastPositionNetwork.x,transform.position.y), new Vector3(_currenPpositionNetwork.x, transform.position.y)  , timeSerialization / rateTime);
    }



    Vector2 _currenPpositionNetwork;
    Vector2 _lastPositionNetwork;
    //PredictionTime predictionTime;
    float timeSerialization = 0;


    public void acceptance()
    {
        float x = target.transform.position.x;
        float y = target.transform.position.y;

        _currenPpositionNetwork = new Vector2(x, transform.position.y);
        //transform.position = _currenPpositionNetwork;
        //var lastPosition = (Vector2)transform.position;
        var directions = _currenPpositionNetwork - _lastPositionNetwork;
        _lastPositionNetwork = _currenPpositionNetwork;
        _currenPpositionNetwork += directions;
        timeSerialization = 0f;
        //pos = new Vector2(x, y);

    }
}
