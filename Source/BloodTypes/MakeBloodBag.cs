using System.Collections.Generic;
using RimWorld;
using Verse;

namespace BloodTypes
{
    public class MakeBloodBag : Recipe_Surgery
    {
        public override void ApplyOnPawn(Pawn pawn, BodyPartRecord part, Pawn billDoer, List<Thing> ingredients,
            Bill bill)
        {
            var bloodBag = (BloodBagThingWithComps)ThingMaker.MakeThing(ThingDefOf.BloodBag);
            bloodBag.BloodType = pawn.GetBloodType().BloodType;
            GenPlace.TryPlaceThing(bloodBag, billDoer.Position, billDoer.Map, ThingPlaceMode.Near);

            var hediff = HediffMaker.MakeHediff(HediffDefOf.GaveBlood, pawn);
            hediff.Severity = 1f;
            pawn.RemoveHediff(HediffDefOf.GotBlood);

            if (pawn.health.hediffSet.HasHediff(HediffDefOf.GaveBlood))
            {
                if (pawn.health.hediffSet.HasHediff(RimWorld.HediffDefOf.BloodLoss))
                {
                    pawn.health.hediffSet.GetFirstHediffOfDef(RimWorld.HediffDefOf.BloodLoss).Severity += 0.25f;
                    base.ApplyOnPawn(pawn, part, billDoer, ingredients, bill);
                    return;
                }

                hediff = HediffMaker.MakeHediff(RimWorld.HediffDefOf.BloodLoss, pawn);
                hediff.Severity = 0.25f;
            }

            pawn.health?.AddHediff(hediff);
            base.ApplyOnPawn(pawn, part, billDoer, ingredients, bill);
        }
    }
}