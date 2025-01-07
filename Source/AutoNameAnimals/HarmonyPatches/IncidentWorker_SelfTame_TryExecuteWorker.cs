using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using RimWorld;
using Verse;

namespace AutoNameAnimals.HarmonyPatches;

[HarmonyPatch(typeof(IncidentWorker_SelfTame), "TryExecuteWorker")]
internal class IncidentWorker_SelfTame_TryExecuteWorker
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

            yield return new CodeInstruction(OpCodes.Ldloc_1);
            yield return CodeInstruction.Call(typeof(AutoNameAnimals),
                nameof(AutoNameAnimals.GeneratePawnNameOnSelfTameHelper));
        }
    }
}