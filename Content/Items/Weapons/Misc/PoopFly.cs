using Eternal.Content.Projectiles.Weapons.Misc;
using Eternal.Content.Rarities;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Misc
{
    public class PoopFly : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemNoGravity[Item.type] = true;

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(1, 2));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 10000;
            Item.width = 24;
            Item.height = 18;
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 0;
            Item.value = Item.sellPrice(100, 0, 0, 0);
            Item.rare = ModContent.RarityType<Aquamarine>();
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.shootSpeed = 8f;
            Item.shoot = ModContent.ProjectileType<PoopFlyProjectile>();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int dmg = tooltips.FindIndex(x => x.Name == "Damage");
            tooltips.RemoveAt(dmg);
            tooltips.Insert(dmg, new TooltipLine(Mod, "Damage", "10000 poop decimating damage"));
        }
    }
}
