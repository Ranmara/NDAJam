using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Junction : MonoBehaviour
{
    public List<Direction.DIRECTION_ENUM> m_outputDirections;
    public List<Sprite> m_sprites;
    public SoundVariation m_SFX_switch;

    int m_currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdateSprite();
    }

    // Update is called once per frame
    void Update()
    {
        
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
