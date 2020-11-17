// ©2020 Maxime Boudreault. All Rights Reserved.

using System.Collections.Generic;
using UnityEngine;

public class StarGenerator : MonoBehaviour
{
    [SerializeField] private SongOfStars[] songs;
    [SerializeField] private AudioSource player = null;
    [SerializeField] private float yThreshold = 3;
    [SerializeField] private AudioClip[] twinkles = null;

    private readonly List<Star> _stars = new List<Star>();
    
    private int _songIndex = -1;
    private SongOfStars _song;
    private int _nextStarIndex = 0;
    private bool _waitForLoop = false;
    bool _isSpawning = false;

    public int Count => _stars. Count;

    private void SetNextStarIndex(int index)
    {
        _nextStarIndex = index;
        int twinkleIndex = ((_nextStarIndex - 1) + twinkles.Length) % twinkles.Length;
        TwinklePlayer.Override(twinkles[twinkleIndex]);
    }

    public void BeginSpawning()
    {
        _isSpawning = true;
        float time = player.time;
        for (SetNextStarIndex(0); _nextStarIndex < _song.stars.Count; SetNextStarIndex(_nextStarIndex+1))
        {
            if (time < _song.stars[_nextStarIndex])
            {
                return;
            }
        }

        SetNextStarIndex(0);
        _waitForLoop = true;
    }

    public void EndSpawning()
    {
        _isSpawning = false;
    }

    public void Boost()
    {
        _songIndex++;
        if (_songIndex < songs.Length)
        {
            SetSong(_songIndex);
        }
    }

    void Start()
    {
        SetSong(0);
    }

    void SetSong(int index)
    {
        _songIndex = index;
        _song = songs[index];
        SetNextStarIndex(0);
        player.clip = _song.song;
        player.Play();
    }

    public void ChargeStars()
    {
        foreach(var star in _stars)
            star.Recharge();
    }
    
    void Update()
    {
        if (!_isSpawning) return;
        
        if (_waitForLoop)
        {
            if (player.time > _song.stars[_song.stars.Count-2]) return;
            _waitForLoop = false;
        }
        if (player.time >= _song.stars[_nextStarIndex])
        {
            if (transform.position.y >= yThreshold)
            {
                _stars.Add(Instantiate(_song.prefab, transform.position, Quaternion.identity));
            }
            SetNextStarIndex(_nextStarIndex+1);
            if (_nextStarIndex == _song.stars.Count)
            {
                _waitForLoop = true;
                SetNextStarIndex(0);
            }
        }
    }
}
