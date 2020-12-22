using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace InfernalReckoning.Items.Weapons
{
    public class BoltRifle : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boltor");
            Tooltip.SetDefault("For the emperor!");
        }

        public override void SetDefaults()
        {
            item.ranged = true;
            item.noMelee = true;
            item.damage = 140;
            item.knockBack = 4;
            item.shootSpeed = 0f;
            item.autoReuse = true;

            item.useTime = 11;
            item.useAnimation = 11;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/BolterShoot");
            item.width = 40;
            item.height = 20;
            item.scale = 0.9f;
            item.rare = ItemRarityID.Green;
            item.value = 10000;

            item.useAmmo = mod.ItemType("Bolt");
            item.shoot = ProjectileID.Bullet; //idk why but all the guns in the vanilla source have this
        }
        public override void AddRecipes()
        {
            SpiritRecipe recipe = new SpiritRecipe(mod, NPCID.Guide, 400);
            recipe.AddIngredient(ItemID.Stynger, 1);
            recipe.AddIngredient(ItemID.HallowedBar, 10);
            recipe.AddTile(ModContent.TileType<Tiles.Altar>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(5, 0);
        }
    }
}