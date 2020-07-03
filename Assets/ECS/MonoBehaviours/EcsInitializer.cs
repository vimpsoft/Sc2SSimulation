using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Rendering;

namespace Sc2Simulation.Runtime
{
    public class EcsInitializer : MonoBehaviour
    {
        [SerializeField]
        private Mesh _cylinderMesh;
        [SerializeField]
        private Material _material;

        private EntityManager _entityManager;

        private void Start()
        {
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            var renderedMeshArchetype = _entityManager.CreateArchetype(typeof(LocalToWorld), typeof(Translation), typeof(RenderBounds), typeof(RenderMesh));
            var entity = _entityManager.CreateEntity(renderedMeshArchetype);

            _entityManager.SetSharedComponentData(entity, new RenderMesh
            {
                mesh = _cylinderMesh,
                material = _material,
                subMesh = 0,
                layer = 0,
                castShadows = ShadowCastingMode.On,
                receiveShadows = true
            });

            var allEntities = FindObjectsOfType<Authoring.EntityAuthoring>();
            for (int i = 0; i < allEntities.Length; i++)
            {
                var newEntity = _entityManager.CreateEntity();
                _entityManager.AddComponent(newEntity, typeof(RenderMesh));
            }
        }
    }
}
