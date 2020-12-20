using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace InfernalReckoning.NPCs.Eligos
{
    // Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/tModLoader/tModLoader/wispirit/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
    public class Eligos : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eligos, The Commander");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void NPCLoot()
        {
            var dropChooser = new WeightedRandom<int>();
            dropChooser.Add(ModContent.ItemType<Items.spirit>());
            //dropChooser.Add(ModContent.ItemType<Items.Armor.BunnyMask>());
            int choice = dropChooser;
            Item.NewItem(npc.getRect(), choice);
        }
        public override void SetDefaults()
        {
            npc.width = 100;
            npc.height = 150;
            npc.lifeMax = 5000;
            npc.damage = 90;
            npc.defense = 50;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath2;
            npc.value = 60f;
            npc.knockBackResist = 0f;
            npc.aiStyle = 3;
            aiType = -1;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.boss = true;
            //banner = Item.NPCtoBanner(NPCID.Zombie);
            //bannerItem = Item.BannerToItem(banner);
        }


        public int demoncount;
        public int Timer;
        public int Timer2;
        public override void AI()
        {
            if (npc.life < 1000 && demoncount < 15)
            {
                Random rnd = new Random();
                int distance = rnd.Next(-281, 281);
                int distance2 = rnd.Next(-281, 281);
                NPC.NewNPC((int)npc.Center.X + distance2, (int)npc.Center.Y + distance, NPCID.Demon);
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
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y + 64, NPCID.Demon);
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 64, NPCID.Demon);
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.RedDevil);
                    Timer = 0;
                }
                //AI code
                if (npc.ai[0] == 0f)
                {
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        return;
                    }
                    npc.ai[0] = 1 + Main.rand.Next(2);
                    npc.netUpdate = true;
                }
                if (npc.ai[0] == 1f)
                {
                    npc.GivenName = "Eligos, The commander";
                }

                if (npc.collideX)
                {
                    npc.velocity.X = npc.oldVelocity.X * -0.5f;
                    if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < 2f)
                    {
                        npc.velocity.X = 2f;
                    }
                    if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -2f)
                    {
                        npc.velocity.X = -2f;
                    }
                }
                if (npc.collideY)
                {
                    npc.velocity.Y = npc.oldVelocity.Y * -0.5f;
                    if (npc.velocity.Y > 0f && npc.velocity.Y < 1f)
                    {
                        npc.velocity.Y = 1f;
                    }
                    if (npc.velocity.Y < 0f && npc.velocity.Y > -1f)
                    {
                        npc.velocity.Y = -1f;
                    }
                }
                if (Main.dayTime && (double)npc.position.Y <= Main.worldSurface * 16.0)
                {
                    if (npc.timeLeft > 10)
                    {
                        npc.timeLeft = 10;
                    }
                    npc.directionY = -1;
                    if (npc.velocity.Y > 0f)
                    {
                        npc.directionY = 1;
                    }
                    npc.direction = -1;
                    if (npc.velocity.X > 0f)
                    {
                        npc.direction = 1;
                    }
                }
                else
                {
                    npc.TargetClosest(true);
                }

                if (npc.ai[0] == 2f)
                {
                    if (npc.direction == -1 && npc.velocity.X > -6f)
                    {
                        npc.velocity.X -= 0.1f;
                        if (npc.velocity.X > 6f)
                        {
                            npc.velocity.X -= 0.1f;
                        }
                        else if (npc.velocity.X > 0f)
                        {
                            npc.velocity.X += 0.05f;
                        }
                        if (npc.velocity.X < -6f)
                        {
                            npc.velocity.X = -6f;
                        }
                    }
                    else if (npc.direction == 1 && npc.velocity.X < 6f)
                    {
                        npc.velocity.X += 0.1f;
                        if (npc.velocity.X < -6f)
                        {
                            npc.velocity.X += 0.1f;
                        }
                        else if (npc.velocity.X < 0f)
                        {
                            npc.velocity.X -= 0.05f;
                        }
                        if (npc.velocity.X > 6f)
                        {
                            npc.velocity.X = 6f;
                        }
                    }
                    if (npc.directionY == -1 && npc.velocity.Y > -4f)
                    {
                        npc.velocity.Y -= 0.1f;
                        if (npc.velocity.Y > 4f)
                        {
                            npc.velocity.Y = npc.velocity.Y - 0.1f;
                        }
                        else if (npc.velocity.Y > 0f)
                        {
                            npc.velocity.Y += 0.05f;
                        }
                        if (npc.velocity.Y < -4f)
                        {
                            npc.velocity.Y = -4f;
                        }
                    }
                    else if (npc.directionY == 1 && npc.velocity.Y < 4f)
                    {
                        npc.velocity.Y += 0.1f;
                        if (npc.velocity.Y < -4f)
                        {
                            npc.velocity.Y += 0.1f;
                        }
                        else if (npc.velocity.Y < 0f)
                        {
                            npc.velocity.Y -= 0.05f;
                        }
                        if (npc.velocity.Y > 4f)
                        {
                            npc.velocity.Y = 4f;
                        }
                    }
                }
                else
                {
                    if (npc.direction == -1 && npc.velocity.X > -4f)
                    {
                        npc.velocity.X -= 0.1f;
                        if (npc.velocity.X > 4f)
                        {
                            npc.velocity.X -= 0.1f;
                        }
                        else if (npc.velocity.X > 0f)
                        {
                            npc.velocity.X += 0.05f;
                        }
                        if (npc.velocity.X < -4f)
                        {
                            npc.velocity.X = -4f;
                        }
                    }
                    else if (npc.direction == 1 && npc.velocity.X < 4f)
                    {
                        npc.velocity.X += 0.1f;
                        if (npc.velocity.X < -4f)
                        {
                            npc.velocity.X += 0.1f;
                        }
                        else if (npc.velocity.X < 0f)
                        {
                            npc.velocity.X -= 0.05f;
                        }
                        if (npc.velocity.X > 4f)
                        {
                            npc.velocity.X = 4f;
                        }
                    }
                    if (npc.directionY == -1 && (double)npc.velocity.Y > -1.5)
                    {
                        npc.velocity.Y -= 0.04f;
                        if ((double)npc.velocity.Y > 1.5)
                        {
                            npc.velocity.Y -= 0.05f;
                        }
                        else if (npc.velocity.Y > 0f)
                        {
                            npc.velocity.Y += 0.03f;
                        }
                        if ((double)npc.velocity.Y < -1.5)
                        {
                            npc.velocity.Y = -1.5f;
                        }
                    }
                    else if (npc.directionY == 1 && (double)npc.velocity.Y < 1.5)
                    {
                        npc.velocity.Y += 0.04f;
                        if ((double)npc.velocity.Y < -1.5)
                        {
                            npc.velocity.Y += 0.05f;
                        }
                        else if (npc.velocity.Y < 0f)
                        {
                            npc.velocity.Y -= 0.03f;
                        }
                        if ((double)npc.velocity.Y > 1.5)
                        {
                            npc.velocity.Y = 1.5f;
                        }
                    }
                }

                if (Main.rand.Next(40) == 0)
                {
                    int dust = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y + (float)npc.height * 0.25f), npc.width, (int)((float)npc.height * 0.5f), 5, npc.velocity.X, 2f, 0, default(Color), 1f);
                    Main.dust[dust].velocity.X *= 0.5f;
                    Main.dust[dust].velocity.Y *= 0.1f;
                }
                if (npc.wet)
                {
                    if (npc.velocity.Y > 0f)
                    {
                        npc.velocity.Y *= 0.95f;
                    }
                    npc.velocity.Y -= 0.5f;
                    if (npc.velocity.Y < -4f)
                    {
                        npc.velocity.Y = -4f;
                    }
                    npc.TargetClosest(true);
                }

                npc.ai[1] += 1f;
                if (npc.ai[1] >= 180f / npc.ai[0])
                {
                    Player player = Main.player[npc.target];
                    if (player.active && !player.dead)
                    {
                        Vector2 distanceTo = player.Center - npc.Center;
                        float angleTo = (float)Math.Atan2(distanceTo.Y, distanceTo.X);
                        if (npc.spriteDirection == -1)
                        {
                            angleTo += (float)Math.PI;
                            angleTo %= 2f * (float)Math.PI;
                        }
                        float distance = (float)Math.Sqrt(distanceTo.X * distanceTo.X + distanceTo.Y * distanceTo.Y);
                        float toleration = (float)Math.PI;
                        if (distance > 0f)
                        {
                            toleration = 1f / distance;
                        }
                        if (toleration < 0.1f)
                        {
                            toleration = 0.1f;
                        }
                        if (Math.Abs(angleTo - npc.rotation) < toleration && Collision.CanHit(npc.position, npc.width, npc.height, player.position, player.width, player.height))
                        {
                            Vector2 unit = new Vector2((float)Math.Cos(npc.rotation), (float)Math.Sin(npc.rotation));
                            unit *= (float)npc.spriteDirection;
                            float speed = 9f;
                            int type = 83;
                            if (npc.ai[0] == 1f)
                            {
                                speed = 6f;
                                type = 96;
                            }
                            Projectile.NewProjectile(npc.Center.X + unit.X, npc.Center.Y + unit.Y, speed * unit.X, speed * unit.Y, type, 40, 0f, Main.myPlayer, 0f, 0f);
                            npc.ai[1] = 0f;
                        }
                    }
                }
            }
        }
    }
}


/*public override void HitEffect(int hitDirection, double damage)
{
			for (int i = 0; i < 10; i++) {
				int dustType = Main.rand.Next(139, 143);
				int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, dustType);
				Dust dust = Main.dust[dustIndex];
				dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 0.01f;
				dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 0.01f;
				dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
			}
		}
	}
}*/