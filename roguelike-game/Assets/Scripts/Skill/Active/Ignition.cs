using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using System;
public class Ignition : Base_Skill
{
    private List<ParticleSystem.Particle> enter = new();
    private ParticleSystem.TriggerModule trigger;
    private ParticleSystem particleSys;
    private int numEnter;
    private float duration;
    private void Awake() { particleSys = GetComponent<ParticleSystem>(); }
    protected override void init()
    {
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
        ParticleSystem.MainModule forceField = particleSys.main;
        forceField.startLifetime = 1;
        forceField.startSpeed = new ParticleSystem.MinMaxCurve(8, 10);
        forceField.startSize = 0.35f;
        forceField.maxParticles = 70;
        trigger = particleSys.trigger;
        trigger.enabled = true;
        trigger.enter = ParticleSystemOverlapAction.Callback;
        trigger.inside = ParticleSystemOverlapAction.Callback;
        trigger.exit = ParticleSystemOverlapAction.Ignore;
        trigger.colliderQueryMode = ParticleSystemColliderQueryMode.All;
        ParticleSystem.EmissionModule emission = particleSys.emission;
        emission.enabled = true;
        emission.rateOverTime = 1000;
        emission.rateOverDistance = 0;
        ParticleSystem.ShapeModule shape = particleSys.shape;
        shape.enabled = true;
        ParticleSystem.LimitVelocityOverLifetimeModule limitVelocity = particleSys.limitVelocityOverLifetime;
        limitVelocity.enabled = true;
        limitVelocity.dampen = 0.2f;
        ParticleSystemRenderer render = particleSys.GetComponent<ParticleSystemRenderer>();
        render.material = Managers.Resource.load<Material>("Material/Fragments");
    }
    protected override void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, skill.skillRange * 1.25f, LayerMask.GetMask("Monster"));
        for (int i = 0; i < trigger.colliderCount; i++) { trigger.RemoveCollider(i); }
        for (int i = 0; i < colliders.Count(); i++) { trigger.AddCollider(colliders[i]); }
        duration += Time.deltaTime;
        if (duration >= skill.skillDuration) { Managers.Game.objectPool.disableObject(this.GetType().Name, this.gameObject); }
    }
    private void OnParticleTrigger()
    {
        numEnter = particleSys.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter, out ParticleSystem.ColliderData insideData);
        for (int i = 0; i < numEnter; i++) { insideData.GetCollider(i, 0).gameObject.GetComponent<Monster_Controller>().attacked(skill.skillDamage); }
    }
}