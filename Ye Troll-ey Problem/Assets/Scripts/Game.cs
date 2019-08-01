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

    // Start is called before the first frame update
    void Start()
    {
        m_timer = 60.0f;
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_gameState)
        {
            case GAMESTATE.PLAYING:
                {
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
}
