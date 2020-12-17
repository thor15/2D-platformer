using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private List<PartOfLevel> groundList= new List<PartOfLevel>();
    private List<Vector3> vectorList= new List<Vector3>();
    
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
