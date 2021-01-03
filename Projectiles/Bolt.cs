using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InfernalReckoning.Projectiles
{
    public class Bolt : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.Name = "The Boltor"; //Name of the projectile, only shows this if you get killed by it
            projectile.width = 14; //Set the hitbox width
            projectile.height = 14; //Set the hitbox height
            drawOffsetX = -23;
            drawOriginOffsetY = -1;
            drawOriginOffsetX = 11;
            projectile.scale = 1f;

            projectile.timeLeft = 300; //The amount of time the projectile is alive for
            projectile.penetrate = 1; //Tells the game how many enemies it can hit before being destroyed
            projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.tileCollide = true; //Tells the game whether or not it can collide with a tile
            projectile.ignoreWater = true; //Tells the game whether or not projectile will be affected by water
            projectile.ranged = true; //Tells the game whether it is a ranged projectile or not
            projectile.aiStyle = 0; //How the projectile works, this is no AI, it just goes a straight path
            projectile.extraUpdates = 4;
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation();
            Lighting.AddLight(projectile.position, 0.5f, 0.25f, 0.125f);
        }

        /*public override Color? GetAlpha(Color lightColor)
        {
            return Color.Lerp(lightColor, new Color(1f, 1f, 1f), 0.5f);
        }*/
        public override bool PreKill(int timeLeft)
        {
            projectile.tileCollide = false;
            projectile.alpha = 255;
            projectile.position = projectile.Center;
            projectile.width = 64;
            projectile.height = 64;
            projectile.Center = projectile.position;

            projectile.knockBack = 4f;
            projectile.damage = 2000;
            projectile.penetrate = -1;
            return true;
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.NPCDeath14.WithVolume(.3f).WithPitchVariance(.5f),
                projectile.position); //plays impact sound
            // Smoke Dust spawn
            for (int i = 0; i < 50; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y),
                    projectile.width, projectile.height, 31, 0f, 0f, 100);
                Main.dust[dustIndex].velocity *= 1.4f;
            }

            // Fire Dust spawn
            for (int i = 0; i < 80; i++)
            {
                int dustIndex = Dust.NewDust(
                    Vector2.Lerp(new Vector2(projectile.position.X, projectile.position.Y),
                        new Vector2(projectile.Center.X, projectile.Center.Y), 0.5f), projectile.width / 2,
                    projectile.height / 2, 6, 0f, 0f, 50, default(Color), 1.5f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 5f;
            }

            // Large Smoke Gore spawn
            for (int g = 0; g < 2; g++)
            {
                int goreIndex =
                    Gore.NewGore(
                        new Vector2(projectile.position.X + projectile.width / 2 - 24f,
                            projectile.position.Y + projectile.height / 2 - 24f), default(Vector2),
                        Main.rand.Next(61, 64), 0.25f);
                Main.gore[goreIndex].scale = 0.2f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                goreIndex = Gore.NewGore(
                    new Vector2(projectile.position.X + projectile.width / 2 - 24f,
                        projectile.position.Y + projectile.height / 2 - 24f), default(Vector2), Main.rand.Next(61, 64),
                    0.25f);
                Main.gore[goreIndex].scale = 0.2f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                goreIndex = Gore.NewGore(
                    new Vector2(projectile.position.X + projectile.width / 2 - 24f,
                        projectile.position.Y + projectile.height / 2 - 24f), default(Vector2), Main.rand.Next(61, 64),
                    0.25f);
                Main.gore[goreIndex].scale = 0.2f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                goreIndex = Gore.NewGore(
                    new Vector2(projectile.position.X + projectile.width / 2 - 24f,
                        projectile.position.Y + projectile.height / 2 - 24f), default(Vector2), Main.rand.Next(61, 64),
                    0.25f);
                Main.gore[goreIndex].scale = 0.2f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
            }
        }
    }
}