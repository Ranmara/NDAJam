using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour
{
    public enum DIRECTION_ENUM
    {
        NORTH,
        EAST,
        SOUTH,
        WEST
    };

    public const int NORTH = 0;
    public const int EAST = 1;
    public const int SOUTH = 2;
    public const int WEST = 3;

    public DIRECTION_ENUM m_direction = DIRECTION_ENUM.NORTH;
}
