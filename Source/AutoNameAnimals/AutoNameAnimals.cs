using System;
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
    public static string CurrentVersion;

    public AutoNameAnimals(ModContentPack content)
        : base(content)
    {
        CurrentVersion = VersionFromManifest.GetVersionFromModMetaData(content.ModMetaData);
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

    private static bool shouldName(Pawn pawn, bool allowed)
    {
        if (allowed && !pawn.RaceProps.Humanlike && pawn.Name?.Numerical == true && pawn.Faction != null)
        {
            return pawn.Faction == Faction.OfPlayer;
        }

        return false;
    }

    private static bool isExcluded(Name name)
    {
        if (name is not NameSingle single)
        {
            return false;
        }

        var n = single.Name ?? string.Empty;
        return Settings.ExcludedNames.Any(x => string.Equals(x, n, StringComparison.OrdinalIgnoreCase));
    }

    private static Name generateFilteredPawnName(Pawn pawn)
    {
        // Try multiple times to avoid excluded names, fall back if all attempts are excluded
        Name last = null;
        for (var i = 0; i < 20; i++)
        {
            var candidate = PawnBioAndNameGenerator.GeneratePawnName(pawn);
            last = candidate;
            if (!isExcluded(candidate))
            {
                return candidate;
            }
        }

        return last ?? PawnBioAndNameGenerator.GeneratePawnName(pawn);
    }

    public static void GeneratePawnNameOnBirthHelper(Pawn pawn)
    {
        if (shouldName(pawn, Settings.NameOnBirth))
        {
            pawn.Name = generateFilteredPawnName(pawn);
        }
    }

    public static void GeneratePawnNameOnHatchHelper(Pawn pawn)
    {
        if (shouldName(pawn, Settings.NameOnHatch))
        {
            pawn.Name = generateFilteredPawnName(pawn);
        }
    }

    public static void GeneratePawnNameOnTameHelper(Pawn pawn)
    {
        if (shouldName(pawn, Settings.NameOnTame))
        {
            pawn.Name = generateFilteredPawnName(pawn);
        }
    }

    public static void GeneratePawnNameOnSelfTameHelper(Pawn pawn)
    {
        if (shouldName(pawn, Settings.NameOnSelfTame))
        {
            pawn.Name = generateFilteredPawnName(pawn);
        }
    }

    public static void GeneratePawnNameOnWanderHelper(Pawn pawn)
    {
        if (shouldName(pawn, Settings.NameOnWander))
        {
            pawn.Name = generateFilteredPawnName(pawn);
        }
    }

    public static void GeneratePawnNameHelper(Pawn pawn)
    {
        if (!pawn.RaceProps.Humanlike)
        {
            pawn.Name = generateFilteredPawnName(pawn);
        }
    }
}