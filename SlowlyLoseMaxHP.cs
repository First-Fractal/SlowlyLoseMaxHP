using System.IO;
using Terraria;
using Terraria.ModLoader;

namespace SlowlyLoseMaxHP
{
	public class SlowlyLoseMaxHP : Mod
	{
        //tell the mod what to do when it reccive a packet
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            SLMHPlayer SLMHP = Main.CurrentPlayer.GetModPlayer<SLMHPlayer>();

            SLMHP.cooldownMax = reader.ReadInt32();
            SLMHP.cooldown = reader.ReadInt32();
        }
    }
}