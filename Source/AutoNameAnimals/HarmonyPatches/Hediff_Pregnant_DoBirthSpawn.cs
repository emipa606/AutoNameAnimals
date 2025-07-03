using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using RimWorld;
using Verse;

namespace AutoNameAnimals.HarmonyPatches;

[HarmonyPatch(typeof(Hediff_Pregnant), nameof(Hediff_Pregnant.DoBirthSpawn))]
internal class Hediff_Pregnant_DoBirthSpawn
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var codes = new List<CodeInstruction>(instructions);
        for (var i = 0; i < codes.Count; i++)
        {
            yield return codes[i];
            if (i > 0 && codes[i - 1].Calls(AccessTools.Method(typeof(PawnGenerator),
                    nameof(PawnGenerator.GeneratePawn),
                    [typeof(PawnGenerationRequest)])) && codes[i].opcode == OpCodes.Stloc_2)
            {
                yield return new CodeInstruction(OpCodes.Ldloc_2);
                yield return CodeInstruction.Call(typeof(AutoNameAnimals),
                    nameof(AutoNameAnimals.GeneratePawnNameOnBirthHelper));
            }
            else if (codes[i].Calls(AccessTools.Method(typeof(TaleRecorder), nameof(TaleRecorder.RecordTale))))
            {
                yield return new CodeInstruction(OpCodes.Ldarg_0);
                yield return new CodeInstruction(OpCodes.Ldloc_2);
                yield return CodeInstruction.Call(typeof(Hediff_Pregnant_DoBirthSpawn), nameof(gaveBirthMessageHelper));
            }
        }
    }

    private static void gaveBirthMessageHelper(Pawn mother, Pawn pawn)
    {
        if (PawnUtility.ShouldSendNotificationAbout(mother) || PawnUtility.ShouldSendNotificationAbout(pawn))
        {
            Messages.Message("MessageGaveBirthTo".Translate(mother, pawn), pawn, MessageTypeDefOf.PositiveEvent);
        }
    }
}