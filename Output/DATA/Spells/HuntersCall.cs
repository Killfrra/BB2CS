#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class HuntersCall : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {0.4f, 0.5f, 0.6f, 0.7f, 0.8f};
        float[] effect1 = {0.2f, 0.25f, 0.3f, 0.35f, 0.4f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_AttackSpeedVar;
            float nextBuffVars_AttackSpeedOther;
            nextBuffVars_AttackSpeedVar = this.effect0[level];
            nextBuffVars_AttackSpeedOther = this.effect1[level];
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1200, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
            {
                AddBuff((ObjAIBase)owner, unit, new Buffs.HuntersCall(nextBuffVars_AttackSpeedVar, nextBuffVars_AttackSpeedOther), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true);
            }
        }
    }
}
namespace Buffs
{
    public class HuntersCall : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "r_hand", "l_hand", },
            AutoBuffActivateEffect = new[]{ "Global_DmgHands_buf.troy", "Global_DmgHands_buf.troy", },
            BuffName = "Hunter's Call",
            BuffTextureName = "Wolfman_FuryStance.dds",
        };
        float attackSpeedVar;
        float attackSpeedOther;
        public HuntersCall(float attackSpeedVar = default, float attackSpeedOther = default)
        {
            this.attackSpeedVar = attackSpeedVar;
            this.attackSpeedOther = attackSpeedOther;
        }
        public override void OnActivate()
        {
            //RequireVar(this.attackSpeedVar);
            //RequireVar(this.attackSpeedOther);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnUpdateStats()
        {
            if(owner == attacker)
            {
                IncPercentAttackSpeedMod(owner, this.attackSpeedVar);
            }
            else
            {
                IncPercentAttackSpeedMod(owner, this.attackSpeedOther);
            }
        }
    }
}