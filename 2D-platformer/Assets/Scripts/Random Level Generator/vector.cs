public class vector
{
    public float x;
    public float y;

    public vector(float s, float e)
    {
        x = s;
        y = e;
    }

    public vector multiply(float r)
    {
        return new vector(x * r, y * r);
    }
}
