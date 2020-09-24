using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Magic
{
    public class FuryFlare : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Firea an Inferno Bolt, like the Inferno Fork\n'Not to be confused with Fury Forged'");
            Item.staff[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.damage = 90;
            item.magic = true;
            item.mana = 10;
            item.width = 28;
            item.height = 34;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 3.5f;
            item.value = Item.sellPrice(gold: 12);
            item.rare = ItemRarityID.Pink;
            item.autoReuse = true;
            item.shootSpeed = 24f;
            item.shoot = ProjectileID.InfernoFriendlyBolt;
            item.UseSound = SoundID.Item8;
        }

    }
}
