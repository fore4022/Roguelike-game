using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ForceField : Base_Skill
{
    private ParticleSystem particleSystem;
    private List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    private int numEnter;
    private void Awake() { particleSystem = GetComponent<ParticleSystem>(); }
    protected override void init()
    {
        Collider2D collider;
        int maxCount = 0;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(Managers.Game.player.gameObject.transform.position, new Vector2(Managers.Game.camera_h, Managers.Game.camera_v), LayerMask.GetMask("Monster"));
        foreach (Collider2D col in colliders)
        {
            if (maxCount < col.gameObject.GetComponent<Monster_Controller>().monsterCount())
            {
                maxCount = col.gameObject.GetComponent<Monster_Controller>().monsterCount();
                collider = col;
            }
        }
    }
    private void OnParticleTrigger()
    {
        numEnter = particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        for(int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];
            enter[i] = p;
            Debug.Log("qwe");
        }
    }
}
