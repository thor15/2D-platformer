using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGround
{
    private int imageWidth;
    private point[] listOfPoint;
    private point[] listOfPoint2;
    private point[] finalList;
    public GameObject cube;
    public GameObject meshCreator;

    // Start is called before the first frame update
    public GenerateGround(int width)
    {
        imageWidth = width;
        Mesh meshRender = new Mesh();
        listOfPoint = new point[imageWidth * imageWidth];
        listOfPoint2 = new point[imageWidth * imageWidth];
        finalList = new point[imageWidth * imageWidth];
        //generateFractalSumForList2(.02f, 1.8f, 10, 0.35f, 5);
        //generateFractalSum(.02f, 1.8f, 10, 0.35f, 5);
        //generateTurbulence(0.02f, 2f, 0.5f, 5);
        //generateWood(.01f, 10);
        /*for(int i = 0; i < listOfPoint.Length; i++)
        {
            finalList[i] = new point(listOfPoint[i].x, listOfPoint[i].y * listOfPoint2[i].y * listOfPoint2[i].y, listOfPoint[i].z);
        }*/
        //generateWood(.01f, 10);
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void generateFractalSumForList2(float frequency, float frequencyMultiplier, float amplitude, float amplitudeMultipier, int layers)
    {
        Noise noise = new Noise(imageWidth);
        float maxNoise = 0;
        for (int height = 0; height < imageWidth; height++)
        {
            for (int length = 0; length < imageWidth; length++)
            {
                vector pointVector = new vector((float)length, (float)height);
                pointVector = pointVector.multiply(frequency);
                float amplitudeNow = amplitude;
                listOfPoint2[height * imageWidth + length] = new point(length, 0, height);
                for (int layer = 0; layer < layers; layer++)
                {
                    // noiseMap[j * imageWidth + i] += noise.eval(pNoise) * amplitude; 
                    // pNoise *= frequencyMult; 
                    // amplitude *= amplitudeMult;

                    listOfPoint2[height * imageWidth + length].y += noise.Evaluate(pointVector) * amplitudeNow;
                    pointVector = pointVector.multiply(frequencyMultiplier);
                    amplitudeNow *= amplitudeMultipier;
                }
                if (listOfPoint2[height * imageWidth + length].y > maxNoise)
                {
                    maxNoise = listOfPoint2[height * imageWidth + length].y;
                }
            }
        }
        foreach(point p in listOfPoint2)
        {
            p.y /= maxNoise;
        }
    }

    public point[] generateFractalSum(float frequency, float frequencyMultiplier, float amplitude, float amplitudeMultipier, int layers)
    {
        Noise noise = new Noise(imageWidth);
        float maxNoise = 0;
        for (int height = 0; height < imageWidth; height++)
        {
            for (int length = 0; length < imageWidth; length++)
            {
                vector pointVector = new vector((float)length, (float)height);
                pointVector = pointVector.multiply(frequency);
                float amplitudeNow = amplitude;
                listOfPoint[height * imageWidth + length] = new point(length, 0, height);
                for (int layer = 0; layer < layers; layer++)
                {
                    // noiseMap[j * imageWidth + i] += noise.eval(pNoise) * amplitude; 
                    // pNoise *= frequencyMult; 
                    // amplitude *= amplitudeMult;

                    listOfPoint[height * imageWidth + length].y += noise.Evaluate(pointVector) * amplitudeNow;
                    pointVector = pointVector.multiply(frequencyMultiplier);
                    amplitudeNow *= amplitudeMultipier;
                }
                if (listOfPoint[height * imageWidth + length].y > maxNoise)
                {
                    maxNoise = listOfPoint[height * imageWidth + length].y;
                }
            }
        }
        foreach (point p in listOfPoint)
        {
            p.y /= maxNoise;
        }
        return listOfPoint;
    }

    private void generateTurbulence(float frequency, float frequencyMultiplier, float amplitudeMultipier, int layers)
    {
        Noise noise = new Noise(imageWidth);
        float maxNoise = 0;
        for (int height = 0; height < imageWidth; height++)
        {
            for (int length = 0; length < imageWidth; length++)
            {
                vector pointVector = new vector((float)length, (float)height);
                pointVector = pointVector.multiply(frequency);
                float amplitude = 1;
                listOfPoint2[height * imageWidth + length] = new point(length, 0, height);
                for (int layer = 0; layer < layers; layer++)
                {
                    // noiseMap[j * imageWidth + i] += noise.eval(pNoise) * amplitude; 
                    // pNoise *= frequencyMult; 
                    // amplitude *= amplitudeMult;

                    listOfPoint2[height * imageWidth + length].y += Mathf.Abs(2 * noise.Evaluate(pointVector) - 1) * amplitude;
                    pointVector = pointVector.multiply(frequencyMultiplier);
                    amplitude *= amplitudeMultipier;
                }
                if (listOfPoint2[height * imageWidth + length].y > maxNoise)
                {
                    maxNoise = listOfPoint2[height * imageWidth + length].y;
                }
            }
        }
        foreach (point p in listOfPoint2)
        {
            p.y /= maxNoise;
        }
    }

    private void generateWood(float frequency, float amplitude)
    {
        Noise noise = new Noise(imageWidth);

        for (int height = 0; height < imageWidth; height++)
        {
            for (int length = 0; length < imageWidth; length++)
            {
                point color = new point(length, 0, height);
                listOfPoint[height * 256 + length] = color;
                float g = noise.Evaluate(new vector((float)length, (float)height).multiply(frequency)) * amplitude;
                listOfPoint[height * 256 + length].y = g - (int)g;
            }
        }
    }
}
