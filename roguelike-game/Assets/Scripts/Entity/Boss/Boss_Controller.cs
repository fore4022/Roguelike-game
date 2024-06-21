using UnityEngine;
public class Boss_Controller : Base_Controller
{
    [HideInInspector]
    public Monster monsterType;
    private enum State
    {
        Moving,
        Pattern1,
        Pattern2,
        Pattern3,
        Death 
    }
    private State state;

    protected override void Start() { base.Start(); }
    private void OnEnable() { Init(); }
    protected override void Init()
    {
        damage = monsterType.attackDamage;
        maxHp = monsterType.maxHp;
        gold = monsterType.gold;
        exp = monsterType.exp;
        moveSpeed = monsterType.moveSpeed;
        hp = maxHp;
    }
    protected override void Update()
    {
        if(Managers.Game.player.Hp > 0)
        {
            if (state == State.Death) { return; }

            if (hp <= 0)
            {
                boxCollider.enabled = false;
                state = State.Death;
            }
            SetState();
        }
    }
    protected void SetState() { }
    protected override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, (transform.localScale.x + transform.localScale.y) / 2.75f);
    }
}
