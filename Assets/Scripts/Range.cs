using System;
[Serializable]
public class Range<T>
{
    public T Min, Max;
    public Range(T min, T max)
    {
        Min = min;
        Max = max;
    }
}
[Serializable]
public class RangeInt : Range<int>
{
    public RangeInt(int min, int max) : base(min, max)
    {
        Min = min;
        Max = max;
    }
    public int GetRandom() { return UnityEngine.Random.Range(Min, Max); }
}
[Serializable]
public class RangeFloat : Range<float>
{
    public RangeFloat(float min, float max) : base(min, max)
    {
        Min = min;
        Max = max;
    }
    public float GetRandom() { return UnityEngine.Random.Range(Min, Max); }
}