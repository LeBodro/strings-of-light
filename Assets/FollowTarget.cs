// ©2020 Maxime Boudreault. All Rights Reserved.

using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target = null;
    [SerializeField] private Transform self = null;
    [SerializeField] private float offset = 0;

    private float _minHeight = 0;

    private void Start()
    {
        _minHeight = self.position.y;
    }

    void Update()
    {
        Vector3 position = self.position;
        position.y = Mathf.Max(target.position.y + offset, _minHeight);
        self.position = position;
    }
}
