using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformWrapper : ITransform
{
    Transform transform;

    public TransformWrapper(Transform transform)
    {
        this.transform = transform;
    }

    public Vector3 position 
    { 
        get => transform.position; 
        set => transform.position = value;
    }
}
