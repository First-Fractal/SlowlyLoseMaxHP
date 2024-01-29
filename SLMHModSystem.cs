using System;
using System.Collections.Generic;
using Terraria.UI;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace SlowlyLoseMaxHP
{
    internal class SLMHModSystem : ModSystem
    {
        internal UserInterface nurseInterface;
        internal SLMHUIState nurseUI;
        private GameTime lastGameTime;
        public override void Load()
        {
            if (!Main.dedServ)
            {
                nurseInterface = new UserInterface();
                nurseUI = new SLMHUIState();
                nurseUI.Activate();
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            lastGameTime = gameTime;
            if (nurseInterface?.CurrentState != null)
            {
                nurseInterface.Update(gameTime);
            }

            nurseInterface?.SetState(nurseUI);
            base.UpdateUI(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "Boss Adrenaline Counter",
                    delegate
                    {
                        if (lastGameTime != null && nurseInterface?.CurrentState != null)
                        {
                            nurseInterface.Draw(Main.spriteBatch, lastGameTime);
                        }
                        return true;
                    },
                    InterfaceScaleType.UI));
            }
            base.ModifyInterfaceLayers(layers);
        }
    }
}
