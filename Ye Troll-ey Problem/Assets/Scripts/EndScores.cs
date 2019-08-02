using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScores : MonoBehaviour
{
    [SerializeField]

    public Text firstText;
    public Text secondText;
    public Text thirdText;
    public Text fourthText;
    
    public void DisplayScores(int[] passedScores)
    {
        ChangeText();
    }

    public void ChangeText()
    {
        firstText.text = Game.s_instance.m_scores[0].ToString();
        secondText.text = Game.s_instance.m_scores[1].ToString();
        thirdText.text = Game.s_instance.m_scores[2].ToString();
        fourthText.text = Game.s_instance.m_scores[3].ToString();
    }
    
}
