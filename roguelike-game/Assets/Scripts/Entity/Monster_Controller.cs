using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class Monster_Controller : Base_Controller
{
    [HideInInspector]
    public Vector2 velocity;
    [SerializeField]
    protected float alignmentAmount = 1f;
    [SerializeField]
    protected float cohesionAmount = 1f;
    [SerializeField]
    protected float separationAmount = 1f;
    [SerializeField]
    protected float neighborhoodRadius = 2.75f;
    [SerializeField]
    protected float maxDistance = 3f;
    protected Vector2 acceleration;
    protected float correction = 0.005f;
    protected override void Start()
    {
        base.Start();
        state = State.Moving;
        moveSpeed = 4f;
    }
    protected override void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, neighborhoodRadius);
        List<Monster_Controller> boids = colliders.Select(o => o.gameObject.GetComponent<Monster_Controller>()).ToList();
        boids.RemoveAll(o => o == null);
        if(boids.Count() != 0)
        {
            flock(boids);
            updateVelocity();
            updatePosition();
            updateRotation();
        }
    }
    protected void flock(IEnumerable<Monster_Controller> boids)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, (transform.localScale.x + transform.localScale.y) / 2.8f + correction);
        List<Monster_Controller> monster = colliders.Select(o => o.gameObject.GetComponent<Monster_Controller>()).ToList();
        List<Player_Controller> player = colliders.Select(o => o.gameObject.GetComponent<Player_Controller>()).ToList();
        acceleration = alignment(boids) * alignmentAmount + cohesion(boids) * cohesionAmount + separation(boids) * separationAmount;
        if(monster.Count() != 0) { monster.RemoveAll(o => o == null); }
        if(player.Count() != 0) { player.RemoveAll(o => o == null); }
        if (monster.Count() == 1 && player.Count() == 0) { acceleration += move(); }
        else if (player.Count() == 1) { acceleration = Vector2.zero; }
        else if (monster.Count() > 1) { acceleration = separation(boids) * separationAmount * 2; }
    }
    protected void updateVelocity()
    {
        velocity += acceleration;
        velocity = limitMagnitude(velocity, moveSpeed);
    }
    protected void updatePosition()
    {
        if(acceleration != Vector2.zero) { transform.position += new Vector3(velocity.x, velocity.y, 0) * Time.deltaTime; }
    }
    protected void updateRotation()
    {
        float angle = Mathf.Atan2(move().y, move().x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    protected Vector2 alignment(IEnumerable<Monster_Controller> boids)
    {
        Vector2 velocity = Vector2.zero;
        if (!boids.Any()) { return velocity; }
        foreach (Monster_Controller boid in boids) { velocity += boid.velocity; }
        velocity /= boids.Count();
        return steer(velocity.normalized * moveSpeed);
    }
    protected Vector2 cohesion(IEnumerable<Monster_Controller> boids)
    {
        if (!boids.Any()) { return Vector2.zero; }
        Vector2 sumPositions = Vector2.zero;
        foreach (Monster_Controller boid in boids) { sumPositions += (Vector2)boid.transform.position; }
        Vector2 average = sumPositions / boids.Count();
        Vector2 direction = average - (Vector2)transform.position;
        return steer(direction.normalized * moveSpeed);
    }
    protected Vector2 separation(IEnumerable<Monster_Controller> boids)
    {
        Vector2 direction = Vector2.zero;
        boids = boids.Where(o => distanceTo(o) <= neighborhoodRadius / 2);
        if (!boids.Any()) { return direction; }
        foreach (Monster_Controller boid in boids)
        {
            float a = Vector2.Distance(boid.transform.position, transform.position);
            Vector2 difference = transform.position - boid.transform.position;
            direction += difference.normalized * moveSpeed;
        }
        direction /= boids.Count();
        return steer(direction.normalized * moveSpeed);
    }
    protected Vector2 steer(Vector2 desired)
    {
        Vector2 vec = desired - velocity;
        return limitMagnitude(vec, maxDistance);
    }
    protected Vector2 limitMagnitude(Vector2 baseVector, float maxMagnitude)
    {
        if (baseVector.sqrMagnitude > maxMagnitude * maxMagnitude) { baseVector = baseVector.normalized * maxMagnitude; }
        return baseVector;
    }
    protected float distanceTo(Monster_Controller boid)
    {
        return Vector2.Distance(boid.gameObject.transform.position, transform.position);
    }
    protected Vector2 move()
    {
        return (Managers.Game.PlayerController.gameObject.transform.position - transform.position).normalized;
    }
    protected override void moving()
    {
        
    }
    protected override void death()
    {
        Managers.Game.PlayerController.Gold += gold;
        Managers.Game.PlayerController.Exp += exp;
    }
    protected void crash(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) { Managers.Game.PlayerController.Hp -= damage * attackSpeed * Time.deltaTime; }
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        crash(collision);
    }
    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        crash(collision);
    }
    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, neighborhoodRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, neighborhoodRadius / 2);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, (transform.localScale.x + transform.localScale.y) / 2.8f + correction);
    }
}