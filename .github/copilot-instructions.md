# Copilot Instructions for Auto-Name Animals (Continued) Mod

## Mod Overview and Purpose
The **Auto-Name Animals (Continued)** mod is a continuation of an existing mod by Sky, designed for the game RimWorld. The primary function of this mod is to automatically assign names to newborn, hatched, and newly tamed animals, mimicking the behavior of the 'Randomize' option in RimWorld's naming UI, but automating the process. Additionally, a notification message is displayed when an animal hatches.

## Key Features and Systems
- **Automatic Animal Naming**: Automatically names animals when they are born, hatched, or tamed.
- **Notification System**: Alerts the player when an animal hatches.
- **Seamless Integration**: Works in the background, requiring minimal player interaction once active.

## Coding Patterns and Conventions
- **Class Structures**: Most classes are designed to be internal and are tailored to perform specific functions related to animal lifecycle events (e.g., birth, hatching, taming).
- **Naming Conventions**: Class names are PascalCase, and method names are also PascalCase, following C# conventions.
- **Modular Design**: Each key functionality is segregated into different files and classes to ensure clarity and ease of maintenance.

## XML Integration
- The mod interfaces with RimWorld's XML definitions to identify and interact with animal entities and events.
- Ensure your XML patches or modifications are properly aligned with RimWorld’s schema to avoid compatibility issues.

## Harmony Patching
- Harmony is used to apply patches to RimWorld’s core methods, allowing the mod to introduce or alter behaviors seamlessly.
- Pay attention to method signatures and patch targets to avoid runtime errors.
- Use descriptive method names and comments to clarify the purpose of each patch.

## Suggestions for Copilot

1. **Automatic Naming Function**:
   - Suggest code snippets for generating random names based on animal type or biome.
   - Provide examples for customizing naming patterns or themes.

2. **Notification Handling**:
   - Generate methods to create and display game notifications when specific events, like hatching or taming, occur.

3. **XML and Def Handling**:
   - Offer code samples for reading and interpreting XML animal definitions.
   - Provide guidance on patching XML files if new animal types need to be supported.

4. **Harmony Patches**:
   - Suggest safe Harmony patch templates with error logging to handle cases where the game's methods change due to updates.

5. **Best Practices**:
   - Recommend performance improvements if the code involves frequently called methods.
   - Interpret existing mod settings and provide templates for extending them.

By following these structured guidelines, you can effectively utilize GitHub Copilot to enhance and maintain the **Auto-Name Animals (Continued)** mod, ensuring it remains robust, efficient, and compatible with future RimWorld updates.
