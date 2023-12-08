using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(TagID), true)]
public class TagIDPropertyDrawer : PropertyDrawer
{
    private TagsConfig _config;
    private string[] _strings;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        Rect pathRect = new Rect(position.x, position.y, position.width - 6, position.height);
        var propertyVal = property.FindPropertyRelative("value");
        var stringValue = propertyVal.stringValue;

        if (_config == null)
        {
            _config = AssetDatabase.LoadAssetAtPath<TagsConfig>("Assets/Configs/TagsConfig.asset");
            List<string> list = new List<string>();

            foreach (var element in _config.Tags)
            {
                list.Add(element.Name);
            }

            _strings = list.ToArray();
        }

        var stringIndex = GetIndex(stringValue, _strings);
        stringIndex = EditorGUI.Popup(pathRect, stringIndex, _strings);
        stringValue = stringIndex < 0 ? "" : _strings[stringIndex];
        propertyVal.stringValue = stringValue;

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }

    private int GetIndex(string element, string[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (element == array[i])
                return i;
        }
        return -1;
    }
}
