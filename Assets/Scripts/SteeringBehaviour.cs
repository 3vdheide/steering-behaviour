using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteeringBehaviour : MonoBehaviour
{
    [SerializeField]
    protected Transform target;

    protected Vector3 currentVelocity;
    protected Vector3 steeringForce;
    protected Vector3 newPosition;

    [Range(0f,10f)]
    [SerializeField]
    protected float MaxSpeed = 5f;
    protected float MaxForce = 0.8f;
    float halfAgentWidth;
    float halfAgentHeight;
    float halfScreenHeight;
    float halfScreenWidth;

    void Awake()
    {
        halfAgentHeight = transform.localScale.y/2f;
        halfAgentWidth = transform.localScale.x/2f;
        halfScreenHeight = Camera.main.orthographicSize;
        halfScreenWidth = Camera.main.aspect * halfScreenHeight;
    }

    void Start()
    {
        steeringForce = Vector3.zero;
        currentVelocity = Vector3.zero;
        newPosition = transform.position;
    }

    protected abstract Vector3 CalculateSteeringForce();

    protected virtual void RotateToTarget()
    {
        Vector3 directionToNewPostion = newPosition - transform.position;
        directionToNewPostion.Normalize();
        transform.rotation = Quaternion.Euler(0, 0, (Mathf.Atan2(directionToNewPostion.y, directionToNewPostion.x) * Mathf.Rad2Deg) - 90);
    }

    protected void ApplySteering()
    {
        currentVelocity = Vector3.ClampMagnitude(currentVelocity + steeringForce, MaxSpeed);
        newPosition += currentVelocity * Time.deltaTime;

        RotateToTarget();

        transform.position = newPosition;
    }

    protected Vector3 GetDesiredVelocity()
    {
        Vector3 desiredVelocity = target.position - transform.position;
        desiredVelocity.Normalize();
        return desiredVelocity *= MaxSpeed;
    }

    protected void WrapAroundCamera()
    {
        float yBoundry = halfScreenHeight + halfAgentHeight;
        float xBoundry = halfScreenWidth + halfAgentWidth;
        
        if (transform.position.y > yBoundry)
        {
            transform.position = new Vector3(transform.position.x, -yBoundry, transform.position.z);
        }
        else if(transform.position.y < -yBoundry)
        {
            transform.position = new Vector3(transform.position.x, yBoundry, transform.position.z);
        }
        else if (transform.position.x > xBoundry)
        {
            transform.position = new Vector3(-xBoundry, transform.position.y, transform.position.z);
        }
        else if(transform.position.x < -xBoundry)
        {
            transform.position = new Vector3(xBoundry, transform.position.y, transform.position.z);
        }
        newPosition = transform.position;
    }
}
