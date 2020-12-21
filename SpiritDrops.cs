using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace InfernalReckoning
{
    public class SpiritDrops : ModPlayer
    {
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            NPCHit(target);
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            NPCHit(target);
        }

        public void NPCHit(NPC target)
        {
            if (target.life <= 0 && target.lifeMax > 50 && player.GetModPlayer<MyPlayer>().Spirit < player.GetModPlayer<MyPlayer>().SpiritMax)
            {
                for (int num = 0; num < 20; num++)
                {
                    int DustId = Dust.NewDust(new Vector2(target.Center.X, target.Center.Y), 2, 2, 182, 0f, 0f, 100, default(Color), 2f);
                    Main.dust[DustId].noGravity = true;
                    Dust dust = Main.dust[DustId];
                    dust.velocity = Vector2.Normalize(dust.velocity) * 2f;
                }
                float spiritAmount = (float)Math.Round((25f / 100f) * (float)target.lifeMax);
                Projectile.NewProjectile(new Vector2(target.Center.X, target.Center.Y), new Vector2(0f, 0f), mod.ProjectileType("SpiritProjectile"), 0, 0f, Main.myPlayer, spiritAmount, 0);
            }
        }
    }
}
