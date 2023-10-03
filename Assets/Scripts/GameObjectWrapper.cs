using UnityEngine;

public class GameObjectWrapper : IGameObject
{
    GameObject gameObject;

    public GameObjectWrapper(GameObject gameObject)
    {
        this.gameObject = gameObject;
    }

    public string name {
        get => gameObject.name;
        set => gameObject.name = value;
    }
}