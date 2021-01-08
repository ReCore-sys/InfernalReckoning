using Terraria;
using Terraria.ModLoader;

namespace InfernalReckoning.Items.Weapons
{
    public class SpiritRecipe : ModRecipe //Renamed to SpiritRecipe, to be more general in case it's used as a separate file.
    {
        private int spiritReq; //Is given a value in constructor.

        public SpiritRecipe(Mod mod, short guide, int s) : base(mod)
        {
            spiritReq = s;
        }

        public override bool RecipeAvailable()
        {
            Player player = Main.LocalPlayer;
            if (player.GetModPlayer<MyPlayer>().Spirit >= spiritReq)
                return true;
            else
                return false;
        }

        public override void OnCraft(Item item)
        {
            Player player = Main.LocalPlayer; //Should be same Player variable as RecipeAvailable() hook.
            player.GetModPlayer<MyPlayer>().Spirit -= spiritReq; //Went ahead and added this. Removes 0 spirit if spiritReq is 0.
            //Might just be entirely worth making most/all recipes into SpiritRecipes.
        }
    }
}