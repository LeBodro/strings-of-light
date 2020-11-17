// ©2020 Maxime Boudreault. All Rights Reserved.

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SoL/Song of Stars")]
public class SongOfStars : ScriptableObject
{
    public AudioClip song;
    public List<float> stars;
    public Star prefab;
}
