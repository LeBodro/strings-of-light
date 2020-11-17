// ©2020 Maxime Boudreault. All Rights Reserved.

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HeightDisplay : MonoBehaviour
{
    [SerializeField] private CanvasGroup group = null;
    [SerializeField] private Text height = null;
    [SerializeField] private Text goal = null;
    [SerializeField] private Transform heightTarget = null;

    private bool _visible = false;
    
    private void Start()
    {
        group.alpha = 0;
    }

    public void Show()
    {
        if (_visible) return;

        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        const float delay = 1.67f;
        float t = 0;
        while (t < delay)
        {
            t += Time.deltaTime;
            group.alpha = Mathf.Lerp(0, 1, t);
            yield return null;
        }

        group.alpha = 1;
    }

    public void SetGoal(Transform g)
    {
        goal.text = $"{Mathf.RoundToInt(g.position.y).ToString()} ({g.name})";
    }

    void Update()
    {
        height.text = Mathf.RoundToInt(heightTarget.position.y).ToString();
    }
}
