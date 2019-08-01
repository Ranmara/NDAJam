using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victim : MonoBehaviour
{
   
    public float m_speed = 1.0f;
    public float m_minTimeBetweenDirectionChange = 0.5f;
    public float m_maxTimeBetweenDirectionChange = 1.5f;
    public GameObject m_blobShadow;
    public float m_fallSpeed = 1.0f;
    public float m_fallHeight = 20.0f;

    Direction m_directionComponent;
    int m_playerID;
    float m_changeDirectionTimer = 0;
    bool m_spawning = true;
    Vector3 m_targetSpawnPos;

    // Start is called before the first frame update
    void Start()
    {
        m_directionComponent = gameObject.GetComponent<Direction>();
        RandomiseDirection();
        m_playerID = Random.Range((int)1, (int)4);
        m_targetSpawnPos = new Vector3(Random.Range((float)-10.0f, (float)10.0f), Random.Range((float)-10.0f, (float)10.0f), -1.0f);
        gameObject.transform.position = m_targetSpawnPos;
        gameObject.transform.position += Vector3.up * m_fallHeight;
        m_blobShadow.transform.position = m_targetSpawnPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_spawning)
        {
            gameObject.transform.position += Vector3.down * m_fallSpeed * Time.deltaTime;
            if(gameObject.transform.position.y <= m_targetSpawnPos.y)
            {
                gameObject.transform.position = m_targetSpawnPos;
                m_spawning = false;
            }
            m_blobShadow.transform.position = m_targetSpawnPos;
        }
        else
        {
            m_changeDirectionTimer -= Time.deltaTime;
            if (m_changeDirectionTimer <= 0)
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

        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1.0f);
    }

    void RandomiseDirection()
    {
        float difference = m_maxTimeBetweenDirectionChange - m_minTimeBetweenDirectionChange;
        m_changeDirectionTimer = m_minTimeBetweenDirectionChange + Random.Range(0.0f, difference);
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
