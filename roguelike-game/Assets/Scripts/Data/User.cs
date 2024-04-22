using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
[Serializable]
public class User
{
    public List<Base_Skill> skills;
    public Stat stat;

    public int level;
    public int monsterCount;
    public float exp;
    public float time;
}
