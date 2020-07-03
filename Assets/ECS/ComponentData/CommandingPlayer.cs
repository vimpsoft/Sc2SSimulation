using Unity.Entities;

[GenerateAuthoringComponent]
public struct CommandingPlayer : IComponentData
{
    public byte PlayerIndex;
}
