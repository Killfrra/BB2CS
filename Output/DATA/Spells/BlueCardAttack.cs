#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class BlueCardAttack : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
        };
        int[] effect0 = {40, 60, 80, 100, 120};
        public override void SelfExecute()
        {
            SpellBuffRemove(owner, nameof(Buffs.PickACard), (ObjAIBase)owner);
            SpellBuffRemove(owner, nameof(Buffs.BlueCardPreattack), (ObjAIBase)owner);
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            teamID = GetTeamID(attacker);
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(target is ObjAIBase)
            {
                float totalDamage;
                float bonusDamage;
                float damageToDeal;
                if(!target.IsDead)
                {
                    Particle asdf1; // UNUSED
                    BreakSpellShields(target);
                    SpellEffectCreate(out asdf1, out _, "PickaCard_blue_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
                }
                totalDamage = GetTotalAttackDamage(owner);
                bonusDamage = this.effect0[level];
                damageToDeal = totalDamage + bonusDamage;
                AddBuff((ObjAIBase)target, owner, new Buffs.CardmasterBlueCardMana(), 1, 1, 0.1f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.4f, 1, false, false, attacker);
                ApplyDamage(attacker, target, 0, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
            }
            else
            {
                float baseDamage;
                Particle a; // UNUSED
                baseDamage = GetBaseAttackDamage(attacker);
                ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
                SpellEffectCreate(out a, out _, "soraka_infuse_ally_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true);
            }
        }
    }
}