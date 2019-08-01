using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victim : MonoBehaviour
{
    int m_playerID;
    Direction m_directionComponent;
    public float m_speed = 1.0f;
    float m_changeDirectionTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_directionComponent = gameObject.GetComponent<Direction>();
        RandomiseDirection();
        m_playerID = Random.Range((int)1, (int)4);
    }

    // Update is called once per frame
    void Update()
    {
        m_changeDirectionTimer -= Time.deltaTime;
        if(m_changeDirectionTimer <= 0)
            RandomiseDirection();

        Vector3 directionVector = Vector3.zero;
        switch (m_directionComponent.m_direction)
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
    }

    void RandomiseDirection()
    {
        m_changeDirectionTimer = 1.0f + Random.Range(0.0f, 2.0f);
        m_directionComponent.m_direction = (Direction.DIRECTION_ENUM)Random.Range((int)0, (int)4);
    }

    public void Hit()
    {
        GameObject.Destroy(gameObject);

        // TODO: VFX
        PlayerCursor player = PlayerCursor.GetPlayer(m_playerID);
        if(player)
        {
            ++player.m_score;
        }
    }
}
