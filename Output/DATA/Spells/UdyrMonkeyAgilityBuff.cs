#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class UdyrMonkeyAgilityBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "UdyrMonkeyAgilityBuff",
            BuffTextureName = "Udyr_MonkeysAgility.dds",
        };
        Particle a;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.a, out _, "UdyrBuff.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.a);
        }
        public override void OnUpdateStats()
        {
            int monkeyStacks;
            float attackSpeedMod;
            IncPercentAttackSpeedMod(owner, 0.1f);
            monkeyStacks = GetBuffCountFromAll(owner, nameof(Buffs.UdyrMonkeyAgilityBuff));
            attackSpeedMod = 10 * monkeyStacks;
            SetBuffToolTipVar(1, attackSpeedMod);
        }
    }
}