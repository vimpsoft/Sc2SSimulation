using System;
using UnityEngine;

namespace Sc2Simulation.Brirge
{
    [Serializable]
    public struct EntityInfo
    {
        public GameObject Entity;
        public Vector2Int GridPosition;
        public GridOccupierRotation GridRotation;
        public Vector2 Position;
        public float Rotation;
    }
}
