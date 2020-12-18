using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Ground
{
    [SerializeField] private List<PartOfLevel> groundList= new List<PartOfLevel>();
    [SerializeField]private List<Vector3> vectorList= new List<Vector3>();
    
    public void addToList( PartOfLevel level, Vector3 location)
    {
        groundList.Add(level);
        vectorList.Add(location);
    }

    public List<PartOfLevel> retrieveGroundList()
    {
        return groundList;
    }

    public List<Vector3> retrieveVectorList()
    {
        return vectorList;
    }
    
}
