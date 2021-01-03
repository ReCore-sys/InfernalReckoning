using System;
using InfernalReckoning.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InfernalReckoning.NPCs.Eligos
{
    public class Eligos : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eligos");
        }

        public override void NPCLoot()
        {
            Item.NewItem(npc.getRect(), mod.ItemType("Veerium"), Main.rand.Next(8, 15));
            Item.NewItem(npc.getRect(), ItemID.GreaterHealingPotion, Main.rand.Next(5, 10));
            Item.NewItem(npc.getRect(), ItemID.GreaterManaPotion, Main.rand.Next(5, 10));
        }

        public override void SetDefaults()
        {
            npc.width = 100;
            npc.height = 150;
            npc.damage = 140;
            npc.defense = 14;
            npc.boss = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.value = 60f;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            //aiType = 10;
            animationType = -1;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.lifeMax = 30000;
            npc.buffImmune[20] = true;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.HealingPotion;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int) (npc.lifeMax * 0.6f * bossLifeScale);
            npc.damage = (int) (npc.damage * .6f);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return 0f;
        }

        public static Vector2 PolarVector(float radius, float theta)
        {
            return new Vector2((float) Math.Cos(theta), (float) Math.Sin(theta)) * radius;
        }

        public static float AngularDifference(float angle1, float angle2)
        {
            angle1 = PolarVector(1f, angle1).ToRotation();
            angle2 = PolarVector(1f, angle2).ToRotation();
            if (Math.Abs(angle1 - angle2) > Math.PI)
            {
                return (float) Math.PI * 2 - Math.Abs(angle1 - angle2);
            }

            return Math.Abs(angle1 - angle2);
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Vector2 pos = npc.Center + PolarVector(98, npc.rotation) +
                              PolarVector(120, npc.rotation + (float) Math.PI / 2);


                pos = npc.Center + PolarVector(98, npc.rotation) + PolarVector(120, npc.rotation + (float) Math.PI / 2);


                pos = npc.Center + PolarVector(144, npc.rotation) + PolarVector(67, npc.rotation + (float) Math.PI / 2);

                pos = npc.Center + PolarVector(144, npc.rotation) +
                      PolarVector(-67, npc.rotation + (float) Math.PI / 2);


                pos = npc.Center + PolarVector(-15, npc.rotation) +
                      PolarVector(102, npc.rotation + (float) Math.PI / 2);

                pos = npc.Center + PolarVector(-15, npc.rotation) +
                      PolarVector(-102, npc.rotation + (float) Math.PI / 2);


                pos = npc.Center + PolarVector(-15, npc.rotation) + PolarVector(0, npc.rotation + (float) Math.PI / 2);

                pos = npc.Center + PolarVector(-15, npc.rotation) + PolarVector(0, npc.rotation + (float) Math.PI / 2);

                pos = npc.Center + PolarVector(-154, npc.rotation) + PolarVector(0, npc.rotation + (float) Math.PI / 2);

                pos = npc.Center + PolarVector(77, npc.rotation) + PolarVector(0, npc.rotation + (float) Math.PI / 2);

                pos = npc.Center + PolarVector(166, npc.rotation) + PolarVector(0, npc.rotation + (float) Math.PI / 2);


                pos = npc.Center + PolarVector(-65, npc.rotation) + PolarVector(79, npc.rotation + (float) Math.PI / 2);

                pos = npc.Center + PolarVector(-65, npc.rotation) +
                      PolarVector(-79, npc.rotation + (float) Math.PI / 2);
            }
        }

        private Vector2 MissileOffset = new Vector2();
        private const int defaultFrameX = 22;
        private const int defaultFrameY = 148;


        public const int RingRadius = 300;
        public const int RingDustQty = 400;
        public int damage = 30;
        public int switchTime = 150;
        public int moveCount = -1;
        public int fireCount = 0;
        public int attackType = 1;
        public int AI_Timer;
        public int AI_Timer2;
        public bool runOnce = true;
        private Vector2 moveTo;
        private float orbSpeed = 12;
        private bool angry;
        private bool justTeleported;
        private int missileReloadCounter;
        private int missileFrame = 0;
        private int missileFlashCounter;
        private int missileGlowFrame;
        private float angle = (float) Math.PI / 6;
        public int demoncount;
        public int Timer2;
        public int Timer;

        public override void AI()
        {
            if (npc.life < 1000 && demoncount < 15)
            {
                Random rnd = new Random();
                int distance = rnd.Next(-281, 281);
                int distance2 = rnd.Next(-281, 281);
                NPC.NewNPC((int) npc.Center.X + distance2, (int) npc.Center.Y + distance, NPCID.Demon);
                demoncount++;
            }

            Timer2++;
            if (Timer2 > 40)
            {
                npc.TargetClosest();
                if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 position = npc.Center;
                    Vector2 targetPosition = Main.player[npc.target].Center;
                    Vector2 direction = targetPosition - position;
                    direction.Normalize();
                    float speed = 10f;
                    int type = mod.ProjectileType("Bloodblade");
                    int damage = 40;
                    Projectile.NewProjectile(position, direction * speed, type, damage, 0f, Main.myPlayer);
                }

                Timer2 = 0;
            }

            Timer++;
            if (Timer > 500)
            {
                Timer2++;
                Timer++;
                if (Timer > 20)
                {
                    NPC.NewNPC((int) npc.Center.X, (int) npc.Center.Y + 64, NPCID.Demon);
                    NPC.NewNPC((int) npc.Center.X, (int) npc.Center.Y - 64, NPCID.Demon);
                    NPC.NewNPC((int) npc.Center.X, (int) npc.Center.Y, NPCID.Demon);
                    Timer = 0;
                }
            }

            missileFlashCounter++;
            if (missileFlashCounter > 60)
            {
                missileFlashCounter = 0;
            }
            else if (missileFlashCounter > 30)
            {
                missileGlowFrame = 1;
            }
            else
            {
                missileGlowFrame = 0;
            }

            if (missileReloadCounter > 0)
            {
                missileReloadCounter--;
            }

            //Main.NewText(npc.Size);
            //Main.NewText(npc.scale);
            if (npc.life < npc.lifeMax / 2 && Main.expertMode)
            {
                angry = true;
            }

            switchTime = (int) ((npc.life / (float) npc.lifeMax) * 60) + 90;
            Player player = Main.player[npc.target];
            npc.TargetClosest();
            if (runOnce)
            {
                if (Main.netMode != 1)
                {
                    npc.ai[0] = Main.rand.NextFloat(-(float) Math.PI, (float) Math.PI);
                    npc.netUpdate = true;
                }

                runOnce = false;
                moveTo = new Vector2(player.Center.X + (float) Math.Cos(npc.ai[0]) * 700,
                    player.Center.Y + (float) Math.Sin(npc.ai[0]) * 400);
            }

            AI_Timer++;
            AI_Timer2++;

            if (Main.expertMode)
            {
                #region exerpt aggression

                damage = 20;

                #endregion exerpt aggression
            }

            if (!player.active || player.dead)
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];
                if (!player.active || player.dead)
                {
                    npc.velocity = new Vector2(0f, 10f);
                    if (npc.timeLeft > 10)
                    {
                        npc.timeLeft = 10;
                    }

                    return;
                }
            }
            //float targetAngle = new Vector2(player.Center.X - npc.Center.X, player.Center.Y - npc.Center.Y).ToRotation();

            //npc.rotation = targetAngle;

            /*
            if( AI_Timer<6)
            {
            Vector2 teleTo = new Vector2(player.Center.X + 400f, player.Center.Y + -400f );
            npc.position = (teleTo);
            }
            */

            if (AI_Timer > switchTime)
            {
                moveCount++;
                //Main.NewText(moveCount);
                for (int i = 0; i < RingDustQty; i++)
                {
                    float theta = Main.rand.NextFloat(-(float) Math.PI, (float) Math.PI);

                    Dust dust = Dust.NewDustPerfect(npc.Center + PolarVector(RingRadius, theta),
                        mod.DustType("AncientGlow"), PolarVector(-RingRadius / 10, theta));
                    dust.noGravity = true;
                }

                if (Main.netMode != 1)
                {
                    npc.ai[0] = Main.rand.NextFloat(-(float) Math.PI, (float) Math.PI);
                    npc.netUpdate = true;
                }

                moveTo = new Vector2(player.Center.X + (float) Math.Cos(npc.ai[0]) * 700,
                    player.Center.Y + (float) Math.Sin(npc.ai[0]) * 400);
                if (Main.netMode != 1)
                {
                    npc.ai[2] = moveTo.X;
                    npc.ai[3] = moveTo.Y;
                    npc.netUpdate = true;
                }

                justTeleported = true;
                AI_Timer = 0;
                AI_Timer2 = 0;
            }

            if (moveCount >= 3)
            {
                #region special attacks

                npc.velocity = new Vector2(0, 0);

                if (AI_Timer == switchTime / 2)
                {
                    if (Main.netMode != 1)
                    {
                        npc.ai[1] = Main.rand.Next(3);
                        npc.netUpdate = true;
                        /*
                        NPC.NewNPC((int)(player.Center.X + 565.7f), (int)npc.Center.Y, mod.NPCType("AncientMinion"));
                        NPC.NewNPC((int)(player.Center.X + -565.7f), (int)npc.Center.Y, mod.NPCType("AncientMinion"));

                        if (Main.expertMode)
                        {
                            NPC.NewNPC((int)npc.Center.X, (int)(player.Center.Y + -565.7f), mod.NPCType("AncientMinion"));
                            NPC.NewNPC((int)npc.Center.X, (int)(player.Center.Y + 565.7f), mod.NPCType("AncientMinion"));
                        }
                        */
                    }

                    if (npc.ai[1] == 0)
                    {
                        Main.PlaySound(25, npc.position, 0);
                        for (int r = 0; r < 5; r++)
                        {
                            if (Main.netMode != 1)
                            {
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y,
                                    (float) Math.Cos((npc.rotation + r * (float) Math.PI / 8) - (float) Math.PI / 4) *
                                    orbSpeed,
                                    (float) Math.Sin((npc.rotation + r * (float) Math.PI / 8) - (float) Math.PI / 4) *
                                    orbSpeed, ModContent.ProjectileType<Bloodblade>(), damage, 3f, Main.myPlayer);
                            }
                        }
                    }

                    if (npc.ai[1] == 1)
                    {
                        Main.PlaySound(25, npc.position, 0);
                        missileReloadCounter = 60;
                        if (Main.netMode != 1)
                        {
                            Projectile.NewProjectile(
                                npc.Center + PolarVector(MissileOffset.X, npc.rotation) +
                                PolarVector(MissileOffset.Y, npc.rotation + (float) Math.PI / 2),
                                PolarVector(orbSpeed, npc.rotation + angle), mod.ProjectileType("BloodBlade"), damage,
                                3f, Main.myPlayer);
                            Projectile.NewProjectile(
                                npc.Center + PolarVector(MissileOffset.X, npc.rotation) +
                                PolarVector(-MissileOffset.Y, npc.rotation + (float) Math.PI / 2),
                                PolarVector(orbSpeed, npc.rotation - angle), mod.ProjectileType("BloodBlade"), damage,
                                3f, Main.myPlayer);
                        }
                    }

                    if (npc.ai[1] == 2)
                    {
                        if (Main.netMode != 1)
                        {
                            float d = new Vector2(player.Center.X - npc.Center.X, player.Center.Y - npc.Center.Y)
                                .ToRotation();
                            Vector2 pos = npc.Center + PolarVector(200, npc.rotation) +
                                          PolarVector(100, npc.rotation + (float) Math.PI / 2);
                            NPC.NewNPC((int) pos.X, (int) pos.Y, mod.NPCType("AncientMinion"), 0, npc.whoAmI);
                            pos = npc.Center + PolarVector(200, npc.rotation) +
                                  PolarVector(-100, npc.rotation + (float) Math.PI / 2);
                            NPC.NewNPC((int) pos.X, (int) pos.Y, mod.NPCType("AncientMinion"), 0, npc.whoAmI);
                            if (angry)
                            {
                                pos = npc.Center + PolarVector(100, npc.rotation) +
                                      PolarVector(-200, npc.rotation + (float) Math.PI / 2);
                                NPC.NewNPC((int) pos.X, (int) pos.Y, mod.NPCType("AncientMinion"), 0, npc.whoAmI);
                                pos = npc.Center + PolarVector(100, npc.rotation) +
                                      PolarVector(200, npc.rotation + (float) Math.PI / 2);
                                NPC.NewNPC((int) pos.X, (int) pos.Y, mod.NPCType("AncientMinion"), 0, npc.whoAmI);
                            }
                        }
                    }
                }

                if (AI_Timer == 3 * switchTime / 4)
                {
                    if (angry)
                    {
                        if (npc.ai[1] == 0)
                        {
                            Main.PlaySound(25, npc.position, 0);
                            for (int r = 0; r < 4; r++)
                            {
                                if (Main.netMode != 1)
                                {
                                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y,
                                        (float) Math.Cos((npc.rotation + r * (float) Math.PI / 6) -
                                                         (float) Math.PI / 4) * orbSpeed,
                                        (float) Math.Sin((npc.rotation + r * (float) Math.PI / 6) -
                                                         (float) Math.PI / 4) * orbSpeed,
                                        ModContent.ProjectileType<Bloodblade>(), damage, 3f, Main.myPlayer);
                                }
                            }
                        }

                        if (npc.ai[1] == 1)
                        {
                            Main.PlaySound(25, npc.position, 0);
                            missileReloadCounter = 60;
                            if (Main.netMode != 1)
                            {
                                Projectile.NewProjectile(
                                    npc.Center + PolarVector(MissileOffset.X, npc.rotation) +
                                    PolarVector(MissileOffset.Y, npc.rotation + (float) Math.PI / 2),
                                    PolarVector(orbSpeed, npc.rotation + angle), mod.ProjectileType("BloodBlade"),
                                    damage, 3f, Main.myPlayer);
                                Projectile.NewProjectile(
                                    npc.Center + PolarVector(MissileOffset.X, npc.rotation) +
                                    PolarVector(-MissileOffset.Y, npc.rotation + (float) Math.PI / 2),
                                    PolarVector(orbSpeed, npc.rotation - angle), mod.ProjectileType("BloodBlade"),
                                    damage, 3f, Main.myPlayer);
                            }
                        }
                    }

                    moveCount = -1;
                }

                #endregion special attacks
            }
            else
            {
                if (AI_Timer == switchTime / 2)
                {
                    Main.PlaySound(25, npc.position, 0);
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center,
                            new Vector2((float) Math.Cos((npc.rotation)), (float) Math.Sin(npc.rotation)) * orbSpeed,
                            ModContent.ProjectileType<Bloodblade>(), damage, 3f, Main.myPlayer);
                    }
                }

                if (AI_Timer == 3 * switchTime / 4 && angry)
                {
                    Main.PlaySound(25, npc.position, 0);
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center,
                            new Vector2((float) Math.Cos((npc.rotation)), (float) Math.Sin(npc.rotation)) * orbSpeed,
                            ModContent.ProjectileType<Bloodblade>(), damage, 3f, Main.myPlayer);
                    }
                }
            }

            //npc.velocity = (moveTo - npc.Center) * .02f;
            npc.Center = new Vector2(npc.ai[2], npc.ai[3]);

            if (justTeleported)
            {
                Main.PlaySound(SoundID.Item8, npc.position);
                for (int i = 0; i < RingDustQty; i++)
                {
                    float theta = Main.rand.NextFloat(-(float) Math.PI, (float) Math.PI);
                    Dust dust = Dust.NewDustPerfect(npc.Center, DustID.Blood, PolarVector(RingRadius / 10, theta));
                    dust.noGravity = true;
                }

                justTeleported = false;
            }
        }
    }
}