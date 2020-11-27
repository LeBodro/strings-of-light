// ©2020 Maxime Boudreault. All Rights Reserved.

using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FitToRect : MonoBehaviour
{
    [SerializeField] private Camera viewport = null;
    [SerializeField] private RectTransform rect = null;
    [SerializeField] private Transform[] walls = null;

    private bool _wasFullscreen;
    
    void Reset()
    {
        viewport = GetComponent<Camera>();
    }

    void Start()
    {
        Fit();
    }

    void Fit()
    {
        _wasFullscreen = Screen.fullScreen;
        
        float height = rect.rect.height;
        float heightDelta = rect.sizeDelta.y;
        
        float ratio = Mathf.Abs(heightDelta / height);
        viewport.orthographicSize /= 1 - ratio;
        foreach (Transform wall in walls)
        {
            wall.localPosition /= 1 - ratio * 0.5f;
        }
    }

    void Update()
    {
        if(Screen.fullScreen != _wasFullscreen)
        {
            Fit();
        }
    }
}
