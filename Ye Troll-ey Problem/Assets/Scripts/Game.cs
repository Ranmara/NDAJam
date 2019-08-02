﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game s_instance;

    public UnityEngine.UI.Text m_timeText;

    public Vector2 m_gameplayArea = new Vector2(10.0f, 10.0f);
    public Rect m_screenExtents = Rect.zero;

    public SoundVariation m_SFX_playClicked;
    public SoundVariation m_SFX_startGame;
    public SoundVariation m_SFX_endGame;

    public enum GAMESTATE
    {
        INIT,
        FRONTEND,
        GAMEINTRO,
        PLAYING,
        ENDSCORES,
    }

    public GAMESTATE m_gameState = GAMESTATE.FRONTEND;
    public float m_timer = 0;
    public float m_gameTime = 60.0f;
    public int[] m_scores = new int[4];
    public GameObject m_frontEnd;
    public GameObject m_hud;
    public float m_spawnVictimTimer;
    public GameObject m_victimPrefab;
    public int m_maxVictims = 100;
    public GameObject m_endScores;
    public GameObject m_playerEndScores;
    public EndScores m_endScoresScript;
    public bool m_scoresDisplayed;
    int m_clustersSpawned = 0;

    float m_gameIntroTimer;

    // Start is called before the first frame update
    void Start()
    {
        m_screenExtents = new Rect(-m_gameplayArea.x / 2, -m_gameplayArea.y / 2, m_gameplayArea.x, m_gameplayArea.y);
        s_instance = this;
        m_timer = m_gameTime;

        SetState(GAMESTATE.INIT);
    }

    void SetState(GAMESTATE gameState)
    {
        // ====================================
        // State initialisation
        // ====================================

        switch (gameState)
        {
            case GAMESTATE.FRONTEND:
                break;

            case GAMESTATE.GAMEINTRO:
                m_gameIntroTimer = 1.5f;
                if (m_SFX_playClicked)
                    m_SFX_playClicked.PlayRandom();
                break;

            case GAMESTATE.PLAYING:
                m_clustersSpawned = 0;
                m_timer = m_gameTime;
                if (m_SFX_startGame)
                    m_SFX_startGame.PlayRandom();
                break;

            case GAMESTATE.ENDSCORES:
                if (m_SFX_endGame)
                    m_SFX_endGame.PlayRandom();
                m_scoresDisplayed = false;
                if (Victim.s_victims != null)
                {
                    for (int i = 0; i < Victim.s_victims.Count; ++i)
                        GameObject.Destroy(Victim.s_victims[i].gameObject);
                }
                break;
        }

        // ====================================
        // Activate objects for specific states
        // ====================================

        m_frontEnd.SetActive(gameState == GAMESTATE.FRONTEND);
        m_hud.SetActive(gameState != GAMESTATE.FRONTEND);
        m_endScores.SetActive(gameState == GAMESTATE.ENDSCORES);

        if (PlayerCursor.s_players != null)
        {
            for (int i = 0; i < PlayerCursor.s_players.Count; ++i)
                PlayerCursor.s_players[i].gameObject.SetActive(gameState == GAMESTATE.PLAYING);
        }

        // ====================================

        m_gameState = gameState;
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_gameState)
        {
            case Game.GAMESTATE.INIT:
                {
                    SetState(GAMESTATE.FRONTEND);
                }
                break;
            case GAMESTATE.FRONTEND:
                {
                    if (!m_frontEnd.activeInHierarchy)
                        PlayClicked();
                }
                break;

            case GAMESTATE.GAMEINTRO:
                {
                    m_gameIntroTimer -= Time.deltaTime;
                    if (m_gameIntroTimer <= 0)
                        SetState(GAMESTATE.PLAYING);
                }
                break;

            case GAMESTATE.PLAYING:
                {
                    m_spawnVictimTimer -= Time.deltaTime;
                    if(m_spawnVictimTimer <= 0 )
                    {
                        if (Victim.s_victims == null || Victim.s_victims.Count < m_maxVictims)
                            Instantiate(m_victimPrefab, Vector3.zero, Quaternion.identity, this.transform);
                        m_spawnVictimTimer = 0.5f;
                    }

                    if(m_clustersSpawned == 0 && m_timer < m_gameTime - 15.0f ||
                        m_clustersSpawned == 1 && m_timer < m_gameTime - 30.0f ||
                        m_clustersSpawned == 2 && m_timer < m_gameTime - 45.0f)
                    {
                        for (int p = 0; p < 4; ++p)
                        {
                            int spawnPoint = Random.Range((int)0, (int)Spawnpoint.s_spawnpoints.Count);
                            for (int i = 0; i < 3 + m_clustersSpawned; ++i)
                            {
                                if (Victim.s_victims == null || Victim.s_victims.Count < m_maxVictims)
                                {
                                    GameObject newGO = Instantiate(m_victimPrefab, Vector3.zero, Quaternion.identity, this.transform);
                                    Victim victim = newGO.GetComponent<Victim>();
                                    if (victim)
                                    {
                                        victim.m_forcePlayerID = p;
                                        victim.m_forceSpawnPoint = spawnPoint;
                                    }
                                }
                            }
                        }
                        ++m_clustersSpawned;
                    }

                    m_timer -= Time.deltaTime;
                    if (m_timer <= 0)
                        SetState(GAMESTATE.ENDSCORES);
                }
                break;

            case GAMESTATE.ENDSCORES:
                if (m_scoresDisplayed == false)
                {
                    m_endScoresScript = m_playerEndScores.GetComponent<EndScores>();

                    m_endScoresScript.DisplayScores(m_scores);

                    m_scoresDisplayed = true;
                }

                if (!m_endScores.activeInHierarchy)
                    RestartClicked();
                break;
        }

        int seconds = (int)m_timer;
        int miliseconds = (int)((m_timer - seconds) * 100.0f);
        m_timeText.text = seconds.ToString();
        m_timeText.text += ":";
        m_timeText.text += miliseconds.ToString();
    }

    public void PlayClicked()
    {
        SetState(GAMESTATE.GAMEINTRO);
    }

    public void RestartClicked()
    {
        SetState(GAMESTATE.FRONTEND);
    }
}
