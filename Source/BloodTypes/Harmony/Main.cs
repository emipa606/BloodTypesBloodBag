using System.Reflection;
using Verse;

namespace BloodTypes.Harmony
{
    [StaticConstructorOnStartup]
    internal class Main : Mod
    {
        public Main(ModContentPack content) : base(content)
        {
            var harmony = new HarmonyLib.Harmony("BloodTypes.Harmony");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}