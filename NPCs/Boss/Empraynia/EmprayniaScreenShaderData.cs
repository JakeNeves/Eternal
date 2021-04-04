using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace Eternal.NPCs.Boss.Empraynia
{
    public class EmprayniaScreenShaderData : ScreenShaderData
    {
        private int EmprayniaIndex;

        public EmprayniaScreenShaderData(string passName)
            : base(passName)
        {
        }

		private void UpdateEmprayniaIndex()
		{
			int EmprayniaType = ModLoader.GetMod("Eternal").NPCType("Empraynia");
			if (EmprayniaIndex >= 0 && Main.npc[EmprayniaIndex].active && Main.npc[EmprayniaIndex].type == EmprayniaType)
				return;

			EmprayniaIndex = -1;
			for (int i = 0; i < Main.npc.Length; i++)
			{
				if (Main.npc[i].active && Main.npc[i].type == EmprayniaIndex)
				{
					EmprayniaIndex = i;
					break;
				}
			}
		}

		public override void Apply()
		{
			UpdateEmprayniaIndex();
			if (EmprayniaIndex != -1)
				UseTargetPosition(Main.npc[EmprayniaIndex].Center);

			base.Apply();
		}
	}
}
