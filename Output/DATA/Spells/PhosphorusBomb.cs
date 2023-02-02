#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class PhosphorusBomb : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        public override void SelfExecute()
        {
            TeamId teamID; // UNUSED
            Vector3 targetPos;
            Vector3 nextBuffVars_TargetPos;
            teamID = GetTeamID(owner);
            targetPos = GetCastSpellTargetPos();
            nextBuffVars_TargetPos = targetPos;
            AddBuff(attacker, attacker, new Buffs.PhosphorusBomb(nextBuffVars_TargetPos), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class PhosphorusBomb : BBBuffScript
    {
        Vector3 targetPos;
        Particle particle;
        Region bubbleID;
        int[] effect0 = {80, 130, 180, 230, 280};
        public PhosphorusBomb(Vector3 targetPos = default)
        {
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            int level;
            TeamId casterID;
            Vector3 targetPos;
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            //RequireVar(this.targetPos);
            casterID = GetTeamID(attacker);
            targetPos = this.targetPos;
            if(casterID == TeamId.TEAM_BLUE)
            {
                SpellEffectCreate(out this.particle, out _, "corki_phosphorous_bomb_tar.troy", default, TeamId.TEAM_BLUE, 250, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, target, default, default, true, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out this.particle, out _, "corki_phosphorous_bomb_tar.troy", default, TeamId.TEAM_PURPLE, 250, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, target, default, default, true, false, false, false, false);
            }
            this.bubbleID = AddPosPerceptionBubble(casterID, 375, targetPos, 6, default, false);
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, this.targetPos, 275, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, default, true))
            {
                AddBuff(attacker, unit, new Buffs.PhosphorusBombBlind(), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            }
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, this.targetPos, 275, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                ApplyDamage(attacker, unit, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.5f, 1, false, false, attacker);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(this.bubbleID);
            SpellEffectRemove(this.particle);
        }
    }
}