using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victim : MonoBehaviour
{
    public static List<Victim> s_victims;
    float m_deathTimer;

    enum STATE
    {
        Spawning,
        Walking,
        Dying
    }

    public float m_speed = 1.0f;
    public float m_minTimeBetweenDirectionChange = 0.5f;
    public float m_maxTimeBetweenDirectionChange = 1.5f;
    public GameObject m_blobShadow;
    public float m_fallSpeed = 1.0f;
    public float m_fallHeight = 20.0f;
    public float m_roamDistance = 0.6f;
    public SoundVariation m_SFX_splat;
    public SoundVariation m_SFX_scream;
    public int m_forceSpawnPoint = -1;
    public int m_forcePlayerID = -1;

    Direction m_directionComponent;
    int m_playerID;
    float m_changeDirectionTimer = 0;
    STATE m_state = STATE.Spawning;
    Vector3 m_targetSpawnPos;

    public Animator animator;
    public Animator[] my_animators;


    // Start is called before the first frame update
    void Start()
    {
        if (s_victims == null)
            s_victims = new List<Victim>();

        s_victims.Add(this);

        m_directionComponent = gameObject.GetComponent<Direction>();
        RandomiseDirection();
        m_playerID = m_forcePlayerID;
        if (m_playerID == -1)
            m_playerID = Random.Range((int)0, (int)4);

        ChangeColour(m_playerID);

        // Spawn at random junction
        if (Spawnpoint.s_spawnpoints != null && Spawnpoint.s_spawnpoints.Count > 0)
        {
            int rand = m_forceSpawnPoint;
            if (rand == -1)
                rand = Random.Range((int)0, (int)Spawnpoint.s_spawnpoints.Count);
            m_targetSpawnPos = Spawnpoint.s_spawnpoints[rand].gameObject.transform.position;
            m_targetSpawnPos.x -= m_roamDistance / 2;
            m_targetSpawnPos.y -= m_roamDistance / 2;
            m_targetSpawnPos.x += Random.Range(0, m_roamDistance);
            m_targetSpawnPos.y += Random.Range(0, m_roamDistance);
        }
        else
            m_targetSpawnPos = new Vector3(Random.Range((float)-10.0f, (float)10.0f), Random.Range((float)-10.0f, (float)10.0f), -1.0f);

        gameObject.transform.position = m_targetSpawnPos;
        gameObject.transform.position += Vector3.up * m_fallHeight;
        m_blobShadow.transform.position = m_targetSpawnPos;
    }

    private void OnDestroy()
    {
        s_victims.Remove(this);
    }

    // Update is called once per frame
    void Update()
    {
        switch(m_state)
        { 
            case STATE.Spawning:
                gameObject.transform.position += Vector3.down * m_fallSpeed * Time.deltaTime;
                if (gameObject.transform.position.y <= m_targetSpawnPos.y)
                {
                    gameObject.transform.position = m_targetSpawnPos;
                    m_state = STATE.Walking;
                }
                m_blobShadow.transform.position = m_targetSpawnPos;
                animator.SetInteger("TrollState", 0);
                break;
            case STATE.Walking:
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

                    // Move
                    gameObject.transform.position += directionVector * Time.deltaTime * m_speed;

                    // Clamp
                    if (gameObject.transform.position.x < m_targetSpawnPos.x - m_roamDistance)
                    {
                        gameObject.transform.position = new Vector3(m_targetSpawnPos.x - m_roamDistance, gameObject.transform.position.y, gameObject.transform.position.z);
                        RandomiseDirection();
                    }
                    else if (gameObject.transform.position.x > m_targetSpawnPos.x + m_roamDistance)
                    {
                        gameObject.transform.position = new Vector3(m_targetSpawnPos.x + m_roamDistance, gameObject.transform.position.y, gameObject.transform.position.z);
                        RandomiseDirection();
                    }

                    if (gameObject.transform.position.y < m_targetSpawnPos.y - m_roamDistance)
                    {
                        gameObject.transform.position = new Vector3(gameObject.transform.position.x, m_targetSpawnPos.y - m_roamDistance, gameObject.transform.position.z);
                        RandomiseDirection();
                    }
                    else if (gameObject.transform.position.y > m_targetSpawnPos.y + m_roamDistance)
                    {
                        gameObject.transform.position = new Vector3(gameObject.transform.position.x, m_targetSpawnPos.y + m_roamDistance, gameObject.transform.position.z);
                        RandomiseDirection();
                    }

                    if (m_directionComponent.m_direction == Direction.DIRECTION_ENUM.EAST)
                        animator.SetBool("FlipSprite", false);
                    if (m_directionComponent.m_direction == Direction.DIRECTION_ENUM.WEST)
                        animator.SetBool("FlipSprite", true);
                    animator.SetInteger("TrollState", 1);
                }
                break;

            case STATE.Dying:
                {
                    m_deathTimer -= Time.deltaTime;
                    if (m_deathTimer <= 0)
                    {
                        GameObject.Destroy(gameObject);
                        return;
                    }

                    animator.SetInteger("TrollState", 2);
                }
                break;
        }

        // Force Z depth
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
        if (m_state != STATE.Walking)
            return;

        // TODO: Splatter VFX, +1
        if (m_SFX_splat)
            m_SFX_splat.PlayRandom();
        if(m_SFX_scream)
        {
            if(Random.Range((int)1, (int)5) == 1)
                m_SFX_scream.PlayRandom();
        }
        m_state = STATE.Dying;
        m_deathTimer = 1.0f;

        PlayerCursor player = PlayerCursor.GetPlayer(m_playerID);
        if(player)
            ++player.m_score;
    }

    void ChangeColour(int player_id)
    {
        animator.SetInteger("TrollId", player_id);
    }
}
