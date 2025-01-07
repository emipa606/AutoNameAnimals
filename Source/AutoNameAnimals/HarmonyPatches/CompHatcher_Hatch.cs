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
            yield return codes[i];
            if (i > 0 && codes[i - 1].Calls(AccessTools.Method(typeof(PawnGenerator),
                    nameof(PawnGenerator.GeneratePawn),
                    [typeof(PawnGenerationRequest)])) && codes[i].opcode == OpCodes.Stloc_S)
            {
                yield return new CodeInstruction(OpCodes.Ldloc_S, (byte)5);
                yield return CodeInstruction.Call(typeof(AutoNameAnimals),
                    nameof(AutoNameAnimals.GeneratePawnNameOnHatchHelper));
            }
            else if (codes[i].Calls(AccessTools.Method(typeof(WorldPawns), nameof(WorldPawns.PassToWorld))))
            {
                yield return new CodeInstruction(OpCodes.Ldloc_S, (byte)5)
                {
                    labels = codes[i + 1].ExtractLabels()
                };
                yield return CodeInstruction.Call(typeof(CompHatcher_Hatch), nameof(HatchedMessageHelper));
            }
        }
    }

    private static void HatchedMessageHelper(Pawn pawn)
    {
        if (PawnUtility.ShouldSendNotificationAbout(pawn))
        {
            Messages.Message("MessageHatched".Translate(pawn), pawn, MessageTypeDefOf.PositiveEvent);
        }
    }
}