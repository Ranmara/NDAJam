using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{
    Direction m_directionComponent;
    public float m_speed = 2.0f;
    Vector3 m_originalPosition;
    Direction.DIRECTION_ENUM m_originalDirection;

    // Start is called before the first frame update
    void Start()
    {
        m_directionComponent = gameObject.GetComponent<Direction>();
        m_originalPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Game.s_instance.m_gameState != Game.GAMESTATE.PLAYING)
            return;

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

        // Move
        gameObject.transform.position += directionVector * Time.deltaTime * m_speed;

        // Clamp
        if (gameObject.transform.position.x < Game.s_instance.m_screenExtents.xMin ||
            gameObject.transform.position.x > Game.s_instance.m_screenExtents.xMax ||
            gameObject.transform.position.y < Game.s_instance.m_screenExtents.yMin ||
            gameObject.transform.position.y > Game.s_instance.m_screenExtents.yMax)
        {
            gameObject.transform.position = m_originalPosition;
            m_directionComponent.m_direction = m_originalDirection;
        }

        // Force Z depth
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
