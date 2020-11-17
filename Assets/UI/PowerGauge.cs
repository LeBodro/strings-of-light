// ©2020 Maxime Boudreault. All Rights Reserved.

using UnityEngine;
using UnityEngine.UI;

public class PowerGauge : MonoBehaviour
{
    [SerializeField] private Image fill = null;
    [SerializeField] private float fillRate = 0.5f;

    private float _tZero = 0;
    
    private System.Action<float> _onPress = delegate { };

    public void Launch(System.Action<float> onPress)
    {
        _onPress = onPress;
        enabled = true;
    }

    public void Reset()
    {
        fill.fillAmount = 0;
    }
    
    void Update()
    {
        float amount = 1 - Mathf.Abs(Mathf.Cos((Time.time - _tZero) * Mathf.PI * 2 * fillRate));

        fill.fillAmount = amount;

        if (Input.GetMouseButtonDown(0))
        {
            _onPress(amount);
            enabled = false;
        }
    }

    private void OnEnable()
    {
        _tZero = Time.time;
    }
}
