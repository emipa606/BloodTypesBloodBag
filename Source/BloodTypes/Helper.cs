using System;
using System.Linq;
using Verse;

namespace BloodTypes;

public static class Helper
{
    public static BloodTypeHediffWithComps GetBloodType(this Pawn pawn)
    {
        return (BloodTypeHediffWithComps)pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.BloodType);
    }

    public static void Clot(this Pawn pawn, int maxTends = 5)
    {
        foreach (var hediff in pawn.health.hediffSet.hediffs.Where(x => x.Bleeding)
                     .OrderByDescending(x => x.BleedRate))
        {
            hediff.Tended(Math.Min(Rand.Value + Rand.Value + Rand.Value, 1f), 0.7f);
            maxTends--;

            if (maxTends <= 0)
            {
                return;
            }
        }
    }

    public static void RemoveHediff(this Pawn pawn, HediffDef hediffDef)
    {
        if (hediffDef == null || pawn == null)
        {
            return;
        }

        var toxic = pawn.health.hediffSet.GetFirstHediffOfDef(hediffDef);
        if (toxic != null)
        {
            pawn.health.RemoveHediff(toxic);
        }
    }
}