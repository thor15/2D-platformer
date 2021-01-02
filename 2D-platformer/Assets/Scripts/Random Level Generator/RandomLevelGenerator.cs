using UnityEngine;

public class RandomLevelGenerator
{
    private float speedOfPlayer;
    private float powerOfJump;
    private RandomGameManager gameManager;

    private float maxJumpHeight;
    private float maxJumpTime;
    private float maxDistanceTravled;

    private float parabolaA = 0;
    private float parabolaB = 0;

    private Vector3 lastPlacement = new Vector3(0,0,0);
    private PartOfLevel levelPart = PartOfLevel.Ground;


    public RandomLevelGenerator(float playerSpeed, float jumpHeight, RandomGameManager randomGameManager)
    {
        speedOfPlayer = playerSpeed;
        powerOfJump = jumpHeight;
        gameManager = randomGameManager;
        MakeParabola();
    }


    private void MakeParabola()
    {
        maxJumpHeight = .0509669242f * Mathf.Pow(powerOfJump, 2) - .0101318455f * powerOfJump + 1.000670667f;
        maxJumpTime = .2f * powerOfJump;
        maxDistanceTravled = maxJumpTime * speedOfPlayer;

        /*
         * f(x) = a(x - r)(x - s)      f(x) = ax^2 + bx + c
         * maxJumpHeight = a(maxDistance/2 - 0)(maxDistance/2 - maxDistance)
         * maxJumpHeight = a((Mathf.pow(maxDistance/2,2) - Mathf.pow(maxDistance,2)/2)
         * maxJumpHeight/((Mathf.pow(maxDistance/2,2) - Mathf.pow(maxDistance,2)/2) = a
         * maxDistanceTravled/2 = -b/2a
         * a * maxDistanceTravled = -b
         */

        parabolaA = maxJumpHeight / ((Mathf.Pow(maxDistanceTravled / 2, 2) - Mathf.Pow(maxDistanceTravled, 2) / 2));
        parabolaB = -(parabolaA * maxDistanceTravled);
    }


    public void RandomlyCreateLevel()
    {
        lastPlacement = new Vector3(0, 0, 0);
        gameManager.currentGroundEnum.Add(PartOfLevel.Ground);
        gameManager.currentGroundPosistions.Add(new Vector3(0, -1, 0));

        int numberOfBlocks = Random.Range(10, 21);

        

        for (int i = 0; i < numberOfBlocks; i++)
        {
            int secotionOfLevel = Random.Range(0, 2);
            switch (secotionOfLevel)
            {
                case 0:
                    levelPart = PartOfLevel.Ground;
                    break;
                case 1:
                    levelPart = PartOfLevel.Wall;
                    break;
            }
            lastPlacement = new Vector3(Random.Range(2 + lastPlacement.x, maxDistanceTravled + lastPlacement.x + gameManager.partsOfLevels[(int)levelPart].transform.localScale.x / 2), Random.Range(0, maxJumpHeight -3 - gameManager.partsOfLevels[(int)levelPart].transform.localScale.y / 2), 0); 
            gameManager.currentGroundPosistions.Add(lastPlacement);
            gameManager.currentGroundEnum.Add(levelPart);
        }
        lastPlacement = new Vector3(Random.Range(2 + lastPlacement.x, maxDistanceTravled+ lastPlacement.x), Random.Range(0, maxJumpHeight - gameManager.partsOfLevels[(int)PartOfLevel.Portal].transform.localScale.y / 2), 0);
        gameManager.currentGroundEnum.Add(PartOfLevel.Portal);
        gameManager.currentGroundPosistions.Add(lastPlacement);

    }


    /*private Vector3 CreatePosition()
    {
        Vector3 position = new Vector3(0, 0, 0);

        position.x = Random.Range(2 + lastPlacement.x + 6, maxDistanceTravled + lastPlacement.x + 6);

        Debug.Log("X position: " + position.x);
        Debug.Log("Last x postion: " + lastPlacement.x);

        float topJump = parabolaA * Mathf.Pow(position.x, 2) + parabolaB * (position.x)
            - gameManager.partsOfLevels[(int)levelPart].transform.localScale.y / 2;

        position.y = Random.Range(0, topJump);

        ;

        return position;
    }*/
}
