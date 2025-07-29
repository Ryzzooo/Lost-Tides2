using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform platform;
    public Transform StartPoint;
    public Transform EndPoint;
    int direction = 1;
    public float moveSpeed = 2f;

    private void Update()
    {
        Vector2 target = currentMovementTarget();
        platform.position = Vector2.MoveTowards(platform.position, target, moveSpeed * Time.deltaTime);
        float distance = (target - (Vector2)platform.position).magnitude;
        if (distance < 0.1f)
        {
            direction *= -1; // Reverse direction
        }
    }

    private Vector2 currentMovementTarget()
    {
        if (direction == 1)
        {
            return StartPoint.position;
        }
        else
        {
            return EndPoint.position;
        }
    }

    private void OnDrawGizmos()
    {
        if (platform != null && StartPoint != null && EndPoint != null)
        {

            Gizmos.DrawLine(platform.position, StartPoint.position);
            Gizmos.DrawLine(platform.position, EndPoint.position);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(platform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}



    // Start is called before the first frame update
