using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.Audio;
using Terraria.UI.Chat;
using Terraria.GameContent;
using Terraria.Localization;
using ReLogic.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SlowlyLoseMaxHP
{
    //NOTE THAT SOME PARTS OF THIS FILE WAS INSPIRED BY (AND COPY AND PASTED FROM) https://github.com/NotLe0n/AnglerShop/blob/1.4.4/src/AnglerShopUI.cs
    internal class SLMHUIState : UIState
    {
        private static object TextDisplayCache => typeof(Main).GetField("_textDisplayCache", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(Main.instance);
        bool focused = false;
        public override void Draw(SpriteBatch spriteBatch)
        {
            //get the current language version of Shop
            string text = Language.GetTextValue("LegacyInterface.28");

            //get the font that the mouse uses
            DynamicSpriteFont font = FontAssets.MouseText.Value;

            //set the scale for the text
            Vector2 scale = new Vector2(0.9f);

            

            //get the number of lines that the npc said
            int numLines = (int)TextDisplayCache.GetType().GetProperty("AmountOfLines", BindingFlags.Instance | BindingFlags.Public).GetValue(TextDisplayCache);

            //save the positions of all 4 buttons 
            float posButton1 = 180 + (Main.screenWidth - 780) / 2;
            float posButton2 = posButton1 + ChatManager.GetStringSize(FontAssets.MouseText.Value, Language.GetTextValue("LegacyInterface.64"), scale).X + 30f; // Position of the second button (Close)
            float posButton3 = posButton2 + ChatManager.GetStringSize(FontAssets.MouseText.Value, Language.GetTextValue("LegacyInterface.52"), scale).X + 30f; // Position of the third button (Happiness)
            float posButton4 = posButton3 + ChatManager.GetStringSize(FontAssets.MouseText.Value, Language.GetTextValue("UI.NPCCheckHappiness"), scale).X + 30f; // Position of the new button

            //set the position of the text
            Vector2 pos = new(posButton4, 143 + numLines * 31);

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
