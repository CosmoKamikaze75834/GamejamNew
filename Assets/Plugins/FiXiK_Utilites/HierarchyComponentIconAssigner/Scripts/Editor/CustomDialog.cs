#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

public class CustomDialog : EditorWindow
{
    private static GUIStyle s_RichWordWrappedLabel;

    private string message;
    private string buttonText;
    private string secondaryButtonText;
    private Action secondaryButtonAction;
    private int space;
    private Vector2 buttonSize;
    private Vector2 secondaryButtonSize;
    private Action onConfirm;


    private static GUIStyle RichWordWrappedLabel
    {
        get
        {
            s_RichWordWrappedLabel ??= new GUIStyle(EditorStyles.wordWrappedLabel)
            {
                richText = true
            };

            return s_RichWordWrappedLabel;
        }
    }

    public static void ShowDialog(
        string title,
        string message,
        string buttonText,
        Vector2 size,
        Vector2 buttonSize,
        int space,
        System.Action onConfirm = null)
    {
        CustomDialog window = GetWindow<CustomDialog>(true, title, true);
        window.message = message;
        window.buttonText = buttonText;
        window.onConfirm = onConfirm;
        window.minSize = size;
        window.maxSize = size;
        window.buttonSize = buttonSize;
        window.space = space;
        window.ShowUtility();
    }

    public static void ShowDialog(
        string title,
        string message,
        string primaryButtonText,
        string secondaryButtonText,
        Action secondaryButtonAction,
        Vector2 size,
        Vector2 buttonSize,
        Vector2 secondButtonSize,
        int space,
        Action onPrimaryConfirm = null)
    {
        CustomDialog window = GetWindow<CustomDialog>(true, title, true);
        window.message = message;
        window.buttonText = primaryButtonText;
        window.secondaryButtonText = secondaryButtonText;
        window.secondaryButtonAction = secondaryButtonAction;
        window.onConfirm = onPrimaryConfirm;
        window.minSize = size;
        window.maxSize = size;
        window.buttonSize = buttonSize;
        window.secondaryButtonSize = secondButtonSize;
        window.space = space;
        window.ShowUtility();
    }

    private void OnGUI()
    {
        GUILayout.Space(space);
        EditorGUILayout.LabelField(message, RichWordWrappedLabel, GUILayout.ExpandHeight(true));
        GUILayout.Space(space);
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        DrawFirstButton();

        if (string.IsNullOrEmpty(secondaryButtonText) == false && secondaryButtonAction != null)
        {
            GUILayout.Space(space);
            DrawSecondButton();
        }

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        GUILayout.Space(space);
    }

    private void DrawFirstButton()
    {
        if (GUILayout.Button(
            buttonText,
            GUILayout.Width(buttonSize.x), GUILayout.Height(buttonSize.y)))
        {
            onConfirm?.Invoke();
            Close();
        }
    }

    private void DrawSecondButton()
    {
        if (GUILayout.Button(secondaryButtonText, GUILayout.Width(secondaryButtonSize.x), GUILayout.Height(secondaryButtonSize.y)))
            secondaryButtonAction?.Invoke();     
    }
}
#endif