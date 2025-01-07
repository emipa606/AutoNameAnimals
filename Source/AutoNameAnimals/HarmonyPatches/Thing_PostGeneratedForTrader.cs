using HarmonyLib;
using Verse;

namespace AutoNameAnimals.HarmonyPatches;

[HarmonyPatch(typeof(Thing), nameof(Thing.PostGeneratedForTrader))]
internal class Thing_PostGeneratedForTrader
{
    private static void Postfix(Thing __instance)
    {
        if (__instance is Pawn pawn)
        {
            AutoNameAnimals.GeneratePawnNameHelper(pawn);
        }
    }
}