using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScores : MonoBehaviour
{
    [SerializeField]
    public int[ , ] scoreArray;

    public Text firstText;
    public Text secondText;
    public Text thirdText;
    public Text fourthText;
    
    public void DisplayScores(int[] passedScores)
    {
        // [x,0] - player Id, [x,1] - their score
        scoreArray[0, 0] = 1;
        scoreArray[0, 1] = passedScores[0];

        scoreArray[1, 0] = 2;
        scoreArray[1, 1] = passedScores[1];

        scoreArray[2, 0] = 3;
        scoreArray[2, 1] = passedScores[2];

        scoreArray[3, 0] = 4;
        scoreArray[3, 1] = passedScores[3];

        ArrangeScores();
        ChangeText();

    }


    public void ArrangeScores()
    {
        int temp_var = 0;

        for (int i = 0; i < (scoreArray.Length - 1) ; i++)
        {
            for (int j = 0; j < scoreArray.Length; j++)
            {
                if (scoreArray[i,1] < scoreArray[j,1])
                {
                    temp_var = scoreArray[i, 0];
                    scoreArray[i, 0] = scoreArray[j, 0];
                    scoreArray[j, 0] = temp_var;

                    temp_var = scoreArray[i, 1];
                    scoreArray[i, 1] = scoreArray[j, 1];
                    scoreArray[j, 1] = temp_var;

                }
            }

        }

    }


    public void ChangeText()
    {
        firstText.text = "Player " + scoreArray[0, 0] + " : " + scoreArray[0, 1];
        secondText.text = "Player " + scoreArray[1, 0] + " : " + scoreArray[1, 1];
        thirdText.text = "Player " + scoreArray[2, 0] + " : " + scoreArray[2, 1];
        fourthText.text = "Player " + scoreArray[3, 0] + " : " + scoreArray[3, 1];

    }
    
}
