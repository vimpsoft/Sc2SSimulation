using Sc2Simulation.Brirge;
using UnityEngine;

namespace Sc2Simulation.Authoring
{
    public class GridOccupier : MonoBehaviour
    {
        public int X;
        public int Y;

        public int CenterX;
        public int CenterY;

        public Vector2Int Size;

        //[HideInInspector]
        public GridOccupierRotation Rotation;
    }
}
