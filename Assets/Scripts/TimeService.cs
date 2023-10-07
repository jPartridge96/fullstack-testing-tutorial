using UnityEngine;

public class TimeService : ITime
{
    public float deltaTime { get => Time.deltaTime;  }
}