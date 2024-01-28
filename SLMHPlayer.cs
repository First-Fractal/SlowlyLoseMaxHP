using System;
using Terraria;
using Terraria.ModLoader;

namespace SlowlyLoseMaxHP
{
    internal class SLMHPlayer : ModPlayer
    {
        static int timerMax = ffFunc.TimeToTick(10);
        static int timer = timerMax;

        public override void PostUpdate()
        {
            if (timer > 0)
            {
                timer--;
                Console.WriteLine(timer);
            } else
            {
                Player.ConsumedLifeCrystals--;
                timer = timerMax;
            }
            base.PostUpdate();
        }
    }
}
