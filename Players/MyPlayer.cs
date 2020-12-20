using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace InfernalReckoning
{
    public class MyPlayer : ModPlayer
    {
        public static MyPlayer ModPlayer(Player player)
        {
            return player.GetModPlayer<MyPlayer>();
        }
        public int Spirit;
        public int PreSpirit;
        public int SpiritMax = 1000;

        public override TagCompound Save()
        {
            return new TagCompound {
                {"SpiritInt", Spirit},
                //{"SpiritMax", SpiritMax},
            };
        }
        public override void Load(TagCompound tag)
        {
            Spirit = tag.GetInt("SpiritInt");
            //SpiritMax = tag.GetInt("SpiritMax");
        }
    }
};
    