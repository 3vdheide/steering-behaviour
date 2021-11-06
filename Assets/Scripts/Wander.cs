using System.Collections;
using UnityEngine;

public class Wander : Seek
{   
    [Range(2f,7f)]
    public float circleDistance = 2f;

    [Range(0f,3f)]
    public float circleRaduis = 0.5f;
    
    [Range(0f,1f)]
    public float timeBetweenSpawns = 0.2f;
    public bool wrapCamera= true;
    public bool smoothRotation= true;
    float nextSpawnTime;
    float wanderAngle;
    Vector3 circleCenter;
    Vector3 displacement;
    
    float turnTime = 0.8f;

    void Start()
    {
        circleCenter = currentVelocity.normalized * circleDistance;
    }

    void Update()
    {
        target.position = SpawnNewTarget();
        if (wrapCamera) WrapAroundCamera();
        CalculateSteeringForce();
        ApplySteering();
    }

    protected override void RotateToTarget()
    {
        if (smoothRotation)
        {
            Vector3 targetDirection = target.position - transform.position;
            targetDirection.Normalize();
            StartCoroutine(Rotate(targetDirection, turnTime));
        }
        else 
        {
            base.RotateToTarget();
        }
    }

    IEnumerator Rotate(Vector3 targetDirection, float turnTime)
    {
        Quaternion startingRotation = transform.rotation; 
        Quaternion targetRotation =  Quaternion.Euler(0, 0, (Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg) - 90);

        float elapsedTime = 0;
        while (elapsedTime < turnTime) {
            
            elapsedTime += Time.deltaTime; 
            transform.rotation = Quaternion.Slerp(startingRotation, targetRotation, (elapsedTime/turnTime));

            yield return new WaitForEndOfFrame();
        }
    }

    private Vector3 SpawnNewTarget()
    {
        if (Time.time > nextSpawnTime)
        {
            nextSpawnTime = Time.time + timeBetweenSpawns;

            circleCenter = newPosition + currentVelocity.normalized * circleDistance;

            displacement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * circleRaduis;

            Debug.DrawLine(newPosition, circleCenter, Color.red, 1f);
            Debug.DrawLine(newPosition, circleCenter + displacement, Color.magenta, 1f);            
            Debug.DrawLine(circleCenter, circleCenter + displacement, Color.magenta, 1f);
        }

        return circleCenter + displacement;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(circleCenter, circleRaduis);
        
        Gizmos.DrawCube(circleCenter + displacement, new Vector2(0.2f, 0.2f));
    }
}
