using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Buffs
{
	public class IncineriusPet : ModBuff
	{

		public override void SetDefaults()
		{
			DisplayName.SetDefault("Incinerius?");
			Description.SetDefault("Incinerius will provide light...\n'I thought he wanted to kill me...'");
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<EternalPlayer>().incineriusPet = true;
			player.buffTime[buffIndex] = 18000;
			bool petProjectileNotSpawned = player.ownedProjectileCounts[ProjectileType<Projectiles.Pets.IncineriusClone>()] <= 0;
			if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ProjectileType<Projectiles.Pets.IncineriusClone>(), 0, 0f, player.whoAmI, 0f, 0f);
			}
			if (player.controlDown && player.releaseDown)
			{
				if (player.doubleTapCardinalTimer[0] > 0 && player.doubleTapCardinalTimer[0] != 15)
				{
					for (int j = 0; j < 1000; j++)
					{
						if (Main.projectile[j].active && Main.projectile[j].type == ProjectileType<Projectiles.Pets.IncineriusClone>() && Main.projectile[j].owner == player.whoAmI)
						{
							Projectile lightpet = Main.projectile[j];
							Vector2 vectorToMouse = Main.MouseWorld - lightpet.Center;
							lightpet.velocity += 5f * Vector2.Normalize(vectorToMouse);
						}
					}
				}
			}

		}
	}
}
