using System.Collections;
using UnityEngine;

public class FloatableImpl
{
    public float Ceiling { get; set; } = 1000;
    public float Speed { get; set; } = 1f;

    ICoroutineService coroutineService;
    IGameObject objectToFloat;
    ITime timeService;

    public FloatableImpl(
        ICoroutineService coroutineService,
        IGameObject objectToFloat,
        ITime timeService
    )
    {
        this.coroutineService = coroutineService;
        this.objectToFloat = objectToFloat;
        this.timeService = timeService;
    }

    public void Start()
    {
        coroutineService.StartCoroutine(StartFloating());
    }

    IEnumerator StartFloating()
    {
        while (objectToFloat.transform.position.y < Ceiling)
        {
            float displacement = Speed * timeService.deltaTime;
            objectToFloat.transform.position += new Vector3(0, displacement, 0);
            yield return null;
        }
    }
}