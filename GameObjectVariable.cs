using System;
using UnityEngine;

/// <summary>
/// GameObjectReference Class.
/// </summary>
[Serializable]
public class GameObjectReference : Reference<GameObject, GameObjectVariable>
{
    public GameObjectReference(GameObject Value) : base(Value)
    {
    }

    public GameObjectReference()
    {
    }
}

/// <summary>
/// GameObjectVariable Class.
/// </summary>
[CreateAssetMenu]
public class GameObjectVariable : Variable<GameObject>
{
}