using Unity.Entities;

public class HatcheriesSystem : SystemBase
{
    private const int _naturalLarvaeMaxCount = 3;
    private const float _larvaeNaturalProductionTime = 11f;

    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;

        //Entities
        //    .WithName("Hatchery")
        //    .ForEach((ref Hatchery hatch) =>
        //    {
        //        if (hatch.LarvaeSupply >= _naturalLarvaeMaxCount)
        //            return;

        //        hatch.NaturalLarvaeProductionTimer += deltaTime;
        //        if (hatch.NaturalLarvaeProductionTimer >= _larvaeNaturalProductionTime)
        //        {
        //            var remainder = hatch.NaturalLarvaeProductionTimer - _larvaeNaturalProductionTime;
        //            hatch.LarvaeSupply += 1;
        //            if (hatch.LarvaeSupply < _naturalLarvaeMaxCount)
        //                hatch.NaturalLarvaeProductionTimer = remainder;
        //        }
        //    })
        //    .ScheduleParallel();
    }
}
