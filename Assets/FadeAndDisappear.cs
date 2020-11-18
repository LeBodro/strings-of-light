using UnityEngine;
using UnityEngine.UI;

public class FadeAndDisappear : MonoBehaviour
{
    void Start()
    {
        const float delay = 2f;
        Image img = GetComponent<Image>();
        img.enabled = true;
        GetComponent<Image>().CrossFadeAlpha(0, delay, false);
        Destroy(gameObject, delay);
    }
}
