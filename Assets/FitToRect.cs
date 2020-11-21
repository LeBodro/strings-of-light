using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FitToRect : MonoBehaviour
{
    [SerializeField] private Camera viewport = null;
    [SerializeField] private RectTransform rect = null;
    [SerializeField] private Transform[] walls = null;

    void Reset()
    {
        viewport = GetComponent<Camera>();
    }

    void Start()
    {
        float height = rect.rect.height;
        float heightDelta = rect.sizeDelta.y;
        
        float ratio = Mathf.Abs(heightDelta / height);
        Debug.Log(ratio);
        viewport.orthographicSize /= 1 - ratio;
        foreach (Transform wall in walls)
        {
            wall.localPosition /= 1 - ratio * 0.5f;
        }
    }
}
