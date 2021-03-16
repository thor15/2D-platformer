using UnityEngine;
public class point
{
    public float x;
    public float y;
    public float z;
    public point(float s, float e, float r)
    {
        x = s;
        y = e;
        z = r;
    }

    public Vector3 getPoint()
    {
        return new Vector3(x, y, z);
    }
}
