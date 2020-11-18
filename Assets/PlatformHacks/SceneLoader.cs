using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private CanvasGroup ui = null;
    public void LoadScene(int index)
    {
        ui.interactable = false;
        StartCoroutine(FadeTo(index));
    }

    IEnumerator FadeTo(int index)
    {
        const float delay = 1.67f;
        float t = 0;
        while (t < delay)
        {
            t += Time.deltaTime;
            ui.alpha = Mathf.Lerp(1, 0, t / delay);
            yield return null;
        }

        ui.alpha = 0;
        SceneManager.LoadScene(index);
    }
}
