using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using RimWorld;
using Verse;

namespace AutoNameAnimals.HarmonyPatches;

[HarmonyPatch(typeof(InteractionWorker_RecruitAttempt), nameof(InteractionWorker_RecruitAttempt.DoRecruit), [
    typeof(Pawn),
    typeof(Pawn),
    typeof(string),
    typeof(string),
    typeof(bool),
    typeof(bool)
], [
    ArgumentType.Normal,
    ArgumentType.Normal,
    ArgumentType.Out,
    ArgumentType.Out,
    ArgumentType.Normal,
    ArgumentType.Normal
])]
internal class InteractionWorker_RecruitAttempt_DoRecruit
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var codes = new List<CodeInstruction>(instructions);
        foreach (var codeInstruction in codes)
        {
            yield return codeInstruction;
            if (!codeInstruction.Calls(AccessTools.Method(typeof(Thing), nameof(Thing.SetFaction))))
            {
                continue;
            }

            yield return new CodeInstruction(OpCodes.Ldarg_1);
            yield return CodeInstruction.Call(typeof(AutoNameAnimals),
                nameof(AutoNameAnimals.GeneratePawnNameOnTameHelper));
        }
    }
}