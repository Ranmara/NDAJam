using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{
    Direction m_directionComponent;

    // Start is called before the first frame update
    void Start()
    {
        m_directionComponent = gameObject.GetComponent<Direction>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionVector = Vector3.zero;
        switch(m_directionComponent.m_direction)
        {
            case Direction.NORTH:
                directionVector = Vector3.up;
                break;
            case Direction.EAST:
                directionVector = Vector3.right;
                break;
            case Direction.SOUTH:
                directionVector = Vector3.down;
                break;
            case Direction.WEST:
                directionVector = Vector3.left;
                break;
        }

        gameObject.transform.position = gameObject.transform.position + directionVector * Time.deltaTime;
    }
}
