using UnityEngine;
using Verse;

namespace AutoNameAnimals;

public class Settings : ModSettings
{
    public static bool name_on_birth = true;

    public static bool name_on_hatch = true;

    public static bool name_on_tame = true;

    public static bool name_on_self_tame = true;

    public static bool name_on_wander = true;

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref name_on_birth, "name_on_birth", true);
        Scribe_Values.Look(ref name_on_hatch, "name_on_hatch", true);
        Scribe_Values.Look(ref name_on_tame, "name_on_tame", true);
        Scribe_Values.Look(ref name_on_self_tame, "name_on_self_tame", true);
        Scribe_Values.Look(ref name_on_wander, "name_on_wander", true);
    }

    public static void DoSettingsWindowContents(Rect inRect)
    {
        var listing_Standard = new Listing_Standard();
        listing_Standard.Begin(inRect);
        listing_Standard.CheckboxLabeled("name_on_birth_label".Translate(), ref name_on_birth,
            "name_on_birth_note".Translate());
        listing_Standard.CheckboxLabeled("name_on_hatch_label".Translate(), ref name_on_hatch,
            "name_on_hatch_note".Translate());
        listing_Standard.CheckboxLabeled("name_on_tame_label".Translate(), ref name_on_tame,
            "name_on_tame_note".Translate());
        listing_Standard.CheckboxLabeled("name_on_self_tame_label".Translate(), ref name_on_self_tame,
            "name_on_self_tame_note".Translate());
        listing_Standard.CheckboxLabeled("name_on_wander_label".Translate(), ref name_on_wander,
            "name_on_wander_note".Translate());
        if (AutoNameAnimals.currentVersion != null)
        {
            listing_Standard.Gap();
            GUI.contentColor = Color.gray;
            listing_Standard.Label("name_on_version".Translate(AutoNameAnimals.currentVersion));
            GUI.contentColor = Color.white;
        }

        listing_Standard.End();
    }
}