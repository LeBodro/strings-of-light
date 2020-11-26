// ©2020 Maxime Boudreault. All Rights Reserved.

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextLocalizer : MonoBehaviour
{
    [SerializeField] private Text text = null;
    [SerializeField] private string key = string.Empty;
    void Reset() => text = GetComponent<Text>();
    void Start() => text.text = Localization._(key == string.Empty ? text.text : key);
}
