using System;

public class RandomWrapper : IRandom
{
    static readonly Random rng = new();

    public int Next(int max)
    {
        return rng.Next(max);
    }
}