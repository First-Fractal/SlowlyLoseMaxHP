using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SlowlyLoseMaxHP
{
    public class SLMHPlayer : ModPlayer
    {
        //vars for counting down the cooldown for losing max hp
        static int cooldownMax = ffFunc.TimeToTick(0, 5);
        static int cooldown = cooldownMax;
        static bool bossAlive = false;

        public override void PostUpdate()
        {
            //decrease the cooldown if a boss is alive, and reset it if not
            if (ffFunc.IsBossAlive())
            {
                //only set this stuff once
               if (!bossAlive)
               {
                    //lower down the timer
                    cooldownMax = ffFunc.TimeToTick(20);
                    cooldown = cooldownMax;

                    //tell the player that the boss is going to suck up there life at a faster rate then before.
                    string message = "Hey, the boss that is currently alive is going to suck up your max HP at a faster rate then before.";
                    ffFunc.Talk(message, Color.Orange);
                    bossAlive = true;
               }
            } 
            else 
            {
                if (bossAlive)
                {
                    //reset the timer to back to normal
                    cooldownMax = ffFunc.TimeToTick(0, 5);
                    cooldown = cooldownMax;

                    //tell the player that the boss is gone, and the max HP lose rate has gone back to normal
                    string message = "Hey, the boss is gone. The max HP lose rate has gone back to normal.";
                    ffFunc.Talk(message, Color.Orange);
                    bossAlive = false;
                }
            }

            //clamp the cooldown to be inside 0 and max cooldown
            cooldown = Math.Clamp(cooldown, 0, cooldownMax);

            //count down the cooldown
            if (cooldown > 0)
            {
                cooldown--;
            }
            else
            {
                //check if the player has one life fruit
                if (Player.ConsumedLifeFruit > 0)
                {
                    //remove four life fruit from the player, decreasing there max hp
                    Player.ConsumedLifeFruit--;
                    Player.ConsumedLifeFruit--;
                    Player.ConsumedLifeFruit--;
                    Player.ConsumedLifeFruit--;

                    //tell the player that they just lost 20 max hp
                    CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y, 20, 20), Color.Yellow, "You just lost 20 max HP!");
                }
                //check if the player has one life crystal
                else if (Player.ConsumedLifeCrystals > 0)
                {
                    //remove one life crystal from the player, decreasing there max hp
                    Player.ConsumedLifeCrystals--;

                    //tell the player that they just lost 20 max hp
                    CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y, 20, 20), Color.Red, "You just lost 20 max HP!");
                }

                //reset the cooldown
                cooldown = cooldownMax;
            }
            base.PostUpdate();
        }
    }
}
