using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    public static List<Spawnpoint> s_spawnpoints;

    // Start is called before the first frame update
    void Start()
    {
        if (s_spawnpoints == null)
            s_spawnpoints = new List<Spawnpoint>();

        s_spawnpoints.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
