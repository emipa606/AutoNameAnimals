using HarmonyLib;
using Verse;

namespace AutoNameAnimals.HarmonyPatches;

[HarmonyPatch(typeof(Thing), nameof(Thing.PostGeneratedForTrader))]
internal static class Thing_PostGeneratedForTrader
{
    public static void Postfix(Thing __instance)
    {
        if (__instance is Pawn pawn)
        {
            AutoNameAnimals.GeneratePawnNameHelper(pawn);
        }
    }
}