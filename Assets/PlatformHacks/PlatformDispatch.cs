using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformDispatch : MonoBehaviour
{
    #pragma warning disable 0414
    [SerializeField] private int defaultSceneId = 2;
    [SerializeField] private int webGLSceneId = 1;
    #pragma warning restore 0414

    void Awake()
    {
#if UNITY_WEBGL
        SceneManager.LoadScene(webGLSceneId);
#else
        SceneManager.LoadScene(defaultSceneId);
#endif
    }
}
