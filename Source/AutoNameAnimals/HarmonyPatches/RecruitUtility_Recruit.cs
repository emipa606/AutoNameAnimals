using HarmonyLib;
using RimWorld;
using Verse;

namespace AutoNameAnimals.HarmonyPatches;

[HarmonyPatch(typeof(RecruitUtility), nameof(RecruitUtility.Recruit))]
internal class RecruitUtility_Recruit
{
    private static void Postfix(Pawn pawn)
    {
        AutoNameAnimals.GeneratePawnNameOnTameHelper(pawn);
    }
}