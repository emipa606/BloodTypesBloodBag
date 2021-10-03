using Verse;

namespace BloodTypes
{
    public class BloodBagThingWithComps : ThingWithComps
    {
        public BloodType BloodType;

        public override string Label =>
            base.Label + (BloodType != null ? " [" + BloodType + "]" : " [O-]");


        public override bool CanStackWith(Thing other)
        {
            if (!(other is BloodBagThingWithComps withComps))
            {
                return false;
            }

            return withComps.BloodType.Equals(BloodType);
        }

        public override void ExposeData()
        {
            Scribe_Deep.Look(ref BloodType, "BloodType");
            base.ExposeData();
        }
    }
}