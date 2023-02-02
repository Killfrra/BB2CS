#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class ShyvanaDoubleAttackHit : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float baseAttackDamage;
            baseAttackDamage = GetBaseAttackDamage(owner);
            ApplyDamage(attacker, target, baseAttackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
            AddBuff(attacker, target, new Buffs.ShyvanaDoubleAttackHit(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class ShyvanaDoubleAttackHit : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffTextureName = "",
        };
        float[] effect0 = {0.8f, 0.85f, 0.9f, 0.95f, 1};
        public override void OnActivate()
        {
            TeamId teamID;
            int level;
            float baseAttackDamage;
            teamID = GetTeamID(attacker);
            level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            baseAttackDamage = GetBaseAttackDamage(attacker);
            BreakSpellShields(target);
            ApplyDamage(attacker, owner, baseAttackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, this.effect0[level], 0, 1, false, false, attacker);
            if(target is ObjAIBase)
            {
                Particle a; // UNUSED
                SpellEffectCreate(out a, out _, "shyvana_doubleAttack_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
            }
        }
    }
}