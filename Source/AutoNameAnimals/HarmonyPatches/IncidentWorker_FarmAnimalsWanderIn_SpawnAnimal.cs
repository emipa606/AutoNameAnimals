using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using RimWorld;
using Verse;

namespace AutoNameAnimals.HarmonyPatches;

[HarmonyPatch(typeof(IncidentWorker_FarmAnimalsWanderIn), "SpawnAnimal")]
internal class IncidentWorker_FarmAnimalsWanderIn_SpawnAnimal
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var ofPlayerMethod = AccessTools.PropertyGetter(typeof(Faction), nameof(Faction.OfPlayer));
        var setFactionMethod = AccessTools.Method(typeof(Thing), nameof(Thing.SetFaction),
            [typeof(Faction), typeof(Pawn)]);

        var injectedBeforeSetFaction = 0;
        var injectedAfterSetFaction = 0;

        var codes = new List<CodeInstruction>(instructions);
        foreach (var codeInstruction in codes)
        {
            if (codeInstruction.Calls(ofPlayerMethod))
            {
                yield return new CodeInstruction(OpCodes.Dup);
                injectedBeforeSetFaction++;
            }

            yield return codeInstruction;

            if (!codeInstruction.Calls(setFactionMethod))
            {
                continue;
            }

            yield return CodeInstruction.Call(typeof(AutoNameAnimals),
                nameof(AutoNameAnimals.GeneratePawnNameOnWanderHelper));
            injectedAfterSetFaction++;
        }

        if (injectedBeforeSetFaction == 0 || injectedAfterSetFaction == 0)
        {
            Log.Warning(
                $"[AutoNameAnimals] FarmAnimalsWanderIn transpiler did not inject correctly. BeforeSetFaction={injectedBeforeSetFaction}, AfterSetFaction={injectedAfterSetFaction}");
        }
    }
}