#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RiftWalk : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            AutoBuffActivateEvent = "",
            BuffName = "RiftWalk",
            BuffTextureName = "Voidwalker_Riftwalk.dds",
        };
        public override void OnDeactivate(bool expired)
        {
            SetPARCostInc((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
        }
    }
}
namespace Spells
{
    public class RiftWalk : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {60, 90, 120};
        public override void SelfExecute()
        {
            Vector3 castPos;
            Vector3 ownerPos;
            float distance;
            TeamId casterID;
            Particle p3; // UNUSED
            int count;
            float damage;
            float count2;
            float totalDamage;
            float extraCost;
            Particle ar1; // UNUSED
            castPos = GetCastSpellTargetPos();
            ownerPos = GetUnitPosition(owner);
            distance = DistanceBetweenPoints(ownerPos, castPos);
            FaceDirection(owner, castPos);
            if(distance >= 700)
            {
                castPos = GetPointByUnitFacingOffset(owner, 700, 0);
            }
            casterID = GetTeamID(owner);
            if(casterID == TeamId.TEAM_BLUE)
            {
                SpellEffectCreate(out p3, out _, "Riftwalk_flashback.troy", default, TeamId.TEAM_BLUE, 250, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out p3, out _, "Riftwalk_flashback.troy", default, TeamId.TEAM_PURPLE, 250, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
            }
            TeleportToPosition(owner, castPos);
            count = GetBuffCountFromAll(owner, nameof(Buffs.RiftWalk));
            damage = this.effect0[level];
            count2 = 1 + count;
            totalDamage = damage * count2;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 270, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                ApplyDamage(attacker, unit, totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.5f, 1, false, false, attacker);
            }
            extraCost = 100 * count2;
            SetPARCostInc((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, extraCost, PrimaryAbilityResourceType.MANA);
            AddBuff(attacker, owner, new Buffs.RiftWalk(), 10, 1, 8, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            if(casterID == TeamId.TEAM_BLUE)
            {
                SpellEffectCreate(out ar1, out _, "Riftwalk_flash.troy", default, TeamId.TEAM_BLUE, 250, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, castPos, target, default, default, true, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out ar1, out _, "Riftwalk_flash.troy", default, TeamId.TEAM_PURPLE, 250, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, castPos, target, default, default, true, false, false, false, false);
            }
        }
    }
}