using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Melee;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class Lipoma : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.width = 28;
            Item.height = 24;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.shootSpeed = 30f;
            Item.knockBack = 6f;
            Item.damage = 200;
            Item.rare = ModContent.RarityType<Teal>();

            Item.DamageType = DamageClass.Melee;
            Item.channel = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(gold: 4);
            Item.shoot = ModContent.ProjectileType<LipomaProjectile>();
        }

        private static readonly int[] unwantedPrefixes = new int[] { PrefixID.Terrible, PrefixID.Dull, PrefixID.Annoying, PrefixID.Broken, PrefixID.Damaged, PrefixID.Shoddy };

        public override bool AllowPrefix(int pre)
        {
            if (Array.IndexOf(unwantedPrefixes, pre) > -1)
            {
                return false;
            }
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<MysteryMeat>(), 6)
                .AddIngredient(ModContent.ItemType<CoagulatedBlood>(), 12)
                .AddIngredient(ItemID.Vertebrae, 18)
                .AddTile(ModContent.TileType<Starforge>())
                .Register();
        }
    }
}
