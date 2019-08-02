using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{
    Direction m_directionComponent;
    public float m_speed = 2.0f;
    Vector3 m_originalPosition;
    Direction.DIRECTION_ENUM m_originalDirection;
    Junction m_lastCollidedJunction = null;

    // Start is called before the first frame update
    void Start()
    {
        m_directionComponent = gameObject.GetComponent<Direction>();
        m_originalPosition = gameObject.transform.position;
        m_originalDirection = m_directionComponent.m_direction;
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

        if(m_lastCollidedJunction)
        {
            bool turn = false;
            switch (m_directionComponent.m_direction)
            {
                case Direction.DIRECTION_ENUM.NORTH:
                    if (gameObject.transform.position.y >= m_lastCollidedJunction.gameObject.transform.position.y)
                        turn = true;
                    break;
                case Direction.DIRECTION_ENUM.EAST:
                    if (gameObject.transform.position.x >= m_lastCollidedJunction.gameObject.transform.position.x)
                        turn = true;
                    break;
                case Direction.DIRECTION_ENUM.SOUTH:
                    if (gameObject.transform.position.y <= m_lastCollidedJunction.gameObject.transform.position.y)
                        turn = true;
                    break;
                case Direction.DIRECTION_ENUM.WEST:
                    if (gameObject.transform.position.x <= m_lastCollidedJunction.gameObject.transform.position.y)
                        turn = true;
                    break;
            }
            if (turn)
            {
                gameObject.transform.position = m_lastCollidedJunction.gameObject.transform.position;
                m_directionComponent.m_direction = m_lastCollidedJunction.GetCurrentOutputDirection();
            }
            m_lastCollidedJunction = null;
        }

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
                m_lastCollidedJunction = junction;

            Victim victim = collision.gameObject.GetComponent<Victim>();
            if (victim)
            {
                victim.Hit();
            }
        }
    }

}
