using Terraria.ID;
using Terraria.ModLoader;

namespace InfernalReckoning.Items.Placeable
{
    public class AltarItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Needed to create many magical artifacts");
        }

        public override void SetDefaults()
        {
            item.width = 64;
            item.height = 32;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.value = 150;
            item.createTile = ModContent.TileType<Tiles.Altar>();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.WorkBench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}