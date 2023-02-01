#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class UrgotSwap2 : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "UrgotSwapExecute",
            BuffTextureName = "UrgotPositionReverser.dds",
        };
        int isDisabled;
        Particle gateParticle;
        Particle particle3;
        public UrgotSwap2(Particle gateParticle = default)
        {
            this.gateParticle = gateParticle;
        }
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            if(owner.Team != attacker.Team)
            {
                if(type == BuffType.FEAR)
                {
                    this.isDisabled = 1;
                }
                if(type == BuffType.CHARM)
                {
                    this.isDisabled = 1;
                }
                if(type == BuffType.SLEEP)
                {
                    this.isDisabled = 1;
                }
                if(type == BuffType.STUN)
                {
                    this.isDisabled = 1;
                }
                if(type == BuffType.TAUNT)
                {
                    this.isDisabled = 1;
                }
                if(type == BuffType.SILENCE)
                {
                    this.isDisabled = 1;
                }
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            TeamId teamOfOwner;
            //RequireVar(this.gateParticle);
            //RequireVar(this.targetPos);
            this.isDisabled = 0;
            teamOfOwner = GetTeamID(owner);
            SpellEffectCreate(out this.particle3, out _, "UrgotSwapDrip.troy", default, teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, default, default, false, false);
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            PlayAnimation("teleUp", 1.2f, owner, false, false, true);
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamOfOwner; // UNUSED
            float distance;
            SetCanMove(owner, true);
            SetCanAttack(owner, true);
            teamOfOwner = GetTeamID(owner);
            SpellEffectRemove(this.gateParticle);
            SpellEffectRemove(this.particle3);
            SpellBuffRemove(owner, nameof(Buffs.UrgotSwapMissile), attacker, 0);
            SpellBuffRemove(owner, nameof(Buffs.Suppression), attacker, 0);
            SpellBuffRemove(owner, nameof(Buffs.UrgotSwapTarget), attacker, 0);
            UnlockAnimation(owner, false);
            if(this.isDisabled > 0)
            {
            }
            else
            {
                distance = DistanceBetweenObjects("Owner", "Attacker");
                if(distance < 3000)
                {
                    AddBuff((ObjAIBase)owner, attacker, new Buffs.UrgotSwapExecute(), 1, 1, 0.1f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                    AddBuff((ObjAIBase)owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    PlayAnimation("teleDwn", 0.7f, owner, false, false, true);
                }
            }
        }
        public override void OnUpdateStats()
        {
            int isDisabled; // UNITIALIZED
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            if(GetBuffCountFromCaster(attacker, owner, nameof(Buffs.UrgotSwapTarget)) == 0)
            {
                this.isDisabled = 1;
                SpellBuffClear(owner, nameof(Buffs.UrgotSwap2));
            }
            if(isDisabled == 1)
            {
                SpellBuffClear(owner, nameof(Buffs.UrgotSwap2));
            }
        }
    }
}
namespace Spells
{
    public class UrgotSwap2 : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 100f, 85f, 70f, 55f, 40f, },
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        Particle gateParticle;
        int[] effect0 = {80, 105, 130};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            Vector3 targetPos;
            TeamId teamOfOwner;
            Particle nextBuffVars_GateParticle;
            Vector3 nextBuffVars_TargetPos;
            float nextBuffVars_DefInc;
            int defInc;
            float manaRefund;
            if(target is Champion)
            {
                targetPos = GetCastSpellTargetPos();
                teamOfOwner = GetTeamID(owner);
                SpellEffectCreate(out this.gateParticle, out _, "UrgotSwapTarget.troy", default, teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, target, default, default, target, "root", default, false, default, default, false, false);
                nextBuffVars_GateParticle = this.gateParticle;
                nextBuffVars_TargetPos = targetPos;
                FaceDirection(owner, targetPos);
                AddBuff((ObjAIBase)owner, owner, new Buffs.UrgotSwapMarker(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff((ObjAIBase)owner, target, new Buffs.UrgotSwapMarker(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff((ObjAIBase)target, owner, new Buffs.UrgotSwapMissile(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                defInc = this.effect0[level];
                nextBuffVars_DefInc = defInc;
                AddBuff(attacker, attacker, new Buffs.UrgotSwapDef(nextBuffVars_DefInc), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                BreakSpellShields(target);
                AddBuff(attacker, target, new Buffs.Suppression(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.SUPPRESSION, 0, true, false, false);
                AddBuff((ObjAIBase)owner, target, new Buffs.UrgotSwapTarget(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                AddBuff((ObjAIBase)target, owner, new Buffs.UrgotSwap2(nextBuffVars_GateParticle), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            else
            {
                SetSlotSpellCooldownTimeVer2(5, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
                manaRefund = 120;
                IncPAR(owner, manaRefund, PrimaryAbilityResourceType.MANA);
            }
        }
    }
}