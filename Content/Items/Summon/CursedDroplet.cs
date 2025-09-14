using Eternal.Content.Projectiles.Misc;
using Eternal.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.NPCs.Boss.CarminiteAmalgamation;
using Terraria.GameContent.Creative;

namespace Eternal.Content.Items.Summon
{
    public class CursedDroplet : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
        }

        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 22;
            Item.maxStack = 9999;
            Item.rare = ItemRarityID.Lime;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.shoot = ModContent.ProjectileType<Droplet2>();
            Item.shootSpeed = 2.75f;
            Item.UseSound = SoundID.Item1;
            Item.noUseGraphic = true;
        }

        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<CarminiteAmalgamation>()) || !NPC.AnyNPCs(ModContent.NPCType<CarminiteAmalgamation2>()) && player.ownedProjectileCounts[Item.shoot] < 1;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<CoagulatedBlood>(), 15)
                .AddIngredient(ModContent.ItemType<CursedAshes>(), 15)
                .AddIngredient(ModContent.ItemType<SuspiciousLookingDroplet>())
                .Register();
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossSpawners;
        }

    }
}
