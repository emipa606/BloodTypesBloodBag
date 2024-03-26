﻿using System.Reflection;
using Verse;

namespace BloodTypes.Harmony;

[StaticConstructorOnStartup]
internal class Main : Mod
{
    public Main(ModContentPack content) : base(content)
    {
        new HarmonyLib.Harmony("BloodTypes.Harmony").PatchAll(Assembly.GetExecutingAssembly());
    }
}