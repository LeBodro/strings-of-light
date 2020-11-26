// ©2020 Maxime Boudreault. All Rights Reserved.

using UnityEngine;

[CreateAssetMenu(menuName = "SoL/Dictionary")]
public class LocalizationDictionary : ScriptableObject
{
    public SystemLanguage language = SystemLanguage.English;
    [TextArea(1, 7)] [SerializeField] private string[] locals = null;

    public string Get(int index)
    {
        return locals[index];
    }
}
