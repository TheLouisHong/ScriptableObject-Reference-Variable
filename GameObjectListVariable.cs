using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameObjectListReference Class.
/// </summary>
[Serializable]
public class GameObjectListReference : Reference<List<GameObject>, GameObjectListVariable>
{
    public GameObjectListReference(List<GameObject> Value) : base(Value)
    {
    }

    public GameObjectListReference()
    {
    }
}

/// <summary>
/// GameObjectListVariable Class.
/// </summary>
[CreateAssetMenu]
public class GameObjectListVariable : Variable<List<GameObject>>
{
}