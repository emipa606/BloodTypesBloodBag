using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace BloodTypes.Harmony;

[HarmonyPatch(typeof(DefGenerator), "GenerateImpliedDefs_PreResolve")]
public static class DefGenerator_GenerateImpliedDefs_PreResolve
{
    [HarmonyPostfix]
    public static void Postfix()
    {
        var DonateBlood = DefDatabase<RecipeDef>.GetNamed("DonateBlood");

        var humanoidRaces = HumanoidRaces();

        foreach (var humanoidRace in humanoidRaces)
        {
            if (humanoidRace.recipes == null)
            {
                humanoidRace.recipes = [];
            }

            humanoidRace.recipes.Add(DonateBlood);
        }
    }

    public static IEnumerable<ThingDef> HumanoidRaces()
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