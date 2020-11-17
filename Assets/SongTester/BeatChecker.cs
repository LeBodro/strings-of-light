// ©2020 Maxime Boudreault. All Rights Reserved.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class BeatChecker : MonoBehaviour
{
    [SerializeField] private SongOfStars song = null;
    [SerializeField] private Image visual = null;
    [SerializeField] private Text timestamps = null;
    [SerializeField] private AudioSource player = null;

    private bool _pending = true;
    readonly List<float> _timestamps = new List<float>();
    
    void Start()
    {
        timestamps.text = string.Empty;
        player.clip = song.song;
        visual.color = Color.black;
    }

    public void Replace()
    {
        song.stars = _timestamps;
        #if UNITY_EDITOR
        EditorUtility.SetDirty(song);
        #endif
    }
    
    void Update()
    {
        if (_pending)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _pending = false;
                player.Play();
            }
            return;
        }
        
        visual.color = Color.Lerp(visual.color, Color.black, Time.deltaTime);
        if (Input.GetMouseButtonDown(0))
        {
            Pulse();
            _timestamps.Add(player.time);
            timestamps.text += ($"{player.time}\n");
        }
    }

    void Pulse()
    {
        visual.color = Color.white;
    }
}
