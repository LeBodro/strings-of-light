// ©2020 Maxime Boudreault. All Rights Reserved.

using UnityEngine;

public class TwinklePlayer : MonoBehaviour
{
    [SerializeField] private AudioSource sound = null;
    [SerializeField] private AudioClip[] twinkles = null;

    private static TwinklePlayer _instance;

    private int _next = 0;
    private AudioClip _clipOverride = null;

    private void Awake()
    {
        _instance = this;
    }

    public static void Play() => _instance._Play();
    void _Play()
    {
        if (_clipOverride != null)
        {
            sound.PlayOneShot(_clipOverride);
        }
        else
        {
            sound.PlayOneShot(twinkles[_next]);
            _next = (_next + 1) % twinkles.Length;
        }
    }
    
    public static void Override(AudioClip clip) => _instance._Override(clip);
    void _Override(AudioClip clip)
    {
        _clipOverride = clip;
    }
}
