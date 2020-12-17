using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InfernalReckoning.Projectiles
{
	public class Bloodblade : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Demonic blood blade");
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.DemonSickle);
			/*projectile.arrow = true;
			projectile.width = 43;
			projectile.height = 43;
			projectile.aiStyle = 18;
			projectile.friendly = false;
			projectile.ranged = true;
			projectile.damage = 40;*/
			aiType = ProjectileID.DemonSickle;
		}

		// Additional hooks/methods here.
	}
}