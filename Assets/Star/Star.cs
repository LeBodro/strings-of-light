// ©2020 Maxime Boudreault. All Rights Reserved.

using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] private SpriteRenderer glimmer = null;
    [SerializeField] private float power = 1;
    [SerializeField] private ParticleSystem fx = null;

    private bool _charged = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_charged) return;
        if (other.attachedRigidbody.velocity.y < -5) return;
        TwinklePlayer.Play();
        fx.Play();
        other.GetComponent<Light>().Propel(power, Mathf.PI * Random.Range(0.3f, 0.3f));
        _charged = false;
        glimmer.enabled = false;
    }

    public void Recharge()
    {
        glimmer.enabled = true;
        _charged = true;
    }
}
