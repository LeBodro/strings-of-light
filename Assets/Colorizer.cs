// ©2020 Maxime Boudreault. All Rights Reserved.

using System.Collections;
using UnityEngine;

public class Colorizer : MonoBehaviour
{
    [SerializeField] private Camera view = null;
    [SerializeField] private Gradient background;
    [SerializeField] private Color foreground;
    [SerializeField] private float fadeDelay = 1.67f;

    private float _foregroundRatio = 0;
    private float _progress = 0;

    public void SetProgress(float value)
    {
        _progress = value;
        RefreshColor();
    }

    public Coroutine FadeToForeground() => StartCoroutine(_FadeToForeground());
    IEnumerator _FadeToForeground()
    {
        while (_foregroundRatio < 1)
        {
            _foregroundRatio += Time.deltaTime / fadeDelay;
            RefreshColor();
            yield return null;
        }

        _foregroundRatio = 1;
        RefreshColor();
    }
    
    public Coroutine FadeToBackground() => StartCoroutine(_FadeToBackground());
    IEnumerator _FadeToBackground()
    {
        while (_foregroundRatio > 0)
        {
            _foregroundRatio -= Time.deltaTime / fadeDelay;
            RefreshColor();
            yield return null;
        }

        _foregroundRatio = 0;
        RefreshColor();
    }

    void RefreshColor()
    {
        Color bgColor = background.Evaluate(_progress);
        Color mixedColor = Color.Lerp(bgColor, foreground, _foregroundRatio);

        view.backgroundColor = mixedColor;
    }
}