using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{
    Direction m_directionComponent;
    public float m_speed = 2.0f;

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
            case Direction.DIRECTION_ENUM.NORTH:
                directionVector = Vector3.up;
                break;
            case Direction.DIRECTION_ENUM.EAST:
                directionVector = Vector3.right;
                break;
            case Direction.DIRECTION_ENUM.SOUTH:
                directionVector = Vector3.down;
                break;
            case Direction.DIRECTION_ENUM.WEST:
                directionVector = Vector3.left;
                break;
        }

        gameObject.transform.position += directionVector * Time.deltaTime * m_speed;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject)
        {
            Junction junction = collision.gameObject.GetComponent<Junction>();
            if (junction)
            {
                gameObject.transform.position = collision.gameObject.transform.position;
                m_directionComponent.m_direction = junction.GetCurrentOutputDirection();
            }

            Victim victim = collision.gameObject.GetComponent<Victim>();
            if (victim)
            {
                victim.Hit();
            }
        }
    }

}
