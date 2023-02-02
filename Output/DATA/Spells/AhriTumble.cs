#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class AhriTumble : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            ChainMissileParameters = new()
            {
                CanHitCaster = 0,
                CanHitEnemies = 1,
                CanHitFriends = 0,
                CanHitSameTarget = 0,
                CanHitSameTargetConsecutively = 0,
                MaximumHits = new[]{ 10, 10, 10, 10, 10, },
            },
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override bool CanCast()
        {
            bool returnValue = true;
            bool canMove;
            bool canCast;
            canMove = GetCanMove(owner);
            canCast = GetCanCast(owner);
            if(!canMove)
            {
                returnValue = false;
            }
            if(!canCast)
            {
                returnValue = false;
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            int count;
            Vector3 nextBuffVars_TargetPos;
            Particle smokeBomb; // UNUSED
            Vector3 ownerPos; // UNUSED
            Vector3 targetPos;
            float moveSpeed;
            float dashSpeed;
            float distance;
            float nextBuffVars_Distance;
            float nextBuffVars_dashSpeed;
            count = GetBuffCountFromAll(owner, nameof(Buffs.AhriTumble));
            if(count == 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.AhriTumble(), 2, 2, 10, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0.25f, true, false, false);
                SetSlotSpellCooldownTimeVer2(0.75f, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, true);
                SetPARCostInc((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, -150, PrimaryAbilityResourceType.MANA);
            }
            else if(count == 1)
            {
                SpellBuffRemoveStacks(owner, owner, nameof(Buffs.AhriTumble), 1);
            }
            else if(count == 2)
            {
                SpellBuffRemoveStacks(owner, owner, nameof(Buffs.AhriTumble), 1);
                SetSlotSpellCooldownTimeVer2(0.75f, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, true);
            }
            SpellEffectCreate(out smokeBomb, out _, "Ahri_SpiritRush_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_GLB_GROUND_LOC", default, owner, "BUFFBONE_GLB_GROUND_LOC", default, false, false, false, false, false);
            SpellEffectCreate(out smokeBomb, out _, "Ahri_Orb_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_GLB_WEAPON_1", default, owner, "BUFFBONE_GLB_WEAPON_1", default, false, false, false, false, false);
            ownerPos = GetUnitPosition(owner);
            targetPos = GetCastSpellTargetPos();
            moveSpeed = GetMovementSpeed(owner);
            dashSpeed = moveSpeed + 1200;
            distance = DistanceBetweenObjectAndPoint(owner, targetPos);
            if(distance > 500)
            {
                Vector3 nearestAvailablePos;
                float distance2;
                FaceDirection(owner, targetPos);
                targetPos = GetPointByUnitFacingOffset(owner, 500, 0);
                distance = 500;
                nearestAvailablePos = GetNearestPassablePosition(owner, targetPos);
                distance2 = DistanceBetweenPoints(nearestAvailablePos, targetPos);
                if(distance2 > 25)
                {
                    targetPos = GetPointByUnitFacingOffset(owner, 600, 0);
                    distance = 600;
                }
            }
            nextBuffVars_TargetPos = targetPos;
            nextBuffVars_Distance = distance;
            nextBuffVars_dashSpeed = dashSpeed;
            AddBuff((ObjAIBase)owner, owner, new Buffs.AhriTumbleKick(nextBuffVars_TargetPos, nextBuffVars_Distance, nextBuffVars_dashSpeed), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0.25f, true, false, false);
        }
    }
}
namespace Buffs
{
    public class AhriTumble : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "AhriTumble",
            BuffTextureName = "Ahri_SpiritRush.dds",
            PersistsThroughDeath = true,
        };
        float newCd;
        int[] effect0 = {90, 80, 70, 0, 0};
        public override void OnDeactivate(bool expired)
        {
            int count;
            count = GetBuffCountFromAll(owner, nameof(Buffs.AhriTumble));
            if(count == 0)
            {
                int level;
                float cooldownStat;
                float multiplier;
                float newCooldown;
                level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                this.newCd = this.effect0[level];
                cooldownStat = GetPercentCooldownMod(owner);
                multiplier = 1 + cooldownStat;
                newCooldown = multiplier * this.newCd;
                SetSlotSpellCooldownTimeVer2(newCooldown, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, true);
                SetPARCostInc((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
            }
        }
    }
}