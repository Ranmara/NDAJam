using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundVariation : MonoBehaviour
{
    public AudioClip[] clipArray;
    AudioSource m_audioSource;
    int m_clipIndex;
    public float m_pitchMin = 1.0f;
    public float m_pitchMax = 1.0f;
    public float m_volumeMin = 1.0f;
    public float m_volumeMax = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int RepeatCheck(int previousIndex, int range)
    {
        int index = Random.Range(0, range);

        while (index == previousIndex)
        {
            index = Random.Range(0, range);
        }
        return index;
    }

    public void PlayRandom()
    {
        if (m_audioSource == null)
            return;

        m_audioSource.pitch = Random.Range(m_pitchMin, m_pitchMax);
        m_audioSource.volume = Random.Range(m_volumeMin, m_volumeMax);

        m_clipIndex = RepeatCheck(m_clipIndex, clipArray.Length);
        m_audioSource.PlayOneShot(clipArray[m_clipIndex]);
    }
}
