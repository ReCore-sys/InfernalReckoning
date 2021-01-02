using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InfernalReckoning.Items.Weapons.Eligos
{
    public class Rubico : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rubico");
            Tooltip.SetDefault("Where do they come from?");
        }

        public override void SetDefaults()
        {
            item.damage = 30;
            item.width = 46;
            item.height = 46;
            item.useTime = 4;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 1f;
            item.value = 500000;
            item.rare = 7;
            item.UseSound = SoundID.Item96;
            item.autoReuse = true;
            item.shoot = ProjectileID.Bullet;
            item.magic = true;
            item.shootSpeed = 12;
            item.useAmmo = AmmoID.Bullet;
        }

        
        public static float AngularDifference(float angle1, float angle2)
        {
            angle1 = PolarVector(1f, angle1).ToRotation();
            angle2 = PolarVector(1f, angle2).ToRotation();
            if (Math.Abs(angle1 - angle2) > Math.PI)
            {
                return (float)Math.PI * 2 - Math.Abs(angle1 - angle2);
            }
            return Math.Abs(angle1 - angle2);
        }
        public static Vector2 PolarVector(float radius, float theta)
        {
            return new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta)) * radius;
        }
        public override bool ConsumeAmmo(Player player)
        {
            return Main.rand.NextFloat() >= .60f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.GetModPlayer<MyPlayer>().Spirit >= 10)
            {
                if (Main.rand.NextFloat() >= .60f)
                {
                    player.GetModPlayer<MyPlayer>().Spirit -= 1;
                }
                for (int i = 0; i < 1; i++)
                {
                    float trueSpeed = new Vector2(speedX, speedY).Length();
                    float rot = new Vector2(speedX, speedY).ToRotation();
                    Vector2 Rposition = position + PolarVector(-1200, rot + Main.rand.NextFloat(-(float)Math.PI / 32, (float)Math.PI / 32));
                    Vector2 goHere = Main.MouseWorld + PolarVector(Main.rand.NextFloat(-40, 40), rot + (float)Math.PI / 2);
                    Vector2 diff = goHere - Rposition;
                    float dist = diff.Length();
                    int proj = Projectile.NewProjectile(Rposition, diff.SafeNormalize(Vector2.UnitY) * trueSpeed, type, damage, knockBack, player.whoAmI); Main.projectile[proj].tileCollide = false;
                }
            }
            return false;
        }
        public override void AddRecipes()
        {
            SpiritRecipe recipe = new SpiritRecipe(mod, NPCID.Guide, 200);
            recipe.AddIngredient(null, "Veerium", 10);
            recipe.AddIngredient(ItemID.ChlorophyteShotbow, 1);
            recipe.AddTile(ModContent.TileType<Tiles.Altar>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
    
}