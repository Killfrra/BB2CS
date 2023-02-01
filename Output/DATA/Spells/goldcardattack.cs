#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Goldcardattack : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
        };
        float[] effect0 = {15, 22.5f, 30, 37.5f, 45};
        float[] effect1 = {1, 1.25f, 1.5f, 1.75f, 2};
        public override void SelfExecute()
        {
            SpellBuffRemove(owner, nameof(Buffs.GoldCardPreAttack), (ObjAIBase)owner);
            SpellBuffRemove(owner, nameof(Buffs.PickACard), (ObjAIBase)owner);
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            float totalDamage;
            float bonusDamage;
            float goldCardDamage;
            Particle arrm8y; // UNUSED
            float baseDamage;
            Vector3 targetPosition;
            teamID = GetTeamID(attacker);
            if(target is ObjAIBase)
            {
                BreakSpellShields(target);
                level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                totalDamage = GetTotalAttackDamage(owner);
                bonusDamage = this.effect0[level];
                goldCardDamage = bonusDamage + totalDamage;
                ApplyDamage(attacker, target, 0, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
                ApplyDamage(attacker, target, goldCardDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.4f, 1, false, false, attacker);
                SpellEffectCreate(out arrm8y, out _, "PickaCard_yellow_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
                if(target is not BaseTurret)
                {
                    ApplyStun(attacker, target, this.effect1[level]);
                }
            }
            else
            {
                baseDamage = GetBaseAttackDamage(attacker);
                ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
                targetPosition = GetCastSpellTargetPos();
                SpellEffectCreate(out arrm8y, out _, "PickaCard_yellow_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPosition, default, default, default, true);
            }
        }
    }
}