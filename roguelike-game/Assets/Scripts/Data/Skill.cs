using UnityEngine;
[CreateAssetMenu(fileName = "Skill", menuName = "Create New Skill/New Skill")]
public class Skill : ScriptableObject
{
    public Define.Effect effect;

    public string skillName;
    public string explanation;

    public float skillCoolTime;
    public float skillRange;
    public float castingSpeed;
    public float skillDuration;
    public float increaseSkillCooldown;
    public float increaseDamage;
    public float increaseHp;
    public float magnificationExp;

    public int increaseNumberOfCast;
    public int numberOfCast;
    public int skillDamage;
    public int skillLevel;

    public bool useType;
}
