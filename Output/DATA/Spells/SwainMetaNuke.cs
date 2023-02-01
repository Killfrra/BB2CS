#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class SwainMetaNuke : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 0f, 0f, 0f, 0f, 0f, },
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {50, 70, 90};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            bool nextBuffVars_DrainedBool;
            float nextBuffVars_DrainPercent;
            Particle ar; // UNUSED
            Vector3 targetPos;
            bool isTargetable;
            level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            nextBuffVars_DrainedBool = false;
            SpellEffectCreate(out ar, out _, "swain_heal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, target, default, default, false, default, default, false, false);
            targetPos = GetCastSpellTargetPos();
            SpellCast(attacker, owner, attacker.Position, owner.Position, 2, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, targetPos);
            isTargetable = GetTargetable(attacker);
            if(target is Champion)
            {
                nextBuffVars_DrainPercent = 0.75f;
            }
            else
            {
                nextBuffVars_DrainPercent = 0.25f;
            }
            if(!isTargetable)
            {
                SpellEffectCreate(out ar, out _, "swain_heal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, target, default, default, false, default, default, false, false);
            }
            AddBuff((ObjAIBase)owner, owner, new Buffs.GlobalDrain(nextBuffVars_DrainPercent, nextBuffVars_DrainedBool), 1, 1, 0.01f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            ApplyDamage(attacker, target, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.2f, 1, false, false, attacker);
        }
    }
}