#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OnTheHunt : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", "", "", },
            AutoBuffActivateEffect = new[]{ "", "OntheHuntBase_buf.troy", "", "", },
            BuffName = "On The Hunt",
            BuffTextureName = "Sivir_Deadeye.dds",
        };
        float moveSpeedMod;
        float attackSpeedMod;
        float lastTimeExecuted;
        public OnTheHunt(float moveSpeedMod = default, float attackSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
            this.attackSpeedMod = attackSpeedMod;
        }
        public override void OnActivate()
        {
            float duration;
            //RequireVar(this.moveSpeedMod);
            //RequireVar(this.attackSpeedMod);
            //RequireVar(this.allyAttackSpeedMod);
            IncPercentAttackSpeedMod(owner, this.attackSpeedMod);
            IncPercentMovementSpeedMod(owner, this.moveSpeedMod);
            duration = GetBuffRemainingDuration(owner, nameof(Buffs.OnTheHunt));
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, default, true))
            {
                AddBuff(attacker, unit, new Buffs.OnTheHuntAuraBuff(), 1, 1, duration, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, this.attackSpeedMod);
            IncPercentMovementSpeedMod(owner, this.moveSpeedMod);
        }
        public override void OnUpdateActions()
        {
            float duration;
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                duration = GetBuffRemainingDuration(owner, nameof(Buffs.OnTheHunt));
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, default, true))
                {
                    if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.OnTheHuntAuraBuff)) == 0)
                    {
                        AddBuff(attacker, unit, new Buffs.OnTheHuntAuraBuff(), 1, 1, duration, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    }
                }
            }
        }
    }
}
namespace Spells
{
    public class OnTheHunt : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 90f, 90f, 90f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {0.25f, 0.25f, 0.25f};
        float[] effect1 = {0.3f, 0.45f, 0.6f};
        float[] effect2 = {0.15f, 0.225f, 0.3f};
        int[] effect3 = {15, 15, 15};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_MoveSpeedMod;
            float nextBuffVars_AttackSpeedMod;
            float nextBuffVars_AllyAttackSpeedMod;
            nextBuffVars_MoveSpeedMod = this.effect0[level];
            nextBuffVars_AttackSpeedMod = this.effect1[level];
            nextBuffVars_AllyAttackSpeedMod = this.effect2[level];
            AddBuff(attacker, attacker, new Buffs.OnTheHunt(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackSpeedMod), 1, 1, this.effect3[level], BuffAddType.REPLACE_EXISTING, BuffType.HASTE, 0, true, false, false);
        }
    }
}