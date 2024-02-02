using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Map Theme", menuName = "Create New Map Theme/New Map Theme")]
public class Map_Theme : ScriptableObject
{
    public string mapThemeName;
    public List<string> monsterType = new List<string>();
    public int difficulty;
}
