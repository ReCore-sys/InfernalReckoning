using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InfernalReckoning.Projectiles
{
    internal class SpiritProjectile : ModProjectile
    {
        private float a1 = 0f;
        private float a2 = 0f;
        private float a3 = 0f;
        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.aiStyle = 0;
            projectile.alpha = 255;
            projectile.tileCollide = false;
            projectile.extraUpdates = 10;
            projectile.timeLeft = 3600;
        }
		public override void AI()
		{
			Vector2 vector22 = new Vector2(projectile.Center.X, projectile.Center.Y);
			float DistanceX = Main.player[projectile.owner].Center.X - vector22.X;
			float DistanceY = Main.player[projectile.owner].Center.Y - vector22.Y;
			float Distance = (float)Math.Sqrt(DistanceX * DistanceX + DistanceY * DistanceY);
			if (Distance < 50f && projectile.position.X < Main.player[projectile.owner].position.X + (float)Main.player[projectile.owner].width && projectile.position.X + (float)projectile.width > Main.player[projectile.owner].position.X && projectile.position.Y < Main.player[projectile.owner].position.Y + (float)Main.player[projectile.owner].height && projectile.position.Y + (float)projectile.height > Main.player[projectile.owner].position.Y)
			{
				if (projectile.owner == Main.myPlayer)
				{
					projectile.Kill();
					Main.PlaySound(SoundID.NPCHit36.WithVolume(0.1f).WithPitchVariance(0.5f), projectile.Center);
					AddSpirit();
				}
			}
			if (Distance < 240f)
			{
				Distance = 4f / Distance;
				DistanceX *= Distance;
				DistanceY *= Distance;
				projectile.velocity.X = (projectile.velocity.X * 15f + DistanceX) / 16f;
				projectile.velocity.Y = (projectile.velocity.Y * 15f + DistanceY) / 16f;
			}

			float animSpeed = 0.01f;
			a1 += 1f * animSpeed;
			if (a1 >= 360f) { a1 -= 360f; }
			a2 += 1.5f * animSpeed;
			if (a2 >= 360f) { a2 -= 360f; }
			a3 += 2f * animSpeed;
			if (a3 >= 360f) { a3 -= 360f; }

			for (int num = 0; num < 3; num++)
			{
				float numX = projectile.velocity.X * 0.334f * (float)num;
				float numY = (0f - projectile.velocity.Y * 0.334f) * (float)num;
				float PosX = projectile.position.X + ((float)Math.Sin(a1) * 0.5f + (float)Math.Sin(360 - a2) * 0.3f + (float)Math.Sin(a3) * 0.2f) * 16f;
				float PosY = projectile.position.Y + ((float)Math.Cos(a1) * 0.5f + (float)Math.Cos(360 - a2) * 0.3f + (float)Math.Cos(a3) * 0.2f) * 16f;
				int DustId = Dust.NewDust(new Vector2(PosX, PosY), projectile.width, projectile.height, 182, 0f, 0f, 100, default(Color), 1.1f);
				Main.dust[DustId].noGravity = true;
				Dust dust = Main.dust[DustId];
				dust.velocity *= 0f;
				Main.dust[DustId].position.X -= numX;
				Main.dust[DustId].position.Y -= numY;
			}
		}
		public void AddSpirit()
		{
			Player player = Main.player[projectile.owner];
			player.GetModPlayer<MyPlayer>().Spirit = Math.Min(player.GetModPlayer<MyPlayer>().Spirit + (int)projectile.ai[0], player.GetModPlayer<MyPlayer>().SpiritMax);
		}
	}
}
