using UnityEngine;
public class Monster_Controller : Base_Controller
{
    [SerializeField]
    private float animeSpeed = 0.25f;

    public Monster monsterType;

    private float _range = 2f;
    protected override void Start()
    {
        base.Start();

        anime.speed = animeSpeed;
    }
    private void OnEnable() { Init(); }
    private void Update() { Moving(); }
    protected override void Init()
    {
        damage = monsterType.attackDamage;
        maxHp = monsterType.maxHp;
        gold = monsterType.gold;
        exp = monsterType.exp;

        moveSpeed = monsterType.moveSpeed;
        hp = maxHp;
    }
    private Vector3 Move() { return (Managers.Game.player.gameObject.transform.position - transform.position).normalized; }
    private void Moving() { transform.position += Move() * MoveSpeed * Time.deltaTime; }
    public virtual void Attacked(int damage)
    {
        hp -= damage;
    }
    private void Death() 
    {
        anime.speed = 1f;
        anime.Play("death");

        Managers.Game.player.GetLoot(gold, exp);

        //while (true)
        //{
        //    if(anime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f && anime.GetCurrentAnimatorStateInfo(0).IsName("death"))
        //    {
        //        Managers.Game.objectPool.DisableObject(monsterType.monsterName, this.gameObject);

        //        break;
        //    }

        //    yield return null;
        //}
    }
    public int MonsterCount()
    {
        Collider2D[] colliders = new Collider2D[] { };

        return Physics2D.OverlapCircleNonAlloc(transform.position, _range, colliders, LayerMask.GetMask("Monster"));
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}