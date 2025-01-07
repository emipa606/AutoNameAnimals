using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Verse;

namespace AutoNameAnimals.HarmonyPatches;

[HarmonyPatch(typeof(Hediff_Pregnant), nameof(Hediff_Pregnant.Tick))]
internal class Hediff_Pregnant_Tick
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var codes = new List<CodeInstruction>(instructions);
        for (var i = 0; i < codes.Count; i++)
        {
            if (codes[i].opcode == OpCodes.Ldstr && codes[i].operand as string == "MessageGaveBirth")
            {
                for (; i < codes.Count; i++)
                {
                    if (codes[i].Calls(AccessTools.Method(typeof(Messages), nameof(Messages.Message), [
                            typeof(string),
                            typeof(LookTargets),
                            typeof(MessageTypeDef),
                            typeof(bool)
                        ])))
                    {
                        break;
                    }
                }
            }
            else
            {
                yield return codes[i];
            }
        }
    }
}