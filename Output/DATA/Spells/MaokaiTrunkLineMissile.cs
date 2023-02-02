#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class MaokaiTrunkLineMissile : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {70, 115, 160, 205, 250};
        float[] effect1 = {-0.2f, -0.25f, -0.3f, -0.35f, -0.4f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            if(GetBuffCountFromCaster(target, owner, nameof(Buffs.MaokaiTrunkLine)) == 0)
            {
                float baseDamage;
                float nextBuffVars_MoveSpeedMod;
                TeamId casterID;
                bool isStealthed;
                Particle asdf; // UNUSED
                level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                baseDamage = this.effect0[level];
                nextBuffVars_MoveSpeedMod = this.effect1[level];
                casterID = GetTeamID(attacker);
                isStealthed = GetStealthed(target);
                if(!isStealthed)
                {
                    AddBuff((ObjAIBase)owner, target, new Buffs.MaokaiTrunkLine(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    BreakSpellShields(target);
                    SpellEffectCreate(out asdf, out _, "maoki_trunkSmash_unit_tar_02.troy", default, casterID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
                    SpellEffectCreate(out asdf, out _, "maoki_trunkSmash_unit_tar.troy", default, casterID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
                    ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 1, false, false, attacker);
                    AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, true, false);
                }
                else
                {
                    if(target is Champion)
                    {
                        AddBuff((ObjAIBase)owner, target, new Buffs.MaokaiTrunkLine(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        BreakSpellShields(target);
                        SpellEffectCreate(out asdf, out _, "maoki_trunkSmash_unit_tar_02.troy", default, casterID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
                        SpellEffectCreate(out asdf, out _, "maoki_trunkSmash_unit_tar.troy", default, casterID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
                        ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 1, false, false, attacker);
                        AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, true, false);
                    }
                    else
                    {
                        bool canSee;
                        canSee = CanSeeTarget(owner, target);
                        if(canSee)
                        {
                            AddBuff((ObjAIBase)owner, target, new Buffs.MaokaiTrunkLine(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                            BreakSpellShields(target);
                            SpellEffectCreate(out asdf, out _, "maoki_trunkSmash_unit_tar_02.troy", default, casterID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
                            SpellEffectCreate(out asdf, out _, "maoki_trunkSmash_unit_tar.troy", default, casterID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
                            ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 1, false, false, attacker);
                            AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, true, false);
                        }
                    }
                }
            }
        }
    }
}