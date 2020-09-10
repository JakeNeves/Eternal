using Eternal.Projectiles;
using Eternal.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Weapons.Melee
{
    public class VividMilkyWayClimax : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A really interesting looking shortsword\n<right> to swing normally");
        }

        public override void SetDefaults()
        {
            item.width = 88;
            item.height = 90;
            item.damage = 975;
            item.knockBack = 30;
            item.value = Item.buyPrice(platinum: 1);
            item.useTime = 10;
            item.useAnimation = 10;
            item.UseSound = SoundID.Item1;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.rare = ItemRarityID.Red;
            item.autoReuse = true;
            item.melee = true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useStyle = ItemUseStyleID.SwingThrow;
                item.autoReuse = false;
            }
            else
            {
                item.useStyle = ItemUseStyleID.Stabbing;
                item.autoReuse = true;
            }
            return base.CanUseItem(player);
        }
    }
}
