using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin
{
    private List<Vector3> positions = new List<Vector3>();

    public void addList(Vector3 locations)
    {
        positions.Add(locations);
    }
    public List<Vector3> retrieveCoinPositions()
    {
        return positions;
    }
}
