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
using Terraria.GameInput;
using Terraria.ModLoader.IO;
using Terraria.Audio;
using Terraria.ID;

namespace SlowlyLoseMaxHP
{
    internal class SLMHUIState : UIState
    {
        bool focused = false;
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

            //get the text size
            Vector2 textSize = ChatManager.GetStringSize(font, text, scale);

            //set the hitbox for the text
            Vector2 hitboxPos = pos - textSize/2 * scale - new Vector2 (2, 2);
            Vector2 hitboxOffset = textSize * scale;
            hitboxOffset.X += textSize.X/7 * scale.X + 4;

            //check if the mouse is hovering above the hitbox
            if (Main.MouseScreen.Between(hitboxPos, hitboxPos + hitboxOffset))
            {
                //play the sfx once
                if (!focused)
                    SoundEngine.PlaySound(SoundID.MenuTick);

                focused = true;

                //increase the scale to help show that the text is being hovered
                scale *= 1.1f;
            } else
            {
                //play the sfx once
                if (focused)
                    SoundEngine.PlaySound(SoundID.MenuTick);

                focused = false;
            }

            // draw button text shadow
            ChatManager.DrawColorCodedStringShadow(spriteBatch: spriteBatch, font, text, pos,
                (!focused) ? Color.Black : Color.Brown, 
                0f, textSize/2, scale);

            //draw the text
            ChatManager.DrawColorCodedString(spriteBatch: spriteBatch, font, text, pos, 
                (!focused) ? new Color(228, 206, 114, Main.mouseTextColor / 2) : new Color(255, 231, 69),
                0f, textSize/2, scale);

            base.Draw(spriteBatch);
        }
    }
}
