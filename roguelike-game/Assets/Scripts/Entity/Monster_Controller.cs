using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class Monster_Controller : Base_Controller
{
    [SerializeField]
    private float alignmentAmount = 2f;
    [SerializeField]
    private float cohesionAmount = 2f;
    [SerializeField]
    private float separationAmount = 2f;
    [SerializeField]
    private float neighborhoodRadius = 3f;
    [SerializeField]
    private float maxDistance = 6f;
    [HideInInspector]
    public Vector2 velocity;
    private Vector2 acceleration;
    protected override void Start()
    {
        base.Start();
        state = State.Moving;
    }
    protected override void Update()
    {
        base.Update();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange * 10);
        List<Monster_Controller> boids = colliders.Select(o => o.gameObject.GetComponent<Monster_Controller>()).ToList();
        boids.Remove(null);
        flock(boids);
        updateVelocity();
        updatePosition();
        updateRotation();
    }
    protected override void moving()
    {
        transform.position += (Managers.Game.PlayerController.gameObject.transform.position - transform.position)* moveSpeed * Time.deltaTime;
    }
    protected override void death()
    {
        Managers.Game.PlayerController.Gold += gold;
        Managers.Game.PlayerController.Exp += exp;
    }
    protected void flock(IEnumerable<Monster_Controller> boids)
    {
        acceleration = alignment(boids) * alignmentAmount + cohesion(boids) * cohesionAmount + separation(boids) * separationAmount;
    }
    protected void updateVelocity()
    {
        velocity += acceleration;
        velocity = limitMagnitude(velocity, moveSpeed);
    }
    protected void updatePosition()
    {
        transform.position += new Vector3(velocity.x, velocity.y, 0) * Time.deltaTime;
    }
    protected void updateRotation()
    {
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    protected Vector2 alignment(IEnumerable<Monster_Controller> boids)
    {
        Vector2 velocity = Vector2.zero;
        if (!boids.Any()) { return velocity; }
        foreach (Monster_Controller boid in boids)
        {
            velocity += boid.velocity;
        }
        velocity /= boids.Count();
        Vector2 vec = steer(velocity.normalized * moveSpeed);
        return vec;
    }
    protected Vector2 cohesion(IEnumerable<Monster_Controller> boids)
    {
        if (!boids.Any()) { return Vector2.zero; }
        Vector2 sumPositions = Vector2.zero;
        foreach (Monster_Controller boid in boids)
        {
            sumPositions += (Vector2)boid.transform.position;
        }
        Vector2 average = sumPositions / boids.Count();
        Vector2 direction = average - (Vector2)transform.position;
        Vector2 vec = steer(direction.normalized * moveSpeed);
        return vec;
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
        Vector2 vec = steer(direction.normalized * moveSpeed);
        return vec;
    }
    protected Vector2 steer(Vector2 desired)
    {
        Vector2 vec = desired - velocity;
        vec = limitMagnitude(vec, maxDistance);
        return vec;
    }
    protected Vector2 limitMagnitude(Vector2 baseVector, float maxMagnitude)
    {
        if (baseVector.sqrMagnitude > maxMagnitude * maxMagnitude)
        {
            baseVector = baseVector.normalized * maxMagnitude;
        }
        return baseVector;
    }
    protected float distanceTo(Monster_Controller boid)
    {
        return Vector2.Distance(boid.gameObject.transform.position, transform.position);
    }
    protected void crash(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Managers.Game.PlayerController.Hp -= damage * attackSpeed * Time.deltaTime;
        }
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
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}