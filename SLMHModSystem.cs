using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace SlowlyLoseMaxHP
{
    internal class SLMHModSystem : ModSystem
    {
        //vars for counting down the cooldown for losing max hp
        public static int cooldownMax = ffFunc.TimeToTick(0, 5);
        public static int cooldown = cooldownMax;
        public static bool bossAlive = false;

        public override void PostUpdateWorld()
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
                    Main.NewText(message, Color.Orange);
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
                    Main.NewText(message, Color.Orange);
                    bossAlive = false;
                }
            }

            //count down the cooldown when there is an active player and reset it when it hit 0
            if (ffFunc.IsPlayerInWorld() && cooldown > 0)
            {
                cooldown--;
            } else
            {
                cooldown = cooldownMax;
            }

            //clamp the cooldown to be inside 0 and max cooldown
            cooldown = Math.Clamp(cooldown, 0, cooldownMax);

            //send the cooldown info from here to all of the player(s)
            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                ModPacket packet = ModContent.GetInstance<SlowlyLoseMaxHP>().GetPacket();
                packet.Write(cooldownMax);
                packet.Write(cooldown);
                packet.Send();
            } else
            {
                SLMHPlayer player = Main.LocalPlayer.GetModPlayer<SLMHPlayer>();
                player.cooldownMax = cooldownMax;
                player.cooldown = cooldown;
            }

            base.PostUpdateWorld();
        }
    }
}
