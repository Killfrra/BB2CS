#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class StaticField : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {250, 375, 500};
        public override void SelfExecute()
        {
            SpellBuffRemove(owner, nameof(Buffs.StaticField), (ObjAIBase)owner);
            SpellEffectCreate(out _, out _, "StaticField_nova.prt", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            ApplyDamage(attacker, target, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 1, 1, false, false, attacker);
            ApplySilence(attacker, target, 0.5f);
        }
    }
}
namespace Buffs
{
    public class StaticField : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "weapon", },
            AutoBuffActivateEffect = new[]{ "StaticField_ready.troy", },
            BuffName = "StaticField",
            BuffTextureName = "Blitzcrank_StaticField.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTimeExecuted;
        int[] effect0 = {100, 200, 300};
        public override void OnUpdateActions()
        {
            TeamId teamID;
            int level;
            teamID = GetTeamID(owner);
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(ExecutePeriodically(2.5f, ref this.lastTimeExecuted, true))
            {
                if(owner.IsDead)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else
                {
                    foreach(AttackableUnit unit in GetRandomUnitsInArea((ObjAIBase)owner, owner.Position, 425, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, default, true))
                    {
                        bool result;
                        result = CanSeeTarget(owner, unit);
                        if(result)
                        {
                            ApplyDamage((ObjAIBase)owner, unit, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.2f, 1, false, false, attacker);
                            SpellEffectCreate(out _, out _, "StaticField_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, unit, false, unit, default, default, unit, default, default, true);
                        }
                    }
                }
            }
        }
    }
}