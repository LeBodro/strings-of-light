// ©2020 Maxime Boudreault. All Rights Reserved.

using System.Collections.Generic;
using UnityEngine;

public class Localization : MonoBehaviour
{
    [SerializeField] private LocalizationDictionary[] dictionaries = null;
    [SerializeField] private List<string> keys = null;

    private int _dicId = 0;

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
        int index = keys.IndexOf(key);
        if (index > -1) return dictionaries[_dicId].Get(index);
#if UNITY_EDITOR
        Debug.LogError($"Localization key not found: {key}");
#endif
        return key;
    }
}