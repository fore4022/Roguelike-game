using System.Linq;
using UnityEngine;
public class FireBall : Base_Skill
{
    public Animator anime;

    private Vector3 _direction;

    private float _projectileSpeed;
    protected override void Init()
    {
        Transform player = Managers.Game.player.gameObject.transform;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.position, Managers.Game.camera_h + 1.5f, LayerMask.GetMask("Monster"));
       
        if (colliders.Count() == 0) { return; }

        _direction = new();

        foreach (Collider2D col in colliders)
        {
            if (_direction == new Vector3()) { _direction = col.gameObject.transform.position - player.position; }
            else if (_direction.sqrMagnitude > (col.gameObject.transform.position - player.position).sqrMagnitude) { _direction = col.gameObject.transform.position - player.position; }
        }

        _direction = _direction.normalized;
        transform.position = player.position;
        transform.rotation = Quaternion.Euler(0, 0, -90 + Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg);
        _projectileSpeed = 8f;
    }
    protected override void Update()
    {
        if (_projectileSpeed > 0) { transform.position += _direction * Time.deltaTime * _projectileSpeed; }
        else 
        {
            if(anime.GetCurrentAnimatorStateInfo(0).IsName("Boom"))
            {
                if (anime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) { Managers.Game.objectPool.DisableObject(this.GetType().Name, this.gameObject); }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            collision.GetComponent<Monster_Controller>().Attacked(skill.skillDamage);
            anime.Play("Boom");
            _projectileSpeed = 0f;
            Destroy(gameObject.GetComponent<BoxCollider2D>());
        }
    }
    private void OnBecameInvisible()
    {
        if (_projectileSpeed != 0) 
        {
            if(!this.gameObject.activeSelf) { return; }

            Managers.Game.objectPool.DisableObject(this.GetType().Name, this.gameObject); 
        } 
    }
}
