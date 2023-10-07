using System.Collections;
using UnityEngine;

public class CoroutineService : ICoroutineService
{
    MonoBehaviour parent;

    public CoroutineService(MonoBehaviour parent)
    {
        this.parent = parent;
    }

    public void StartCoroutine(IEnumerator coroutine)
    {
        parent.StartCoroutine(coroutine);
    }
}