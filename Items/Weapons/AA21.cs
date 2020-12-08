using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace InfernalReckoning.Items.Weapons
{
    public class AA21 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("AA21");
            Tooltip.SetDefault("You mixed a minigun and a shotgun. You sure this is safe?");
        }

        public override void SetDefaults()
        {
            item.ranged = true;
            item.noMelee = true;
            item.damage = 20;
            item.knockBack = 4;
            item.shootSpeed = 0f;
            item.autoReuse = true;

            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.UseSound = SoundID.Item38;
            item.width = 60;
            item.height = 30;
            item.scale = 0.9f;
            item.rare = ItemRarityID.Green;
            item.value = 10000;

            item.useAmmo = AmmoID.Bullet;
            item.shoot = ProjectileID.Bullet; //idk why but all the guns in the vanilla source have this
        }
       
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
      {
          int numberProjectiles = 10 + Main.rand.Next(5); // 4 or 5 shots
          for (int i = 0; i < numberProjectiles; i++)
          {
              Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10)); // 30 degree spread.
                                                                                                              // If you want to randomize the speed to stagger the projectiles
                                                                                                              //float scale = (Main.rand.NextFloat() * 1.1f);
                float scale = Main.rand.NextFloat(2.0f, 2.3f);
              perturbedSpeed = perturbedSpeed * scale;
              Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
              
            }
          return false; // return false because we don't want tmodloader to shoot projectil
        }
        /*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            return true;
        }*/
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<spirit>(), 50);
            recipe.AddTile(ModContent.TileType<Tiles.Altar>());
            recipe.AddIngredient(ItemID.Minishark);
            recipe.AddIngredient(ItemID.Shotgun);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(5, 0);
        }
    }
}