using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ForceField : Base_Skill
{
    private ParticleSystem particleSys;
    private List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    private int numEnter;
    private void Awake() { particleSys = GetComponent<ParticleSystem>(); }
    protected override void init()
    {
        Collider2D collider;
        int maxCount = 0;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(Managers.Game.player.gameObject.transform.position, new Vector2(Managers.Game.camera_h, Managers.Game.camera_v), LayerMask.GetMask("Monster"));
        foreach (Collider2D col in colliders)
        {
            Monster_Controller mon = col.gameObject.GetComponent<Monster_Controller>();
            Debug.Log(col.gameObject.layer);
            Debug.Log(mon);
            Debug.Log(mon.monsterCount());
            //Debug.Log(col.gameObject.GetComponent<Monster_Controller>().monsterCount());
            //if (maxCount < col.gameObject.GetComponent<Monster_Controller>().monsterCount())
            //{
            //    maxCount = col.gameObject.GetComponent<Monster_Controller>().monsterCount();
            //    collider = col;
            //}
        }
    }
    private void OnParticleTrigger()
    {
        numEnter = particleSys.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        for(int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];
            enter[i] = p;
            Debug.Log("qwe");
        }
        particleSys.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    }
}
