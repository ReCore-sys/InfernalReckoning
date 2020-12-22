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
            Tooltip.SetDefault("A staff that shoots everything\n[c/ff0000:Uses 5-8 spirit]");
        }
        public override void SetDefaults()
        {
            item.magic = true;
            item.mana = 10;
            item.damage = 70;
            item.knockBack = 4;
            item.shootSpeed = 30f;
            item.autoReuse = true;
            Item.sellPrice(0, 2, 50, 0);
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.width = 40;
            item.height = 20;
            item.scale = 0.9f;
            item.rare = ItemRarityID.Green;
            item.value = 10000;
            item.maxStack = 1;
            item.shoot = ProjectileID.RocketSnowmanI;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.GetModPlayer<MyPlayer>().Spirit >= 16)
            {
                // Here we randomly set type to either the original (as defined by the ammo), a vanilla projectile, or a mod projectile.
                int[] projectileArray = { type, ProjectileID.IceBolt, ProjectileID.CursedFlameFriendly, ProjectileID.InfernoFriendlyBolt, ProjectileID.IceBoomerang, ProjectileID.Stynger, ProjectileID.DeathSickle, ProjectileID.NorthPoleSpear };
                type = projectileArray[Main.rand.Next(projectileArray.Length)];
                float numberProjectiles = 5 + Main.rand.Next(3); // 3, 4, or 5 shots
                float rotation = MathHelper.ToRadians(30);
                position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
                    player.GetModPlayer<MyPlayer>().Spirit -= 2;
                }
                return false;
            }
            return true;
        }
        public override void AddRecipes()
        {
            SpiritRecipe recipe = new SpiritRecipe(mod, NPCID.Guide, 150);
            recipe.AddIngredient(null, "Veerium", 8);
            recipe.AddIngredient(ItemID.VenomStaff, 1);
            recipe.AddTile(ModContent.TileType<Tiles.Altar>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    
}