using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    static List<PlayerCursor> s_players;

    public static float s_speed = 30.0f;
    public int m_playerID = 0;
    public int m_score = 0;
    public UnityEngine.UI.Text m_scoreText;

    Junction m_overJunction = null;

    // Start is called before the first frame update
    void Start()
    {
        s_players.Add(this);
    }

    public static PlayerCursor GetPlayer(int playerId)
    {
        for(int i = 0; i < s_players.Count; ++i)
        {
            if (s_players[i].m_playerID == playerId)
                return s_players[i];
        }

        return null;
    }

    // Update is called once per frame
    void Update()
    {
        switch(m_playerID)
        {
            case 0:
                gameObject.transform.position += Vector3.right * Time.deltaTime * Input.GetAxis("P1_Horizontal") * s_speed;
                gameObject.transform.position += Vector3.up * Time.deltaTime * Input.GetAxis("P1_Vertical") * s_speed;
                if (Input.GetButtonDown("P1_Switch"))
                    Switch();
                break;
            case 1:
                gameObject.transform.position += Vector3.right * Time.deltaTime * Input.GetAxis("P2_Horizontal") * s_speed;
                gameObject.transform.position += Vector3.up * Time.deltaTime * Input.GetAxis("P2_Vertical") * s_speed;
                if (Input.GetButtonDown("P2_Switch"))
                    Switch();
                break;
            case 2:
                gameObject.transform.position += Vector3.right * Time.deltaTime * Input.GetAxis("P3_Horizontal") * s_speed;
                gameObject.transform.position += Vector3.up * Time.deltaTime * Input.GetAxis("P3_Vertical") * s_speed;
                if (Input.GetButtonDown("P3_Switch"))
                    Switch();
                break;
            case 3:
                gameObject.transform.position += Vector3.right * Time.deltaTime * Input.GetAxis("P4_Horizontal") * s_speed;
                gameObject.transform.position += Vector3.up * Time.deltaTime * Input.GetAxis("P4_Vertical") * s_speed;
                if (Input.GetButtonDown("P4_Switch"))
                    Switch();
                break;
        }

        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -2.0f);
        m_scoreText.text = m_score.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject)
        {
            Junction junction = collision.gameObject.GetComponent<Junction>();
            if (junction)
            {
                m_overJunction = junction;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject && collision.gameObject == m_overJunction)
            m_overJunction = null;
    }

    void Switch()
    {
        if(m_overJunction)
        {
            m_overJunction.Switch();
        }
    }
}
