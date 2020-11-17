// ©2020 Maxime Boudreault. All Rights Reserved.

using System;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public event Action OnCollision = delegate { };
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        OnCollision();
    }
}
