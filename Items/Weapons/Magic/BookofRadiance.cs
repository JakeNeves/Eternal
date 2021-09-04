using Eternal.Projectiles.Weapons.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Weapons.Magic
{
    public class BookofRadiance : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Book of Radiance");
            Tooltip.SetDefault("Fires a Radiant Orb that chases your enimey" +
                             "\n'To good to be true'");
        }

        public override void SetDefaults()
        {
            item.damage = 2000;
            item.magic = true;
            item.mana = 20;
            item.width = 34;
            item.height = 40;
            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 1.5f;
            item.value = Item.sellPrice(gold: 9);
            item.rare = ItemRarityID.Red;
            item.autoReuse = true;
            item.shootSpeed = 18.6f;
            item.shoot = ProjectileType<RadiantOrbRing>();
            item.UseSound = SoundID.DD2_BetsysWrathShot;
        }

    }
}
