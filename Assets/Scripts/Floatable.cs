using UnityEngine;

public class Floatable : MonoBehaviour
{
    public float Speed
    {
        get => FloatableImpl.Speed;
        set => FloatableImpl.Speed = value;
    }

    public float Ceiling
    {
        get => FloatableImpl.Ceiling;
        set => FloatableImpl.Ceiling = value;
    }

    FloatableImpl _floatableImpl;
    FloatableImpl FloatableImpl
    {
        get
        {
            if (_floatableImpl == null)
            {
                _floatableImpl = new(
                    new CoroutineService(this),
                    new GameObjectWrapper(gameObject),
                    new TimeService());
            }
            return _floatableImpl;
        }
    }

    void Start()
    {
        FloatableImpl.Start();
    }
}