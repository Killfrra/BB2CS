#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SejuaniGlacialPrison : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "head", },
            AutoBuffActivateEffect = new[]{ "Stun_glb.troy", },
            BuffName = "SejuaniGlacialPrison",
            BuffTextureName = "Sejuani_GlacialPrison.dds",
            PopupMessage = new[]{ "game_floatingtext_Stunned", },
        };
        Particle crystalineParticle;
        public override void OnActivate()
        {
            ObjAIBase caster;
            TeamId teamID;
            caster = SetBuffCasterUnit();
            teamID = GetTeamID(caster);
            SetStunned(owner, true);
            PauseAnimation(owner, true);
            SpellEffectCreate(out this.crystalineParticle, out _, "sejuani_ult_tar_03.troy", default, teamID, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, owner, "BUFFBONE_GLB_GROUND_LOC", default, attacker, "Bird_head", default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetStunned(owner, false);
            PauseAnimation(owner, false);
            SpellEffectRemove(this.crystalineParticle);
        }
        public override void OnUpdateStats()
        {
            SetStunned(owner, true);
            PauseAnimation(owner, true);
        }
    }
}
namespace Spells
{
    public class SejuaniGlacialPrison : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {150, 250, 350};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            bool isStealthed;
            float stunDuration;
            float prisonDamage;
            bool canSee;
            isStealthed = GetStealthed(target);
            stunDuration = 2;
            prisonDamage = this.effect0[level];
            if(!isStealthed)
            {
                AddBuff(attacker, target, new Buffs.SejuaniGlacialPrisonCheck(), 1, 1, stunDuration, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                DestroyMissile(missileNetworkID);
                BreakSpellShields(target);
                AddBuff(attacker, target, new Buffs.SejuaniGlacialPrison(), 1, 1, stunDuration, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                ApplyDamage(attacker, target, prisonDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.8f, 1, false, false, attacker);
                SpellCast(attacker, target, default, default, 1, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
            }
            else
            {
                if(target is Champion)
                {
                    AddBuff(attacker, target, new Buffs.SejuaniGlacialPrisonCheck(), 1, 1, stunDuration, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    DestroyMissile(missileNetworkID);
                    BreakSpellShields(target);
                    AddBuff(attacker, target, new Buffs.SejuaniGlacialPrison(), 1, 1, stunDuration, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                    ApplyDamage(attacker, target, prisonDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.8f, 1, false, false, attacker);
                    SpellCast(attacker, target, default, default, 1, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
                }
                else
                {
                    canSee = CanSeeTarget(owner, target);
                    if(canSee)
                    {
                        AddBuff(attacker, target, new Buffs.SejuaniGlacialPrisonCheck(), 1, 1, stunDuration, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        DestroyMissile(missileNetworkID);
                        BreakSpellShields(target);
                        AddBuff(attacker, target, new Buffs.SejuaniGlacialPrison(), 1, 1, stunDuration, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                        ApplyDamage(attacker, target, prisonDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.8f, 1, false, false, attacker);
                        SpellCast(attacker, target, default, default, 1, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
                    }
                }
            }
        }
    }
}