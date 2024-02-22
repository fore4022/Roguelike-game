using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
public class BloodMagicBullet : Base_Skill
{
    private float projectileSpeed;
    private Vector3 direction;
    protected override void Start()
    {
        Transform player = Managers.Game.player.gameObject.transform;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.position, Managers.Game.camera_v, LayerMask.GetMask("Monster"));
        if (colliders.Count() == 0) { colliders = Physics2D.OverlapCircleAll(player.position, Managers.Game.camera_v + 1.5f, LayerMask.GetMask("Monster")); }
        if (colliders.Count() == 0) { return; }
        direction = new();
        foreach (Collider2D col in colliders)
        {
            if (direction == new Vector3()) { direction = col.gameObject.transform.position - player.position; }
            else if (direction.sqrMagnitude > (col.gameObject.transform.position - player.position).sqrMagnitude) { direction = col.gameObject.transform.position - player.position; }
        }
        direction = direction.normalized;
        transform.position = player.position;
        transform.rotation = Quaternion.Euler(0, 0, -90 + Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        init();
    }
    private void init()
    {
        projectileSpeed = 8f;
    }
    protected override void Update()
    {
        if (projectileSpeed > 0) { transform.position += direction * Time.deltaTime * projectileSpeed; }
        else 
        {
            if(anime.GetCurrentAnimatorStateInfo(0).IsName("Boom"))
            {
                if (anime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) { Destroy(gameObject); }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            collision.GetComponent<Monster_Controller>().attacked(skill.skillDamage);
            anime.Play("Boom");
            projectileSpeed = 0f;
            Managers.Game.objectPool.disableObject(this.GetType().Name, transform.gameObject);
        }
    }
}
