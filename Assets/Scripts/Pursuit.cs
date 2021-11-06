using UnityEngine;

public class Pursuit : Seek
{
    float futureProbability;
    Vector3 targetVelocity;
    Vector3 targetPosition;

    void Update()
    {   
        //Pursuit logic

        CalculateSteeringForce();
        ApplySteering();
    }
}
