using UnityEngine;

namespace Sc2Simulation.Authoring
{
    [ExecuteInEditMode, RequireComponent(typeof(GridOccupier))]
    public class EditorTimeGridSnapper : MonoBehaviour
    {
        [SerializeField]
        private Transform[] _gridSnapperTargets;

        private GridOccupier _occupierCache;

        private void OnEnable()
        {
            _occupierCache = GetComponent<GridOccupier>();
        }

        private void Update()
        {
            _occupierCache.X = Mathf.FloorToInt(transform.position.x / MapGridData.Instance.GridX);
            _occupierCache.Y = Mathf.FloorToInt(transform.position.z / MapGridData.Instance.GridY);
            var snappedPosition = new Vector3(_occupierCache.X * MapGridData.Instance.GridX, transform.position.y, _occupierCache.Y * MapGridData.Instance.GridY);
            for (int i = 0; i < _gridSnapperTargets.Length; i++)
                _gridSnapperTargets[i].position = snappedPosition;
        }
    }
}
