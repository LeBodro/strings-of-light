// ©2020 Maxime Boudreault. All Rights Reserved.

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Narration : MonoBehaviour
{
    private const float FadeDelay = 1.67f;

    [SerializeField] private Colorizer view = null;
    [SerializeField] private string segmentPrefix = "game.narration.";
    [SerializeField] private Text text = null;
    [SerializeField] private Text instruction = null;
    [SerializeField] private int segmentCount = 9;

    private int _nextSegmentIndex = 0;
    private bool _isEnding = false;

    string GetSegment(int index)
    {
        return Localization._($"{segmentPrefix}{index}");
    }

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
        while (_nextSegmentIndex < segmentCount)
        {
            yield return StartCoroutine(ShowSegment(null));
        }
    }

    IEnumerator ShowSegment(System.Action onEnd)
    {
        // fade in text
        text.text = GetSegment(_nextSegmentIndex);
        text.CrossFadeAlpha(1, FadeDelay, false);
        _nextSegmentIndex++;

        if (!_isEnding)
            view.FadeToForeground();
        yield return new WaitForSeconds(FadeDelay);

        if (_nextSegmentIndex < segmentCount)
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