using UnityEngine;

public class Arrive : SteeringBehaviour
{
    [SerializeField]
    [Range(0f,5f)]
    float slowingDistance;


    // Update is called once per frame
    void Update()
    {
        CalculateSteeringForce();
        ApplySteering();
    }

    protected override Vector3 CalculateSteeringForce()
    {
        Vector3 desiredVelocity = GetDesiredVelocity();

        float distanceToTarget = Vector2.Distance(target.position, transform.position);

        if(distanceToTarget < slowingDistance)
        {
            desiredVelocity *= distanceToTarget;
        }

        steeringForce = Vector3.ClampMagnitude(desiredVelocity - currentVelocity, MaxForce);
        return steeringForce;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(target.position, slowingDistance);
    }
}
