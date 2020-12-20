using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InfernalReckoning.Items
{
    public class Chug : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("For testing purposes");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 26;
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.useAnimation = 15;
            item.useTime = 15;
            item.useTurn = true;
            item.UseSound = SoundID.Item3;
            item.maxStack = 30;
            item.consumable = false;
            item.rare = ItemRarityID.Orange;
            item.value = Item.buyPrice(gold: 1);
        }
        public override bool UseItem(Player player)
        {
            player.GetModPlayer<MyPlayer>().Spirit += 499;
            return true;
        }
    }
}