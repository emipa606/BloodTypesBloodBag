using Verse;

namespace BloodTypes;

public class BloodBagThingWithComps : ThingWithComps
{
    private BloodType bloodType;

    public override string Label =>
        $"{base.Label} [{BloodType}]";

    public BloodType BloodType
    {
        get
        {
            if (bloodType == null)
            {
                bloodType = BloodType.Random();
            }

            return bloodType;
        }
        set => bloodType = value;
    }


    public override bool CanStackWith(Thing other)
    {
        return other is BloodBagThingWithComps withComps && withComps.BloodType.Equals(BloodType);
    }

    public override void ExposeData()
    {
        Scribe_Deep.Look(ref bloodType, "BloodType");
        base.ExposeData();
    }
}