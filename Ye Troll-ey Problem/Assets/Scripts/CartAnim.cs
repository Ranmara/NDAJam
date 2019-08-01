using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Direction))]

public class CartAnim : MonoBehaviour
{

    [SerializeField] private Animator animComponent;
    [SerializeField] private Direction directionComponent;

    public int animDirection;

    // Update is called once per frame
    void Update()
    {
        
        // 0 - Diag R,  1 - Verticle
        // 2 - Diag L,  3 - Horizontal

        switch (directionComponent.m_direction)
        {
            case (Direction.DIRECTION_ENUM.NORTH):
                animDirection = 1;
                break;

            case (Direction.DIRECTION_ENUM.EAST):
                animDirection = 3;
                break;

            case (Direction.DIRECTION_ENUM.SOUTH):
                animDirection = 1;
                break;

            case (Direction.DIRECTION_ENUM.WEST):
                animDirection = 3;
                break;
        }


        animComponent.SetInteger("CartDirection", animDirection);

    }
}
