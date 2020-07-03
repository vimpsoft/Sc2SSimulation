using Sc2Simulation.Brirge;
using UnityEditor;
using UnityEngine;

namespace Sc2Simulation.Authoring.Editor
{
    [CustomEditor(typeof(GridOccupier))]
    public class GridOccupierEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Rotate right"))
            {
                Undo.RecordObjects(new Object[] { target, (target as GridOccupier).transform }, "Grid occupier rotation changed");
                (target as GridOccupier).Rotation = (GridOccupierRotation)(((int)(target as GridOccupier).Rotation + 1) % 4);
                (target as GridOccupier).transform.RotateAround((target as GridOccupier).transform.position, Vector3.up, 90f);
            }

            if (GUILayout.Button("Rotate left"))
            {
                Undo.RecordObjects(new Object[] { target, (target as GridOccupier).transform }, "Grid occupier rotation changed");
                (target as GridOccupier).Rotation = (GridOccupierRotation)(((int)(target as GridOccupier).Rotation - 1 + 4) % 4);
                (target as GridOccupier).transform.RotateAround((target as GridOccupier).transform.position, Vector3.up, -90f);
            }
        }
    }
}
