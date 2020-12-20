using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InfernalReckoning.Items.Weapons
{
    public class Vorax : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vorax");
            Tooltip.SetDefault("I can feel the dark magic...\n[c/ff0000:Uses 10 spirit]");
        }


        public override void SetDefaults()
        {
            item.melee = true;
            item.damage = 70;
            item.knockBack = 4;
            item.shootSpeed = 0f;
            item.autoReuse = false;
            Item.sellPrice(0, 2, 50, 0);
            item.useTime = 30;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.width = 40;
            item.height = 20;
            item.scale = 0.9f;
            item.rare = ItemRarityID.Green;
            item.value = 10000;
            item.maxStack = 1;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.GetModPlayer<MyPlayer>().Spirit >= 10)
            {
                NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, ModContent.NPCType<NPCs.Skel>());
                player.GetModPlayer<MyPlayer>().Spirit -= 10;
            }
            return false;
        }
        public override void AddRecipes()
        {
            SpiritRecipe recipe = new SpiritRecipe(mod, NPCID.Guide, 20);
            //recipe.AddIngredient(ItemID.DirtBlock, 5);
            recipe.AddTile(ModContent.TileType<Tiles.Altar>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    
}