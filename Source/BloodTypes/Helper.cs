using System;
using System.Collections.Generic;
using System.Linq;
using LudeonTK;
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

    [DebugAction("Spawning", "Spawn bloodbag", allowedGameStates = AllowedGameStates.PlayingOnMap)]
    public static List<DebugActionNode> SpawnBloodBag()
    {
        var list = new List<DebugActionNode>();
        var bloodTypeLabels = new List<string>();
        var possibleBloodTypes = Enum.GetValues(typeof(BloodTypes));
        var possibleRhTypes = Enum.GetValues(typeof(Rh));

        foreach (var bloodTypePrimary in possibleBloodTypes)
        {
            foreach (var bloodTypeSecondary in possibleBloodTypes)
            {
                foreach (var possibleRhTypePrimary in possibleRhTypes)
                {
                    foreach (var possibleRhTypeSecondary in possibleRhTypes)
                    {
                        var bloodType = new BloodType
                        {
                            Primary = (BloodTypes)bloodTypePrimary, Secondary = (BloodTypes)bloodTypeSecondary,
                            RhPrimary = (Rh)possibleRhTypePrimary, RhSecondary = (Rh)possibleRhTypeSecondary
                        };
                        if (bloodTypeLabels.Contains(bloodType.ToString()))
                        {
                            continue;
                        }

                        bloodTypeLabels.Add(bloodType.ToString());

                        list.Add(new DebugActionNode(bloodType.ToString(), DebugActionType.ToolMap, delegate
                        {
                            var bloodBag = (BloodBagThingWithComps)ThingMaker.MakeThing(ThingDefOf.BloodBag);
                            bloodBag.BloodType = bloodType;
                            GenPlace.TryPlaceThing(bloodBag, UI.MouseCell(), Find.CurrentMap, ThingPlaceMode.Near);
                        }));
                    }
                }
            }
        }

        return list;
    }
}