using UnityEngine;

namespace Sc2Simulation.Authoring
{
    [ExecuteInEditMode]
    public class MapGridData : MonoBehaviour
    {
        public static MapGridData Instance;

        [Range(0, float.MaxValue)]
        public float GridX = 1;
        [Range(0, float.MaxValue)]
        public float GridY = 1;

        public int FieldWidth = 100;
        public int FieldHeight = 100;

        private void OnEnable() => Instance = this;
    }
}
