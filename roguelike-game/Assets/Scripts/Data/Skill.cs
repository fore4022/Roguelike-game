using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Skill", menuName = "Create New Skill/New Skill")]
public class Skill : ScriptableObject
{
    public Define.Effect effect;
    public string skillName;
    public string explanation;
    public float skillDamage;
    public float skillCoolTime;
    public float skillRange;
    public float castingSpeed;
    public float skillDuration;
    public float increaseSkillCooldown;
    public float increaseDamage;
    public float increaseHp;
    public float magnificationExp;
    public bool useType;
}
