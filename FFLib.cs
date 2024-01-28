using Terraria;
using Terraria.ID;
using Terraria.Chat;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using System.Linq;

//this is my own libary that I use to store snippits. I don't want it to be it's own mod, so I'll use copy and paste this file when needed.
namespace SlowlyLoseMaxHP
{
    public class ffVar
    {
        //list of all the boss parts
        public static int[] BossParts = { NPCID.EaterofWorldsHead, NPCID.EaterofWorldsBody, NPCID.EaterofWorldsTail, NPCID.Creeper, NPCID.SkeletronHand, NPCID.SkeletronHead, NPCID.WallofFleshEye, NPCID.TheDestroyer, NPCID.TheDestroyerBody, NPCID.TheDestroyerTail, NPCID.Probe, NPCID.PrimeCannon, NPCID.PrimeLaser, NPCID.PrimeSaw, NPCID.PrimeVice, NPCID.PlanterasHook, NPCID.PlanterasTentacle, NPCID.GolemFistLeft, NPCID.GolemFistRight, NPCID.GolemHead, NPCID.GolemHeadFree, NPCID.CultistBossClone, NPCID.MoonLordCore, NPCID.MoonLordHand, NPCID.MoonLordHead, NPCID.MoonLordFreeEye, NPCID.MoonLordLeechBlob };

        public class zombies
        {
            //list of all the different types of zombies in vanilla
            public static int[] normalZombies = {NPCID.Zombie, NPCID.SmallZombie, NPCID.BigZombie, NPCID.ArmedZombie,
            NPCID.BaldZombie, NPCID.SmallBaldZombie, NPCID.BigBaldZombie,
            NPCID.PincushionZombie, NPCID.SmallPincushionZombie, NPCID.BigPincushionZombie, NPCID.ArmedZombiePincussion,
            NPCID.SlimedZombie, NPCID.SmallSlimedZombie, NPCID.BigSlimedZombie, NPCID.ArmedZombieSlimed,
            NPCID.SwampZombie, NPCID.SmallSwampZombie, NPCID.BigSwampZombie, NPCID.ArmedZombieSwamp,
            NPCID.TwiggyZombie, NPCID.SmallTwiggyZombie, NPCID.BigTwiggyZombie, NPCID.ArmedZombieTwiggy,
            NPCID.FemaleZombie, NPCID.SmallFemaleZombie, NPCID.BigFemaleZombie, NPCID.ArmedZombieCenx,
            NPCID.TorchZombie, NPCID.ArmedTorchZombie};

            //list of all the variant zombies in vanilla
            public static int[] variantZombies = {NPCID.ZombieDoctor, NPCID.ZombieSuperman, NPCID.ZombiePixie,
            NPCID.ZombieXmas, NPCID.ZombieSweater,
            NPCID.ZombieEskimo, NPCID.ArmedZombieEskimo,
            NPCID.ZombieRaincoat, NPCID.SmallRainZombie, NPCID.BigRainZombie,
            NPCID.MaggotZombie};

            //list of all the blood moon zombies in vanilla
            public static int[] bloodZombies = { NPCID.BloodZombie, NPCID.TheBride, NPCID.TheGroom };

            //list of all the special zombies in vanilla
            public static int[] specialZombies = { NPCID.DoctorBones, NPCID.ZombieMerman };

            //list of all the hardmode zombies in vanilla
            public static int[] hardmodeZombies = { NPCID.ZombieMushroom, NPCID.ZombieMushroomHat, NPCID.Eyezor };

            //list of all zombies in vanilla
            public static int[] allZombies = normalZombies.Concat(variantZombies).ToArray()
                .Concat(bloodZombies).ToArray()
                .Concat(specialZombies).ToArray()
                .Concat(hardmodeZombies).ToArray();
        }
    }

    public class ffFunc
    {
        //function for saying something in the chat
        public static void Talk(string message, Color color)
        {
            //check if the player is in singleplayer of multiplayer
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                //send a message to the singe player in the chat
                Main.NewText(message, color);
            }
            else
            {
                //Brodcast a message to everyone in the server
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(message), color);
            }
        }

        //function that will convert human time to terraria ticks
        public static int TimeToTick(int secs = 0, int mins = 0, int hours = 0, int days = 0)
        {
            //define the units
            int sec = 60;
            int min = sec * 60;
            int hour = min * 60;
            int day = hours * 24;

            //multiply the units and combine the final time
            return (sec * secs) + (min * mins) + (hour * hours) + (day * days);
        }

        //function for checking if a boss is currently alive
        public static bool IsBossAlive()
        {
            foreach (NPC npc in Main.npc)
            {
                if (npc.boss == true)
                {
                    return true;
                }
                else
                {
                    foreach (int part in ffVar.BossParts)
                    {
                        if (npc.type == part)
                        {
                            return true;
                        }
                    }
                }

            }
            return false;
        }
    }
}