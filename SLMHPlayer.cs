using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

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
                Console.WriteLine($"The Player {Player.name} has {Player.ConsumedLifeCrystals} Life Crystals and {Player.ConsumedLifeFruit} Life Frults", true);

                //check if the player has one life fruit
                if (Player.ConsumedLifeFruit > 0)
                {
                    //remove four life fruit from the player, decreasing there max hp
                    Player.ConsumedLifeFruit--;
                    Player.ConsumedLifeFruit--;
                    Player.ConsumedLifeFruit--;
                    Player.ConsumedLifeFruit--;
                        
                    //tell the player that they just lost 20 max hp
                    CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y, 20, 20), Color.Red, "You just lost 20 max HP!", true);
                }
                //check if the player has one life crystal
                else if (Player.ConsumedLifeCrystals > 0)
                {
                    //remove one life crystal from the player, decreasing there max hp
                    Player.ConsumedLifeCrystals--;
                        
                    //tell the player that they just lost 20 max hp
                    CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y, 20, 20), Color.Red, "You just lost 20 max HP!", true);
                }

                //reset the cooldown
                cooldown = cooldownMax;
            }
            base.PostUpdate();
        }
    }
}
