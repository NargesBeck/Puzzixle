using System;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewTheme", menuName = "Theme", order = 1)]
public class ThemesScriptableObject : ScriptableObject
{
    public List<ThemeData> Themes = new List<ThemeData>();
}

[Serializable]
public class ThemeData
{
    public Color TextColor;
}
