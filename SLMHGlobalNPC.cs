using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SlowlyLoseMaxHP
{
    internal class SLMHGlobalNPC : GlobalNPC
    {
        //modify the current shop that is open
        public override void ModifyActiveShop(NPC npc, string shopName, Item[] items)
        {
            //ignore the rest if the current npc is not the nurse
            if (npc.type != NPCID.Nurse)
                return;

            //if the EoW or BoC is defeated, then sell the life crystal for 10 gold
            if (NPC.downedBoss2)
                items[0] = new Item(ItemID.LifeCrystal) { shopCustomPrice = (int)(Item.sellPrice(0, 5, 0, 0) * SLMHConfig.Instance.lifeCrystalMulti * 0.1)};

            //if Plantera is defeated, then sell the life fruit for 2.5 gold
            if (NPC.downedPlantBoss)
                items[1] = new Item(ItemID.LifeFruit) { shopCustomPrice = (int)(Item.sellPrice(0, 1, 25, 0) * SLMHConfig.Instance.lifeFruitMulti * 0.1)};
        }
    }
}
