using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTester
{
    private float playerJumpPower;
    private float playerSpeed;

    private float maxJumpHeight;
    private float maxJumpTime;
    private float maxDistanceTravled;

    private float a = 0;
    private float b = 0;

    private List<GameObject> partsOfLevel = new List<GameObject>();

    private int index = 0;

    //private GameManagerController gameManager;

    public LevelTester(List<GameObject> gameObjects /*, GameManagerController gameManagerController*/)
    {
        //playerJumpPower = PlayerController.jumpHeight;
        //playerSpeed = PlayerController.speed;
        maxJumpHeight = .0509669242f * Mathf.Pow(playerJumpPower, 2) - .0101318455f * playerJumpPower + 1.000670667f;
        maxJumpTime = .2f * playerJumpPower;
        maxDistanceTravled = maxJumpTime * playerSpeed;
        partsOfLevel = gameObjects;

        //gameManager = gameManagerController;

        MakeParabola();
    }
    
    private void MakeParabola()
    {
        /*
         * f(x) = a(x - r)(x - s)
         * maxJumpHeight = a(maxDistance/2 - 0)(maxDistance/2 - maxDistance)
         * maxJumpHeight = a(Mathf.pow(maxDistance/2,2) - Mathf.pow(maxDistance,2)/2
         * maxJumpHeight/((Mathf.pow(maxDistance/2,2) - Mathf.pow(maxDistance,2)/2) = a
         * maxDistanceTravled/2 = -b/2a
         * a * maxDistanceTravled = -b
         */
        a = maxJumpHeight / ((Mathf.Pow(maxDistanceTravled / 2, 2) - Mathf.Pow(maxDistanceTravled, 2) / 2));
        b = -(a * maxDistanceTravled);
    }

    private bool CanMakeJump(GameObject startObject, GameObject endObject)
    {
        
        
        

        float xCordinateOfEndOfSO = startObject.transform.position.x + startObject.transform.localScale.x / 2;
        float yCordinateOFEndOfSO = startObject.transform.position.y + startObject.transform.localScale.y / 2;

        float xCordinateOfBeginingOfEO = endObject.transform.position.x - endObject.transform.localScale.x / 2;
        float yCordinateOfBeginingOfEO = endObject.transform.position.y + endObject.transform.localScale.y / 2;

        float endXCordinateNormilized = xCordinateOfBeginingOfEO - xCordinateOfEndOfSO;
        float endYCordinateNormilized = yCordinateOfBeginingOfEO - yCordinateOFEndOfSO;

        float supposedYCordinate = a * Mathf.Pow(endXCordinateNormilized, 2) + b * endXCordinateNormilized;
        float otherPotentialYCordinate = 0;

        if(startObject.tag == "MovingRight")
        {
            //MovingRightController movingRight = startObject.GetComponent<MovingRightController>();
            //otherPotentialYCordinate = a * Mathf.Pow(xCordinateOfBeginingOfEO - movingRight.distanceToTravel.x, 2) + b * (xCordinateOfBeginingOfEO - movingRight.distanceToTravel.x);
        }
     
        
        
        if (endYCordinateNormilized <= maxJumpHeight && endXCordinateNormilized == 0)
        {
            return true;
        }
        else if (supposedYCordinate >= endYCordinateNormilized || otherPotentialYCordinate <= supposedYCordinate)
        {
            return true;
        }
        
        return false;
    }

    public void GrabList(List<GameObject> gameObjects)
    {
        partsOfLevel = gameObjects;
    }

    public bool GoThroughLevel()
    {
        if (index + 1 < partsOfLevel.Count - 1)
        {
            if (CanMakeJump(partsOfLevel[index], partsOfLevel[index + 1]))
            {
                index++;
                return GoThroughLevel();
            }

            //gameManager.LevelCantBePlayed(index);
            return false;
        }
        Debug.Log("Level Is Playable");
        return true;
    }
}
