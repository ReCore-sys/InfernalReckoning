using Terraria.ModLoader;

namespace InfernalReckoning.Projectiles
{
    public class BloodbladeP : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Demonic blood blade");
        }

        public override void SetDefaults()
        {
            //projectile.CloneDefaults(ProjectileID.DemonSickle);
            projectile.arrow = true;
            projectile.width = 43;
            projectile.height = 43;
            projectile.aiStyle = 18;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.damage = 20;
            //aiType = ProjectileID.DemonSickle;
        }

        // Additional hooks/methods here.
    }
}