using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.UI.Chat;
using Terraria.Localization;

namespace SlowlyLoseMaxHP
{
    internal class SLMHUIState : UIState
    {
        public override void Draw(SpriteBatch spriteBatch)
        {
            ChatManager.DrawColorCodedString(spriteBatch: spriteBatch, FontAssets.MouseText.Value, Language.GetTextValue("LegacyInterface.28"),
                new Vector2 (Main.screenWidth/2, Main.screenHeight/2), new Color(228, 206, 114, Main.mouseTextColor / 2),
                0f, new Vector2(1, 1), new Vector2(1, 1));

            base.Draw(spriteBatch);
        }
    }
}
