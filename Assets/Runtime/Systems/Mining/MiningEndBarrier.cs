using Sc2Simulation.Runtime.Mining;
using Unity.Entities;

[UpdateAfter(typeof(EmptyMinerSystem))]
public class MiningEndBarrier : EntityCommandBufferSystem
{

}
