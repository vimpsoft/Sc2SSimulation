using UnityEditor;
using UnityEngine;

namespace Sc2Simulation.Authoring.Editor
{
    [CustomEditor(typeof(GridSnapper))]
    public class GridSnapperEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.LabelField(GridSnapper.IsSnapping ? "Snapping..." : "Not snapping.");
            if (GridSnapper.IsSnapping && GUILayout.Button("Stop snapping"))
                GridSnapper.StopSnapping();
            if (!GridSnapper.IsSnapping && GUILayout.Button("Start snapping"))
                GridSnapper.StartSnapping();
        }
    }
}
