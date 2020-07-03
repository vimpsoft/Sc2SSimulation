using UnityEditor;
using UnityEngine;

namespace Sc2Simulation.Authoring.Editor
{
    [CustomEditor(typeof(MapSaver))]
    public class MapSaverEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Save Map"))
                (target as MapSaver).SaveMap();
        }
    }
}
