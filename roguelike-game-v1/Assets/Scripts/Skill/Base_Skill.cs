using UnityEngine;
public class Base_Skill : MonoBehaviour
{
    public Skill skill;
    protected virtual void Start() { Init(); }
    protected virtual void Init() { }
    protected virtual void Update() { }
}
