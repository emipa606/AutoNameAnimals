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

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref NameOnBirth, "name_on_birth", true);
        Scribe_Values.Look(ref NameOnHatch, "name_on_hatch", true);
        Scribe_Values.Look(ref NameOnTame, "name_on_tame", true);
        Scribe_Values.Look(ref NameOnSelfTame, "name_on_self_tame", true);
        Scribe_Values.Look(ref NameOnWander, "name_on_wander", true);
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
        if (AutoNameAnimals.CurrentVersion != null)
        {
            listingStandard.Gap();
            GUI.contentColor = Color.gray;
            listingStandard.Label("name_on_version".Translate(AutoNameAnimals.CurrentVersion));
            GUI.contentColor = Color.white;
        }

        listingStandard.End();
    }
}