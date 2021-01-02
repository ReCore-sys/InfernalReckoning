using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace InfernalReckoning.NPCs.Savitar
{
    public class Savitar : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Savitar");
            Main.npcFrameCount[npc.type] = 1;
        }


        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 40000;
            npc.damage = 100;
            npc.defense = 55;
            npc.knockBackResist = 0f;
            npc.width = 100;
            npc.height = 100;
            npc.value = Item.buyPrice(0, 20, 0, 0);
            npc.npcSlots = 15f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.buffImmune[24] = true;
            music = MusicID.Boss2;
        }



        public const int RingRadius = 300;
        public const int RingDustQty = 400;
        public int damage = 30;
        public int switchTime = 150;
        public int moveCount = -1;
        public int fireCount = 0;
        public int attackType = 1;
        public int AI_Timer = 0;
        public int AI_Timer2 = 0;
        public bool runOnce = true;
        private Vector2 moveTo;
        private float orbSpeed = 12;
        private bool angry;
        private bool justTeleported;
        private int missileReloadCounter;
        private int missileFrame = 0;
        private int missileFlashCounter;
        public int demoncount;
        public int Timer2;
        public int Timer;
        public int distance;
        public int timer;
        public override void AI()
        {
            
            timer++;
            if (timer == 120)
            {
                Player player = Main.player[npc.target];
                Vector2 position = npc.Center;
                moveTo = new Vector2(player.Center.X + (float)Math.Cos(npc.ai[0]) * 700, player.Center.Y + (float)Math.Sin(npc.ai[0]) * 400);
                if (Main.netMode != 1)
                {
                    npc.ai[0] = Main.rand.NextFloat(-(float)Math.PI, (float)Math.PI);
                    npc.netUpdate = true;
                }
                runOnce = false;
                moveTo = new Vector2(player.Center.X + (float)Math.Cos(npc.ai[0]) * 700, player.Center.Y + (float)Math.Sin(npc.ai[0]) * 400);
                /*if (Vector2.DistanceSquared(npc.Center, target) > Math.Pow(10, 2))
                {
                    Vector2 direction = Main.player[npc.target].Center - npc.Center;
                    direction.Normalize();
                    npc.velocity *= 0.985f;
                    int dust2 = Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, 206, npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
                    Main.dust[dust2].noGravity = true;
                    if (Math.Sqrt((npc.velocity.X * npc.velocity.X) + (npc.velocity.Y * npc.velocity.Y)) >= 7f)
                    {
                        int dust = Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, 206, npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].scale = 2f;
                        dust = Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, 206, npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].scale = 2f;
                    }
                    if (Math.Sqrt((npc.velocity.X * npc.velocity.X) + (npc.velocity.Y * npc.velocity.Y)) < 13f)
                    {
                        if (Main.rand.Next(18) == 1)
                        {
                            direction.X = direction.X * Main.rand.Next(21, 27);
                            direction.Y = direction.Y * Main.rand.Next(21, 27);
                            npc.velocity.X = direction.X * 5;
                            npc.velocity.Y = direction.Y * 5;

                        }
                    }
                }*/
            }
            
        }
    }
}