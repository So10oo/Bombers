using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class MatchWidth : MonoBehaviour
{
    public float sceneWidth = 11;
    public float sceneHeight = 7;

    Camera _camera;
    void Start()
    {
        _camera = GetComponent<Camera>();
    }


    private float unitsPerPixel;

    void Update()
    {
        if (Screen.height / (float)Screen.width > sceneHeight / sceneWidth)
           unitsPerPixel = sceneWidth / Screen.width;      
        else
            unitsPerPixel = sceneHeight / Screen.height;
      
        _camera.orthographicSize = 0.5f * unitsPerPixel * Screen.height;
    }
}