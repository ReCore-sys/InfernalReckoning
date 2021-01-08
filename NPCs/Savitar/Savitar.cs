using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InfernalReckoning.NPCs.Savitar
{
    public class Savitar : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Savitar");
        }

        public override void SetDefaults()
        {
            npc.width = 32;
            npc.height = 32;
            npc.aiStyle = -1;
            npc.damage = 7;
            npc.defense = 2;
            npc.lifeMax = 250;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 25f;
            npc.noGravity = true;
        }

        public int timer;

        public override void AI()
        {
            if (npc.ai[0] <= 0f) //Checks whether the NPC is ready to start another charge.
            {
                Player player = Main.player[npc.target];
                Vector2 moveTo = player.Center; //This player is the same that was retrieved in the targeting section.
                float speed = 10f; //Charging is fast.
                Vector2 move = moveTo - npc.Center;
                float magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
                move *= speed / magnitude;
                npc.velocity = move;
                npc.ai[0] = 60f;//There are 60 ticks in one second, so this will make the NPC charge for 3 and 1/3 seconds before changing directions.
            }
            npc.ai[0] -= 1f; //So you can keep track of how long the NPC has been charging.
        }
    }
}