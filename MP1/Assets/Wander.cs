using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour {

    Vector3 startPos;

    // Use this for initialization
    void Start() {
        startPos = transform.position;
        currentXPos = startPos.x;
        lastXPos = startPos.x;
        newXPos = startPos.x;
        moving = true;
    }

    float currentXPos;
    float lastXPos;
    float newXPos;
    float movementTimer;
    float movementTime;

    bool m;
    bool moving
    {
        get
        {
            return m;
        }
        set
        {
            if (value != m)
            {
                m = value;

                if (m)
                {
                    movementTime = Random.Range(0.5f, 0.9f);
                    movementTimer = 0;
                }

                else
                {
                    waitTimer = 0;
                    lastXPos = newXPos;
                    newXPos = startPos.x + Random.Range(-.08f, .08f);
                    waitTime = Random.Range(1.0f, 2.5f);
                }
            }
        }
    }

    float waitTimer;
    float waitTime;

    private void Update()
    {
        if (moving)
        {
            movementTimer += Time.deltaTime;
            currentXPos = Mathf.Lerp(lastXPos, newXPos, movementTimer / movementTime);
            transform.position = new Vector3(currentXPos, transform.position.y, transform.position.z);

            if (movementTimer >= movementTime)
            {
                moving = false;
            }
        }

        else
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                moving = true;
            }
        }
    }
}
