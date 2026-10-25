# GitHub Copilot Instructions for Auto-Name Animals (Continued)

## Mod Overview and Purpose
Auto-Name Animals (Continued) is an updated version of the original AutoNameAnimals mod for the game RimWorld. The primary purpose of this mod is to automatically name newborn, hatched, and newly tamed animals with a proper name. This feature is similar to using the "Randomize" option in the naming UI, but it is done automatically. Additionally, the mod displays a message when an animal is hatched.

## Key Features and Systems
- **Automatic Naming**: Automatically names newborn, hatched, and newly tamed animals.
- **Hatching Message**: Displays a message when an animal is hatched.
- **Settings Interface**: Provides a settings interface to configure the mod's behavior.

## Coding Patterns and Conventions
- **C# Files**: The mod consists of 10 C# files, each responsible for specific functionalities.
- **Harmony Patching**: The mod uses Harmony patching to modify the game's behavior without altering the original source code.
- **XML Integration**: The mod uses XML for metadata and configuration.

## XML Integration
The mod includes one XML file:
- **About.xml**: Contains metadata about the mod, such as its name, author, and package ID.

## Harmony Patching
The mod uses Harmony patching to modify the following game functionalities:
- **Pregnancy and Birth**: Modifies the behavior of the `Hediff_Pregnant` class to automatically name newborn animals and display a message when an animal is hatched.
- **Trading**: Modifies the behavior of the `Thing_PostGeneratedForTrader` class to automatically name newly tamed animals from traders.
- **Hatching**: Modifies the behavior of the `CompHatcher_Hatch` class to automatically name hatched animals and display a message when an animal is hatched.

## Suggestions for Copilot
- **Automatic Naming**: When working with automatic naming, focus on generating unique and meaningful names for animals.
- **Harmony Patching**: When working with Harmony patching, ensure that the patches are correctly applied and do not introduce bugs into the game.
- **XML Integration**: When working with XML files, ensure that the metadata is correctly formatted and adheres to the XML standards.
- **Settings Interface**: When working with the settings interface, ensure that the settings are correctly saved and loaded.

## Example Code Snippet
csharp
using HarmonyLib;
using RimWorld;
using Verse;
using System.Collections.Generic;
using System.Linq;

namespace AutoNameAnimals
{
    public class AutoNameAnimals : Mod
    {
        public static Settings settings;

        public AutoNameAnimals(ModContentPack content) : base(content)
        {
            settings = GetSettings<Settings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            // Code to generate the settings window
        }

        public override string SettingsCategory()
        {
            return "AutoNameAnimals";
        }

        public static void GeneratePawnNameHelper(Pawn pawn)
        {
            // Code to generate a name for the pawn
        }
    }

    [HarmonyPatch(typeof(Hediff_Pregnant), "DoBirthSpawn")]
    public static class Hediff_Pregnant_DoBirthSpawn
    {
        [HarmonyPostfix]
        public static void gaveBirthMessageHelper(Pawn mother, List<Pawn> newborns, Hediff_Pregnant __instance)
        {
            // Code to display a message when an animal is hatched
        }
    }
}


## Conclusion
Auto-Name Animals (Continued) is a mod that automatically names newborn, hatched, and newly tamed animals in RimWorld. The mod uses Harmony patching to modify the game's behavior and XML files for metadata. When working on this mod, follow the coding patterns and conventions outlined above to ensure a smooth and effective development process.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

