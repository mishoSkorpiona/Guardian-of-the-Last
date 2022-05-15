using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleFollower : MonoBehaviour
{
    public Animator animator;

    public float turnTime = 0.4f;
    public float defaultWaitTime = 0;
    public float speed = 0.5f;

    public Direction[] directions;
    public float[] distances;
    public float[] waitTimes;

    int cycleLen;
    int currentPoint;

    const float degrees = 360f / 6;

    Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;

        cycleLen = Mathf.Min(directions.Length, distances.Length);

        Vector3 endPos = startPos;

        for (int i = 0; i < cycleLen; i++)
        {
            endPos += DirectionToVector(directions[i]) * distances[i];
        }

        if (endPos != startPos)
        {
            Debug.LogWarning("Cycle not complete", gameObject);
        }

        StartCoroutine(FollowCycle());
    }

    public IEnumerator FollowCycle()
    {
        currentPoint = 0;

        while (true)
        {
            Vector3 currentDirection = DirectionToVector(directions[currentPoint]);
            float currentDistance = 0;


            //turn time

            Quaternion currentRotation = transform.rotation;
            Quaternion endAngle = Quaternion.LookRotation(currentDirection, Vector3.up);

            float targetTotal = turnTime / Time.fixedDeltaTime;

            for (int i = 0; i / targetTotal < 1; i++)
            {
                transform.rotation = Quaternion.Lerp(currentRotation, endAngle, i/targetTotal);
                yield return new WaitForFixedUpdate();
            }

            animator.SetBool("Moving", true);

            // Big O(no)
            while (currentDistance < distances[currentPoint])
            {
                float distanceMovedThisFrame = speed * Time.fixedDeltaTime;

                transform.position += currentDirection * distanceMovedThisFrame;
                currentDistance += distanceMovedThisFrame;

                yield return new WaitForFixedUpdate();
            }

            transform.position = startPos + currentDirection * distances[currentPoint];

            if (currentPoint < waitTimes.Length && waitTimes[currentPoint] >= 0)
            {
                animator.SetBool("Moving", false);
                yield return new WaitForSeconds(waitTimes[currentPoint]);
            }
            else if (defaultWaitTime > 0 || (currentPoint < waitTimes.Length && waitTimes[currentPoint] == -1))
            {
                animator.SetBool("Moving", false);
                yield return new WaitForSeconds(defaultWaitTime);
            }

            startPos = transform.position;
            currentPoint++;
            currentPoint %= cycleLen;

        }
    }

    public Vector3 DirectionToVector(Direction d)
    {
        return Quaternion.AngleAxis((int)d * degrees, Vector3.up) * Vector3.forward;
    }

    //this isn't very performant, kill the visualisation if it gets too bad, but it's very helpful for editing
    private void OnDrawGizmos()
    {
        cycleLen = Mathf.Min(directions.Length, distances.Length);

        Gizmos.color = Color.green;

        for (int i = 0; i < cycleLen; i++)
        {
            Vector3 pos = startPos;

            for (int j = currentPoint; j != i; j %= cycleLen)
            {
                pos += DirectionToVector(directions[j]) * distances[j];
                j++;
            }

            Gizmos.DrawSphere(pos, 0.5f);
        }
    }
}
