using UnityEngine;

namespace Sc2Simulation.Authoring
{
    [ExecuteInEditMode, RequireComponent(typeof(GridOccupier))]
    public class EditorTimeGridSnapper : MonoBehaviour
    {
        [SerializeField]
        private GameObject _gridSnapperOffset;

        private GridOccupier _occupierCache;

        private void OnEnable()
        {
            _occupierCache = GetComponent<GridOccupier>();
        }

        private void Update()
        {
            _occupierCache.X = Mathf.FloorToInt(transform.position.x / MapGrid.Instance.GridX);
            _occupierCache.Y = Mathf.FloorToInt(transform.position.z / MapGrid.Instance.GridY);
            var snappedPosition = new Vector3(_occupierCache.X * MapGrid.Instance.GridX, transform.position.y, _occupierCache.Y * MapGrid.Instance.GridY);
            _gridSnapperOffset.transform.position = snappedPosition;
        }
    }
}
