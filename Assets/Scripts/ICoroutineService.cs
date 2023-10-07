using System.Collections;

public interface ICoroutineService
{
    void StartCoroutine(IEnumerator coroutine);
}