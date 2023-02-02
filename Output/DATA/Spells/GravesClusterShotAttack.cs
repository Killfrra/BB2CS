#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class GravesClusterShotAttack : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = {60, 105, 150, 195, 240};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamOfCaster;
            Particle part; // UNUSED
            float totalDamage;
            float baseDamage;
            float bonusAD;
            float bonusDamage;
            int count;
            teamOfCaster = GetTeamID(attacker);
            SpellEffectCreate(out part, out _, "Graves_ClusterShot_Tar.troy", default, teamOfCaster ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, target.Position, target, default, default, true, false, false, false, false);
            BreakSpellShields(target);
            totalDamage = GetTotalAttackDamage(attacker);
            baseDamage = GetBaseAttackDamage(owner);
            bonusAD = totalDamage - baseDamage;
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            bonusDamage = this.effect0[level];
            bonusAD *= 0.8f;
            bonusDamage += bonusAD;
            count = GetBuffCountFromAll(target, nameof(Buffs.GravesClusterShotAttack));
            if(count > 0)
            {
                bonusDamage *= 0.25f;
            }
            AddBuff((ObjAIBase)target, target, new Buffs.GravesClusterShotAttack(), 1, 1, 0.25f, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
            ApplyDamage(attacker, target, bonusDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
        }
    }
}
namespace Buffs
{
    public class GravesClusterShotAttack : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Graves_ClusterShot_cas.troy", },
        };
    }
}