using Sc2Simulation.Brirge;
using UnityEditor;
using UnityEngine;

namespace Sc2Simulation.Authoring
{
    [RequireComponent(typeof(MapGridData)), ExecuteInEditMode]
    public class GridGizmoDrawer : MonoBehaviour
    {
        public Color ActiveColor = Color.green;
        public Color InactiveColor = Color.red;

        private static GridGizmoDrawer _instance;

        private static bool[][] _flagsCache;

        private void OnEnable() => _instance = this;

        [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected)]
        private static void DrawMapGridGizmo(GridGizmoDrawer ggd, GizmoType gizmoType)
        {
            if (_instance == default)
                return;

            var mapGrid = _instance.GetComponent<MapGridData>();
            if (mapGrid == default)
                return;

            var fieldWidth = mapGrid.FieldWidth;
            var fieldHeight = mapGrid.FieldHeight;
            var centerX = fieldWidth / 2;
            var centerY = fieldHeight / 2;

            if (_flagsCache == default || _flagsCache.Length != fieldWidth || _flagsCache[0] == default || _flagsCache[0].Length != fieldHeight)
            {
                _flagsCache = new bool[fieldWidth][];
                for (int i = 0; i < fieldWidth; i++)
                    _flagsCache[i] = new bool[fieldHeight];
            }


            for (int x = 0; x < fieldWidth; x++)
                for (int y = 0; y < fieldHeight; y++)
                    _flagsCache[x][y] = false;

            var gridOccupiers = FindObjectsOfType<GridOccupier>();
            void mapOccupierOnDrid(GridOccupier gridOccupier)
            {
                for (int x = 0; x < gridOccupier.Size.x; x++)
                {
                    for (int y = 0; y < gridOccupier.Size.y; y++)
                    {
                        var xx = default(int);
                        var yy = default(int);
                        switch (gridOccupier.Rotation)
                        {
                            case GridOccupierRotation.Forward:
                                xx = gridOccupier.X - gridOccupier.CenterX + x + fieldWidth / 2;
                                yy = gridOccupier.Y - gridOccupier.CenterY + y + fieldHeight / 2;
                                break;
                            case GridOccupierRotation.Right:
                                xx = gridOccupier.X - gridOccupier.CenterX + y + fieldWidth / 2;
                                yy = gridOccupier.Y - gridOccupier.CenterY + x + fieldHeight / 2;
                                break;
                            case GridOccupierRotation.Back:
                                xx = gridOccupier.X - gridOccupier.CenterX + x + fieldWidth / 2;
                                yy = gridOccupier.Y + gridOccupier.CenterY - y + fieldHeight / 2;
                                break;
                            case GridOccupierRotation.Left:
                                xx = gridOccupier.X + gridOccupier.CenterX - y + fieldWidth / 2;
                                yy = gridOccupier.Y + gridOccupier.CenterY - x + fieldHeight / 2;
                                break;
                            default:
                                break;
                        }
                        if (xx >= fieldWidth || yy >= fieldHeight || xx < 0 || yy < 0)
                            continue;
                        _flagsCache[xx][yy] = true;
                    }
                }
            }
            for (int i = 0; i < gridOccupiers.Length; i++)
                mapOccupierOnDrid(gridOccupiers[i]);
            if (GridSnapper.DraggedGridOccupier != default)
                mapOccupierOnDrid(GridSnapper.DraggedGridOccupier);

            for (int x = 0; x < fieldWidth; x++)
            {
                for (int y = 0; y < fieldHeight; y++)
                {
                    Gizmos.color = _flagsCache[x][y] ? ggd.InactiveColor : ggd.ActiveColor;
                    Gizmos.DrawCube(new Vector3(x - centerX, 0f, y - centerY), new Vector3(1f, 0.1f, 1f));
                }
            }
        }
    }
}
