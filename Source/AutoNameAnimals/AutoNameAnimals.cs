using System.Reflection;
using HarmonyLib;
using Mlie;
using RimWorld;
using UnityEngine;
using Verse;

namespace AutoNameAnimals;

[StaticConstructorOnStartup]
public class AutoNameAnimals : Mod
{
    public static string currentVersion;

    public AutoNameAnimals(ModContentPack content)
        : base(content)
    {
        currentVersion = VersionFromManifest.GetVersionFromModMetaData(content.ModMetaData);
        new Harmony("sky.autoname.autonameanimals").PatchAll(Assembly.GetExecutingAssembly());
        GetSettings<Settings>();
    }

    public override string SettingsCategory()
    {
        return "Auto-Name Animals";
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        Settings.DoSettingsWindowContents(inRect);
    }

    private static bool ShouldName(Pawn pawn, bool allowed)
    {
        if (allowed && !pawn.RaceProps.Humanlike && pawn.Name?.Numerical == true && pawn.Faction != null)
        {
            return pawn.Faction == Faction.OfPlayer;
        }

        return false;
    }

    public static void GeneratePawnNameOnBirthHelper(Pawn pawn)
    {
        if (ShouldName(pawn, Settings.name_on_birth))
        {
            pawn.Name = PawnBioAndNameGenerator.GeneratePawnName(pawn);
        }
    }

    public static void GeneratePawnNameOnHatchHelper(Pawn pawn)
    {
        if (ShouldName(pawn, Settings.name_on_hatch))
        {
            pawn.Name = PawnBioAndNameGenerator.GeneratePawnName(pawn);
        }
    }

    public static void GeneratePawnNameOnTameHelper(Pawn pawn)
    {
        if (ShouldName(pawn, Settings.name_on_tame))
        {
            pawn.Name = PawnBioAndNameGenerator.GeneratePawnName(pawn);
        }
    }

    public static void GeneratePawnNameOnSelfTameHelper(Pawn pawn)
    {
        if (ShouldName(pawn, Settings.name_on_self_tame))
        {
            pawn.Name = PawnBioAndNameGenerator.GeneratePawnName(pawn);
        }
    }

    public static void GeneratePawnNameOnWanderHelper(Pawn pawn)
    {
        if (ShouldName(pawn, Settings.name_on_wander))
        {
            pawn.Name = PawnBioAndNameGenerator.GeneratePawnName(pawn);
        }
    }

    public static void GeneratePawnNameHelper(Pawn pawn)
    {
        if (!pawn.RaceProps.Humanlike)
        {
            pawn.Name = PawnBioAndNameGenerator.GeneratePawnName(pawn);
        }
    }
}