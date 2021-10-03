using Verse;

namespace BloodTypes
{
    public class RecievedBloodTypeHediffWithComps : HediffWithComps
    {
        private short _index;
        public BloodType BloodType;

        public override string LabelInBrackets => BloodType?.ToString();

        public override void PostTick()
        {
            _index %= 2579;
            if (_index == 0)
            {
                pawn.Clot();
            }

            _index++;
            base.PostTick();
        }

        public override void ExposeData()
        {
            Scribe_Deep.Look(ref BloodType, "BloodType");
            base.ExposeData();
        }
    }
}