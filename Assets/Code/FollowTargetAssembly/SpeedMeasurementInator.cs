using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedMeasurementInator : MonoBehaviour
{
    Vector2 prevPos;
    Vector2 currentPos;
    [SerializeField] short updatesToSkip = 0;
    [SerializeField] Vector2 difference;
    short updateCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        prevPos = transform.position;
        currentPos = transform.position;
    }

    void FixedUpdate()
    {
        if (updateCount == updatesToSkip)
        {
            prevPos = currentPos;
            currentPos = transform.position;

            difference = currentPos - prevPos;
        }
        else
            updateCount++;
    }

    public Vector2 Get_velocity()
    {
        return difference / (updatesToSkip + 1);
    }
}
