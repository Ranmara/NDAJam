using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game s_instance;

    public enum GAMESTATE
    {
        FRONTEND,
        GAMEINTRO,
        PLAYING,
        GAMEEND,
    }

    public GAMESTATE m_gameState = GAMESTATE.FRONTEND;
    public float m_timer;
    public GameObject m_frontEnd;
    public float m_spawnVictimTimer;
    public GameObject m_victimPrefab;

    // Start is called before the first frame update
    void Start()
    {
        s_instance = this;
        m_timer = 60.0f;
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_gameState)
        {
            case GAMESTATE.FRONTEND:
                {
                    if (!m_frontEnd.activeInHierarchy)
                        PlayClicked();
                }
                break;

            case GAMESTATE.PLAYING:
                {
                    m_spawnVictimTimer -= Time.deltaTime;
                    if(m_spawnVictimTimer <= 0 )
                    {
                        Instantiate(m_victimPrefab, Vector3.zero, Quaternion.identity, this.transform);
                        m_spawnVictimTimer = 0.5f;
                    }

                    m_timer -= Time.deltaTime;
                    if (m_timer <= 0)
                    {
                        m_gameState = GAMESTATE.GAMEEND;
                        // Show game end
                    }
                }
                break;
        }
    }

    public void PlayClicked()
    {
        m_frontEnd.SetActive(false);
        m_gameState = GAMESTATE.PLAYING;
    }
}
