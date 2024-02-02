using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Item", menuName = "Create New Item/New Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public string explanation;
    public Define.Effect effect;
    public float attackDamage;
    public float attackSpeed;
    public float moveSpeed;
    public float skillCooldownReduction;
    public float skillRangeIncrease;
    public float playerSizeIncrease;
    public float expGainMultiplier;
    public float goldAcquisitionMultiplier;
}