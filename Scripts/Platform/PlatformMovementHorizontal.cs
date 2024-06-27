using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovementHorizontal : MonoBehaviour
{
    public GameObject startingPoint;
    public GameObject endingPoint;

    public Vector3 velocity;
    public int platformWidth;

    bool moveRight = true;

    double endPoint;
    double startPoint;

    float moveSpeed = 2f;


    void FixedUpdate()
    {
        if (transform.position.x > 20.4)
        {
            moveRight = false;
        }
        else if (transform.position.x < 16.6)
        {
            moveRight = true;
        }

        if (moveRight)
        {
            transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y );
        }
        else
        {
            transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
