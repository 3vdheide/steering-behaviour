using UnityEngine;

public class Seek : SteeringBehaviour
{
    void Update()
    {
        CalculateSteeringForce();
        ApplySteering();
    }

    protected override Vector3 CalculateSteeringForce()
    {
        steeringForce = Vector3.ClampMagnitude(GetDesiredVelocity() - currentVelocity, MaxForce);
        return steeringForce;
    }
}
