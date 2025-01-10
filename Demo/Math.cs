using System;

namespace Demo;

public class Math
{
    public float Add(float a, float b)
    {
        return a + b;
    }
    public int Add(int a, int b)
    {
        return a + b;
    }
    public int ConvertToInt(long origin){
        return Convert.ToInt16(origin);
    }
}
