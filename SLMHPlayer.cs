using System;
using Terraria;
using Terraria.ModLoader;

namespace SlowlyLoseMaxHP
{
    internal class SLMHPlayer : ModPlayer
    {
        //vars for counting down the cooldown for losing max hp
        static int cooldownMax = ffFunc.TimeToTick(10);
        static int cooldown = cooldownMax;

        //vars for counting down the cooldown for losing max hp
        public override void PostUpdate()
        {
            //count down the cooldown
            if (cooldown > 0)
            {
                cooldown--;
                Console.WriteLine(cooldown);
            } else
            {
                //remove one life crystal from the player, decreasing there max hp
                Player.ConsumedLifeCrystals--;

                //reset the cooldown
                cooldown = cooldownMax;
            }
            base.PostUpdate();
        }
    }
}
