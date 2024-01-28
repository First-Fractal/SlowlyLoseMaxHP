using System;
using Terraria;
using Terraria.ModLoader;

namespace SlowlyLoseMaxHP
{
    internal class SLMHPlayer : ModPlayer
    {
        //vars for counting down the cooldown for losing max hp
        static int cooldownMax = ffFunc.TimeToTick(1);
        static int cooldown = cooldownMax;

        //vars for counting down the cooldown for losing max hp
        public override void PostUpdate()
        {
            //count down the cooldown
            if (cooldown > 0)
            {
                cooldown--;
            } else
            {
                Console.WriteLine($"The Player {Player.name} has {Player.ConsumedLifeCrystals} Life Crystals and {Player.ConsumedLifeFruit} Life Frults");

                //check if the player has one life fruit
                if (Player.ConsumedLifeFruit > 0)
                {
                    //remove one life fruit from the player, decreasing there max hp
                    Player.ConsumedLifeFruit--;
                }
                //check if the player has one life crystal
                else if (Player.ConsumedLifeCrystals > 0)
                {
                    //remove one life crystal from the player, decreasing there max hp
                    Player.ConsumedLifeCrystals--;
                }

                //reset the cooldown
                cooldown = cooldownMax;
            }
            base.PostUpdate();
        }
    }
}
