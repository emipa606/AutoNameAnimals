using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace AutoNameAnimals.HarmonyPatches;

[HarmonyPatch(typeof(CompHatcher), nameof(CompHatcher.Hatch))]
internal class CompHatcher_Hatch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var codes = new List<CodeInstruction>(instructions);
        for (var i = 0; i < codes.Count; i++)
        {
            var code = codes[i];
            yield return code;

            // Look for the call to GeneratePawn
            if (code.Calls(AccessTools.Method(typeof(PawnGenerator), nameof(PawnGenerator.GeneratePawn),
                    [typeof(PawnGenerationRequest)])))
            {
                // The next instruction should be stloc.* (stores the pawn)
                if (i + 1 < codes.Count && codes[i + 1].opcode.Name.StartsWith("stloc"))
                {
                    var pawnStloc = codes[i + 1];
                    yield return pawnStloc; // yield the stloc

                    // Now inject: ldloc (same index as stloc), call GeneratePawnNameOnHatchHelper
                    yield return new CodeInstruction(
                        pawnStloc.opcode == OpCodes.Stloc_0 ? OpCodes.Ldloc_0 :
                        pawnStloc.opcode == OpCodes.Stloc_1 ? OpCodes.Ldloc_1 :
                        pawnStloc.opcode == OpCodes.Stloc_2 ? OpCodes.Ldloc_2 :
                        pawnStloc.opcode == OpCodes.Stloc_3 ? OpCodes.Ldloc_3 :
                        OpCodes.Ldloc_S, pawnStloc.operand);
                    yield return CodeInstruction.Call(typeof(AutoNameAnimals),
                        nameof(AutoNameAnimals.GeneratePawnNameOnHatchHelper));
                    i++; // skip the stloc, already yielded
                    continue;
                }
            }

            // Patch for PassToWorld (optional, same logic applies)
            if (!code.Calls(AccessTools.Method(typeof(WorldPawns), nameof(WorldPawns.PassToWorld))))
            {
                continue;
            }

            // Find the pawn local index used in PassToWorld
            if (i <= 0 || !codes[i - 1].opcode.Name.StartsWith("ldloc"))
            {
                continue;
            }

            var pawnLdloc = codes[i - 1];
            yield return new CodeInstruction(pawnLdloc.opcode, pawnLdloc.operand)
            {
                labels = codes[i + 1].ExtractLabels()
            };
            yield return CodeInstruction.Call(typeof(CompHatcher_Hatch), nameof(hatchedMessageHelper));
        }
    }

    private static void hatchedMessageHelper(Pawn pawn)
    {
        if (PawnUtility.ShouldSendNotificationAbout(pawn))
        {
            Messages.Message("MessageHatched".Translate(pawn), pawn, MessageTypeDefOf.PositiveEvent);
        }
    }
}