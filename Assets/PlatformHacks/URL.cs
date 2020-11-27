// ©2020 Maxime Boudreault. All Rights Reserved.

using UnityEngine;

public class URL : MonoBehaviour
{
    [SerializeField] private string link;

    public void Open()
    {
        Application.OpenURL(link);
    }
}
