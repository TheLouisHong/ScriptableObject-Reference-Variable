using System;
using UnityEngine;

/// <summary>
/// RectReference Class.
/// </summary>
[Serializable]
public class RectReference : Reference<Rect, RectVariable>
{
    public RectReference(Rect Value) : base(Value)
    {
    }

    public RectReference()
    {
    }
}

/// <summary>
/// RectVariable Class.
/// </summary>
[CreateAssetMenu]
public class RectVariable : Variable<Rect>
{
}