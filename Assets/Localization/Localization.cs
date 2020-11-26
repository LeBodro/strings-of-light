// ©2020 Maxime Boudreault. All Rights Reserved.

using System.Collections.Generic;
using UnityEngine;

public class Localization : MonoBehaviour
{
    [SerializeField] private LocalizationDictionary[] dictionaries = null;

    private int _dicId = 0;

    private List<string> _keys = new List<string>
    {
        "webglwarning.text",
        "webglwarning.continue",
        "game.ui.instruction",
        "game.narration.0",
        "game.narration.1",
        "game.narration.2",
        "game.narration.3",
        "game.narration.4",
        "game.narration.5",
        "game.narration.6",
        "game.narration.7",
        "game.narration.8",
        "game.ui.moon",
        "game.ui.venus",
        "game.ui.mercury",
        "game.ui.sun",
    };

    private static Localization _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        for (int i = 0; i < dictionaries.Length; ++i)
        {
            if (dictionaries[i].language == Application.systemLanguage)
            {
                _dicId = i;
            }
        }
    }

    public static void SetLanguage(int index) => _instance._SetLanguage(index);
    void _SetLanguage(int index) => _dicId = index;

    public static string _(string key) => _instance.Get(key);

    string Get(string key)
    {
        int index = _keys.IndexOf(key);
        if (index > -1) return dictionaries[_dicId].Get(index);
#if UNITY_EDITOR
        Debug.LogError($"Localization key not found: {key}");
#endif
        return key;
    }
}