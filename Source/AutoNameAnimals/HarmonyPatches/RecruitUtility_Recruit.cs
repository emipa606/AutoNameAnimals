using HarmonyLib;
using RimWorld;
using Verse;

namespace AutoNameAnimals.HarmonyPatches;

[HarmonyPatch(typeof(RecruitUtility), nameof(RecruitUtility.Recruit))]
internal static class RecruitUtility_Recruit
{
    public static void Postfix(Pawn pawn)
    {
        AutoNameAnimals.GeneratePawnNameOnTameHelper(pawn);
    }
}