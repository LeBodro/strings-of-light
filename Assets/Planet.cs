// ©2020 Maxime Boudreault. All Rights Reserved.

using System;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private GameObject friendInSpace = null;
    [SerializeField] private GameObject friendOnEarth = null;
    [SerializeField] private float power = 5;

    public bool Charged { get; set; } = true;
    
    public event Action OnReach = delegate { };
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<StarGenerator>().EndSpawning();
        if (Charged)
        {
            OnReach();
            if (friendOnEarth != null)
            {
                friendOnEarth.SetActive(true);
            }
                
            Light hope = other.GetComponent<Light>();
            Charged = false;
            hope.FreezeAt(transform.position);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        other.GetComponent<StarGenerator>().EndSpawning();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.GetComponent<StarGenerator>().BeginSpawning();
    }
}
