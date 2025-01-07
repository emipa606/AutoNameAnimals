using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using RimWorld;

namespace AutoNameAnimals.HarmonyPatches;

[HarmonyPatch(typeof(IncidentWorker_FarmAnimalsWanderIn), "SpawnAnimal")]
internal class IncidentWorker_FarmAnimalsWanderIn_SpawnAnimal
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var codes = new List<CodeInstruction>(instructions);
        foreach (var codeInstruction in codes)
        {
            yield return codeInstruction;
            if (codeInstruction.opcode != OpCodes.Dup)
            {
                continue;
            }

            yield return CodeInstruction.Call(typeof(AutoNameAnimals),
                nameof(AutoNameAnimals.GeneratePawnNameOnWanderHelper));
            yield return new CodeInstruction(OpCodes.Dup);
        }
    }
}