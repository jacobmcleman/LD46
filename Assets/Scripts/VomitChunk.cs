using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VomitChunk : MonoBehaviour
{
    public float maxSpeed = 20;
    public float speed = 20;
    public float rSpeed = 5;

    public float stopDistance = 10;

    public IEnumerator HuntWhale(GameObject Whale)
    {
        bool hunting = true;
        bool stopping = false;

        Vector3 targetDir;
        Vector3 stopPoint = transform.forward * stopDistance;

        float rStep;
        float step;

        speed = maxSpeed;

        while (hunting)
        {
            targetDir = Whale.transform.position - transform.position;
            if (Vector3.Angle(targetDir, transform.forward) > 30f)
            {
                hunting = false;
                stopping = true;
                stopPoint = transform.forward * stopDistance;
                break;
            }
            else
            {
                rStep = rSpeed * Time.deltaTime;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDir), rStep);

                step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, Whale.transform.position, step);
            }

            yield return null;
        }

        while (stopping)
        {
            if (Vector3.Distance(transform.position, stopPoint) < 0.001f)
            {
                stopping = false;
                break;
            }
            else
            {
                step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, stopPoint, step);
                speed = Mathf.Clamp(speed - .1f, 0, maxSpeed);
            }

             yield return null;
        }
    }


}
