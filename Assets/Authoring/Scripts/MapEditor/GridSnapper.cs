using System;
using UnityEditor;
using UnityEngine;

namespace Sc2Simulation.Authoring
{
    [RequireComponent(typeof(MapGridData)), ExecuteInEditMode]
    public class GridSnapper : MonoBehaviour
    {
        public static GameObject DraggedObject;
        public static GridOccupier DraggedGridOccupier;
        public static int DraggedGridX;
        public static int DraggedGridY;

        public static bool IsSnapping;

        private static Transform _mapGridInstanceTransform;

        private void OnEnable()
        {
            _mapGridInstanceTransform = transform;
            if (!IsSnapping)
                StartSnapping();
        }

        [MenuItem("Sc2/Start snapping to grid")]
        public static void StartSnapping()
        {
            SceneView.duringSceneGui -= OnSceneGUI;
            SceneView.duringSceneGui += OnSceneGUI;
            IsSnapping = true;
        }

        [MenuItem("Sc2/Stop snapping to grid")]
        public static void StopSnapping()
        {
            IsSnapping = false;
            SceneView.duringSceneGui -= OnSceneGUI;
        }

        [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected)]
        private static void DrawMapGizo(GridGizmoDrawer ggd, GizmoType gizmoType)
        {
            if (DraggedObject == default)
                return;
            var mapGrid = _mapGridInstanceTransform.GetComponent<MapGridData>();
            if (mapGrid == default)
                return;

            var meshFilters = DraggedObject.GetComponentsInChildren<MeshFilter>();
            for (int i = 0; i < meshFilters.Length; i++)
            {
                Gizmos.color = new Color(1f, 1f, 1f, 0.3f);
                Gizmos.DrawMesh(meshFilters[i].sharedMesh, new Vector3(DraggedGridX * mapGrid.GridX, 0f, DraggedGridY * mapGrid.GridY) + meshFilters[i].transform.localPosition, meshFilters[i].transform.rotation, meshFilters[i].transform.localScale);
            }
        }

        private static void OnSceneGUI(SceneView _)
        {
            if (_mapGridInstanceTransform == default)
                return;
            var mapGrid = _mapGridInstanceTransform.GetComponent<MapGridData>();
            if (mapGrid == default)
                return;
            if (mapGrid.GridX == 0 || mapGrid.GridY == 0)
                throw new ApplicationException($"GridX and GridY of MapGrid cannot be 0!");

            if (Event.current.type == EventType.DragUpdated || Event.current.type == EventType.DragPerform)
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy; // show a drag-add icon on the mouse cursor

                if (DraggedObject == null)
                {
                    DraggedObject = (GameObject)DragAndDrop.objectReferences[0];
                    DraggedGridOccupier = DraggedObject.GetComponent<GridOccupier>();
                }

                var ray = Camera.current.ScreenPointToRay(new Vector3(Event.current.mousePosition.x, Camera.current.pixelRect.height - Event.current.mousePosition.y, 0.0f));
                var plane = new Plane(_mapGridInstanceTransform.up, _mapGridInstanceTransform.position);
                if (plane.Raycast(ray, out var intersectionDistance))
                {
                    var pointOfIntersection = ray.origin + ray.direction * intersectionDistance;

                    DraggedGridX = Mathf.FloorToInt(pointOfIntersection.x / mapGrid.GridX);
                    DraggedGridY = Mathf.FloorToInt(pointOfIntersection.z / mapGrid.GridY);

                    var gridOccupier = DraggedObject.GetComponent<GridOccupier>();
                    if (gridOccupier != default)
                    {
                        gridOccupier.X = DraggedGridX;
                        gridOccupier.Y = DraggedGridY;
                    }
                }

                if (Event.current.type == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();
                    var newObj = (GameObject)PrefabUtility.InstantiatePrefab(DraggedObject);
                    newObj.transform.position = new Vector3(DraggedGridX * mapGrid.GridX, 0f, DraggedGridY * mapGrid.GridY);
                    var gridOccupier = newObj.GetComponent<GridOccupier>();
                    if (gridOccupier != default)
                    {
                        gridOccupier.X = DraggedGridX;
                        gridOccupier.Y = DraggedGridY;
                    }
                    Undo.RegisterCreatedObjectUndo(newObj, "Object dragged to scene");
                    Selection.activeGameObject = newObj;
                    DraggedObject = default;
                    DraggedGridOccupier = default;
                }

                Event.current.Use();
            }
        }
    }
}
