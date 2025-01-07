using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using RimWorld;
using Verse;

namespace AutoNameAnimals.HarmonyPatches;

[HarmonyPatch(typeof(IncidentWorker_NeutralGroup), "SpawnPawns")]
internal class IncidentWorker_NeutralGroup_SpawnPawns
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var codes = new List<CodeInstruction>(instructions);
        for (var i = 0; i < codes.Count; i++)
        {
            yield return codes[i];
            if (i <= 0 || !codes[i - 1].Calls(AccessTools.PropertyGetter(typeof(List<Pawn>.Enumerator), "Current")) ||
                codes[i].opcode != OpCodes.Stloc_3)
            {
                continue;
            }

            yield return new CodeInstruction(OpCodes.Ldloc_3);
            yield return CodeInstruction.Call(typeof(AutoNameAnimals), nameof(AutoNameAnimals.GeneratePawnNameHelper));
        }
    }
}