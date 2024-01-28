using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace SlowlyLoseMaxHP.CoolerNurse
{
    [AutoloadHead]
    internal class CoolerNurse : ModNPC
    {
        public const string ShopName = "Shop";
        public int NumberOfTimesTalkedTo = 0;

        private static int ShimmerHeadIndex;
        private static Profiles.StackedNPCProfile NPCProfile;

        //save the map head for the shimmer version of her 
        public override void Load()
        {
            ShimmerHeadIndex = Mod.AddNPCHeadTexture(Type, Texture + "_Shimmer_Head");
        }

        public override void SetStaticDefaults()
        {
            //the total amount of frames that the nurse has
            Main.npcFrameCount[Type] = 23;

            //say what frames are for extra stuff like talking and sitting
            NPCID.Sets.ExtraFramesCount[Type] = 5;
            
            //say what frames are for attacking
            NPCID.Sets.AttackFrameCount[Type] = 4;

            NPCID.Sets.DangerDetectRange[Type] = 700; // The amount of pixels away from the center of the NPC that it tries to attack enemies.
            NPCID.Sets.AttackType[Type] = 0; // The type of attack the Town NPC performs. 0 = throwing, 1 = shooting, 2 = magic, 3 = melee
            NPCID.Sets.AttackTime[Type] = 90; // The amount of time it takes for the NPC's attack animation to be over once it starts.
            NPCID.Sets.AttackAverageChance[Type] = 30; // The denominator for the chance for a Town NPC to attack. Lower numbers make the Town NPC appear more aggressive.
            NPCID.Sets.HatOffsetY[Type] = 0; // For when a party is active, the party hat spawns at a Y offset.
            NPCID.Sets.ShimmerTownTransform[NPC.type] = true; // This set says that the Town NPC has a Shimmered form. Otherwise, the Town NPC will become transparent when touching Shimmer like other enemies.

            //tell the game that the cooler nurse has a different sprite for shimmer
            NPCID.Sets.ShimmerTownTransform[Type] = true; // Allows for this NPC to have a different texture after touching the Shimmer liquid.

            //save the different varation of the cooler nurse
            NPCProfile = new Profiles.StackedNPCProfile(
                new Profiles.DefaultNPCProfile(Texture, NPCHeadLoader.GetHeadSlot(HeadTexture), Texture + "_Party"),
                new Profiles.DefaultNPCProfile(Texture + "_Shimmer", ShimmerHeadIndex, Texture + "_Shimmer_Party")
            );

            base.SetStaticDefaults();
        }

        //load in different varation of the cooler nurse
        public override ITownNPCProfile TownNPCProfile()
        {
            return NPCProfile;
        }

        public override void SetDefaults()
        {
            //clone in the defaults form the nurse
            NPC.CloneDefaults(NPCID.Nurse);

            //tell it to use the same animation as the nurse
            AnimationType = NPCID.Nurse;
            base.SetDefaults();
        }

        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            if (NPC.unlockedNurseSpawn)
                return true;
            return base.CanTownNPCSpawn(numTownNPCs);
        }
    }
}
