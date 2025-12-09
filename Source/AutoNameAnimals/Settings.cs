using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace AutoNameAnimals;

public class Settings : ModSettings
{
    public static bool NameOnBirth = true;

    public static bool NameOnHatch = true;

    public static bool NameOnTame = true;

    public static bool NameOnSelfTame = true;

    public static bool NameOnWander = true;

    // List of names that should be excluded from random selection
    public static List<string> ExcludedNames = [];

    // Backing field for editing the excluded names as a comma-separated string
    private static string ExcludedNamesCsv = string.Empty;

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref NameOnBirth, "name_on_birth", true);
        Scribe_Values.Look(ref NameOnHatch, "name_on_hatch", true);
        Scribe_Values.Look(ref NameOnTame, "name_on_tame", true);
        Scribe_Values.Look(ref NameOnSelfTame, "name_on_self_tame", true);
        Scribe_Values.Look(ref NameOnWander, "name_on_wander", true);
        Scribe_Collections.Look(ref ExcludedNames, "excluded_names", LookMode.Value);
        ExcludedNames ??= [];
    }

    public static void DoSettingsWindowContents(Rect inRect)
    {
        var listingStandard = new Listing_Standard();
        listingStandard.Begin(inRect);
        listingStandard.CheckboxLabeled("name_on_birth_label".Translate(), ref NameOnBirth,
            "name_on_birth_note".Translate());
        listingStandard.CheckboxLabeled("name_on_hatch_label".Translate(), ref NameOnHatch,
            "name_on_hatch_note".Translate());
        listingStandard.CheckboxLabeled("name_on_tame_label".Translate(), ref NameOnTame,
            "name_on_tame_note".Translate());
        listingStandard.CheckboxLabeled("name_on_self_tame_label".Translate(), ref NameOnSelfTame,
            "name_on_self_tame_note".Translate());
        listingStandard.CheckboxLabeled("name_on_wander_label".Translate(), ref NameOnWander,
            "name_on_wander_note".Translate());

        listingStandard.GapLine();
        // Excluded names UI
        if (ExcludedNamesCsv.NullOrEmpty())
        {
            ExcludedNamesCsv = string.Join(", ", ExcludedNames);
        }

        listingStandard.Label("excluded_names_label".Translate(), tooltip: "excluded_names_note".Translate());
        ExcludedNamesCsv = listingStandard.TextEntry(ExcludedNamesCsv, 3);

        if (AutoNameAnimals.CurrentVersion != null)
        {
            listingStandard.Gap();
            GUI.contentColor = Color.gray;
            listingStandard.Label("name_on_version".Translate(AutoNameAnimals.CurrentVersion));
            GUI.contentColor = Color.white;
        }

        listingStandard.End();

        // Update the list from the CSV after editing
        var updated = (ExcludedNamesCsv ?? string.Empty)
            .Split([','], StringSplitOptions.RemoveEmptyEntries)
            .Select(s => s.Trim())
            .Where(s => !s.NullOrEmpty())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();
        ExcludedNames = updated;
    }
}