using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace BloodTypes.Harmony;

[HarmonyPatch(typeof(DefGenerator), nameof(DefGenerator.GenerateImpliedDefs_PreResolve))]
public static class DefGenerator_GenerateImpliedDefs_PreResolve
{
    public static void Postfix()
    {
        var donateBlood = DefDatabase<RecipeDef>.GetNamed("DonateBlood");

        var humanoidRaces = DefGenerator_GenerateImpliedDefs_PreResolve.humanoidRaces();

        foreach (var humanoidRace in humanoidRaces)
        {
            humanoidRace.recipes ??= [];

            humanoidRace.recipes.Add(donateBlood);
        }
    }

    private static IEnumerable<ThingDef> humanoidRaces()
    {
        return DefDatabase<ThingDef>.AllDefsListForReading.Where(ValidRace);
    }

    public static bool ValidRace(ThingDef def)
    {
        return def.race is { IsFlesh: true, Humanlike: true } &&
               def.race.BloodDef?.defName != "ATR_CoolantAndroidTier" &&
               def.race.FleshType?.defName != "ATR_AndroidTier";
    }
}