using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    float m_timer;

    // Start is called before the first frame update
    void Start()
    {
        m_timer = 60.0f;
    }

    // Update is called once per frame
    void Update()
    {
        m_timer -= Time.deltaTime;
        if(m_timer <= 0)
        {

        }
    }
}
