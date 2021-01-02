using InfernalReckoning.Items.Weapons;
using Terraria.ID;
using Terraria.ModLoader;

namespace InfernalReckoning.Items.Ammo
{
    public class Bolt : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boltor Round");
            Tooltip.SetDefault("For when the anti-tank gun just isn't cutting it");
        }
        public override void SetDefaults()
        {
            item.damage = 10; //This is added with the weapon's damage
            item.ranged = true;
            item.width = 14;
            item.height = 32;
            item.maxStack = 999;
            item.consumable = true; //Tells the game that this should be used up once fired
            item.knockBack = 1f; //Added with the weapon's knockback
            item.value = 500;
            item.rare = 2;
            item.shoot = mod.ProjectileType("Bolt");
            item.shootSpeed = 7f; //Added to the weapon's shoot speed
            item.ammo = mod.ItemType("Bolt"); //Tells game that the type of ammo is of ExampleBulletA
        }

        public override void AddRecipes()
        {
            SpiritRecipe recipe = new SpiritRecipe(mod, NPCID.Guide, 20);
            recipe.AddIngredient(ItemID.StyngerBolt, 10);
            recipe.SetResult(this, 10);
            recipe.AddRecipe();
        }
    }

}