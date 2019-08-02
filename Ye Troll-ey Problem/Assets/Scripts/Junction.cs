using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Junction : MonoBehaviour
{
    public static List<Junction> s_junctions;

    public List<Direction.DIRECTION_ENUM> m_outputDirections;
    public List<Sprite> m_sprites;
    public SoundVariation m_SFX_switch;
    public SpriteRenderer m_switchSprite;

    public Sprite m_switchSpriteA;
    public Sprite m_switchSpriteB;

    int m_currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (s_junctions == null)
            s_junctions = new List<Junction>();

        s_junctions.Add(this);

        UpdateSprite();
        if (m_switchSprite)
        {
            if (m_outputDirections.Count > 1)
                m_switchSprite.sprite = m_switchSpriteA;
            else
                m_switchSprite.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
    }

    public Direction.DIRECTION_ENUM GetCurrentOutputDirection()
    {
        if (m_currentIndex >= m_outputDirections.Count)
            return 0;

        return m_outputDirections[m_currentIndex];
    }

    public void Switch()
    {
        if (m_outputDirections.Count < 1)
            return;

        if(m_SFX_switch)
            m_SFX_switch.PlayRandom();

        if (++m_currentIndex >= m_outputDirections.Count)
            m_currentIndex = 0;
        UpdateSprite();

        if (m_switchSprite)
        {
            if (m_switchSprite.sprite == m_switchSpriteA)
                m_switchSprite.sprite = m_switchSpriteB;
            else
                m_switchSprite.sprite = m_switchSpriteA;
        }
    }

    void UpdateSprite()
    {
        if (m_currentIndex < m_sprites.Count)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer)
            {
                spriteRenderer.sprite = m_sprites[m_currentIndex];
            }
        }
    }
}
