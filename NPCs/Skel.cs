using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace InfernalReckoning.NPCs
{
	class Skel : ModNPC
	{
		//public override string Texture => "Terraria/NPC_" + NPCID.SkeletonArcher;

		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.SkeletonArcher);
			npc.friendly = true;
		}
	}
}