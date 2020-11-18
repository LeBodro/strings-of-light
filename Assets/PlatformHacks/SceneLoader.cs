using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private CanvasGroup ui = null;
    public void LoadScene(int index)
    {
        ui.interactable = false;
        StartCoroutine(Fade(1, 0, () => SceneManager.LoadScene(index)));
    }

    void Start()
    {
        ui.interactable = false;
        StartCoroutine(Fade(0, 1, () => ui.interactable = true));
    }

    IEnumerator Fade(float from, float to, Action then)
    {
        const float delay = 1f;
        ui.alpha = from;
        float t = 0;
        while (t < delay)
        {
            t += Time.deltaTime;
            ui.alpha = Mathf.Lerp(from, to, t / delay);
            yield return null;
        }

        ui.alpha = to;
        then?.Invoke();
    }
}
