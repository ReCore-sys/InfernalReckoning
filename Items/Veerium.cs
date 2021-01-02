<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace InfernalReckoning.Items
{
    public class Veerium : ModItem
    {
        public override void SetStaticDefaults()
        {
<<<<<<< Updated upstream
			DisplayName.SetDefault("Veerium bar");
			Tooltip.SetDefault("It pulses with unholy energy");
=======
            Tooltip.SetDefault("It pulses with unholy energy...");
>>>>>>> Stashed changes
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = 0;
            item.rare = ItemRarityID.Blue;
        }
    }
}

