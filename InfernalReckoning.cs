using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace InfernalReckoning
{
    public class InfernalReckoning : Mod
    {
        private UserInterface _spiritUserInterface;
        private UserInterface _spiritBarUserInterface;

        internal UserInterface SpiritPersonUserInterface;
        internal UI.SpiritBar SpiritBar;
        public override void Load()
        {

            base.Load();
            SpiritBar = new UI.SpiritBar();
            _spiritBarUserInterface = new UserInterface();
            _spiritBarUserInterface.SetState(SpiritBar);
            SpiritPersonUserInterface = new UserInterface();
        }
        public override void UpdateUI(GameTime gameTime)
        {
            _spiritBarUserInterface?.Update(gameTime);
            SpiritPersonUserInterface?.Update(gameTime);
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (resourceBarIndex != -1)
            {
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "Infernal Reckoning: Spirit Bar",
                    delegate
                    {
                        _spiritBarUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
        public InfernalReckoning()
        {
            /*
            if (player.GetModPlayer<MyPlayer>().Spirit < 0)
            {
                player.GetModPlayer<MyPlayer>().Spirit = 0;
            }
            if (player.GetModPlayer<MyPlayer>().Spirit > player.GetModPlayer<MyPlayer>().SpiritMax)
            {
                player.GetModPlayer<MyPlayer>().Spirit = player.GetModPlayer<MyPlayer>().SpiritMax;
            }
            */
        }

        internal class Tiles
        {
            internal class Altar
            {
            }
        }
    }
}