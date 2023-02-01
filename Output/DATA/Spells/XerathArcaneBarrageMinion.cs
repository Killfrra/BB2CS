#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class XerathArcaneBarrageMinion : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = false,
        };
        Particle a; // UNUSED
        int[] effect0 = {125, 200, 275, 0, 0};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            float damageAmount;
            Vector3 ownerPos;
            Particle asdf; // UNUSED
            Particle asdf2; // UNUSED
            teamID = GetTeamID(target);
            level = GetSlotSpellLevel((ObjAIBase)target, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            damageAmount = this.effect0[level];
            ownerPos = GetUnitPosition(owner);
            SpellEffectCreate(out this.a, out _, "Xerath_E_tar.troy", default, teamID, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, default, default, ownerPos, true, false, false, false, false);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)target, owner.Position, 275, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                SpellEffectCreate(out asdf, out _, "Xerath_Barrage_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                BreakSpellShields(unit);
                ApplyDamage((ObjAIBase)target, unit, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.6f, 0, false, false, (ObjAIBase)target);
                if(GetBuffCountFromCaster(unit, target, nameof(Buffs.XerathMageChains)) > 0)
                {
                    SpellEffectCreate(out asdf2, out _, "Xerath_MageChains_consume.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                    AddBuff((ObjAIBase)target, unit, new Buffs.XerathMageChainsRoot(), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, true, false);
                    SpellBuffRemove(unit, nameof(Buffs.XerathMageChains), (ObjAIBase)target, 0);
                }
            }
            SetTargetable(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 1000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 0, false, false, attacker);
        }
    }
}