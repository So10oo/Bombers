using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapControl : MonoBehaviour
{
    //private float _defaultHeight;
    //private float _defaultWidth;

    //private void Start()
    //{
    //    _defaultHeight = Camera.main.orthographicSize;
    //    _defaultWidth = Camera.main.orthographicSize * Camera.main.aspect;
    //}

    private float _startAspect = 1920f / 1080f;

    //private float _defaultHeight;
    private float _defaultWidth;

    private void Awake()
    {
        //_defaultHeight = Camera.main.orthographicSize;
        _defaultWidth = Camera.main.orthographicSize * _startAspect;

        Camera.main.orthographicSize = _defaultWidth / Camera.main.aspect;
    }

    private void Update()
    {
        Camera.main.orthographicSize = _defaultWidth / Camera.main.aspect;
    }

}
