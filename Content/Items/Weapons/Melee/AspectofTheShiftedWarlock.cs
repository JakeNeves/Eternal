using Eternal.Content.Rarities;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Common.Systems;
using Terraria.Audio;
using Microsoft.Xna.Framework;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class AspectofTheShiftedWarlock : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 48;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ModContent.RarityType<Teal>();
            Item.damage = 512;
            Item.useAnimation = 18;
            Item.useTime = 18;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2f;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int k = 0; k < 8; k++)
            {
                int dust = Dust.NewDust(player.Center, target.width, target.height, DustID.PinkTorch, target.oldVelocity.X * 1f, target.oldVelocity.Y * 1f);
            }

            if (EventSystem.isRiftOpen)
            {
                SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/RodofDistortion"), player.position);
                for (int k = 0; k < 8; k++)
                {
                    int dust = Dust.NewDust(player.Center, target.width, target.height, DustID.PinkTorch, target.oldVelocity.X * 1f, target.oldVelocity.Y * 1f);
                }
                player.Teleport(target.position + new Vector2(Main.rand.Next(-100, 100), Main.rand.Next(-100, 100)), 2, 0);
            }
        }
    }
}
