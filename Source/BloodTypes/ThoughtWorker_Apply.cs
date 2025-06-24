using System.Linq;
using BloodTypes.Harmony;
using RimWorld;
using Verse;

namespace BloodTypes;

public class ThoughtWorker_Apply : ThoughtWorker
{
    protected override ThoughtState CurrentStateInternal(Pawn p)
    {
        if (p == null)
        {
            return false;
        }

        generateBloodType(p);

        return false;
    }

    private static void generateBloodType(Pawn pawn)
    {
        if (!DefGenerator_GenerateImpliedDefs_PreResolve.ValidRace(pawn.def))
        {
            return;
        }

        if (PawnHelper.IsHaveHediff(pawn, HediffDefOf.BloodType))
        {
            return;
        }

        var moreThanOne = false;
        BloodType current = null;
        var parents = pawn.relations.DirectRelations.Where(x => x.def == PawnRelationDefOf.Parent);
        foreach (var relation in parents)
        {
            var bloodDiff = relation.otherPawn.GetBloodType();
            if (bloodDiff?.BloodType == null)
            {
                continue;
            }

            if (current == null)
            {
                current = bloodDiff.BloodType;
            }
            else
            {
                moreThanOne = true;
                current = current.Child(bloodDiff.BloodType);
            }
        }

        if (current == null)
        {
            current = BloodType.Random();
        }
        else if (!moreThanOne)
        {
            current = current.Child();
        }


        addBloodType(pawn, current);
    }


    private static void addBloodType(Pawn pawn, BloodType current)
    {
        var myDef = (BloodTypeHediffWithComps)pawn.health.AddHediff(HediffDefOf.BloodType);
        myDef.BloodType = current;
    }
}