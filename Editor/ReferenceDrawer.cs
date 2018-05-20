using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.IO;
using Sirenix.Utilities;
using System;

/// <summary>
/// ReferenceDrawer Class.
/// </summary>
[CustomPropertyDrawer(typeof(Reference), true)]
public class ReferenceDrawer : PropertyDrawer
{
    /// <summary>
    /// Options to display in the popup to select constant or variable.
    /// </summary>
    private readonly string[] _PopupOption = { "Use Constant", "Use Variable" };

    /// <summary> Cached style to use to draw the popup button. </summary>
    private GUIStyle _PopupStyle;
    private GUIStyle _ButtonStyle;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (_PopupStyle == null)
        {
            _PopupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"));
            _PopupStyle.imagePosition = ImagePosition.ImageOnly;
        }
        
        if (_ButtonStyle == null)
        {
            _ButtonStyle = new GUIStyle(GUI.skin.button);
        }
        
        label = EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, label);

        EditorGUI.BeginChangeCheck();

        // Get properties
        SerializedProperty useConstant = property.FindPropertyRelative("UseConstant");
        SerializedProperty constantValue = property.FindPropertyRelative("ConstantValue");
        SerializedProperty variable = property.FindPropertyRelative("Variable");

        // Calculate rect for configuration button
        Rect buttonRect = new Rect(position);
        buttonRect.yMin += _PopupStyle.margin.top;
        buttonRect.width = _PopupStyle.fixedWidth + _PopupStyle.margin.right;
        position.xMin = buttonRect.xMax;

        // Store old indent level and set it to 0, the PrefixLabel takes care of it
        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;
        int result = EditorGUI.Popup(buttonRect, useConstant.boolValue ? 0 : 1, _PopupOption, _PopupStyle);
        useConstant.boolValue = result == 0;
        if (!useConstant.boolValue)
        {
            Rect rect = new Rect(position);
            rect.xMin = position.xMax - 100 ;
            position.xMax = rect.xMin - _ButtonStyle.margin.left;
            if (GUI.Button(rect, "Create New"))
            {
                var obj = CreateNew(variable);
                variable.objectReferenceValue = obj;
            }
        }
        EditorGUI.PropertyField(position, useConstant.boolValue ? constantValue : variable, GUIContent.none);
        
        if (EditorGUI.EndChangeCheck())
            property.serializedObject.ApplyModifiedProperties();

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }
    
    public static System.Type GetType(SerializedProperty property)
    {
        string[] parts = property.propertyPath.Split('.');
 
        Type currentType = property.serializedObject.targetObject.GetType();
 
        for (int i = 0; i < parts.Length; i++)
        {
            Debug.Log(currentType + " " + parts[i]);    
            currentType = currentType.GetField(parts[i], BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.Instance).FieldType;
        }
 
        Type targetType = currentType;
        return targetType;
    }
    
    public ScriptableObject CreateNew(SerializedProperty property)
    {
        var type = GetType(property);
        var obj = ScriptableObject.CreateInstance(type);

        string dest = "Assets";

        if (!Directory.Exists(dest))
        {
            Directory.CreateDirectory(dest);
            AssetDatabase.Refresh();
        }

        dest = EditorUtility.SaveFilePanel("Save object as", dest, "New " + type.GetNiceName(), "asset");

        if (!string.IsNullOrEmpty(dest) && PathUtilities.TryMakeRelative(Path.GetDirectoryName(Application.dataPath), dest, out dest))
        {
            AssetDatabase.CreateAsset(obj, dest);
            AssetDatabase.Refresh();
            return obj;
        }
        else
        {
            UnityEngine.Object.DestroyImmediate(obj);
            return null;
        }
    }
}