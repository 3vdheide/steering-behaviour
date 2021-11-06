using UnityEngine;

public class Flee : Seek
{
    void Update()
    {
        WrapAroundCamera();
        CalculateSteeringForce();
        ApplySteering();
    }

    protected override Vector3 CalculateSteeringForce()
    {
        steeringForce = base.CalculateSteeringForce()  * -1;
        return steeringForce;
    }
}
