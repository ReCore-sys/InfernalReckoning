using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InfernalReckoning.Items.Weapons.Eligos
{
    //Here's the item where we will add our recipe
    public class Attarax : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Attarax");
            Tooltip.SetDefault("This doesn't feel quite right...\n[c/ff0000:Uses 10 spirit]");
        }


        public override void SetDefaults()
        {
            item.melee = true;
            item.damage = 70;
            item.knockBack = 4;
            item.shootSpeed = 0f;
            item.autoReuse = true;
            Item.sellPrice(0, 2, 50, 0);
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.width = 40;
            item.height = 20;
            item.scale = 0.5f;
            item.rare = 7;
            item.value = 10000;
            item.maxStack = 1;
            item.shoot = mod.ProjectileType("BloodbladeP");
            item.shootSpeed = 9;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.GetModPlayer<MyPlayer>().Spirit >= 10)
            {
                int numberProjectiles = 1 + Main.rand.Next(6);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
                    float scale = 1f - (Main.rand.NextFloat() * .3f);
                    perturbedSpeed = perturbedSpeed * scale;
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
                }
                player.GetModPlayer<MyPlayer>().Spirit -= 10;
            }

            return false; // return false because we don't want tmodloader to shoot projectile
        }

        //Using our custom recipe type
        public override void AddRecipes()
        {
            SpiritRecipe recipe = new SpiritRecipe(mod, NPCID.Guide, 200);
            recipe.AddIngredient(null, "Veerium", 10);
            recipe.AddIngredient(ItemID.ChlorophyteClaymore, 1);
            recipe.AddTile(ModContent.TileType<Tiles.Altar>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}