using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Terraria.ID;

namespace SlowlyLoseMaxHP
{
    public class SLMHPlayer : ModPlayer
    {
        //set the variables to be sync across the server
        public int cooldownMax = 99999;
        public int cooldown = 99999;

        public override void PostUpdate()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Main.NewText(cooldown);

                //check if the cooldown is at 0
                if(cooldown == 0)
                {
                    //check if the player has one life fruit
                    if (Player.ConsumedLifeFruit > 0)
                    {
                        //variable for counting how many life fruits were removed
                        int counter = 0;

                        //removed the life fruit based on the amount of times the player wanted to
                        for (int i = 0; i < SLMHConfig.Instance.lifeFruitRemove; i++)
                        {
                            Player.ConsumedLifeFruit--;
                            counter++;

                            //safe guard to make sure that the life fruits dont become negative
                            if (Player.ConsumedLifeFruit <= 0)
                            {
                                Player.ConsumedLifeFruit = 0;
                                break;
                            }
                        }

                        //make sure the life fruits dont go below zero
                        Player.ConsumedLifeFruit = Math.Clamp(Player.ConsumedLifeFruit, 0, Player.LifeFruitMax);

                        //tell the player that they just lost some health
                        CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y, 20, 20), Color.Yellow, Language.GetTextValue("Mods.SlowlyLoseMaxHP.Chat.MissingHP", (5 * counter).ToString()));
                    }
                    //check if the player has one life crystal
                    else if (Player.ConsumedLifeCrystals > 0)
                    {
                        //variable for counting how many life crystals were removed
                        int counter = 0;

                        //removed the life crystal based on the amount of times the player wanted to
                        for (int i = 0; i < SLMHConfig.Instance.lifeCrystalRemove; i ++)
                        {
                            Player.ConsumedLifeCrystals--;
                            counter++;
                        
                            //safe guard to make sure that the life crystal dont become negative
                            if (Player.ConsumedLifeCrystals <= 0)
                            {
                                Player.ConsumedLifeCrystals = 0;
                                break;
                            }
                        }

                        //make sure the life crystals dont go below zero
                        Player.ConsumedLifeCrystals = Math.Clamp(Player.ConsumedLifeCrystals, 0, Player.LifeCrystalMax);

                        //tell the player that they just lost some health
                        CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y, 20, 20), Color.Red, Language.GetTextValue("Mods.SlowlyLoseMaxHP.Chat.MissingHP", (20 * counter).ToString()));
                    }
                }
            }
            base.PostUpdate();
        }
    }
}
