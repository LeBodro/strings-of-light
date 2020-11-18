// ©2020 Maxime Boudreault. All Rights Reserved.

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Narration : MonoBehaviour
{
    private const float FadeDelay = 1.67f;

    [SerializeField] private Colorizer view = null;
    [SerializeField] [TextArea(1, 7)] private string[] segments = null;
    [SerializeField] private Text text = null;
    [SerializeField] private Text instruction = null;

    private int _nextSegmentIndex = 0;
    private bool _isEnding = false;

    public void BeginShowNextSegment(System.Action onEnd)
    {
        StartCoroutine(ShowSegment(onEnd));

        text.canvasRenderer.SetAlpha(0);
        instruction.canvasRenderer.SetAlpha(0);
    }

    public void BeginShowEnding()
    {
        StartCoroutine(ShowEnding());
    }

    IEnumerator ShowEnding()
    {
        _isEnding = true;
        yield return view.FadeToForeground();
        while (_nextSegmentIndex < segments.Length)
        {
            yield return StartCoroutine(ShowSegment(null));
        }
    }

    IEnumerator ShowSegment(System.Action onEnd)
    {
        // fade in text
        text.text = segments[_nextSegmentIndex];
        text.CrossFadeAlpha(1, FadeDelay, false);
        _nextSegmentIndex++;

        if (!_isEnding)
            view.FadeToForeground();
        yield return new WaitForSeconds(FadeDelay);

        if (_nextSegmentIndex < segments.Length)
        {
            // show instruction
            instruction.CrossFadeAlpha(1, FadeDelay, false);

            // wait for mouse click
            while (!Input.GetMouseButton(0)) yield return null;

            // fade out text
            text.CrossFadeAlpha(0, FadeDelay, false);
            instruction.CrossFadeAlpha(0, FadeDelay, false);

            if (!_isEnding)
                view.FadeToBackground();
            yield return new WaitForSeconds(FadeDelay);

            onEnd?.Invoke();
        }
    }
}