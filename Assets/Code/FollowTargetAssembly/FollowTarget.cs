using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    GameObject target;

    Vector3 offset;
    Vector2 initialDirection;
    SpeedMeasurementInator speedMeasure;

    Vector2 currentDirection;
    public bool isActive = false; // DON'T CHANGE THIS OUTSIDE OF CLASS
    [SerializeField] short toleranceLoops = 4;
    short measurementStrikes = 0;

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            currentDirection = speedMeasure.Get_velocity().normalized;

            if (Vector2.Dot(currentDirection, initialDirection) <= 0)
            {
                measurementStrikes++;
                if (measurementStrikes >= toleranceLoops)
                {
                    StopFollowing();
                }
                return;
            }

            measurementStrikes = 0;
            transform.position = target.transform.position + offset;

        }
    }

    public void StartFollowing(GameObject target)
    {
        this.target = target;

        speedMeasure = target.GetComponent<SpeedMeasurementInator>();
        offset = transform.position - target.transform.position;
        initialDirection = speedMeasure.Get_velocity().normalized;

        GetComponent<Rigidbody2D>().simulated = false;
        isActive = true;
    }
    public void StopFollowing()
    {
        measurementStrikes = 0;
        GetComponent<Rigidbody2D>().simulated = true;
        isActive = false;
    }
}
