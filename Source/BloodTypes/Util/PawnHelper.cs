using Verse;

namespace BloodTypes
{
    internal static class PawnHelper
    {
        public static bool IsHaveHediff(Pawn pawn, HediffDef what)
        {
            return pawn?.health?.hediffSet != null && pawn.health.hediffSet.HasHediff(what);
        }
    }
}