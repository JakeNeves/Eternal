using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Accessories.Expert
{
    public class FlameInfusedJewel : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Creates an Incinerius Clone to Provide Light\n'Whoever knew Incinerius would look like a stand'\n[W.I.P]");
        }

		public override void SetDefaults()
		{
			item.damage = 0;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.shoot = ProjectileType<Projectiles.Pets.IncineriusClone>();
			item.width = 16;
			item.height = 30;
			item.UseSound = SoundID.Item2;
			item.useAnimation = 20;
			item.useTime = 20;
			item.rare = ItemRarityID.Expert;
			item.expert = true;
			item.noMelee = true;
			item.value = Item.sellPrice(gold: 5);
			item.buffType = BuffType<Buffs.IncineriusPet>();
		}

		public override void UseStyle(Player player)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(item.buffType, 3600, true);
			}
		}

	}
}
