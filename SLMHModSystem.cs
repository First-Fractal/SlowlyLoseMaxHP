using System.Collections.Generic;
using Terraria.UI;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace SlowlyLoseMaxHP
{
    //make it only load in on the client side
    [Autoload(Side = ModSide.Client)]
    internal class SLMHModSystem : ModSystem
    {
        internal UserInterface nurseInterface;
        internal SLMHUIState nurseUI;
        public override void Load()
        {
            //load in the interface for the nurse shop
            nurseInterface = new UserInterface();
            nurseUI = new SLMHUIState();
            nurseInterface.SetState(nurseUI);
        }

        //update the UI tick counter on every tick
        public override void UpdateUI(GameTime gameTime)
        {
            // if the player is talking to the Angler and the new shop isn't opened
            if (Main.LocalPlayer.talkNPC != -1 && Main.npc[Main.LocalPlayer.talkNPC].type == NPCID.Nurse && Main.npcShop != 99)
            {
                nurseInterface.Update(gameTime);
            }
        }


        //load in the UI
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (resourceBarIndex != -1)
            {
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "SlowlyLoseMaxHP: Nurse Shop",
                    delegate {
                        if (Main.LocalPlayer.talkNPC != -1 && Main.npc[Main.LocalPlayer.talkNPC].type == NPCID.Nurse && Main.npcShop != 99)
                            nurseInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }

            base.ModifyInterfaceLayers(layers);
        }
    }
}
