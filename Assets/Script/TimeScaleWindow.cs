using UnityEditor;
using UnityEngine;

/// <summary>
/// This script was just to allow me to slow thing down and get
/// a better look at the animtions playing
/// </summary>
public class TimeScaleWindow : EditorWindow
{
    private float timeScale = 1f;

    private const float minTimeScale = 0.1f;
    private const float maxTimeScale = 2f;

    [MenuItem("Window/Time Scale Window")]
    static void Init()
    {
        TimeScaleWindow window = (TimeScaleWindow)EditorWindow.GetWindow(typeof(TimeScaleWindow));
        window.Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.PrefixLabel("Time Scale");
        timeScale = GUILayout.HorizontalSlider(timeScale, minTimeScale, maxTimeScale, GUILayout.Width(100f));

        if (GUILayout.Button("Apply", GUILayout.Width(60f)))
        {
            Time.timeScale = timeScale;
        }

        EditorGUILayout.EndHorizontal();
    }
}
