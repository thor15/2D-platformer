using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Coin
{
    [SerializeField] private List<Vector3> positions = new List<Vector3>();

    public void addList(Vector3 locations)
    {
        positions.Add(locations);
    }
    public List<Vector3> retrieveCoinPositions()
    {
        return positions;
    }
}
