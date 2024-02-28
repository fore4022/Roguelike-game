using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using System;
public class ForceField : Base_Skill
{
    private List<ParticleSystem.Particle> inside = new();
    private ParticleSystem.TriggerModule trigger;
    private ParticleSystem particleSys;
    private int numInside;
    private float duration;
    private void Awake() { particleSys = GetComponent<ParticleSystem>(); }
    protected override void init()
    {
        Debug.Log("asdf");
        duration = 0;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(Managers.Game.player.gameObject.transform.position, new Vector2(Managers.Game.camera_h, Managers.Game.camera_v), LayerMask.GetMask("Monster"));
        List<Monster_Controller> monsters = colliders.Select(o => o.gameObject.GetComponent<Monster_Controller>()).ToList();
        monsters.RemoveAll(o => o == null);
        Vector3 pos = Vector3.zero;
        if (monsters.Count() > 0)
        {
            int maxCount = 0;
            foreach (Monster_Controller monster in monsters)
            {
                if (maxCount < monster.monsterCount())
                {
                    maxCount = monster.monsterCount();
                    pos = monster.gameObject.transform.position;
                }
            }
        }
        else { pos = new Vector3(Managers.Game.camera_h, Managers.Game.camera_v); }
        transform.position = pos;
        trigger = particleSys.trigger;
        trigger.enabled = true;
        trigger.enter = ParticleSystemOverlapAction.Callback;
        trigger.inside = ParticleSystemOverlapAction.Callback;
        trigger.exit = ParticleSystemOverlapAction.Ignore;
        trigger.colliderQueryMode = ParticleSystemColliderQueryMode.All;
    }
    protected override void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, skill.skillRange * 1.25f, LayerMask.GetMask("Monster"));
        if(colliders.Count() != 0)
        {
            if(trigger.colliderCount != 0)
            {
                foreach(Collider2D col in colliders) { trigger.AddCollider(col); }
            }
            else
            {
                int num;
                for(int i = 0; i < trigger.colliderCount; i++)
                {
                    
                }
            }
        }
        duration += Time.deltaTime;
        if (duration >= skill.skillDuration) { Managers.Game.objectPool.disableObject(this.GetType().Name, this.gameObject); }
    }
    private void OnParticleTrigger()
    {
        numInside = particleSys.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside, out ParticleSystem.ColliderData insideData);
        for (int i = 0; i < numInside; i++) { insideData.GetCollider(i, 0).gameObject.GetComponent<Monster_Controller>().attacked(skill.skillDamage); }
    }
}