using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using JetBrains.Annotations;
[Serializable]
public class User
{
    public int level;
    public int exp;
    public int gold;
    public int topStage;
    public int stage;

    public string equippedItem;
}