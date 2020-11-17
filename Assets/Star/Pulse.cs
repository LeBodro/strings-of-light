// ©2020 Maxime Boudreault. All Rights Reserved.

using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Pulse : MonoBehaviour
{
    [SerializeField] private Transform self;
    [SerializeField] private float frequency = 1;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float targetScale = 1;

    private void Reset()
    {
        self = transform;
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (sprite.isVisible)
        {
            self.localScale = Vector3.one * (Mathf.Abs(Mathf.Sin(Time.time * frequency * Mathf.PI)) * targetScale);
        }
    }
}
