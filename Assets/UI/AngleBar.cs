// ©2020 Maxime Boudreault. All Rights Reserved.

using UnityEngine;

public class AngleBar : MonoBehaviour
{
    [SerializeField] private float fillRate = 0.5f;
    [SerializeField] private float minAmount = 0.082f;
    [SerializeField] private float maxAmount = 0.419f;
    [SerializeField] private Transform arrow = null;

    private float _tZero = 0;

    private System.Action<float> _onPress = delegate { };

    public void Launch(System.Action<float> onPress)
    {
        _onPress = onPress;
        enabled = true;
    }
    
    private void Update()
    {
        float halfDelta = 0.5f * (maxAmount - minAmount);
        float amount = (-Mathf.Cos((Time.time - _tZero) * Mathf.PI * 2 * fillRate) + 1) * halfDelta + minAmount;

        arrow.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * (Mathf.Cos((Time.time - _tZero) * Mathf.PI * 2 * fillRate) + 1) - 57);

        if (Input.GetMouseButtonDown(0))
        {
            _onPress(amount * Mathf.PI * 2);
            enabled = false;
        }
    }

    private void OnEnable()
    {
        _tZero = Time.time;
    }
}
