// ©2020 Maxime Boudreault. All Rights Reserved.

using UnityEngine;

public class Light : MonoBehaviour
{
    [Header("Physics")]
    [SerializeField] private float maxSpeed = 30;
    [SerializeField] private Rigidbody2D body = null;

    private float _sqrMaxSpeed;
    private Vector3 _startingPosition;
    private Vector2 _velocityBeforeFreeze;

    void Start()
    {
        _sqrMaxSpeed = maxSpeed * maxSpeed;
        _startingPosition = transform.position;
        FreezeAtStart();
    }

    public void FreezeAt(Vector3 position)
    {
        if (body.isKinematic) return;
        _velocityBeforeFreeze = body.velocity;
        body.velocity = Vector2.zero;
        body.isKinematic = true;
        transform.position = position;
    }

    public void FreezeInPlace()
    {
        FreezeAt(transform.position);
    }

    public void Unfreeze()
    {
        body.isKinematic = false;
        body.velocity = _velocityBeforeFreeze;
    }

    public void FreezeAtStart()
    {
        FreezeAt(_startingPosition);
    }

    public void Propel(float power, float angle)
    {
        body.isKinematic = false;
        Vector2 force = new Vector2(-Mathf.Cos(angle), Mathf.Sin(angle)) * power;
        body.AddForce(force, ForceMode2D.Impulse);
        Pulse(power);
    }

    void FixedUpdate()
    {
        Vector3 velocity = body.velocity;
        if (velocity.sqrMagnitude > _sqrMaxSpeed)
        {
            body.velocity = velocity.normalized * maxSpeed;
        }
    }
    
    [Header("Pulse")]
    [SerializeField] private float pulseScale = 1;
    [SerializeField] private float pulseDamping = 1;
    [SerializeField] private float maxScale = 1.75f;
    [SerializeField] private Transform visual = null;
    private float _scale = 1;
    
    void Update()
    {
        visual.localScale = Vector3.one * _scale;
        _scale = Mathf.Max(1, _scale - Time.deltaTime * pulseDamping);
    }
    
    void Pulse(float gain)
    {
        _scale = Mathf.Min(maxScale, _scale + gain * pulseScale);
    }
}
