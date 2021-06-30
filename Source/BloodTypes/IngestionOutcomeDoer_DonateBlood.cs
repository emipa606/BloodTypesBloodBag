using RimWorld;
using Verse;

namespace BloodTypes
{
    public class IngestionOutcomeDoer_DonateBlood : IngestionOutcomeDoer
    {
        private readonly bool divideByBodySize;
        public HediffDef hediffDef;
        public float severity = -1f;
        public ChemicalDef toleranceChemical;

        protected override void DoIngestionOutcomeSpecial(Pawn pawn, Thing ingested)
        {
            if (ingested is BloodBagThingWithComps bag)
            {
                if (!pawn.GetBloodType()?.BloodType.CanGetBlood(bag.BloodType) ?? false)
                {
                    //TODO blood incompatibility, MVP FoodPoison
                    var d = pawn?.health?.AddHediff(RimWorld.HediffDefOf.FoodPoisoning);
                    if (d != null)
                    {
                        d.Severity = Rand.Value / 3f;
                    }
                }
            }

            var hediff = HediffMaker.MakeHediff(hediffDef, pawn);
            var effect = (double) severity <= 0.0 ? hediffDef.initialSeverity : severity;
            if (divideByBodySize)
            {
                if (pawn != null)
                {
                    effect /= pawn.BodySize;
                }
            }

            AddictionUtility.ModifyChemicalEffectForToleranceAndBodySize(pawn, toleranceChemical, ref effect);
            hediff.Severity = effect;
            pawn?.health?.AddHediff(hediff);
        }
    }
}