using System;
using UnityEngine;

/// <summary>
/// StringReference Class.
/// </summary>
[Serializable]
public class StringReference : Reference<string, StringVariable>
{
    public StringReference(string Value) : base(Value) { }
    public StringReference() { }
}

/// <summary>
/// StringVariable Class.
/// </summary>
[CreateAssetMenu]
public class StringVariable : Variable<string> { }