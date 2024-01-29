using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.UI.Chat;
using Terraria.Localization;
using ReLogic.Graphics;

namespace SlowlyLoseMaxHP
{
    internal class SLMHUIState : UIState
    {
        public override void Draw(SpriteBatch spriteBatch)
        {
            //get the current language version of Shop
            string text = Language.GetTextValue("LegacyInterface.28");

            //get the font that the mouse uses
            DynamicSpriteFont font = FontAssets.MouseText.Value;

            //set the scale for the text
            Vector2 scale = new Vector2(0.9f);

            //set the position of the text
            Vector2 pos = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2 - 40);

            // draw button text shadow
            ChatManager.DrawColorCodedStringShadow(spriteBatch: spriteBatch, font, text,
                pos, Color.Black,
                0f, ChatManager.GetStringSize(font, text, scale) / 2, scale);

            //draw the text
            ChatManager.DrawColorCodedString(spriteBatch: spriteBatch, font, text,
                pos, new Color(228, 206, 114, Main.mouseTextColor / 2),
                0f, ChatManager.GetStringSize(font, text, scale)/2, scale);

            base.Draw(spriteBatch);
        }
    }
}
