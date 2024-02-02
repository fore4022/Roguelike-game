using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Skill", menuName = "Create New Skill/New Skill")]
public class Skill : ScriptableObject
{
    public string skillName;
    public string explanation;
    public Define.Effect effect;
    public float skillDamage;
    public float skillCoolTime;
    public float skillRange;
    public float castingSpeed;
    public float skillDuration;
    public float skillCooldownReduction;
    public bool combatType;
    public bool useType;
}
