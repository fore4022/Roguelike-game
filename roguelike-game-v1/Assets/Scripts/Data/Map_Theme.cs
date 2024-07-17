using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Map Theme", menuName = "Create New Map Theme/New Map Theme")]
public class Map_Theme : ScriptableObject
{
    public List<string> monsterType = new List<string>();

    public string mapThemeName;
    public string bossMonsterType;
    public string specialMonsterType;

    public int difficulty;
}
