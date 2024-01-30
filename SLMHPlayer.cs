using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Localization;

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
            //change the cooldown if a boss is alive and the config allows it, and reset it if not
            if (ffFunc.IsBossAlive() && SLMHConfig.Instance.bossChangeCooldown)
            {
                //update the max cooldown based on the new time from the boss
                if (SLMHConfig.Instance.bossCooldownMinute)
                    cooldownMax = ffFunc.TimeToTick(0, SLMHConfig.Instance.bossCooldown);
                else
                    cooldownMax = ffFunc.TimeToTick(SLMHConfig.Instance.bossCooldown);

                //only set this stuff once
                if (!bossAlive)
               {
                    //reset the cooldown
                    cooldown = cooldownMax;

                    //tell the player that the boss is going to suck up there life at a faster rate then before.
                    string message = Language.GetTextValue("Mods.SlowlyLoseMaxHP.Chat.BossSpawn");
                    ffFunc.Talk(message, Color.Orange);
                    bossAlive = true;
               }
            } 
            else 
            {
                //update the max cooldown based on the new time
                if (SLMHConfig.Instance.generalCooldownMinute)
                    cooldownMax = ffFunc.TimeToTick(0, SLMHConfig.Instance.generalCooldown);
                else
                    cooldownMax = ffFunc.TimeToTick(SLMHConfig.Instance.generalCooldown);

                if (bossAlive)
                {
                    //reset the cooldown
                    cooldown = cooldownMax;

                    //tell the player that the boss is gone, and the max HP lose rate has gone back to normal
                    string message = Language.GetTextValue("Mods.SlowlyLoseMaxHP.Chat.BossDespawn");
                    ffFunc.Talk(message, Color.Orange);
                    bossAlive = false;
                }
            }

            //clamp the cooldown to be inside 0 and max cooldown
            cooldown = Math.Clamp(cooldown, 0, cooldownMax);

            //count down the cooldown
            if (cooldown >= 0)
            {
                cooldown--;
            }
            else
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

                //reset the cooldown
                cooldown = cooldownMax;
            }
            base.PostUpdate();
        }
    }
}
