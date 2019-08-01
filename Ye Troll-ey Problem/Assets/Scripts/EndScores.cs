using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScores : MonoBehaviour
{

    public PlayerCursor player1CursorScript;
    public PlayerCursor player2CursorScript;
    public PlayerCursor player3CursorScript;
    public PlayerCursor player4CursorScript;

    public int[ , ] playerScoreIndexArray;

    // Start is called before the first frame update
    void Start()
    {
        playerScoreIndexArray[0, 0] = player1CursorScript.m_playerID;
        playerScoreIndexArray[0, 1] = player1CursorScript.m_score;

        playerScoreIndexArray[1, 0] = player2CursorScript.m_playerID;
        playerScoreIndexArray[1, 1] = player2CursorScript.m_score;

        playerScoreIndexArray[2, 0] = player3CursorScript.m_playerID;
        playerScoreIndexArray[2, 1] = player3CursorScript.m_score;

        playerScoreIndexArray[3, 0] = player4CursorScript.m_playerID;
        playerScoreIndexArray[3, 1] = player4CursorScript.m_score;

    }

    
}
