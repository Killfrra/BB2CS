#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CaitlynEntrapmentMissile : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "caitlyn_entrapment_slow.troy", },
            BuffName = "",
            BuffTextureName = "",
        };
    }
}
namespace Spells
{
    public class CaitlynEntrapmentMissile : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        float[] effect0 = {1, 1.25f, 1.5f, 1.75f, 2};
        float[] effect1 = {1, 1.25f, 1.5f, 1.75f, 2};
        int[] effect2 = {80, 130, 180, 230, 280};
        float[] effect3 = {1, 1.25f, 1.5f, 1.75f, 2};
        float[] effect4 = {1, 1.25f, 1.5f, 1.75f, 2};
        int[] effect5 = {80, 130, 180, 230, 280};
        float[] effect6 = {1, 1.25f, 1.5f, 1.75f, 2};
        float[] effect7 = {1, 1.25f, 1.5f, 1.75f, 2};
        int[] effect8 = {80, 130, 180, 230, 280};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            float nextBuffVars_MoveSpeedMod;
            bool isStealthed;
            Particle asdf; // UNUSED
            bool canSee;
            teamID = GetTeamID(owner);
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            nextBuffVars_MoveSpeedMod = -0.5f;
            isStealthed = GetStealthed(target);
            if(!isStealthed)
            {
                BreakSpellShields(target);
                AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, this.effect0[level], BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                AddBuff(attacker, target, new Buffs.CaitlynEntrapmentMissile(), 100, 1, this.effect1[level], BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, true);
                ApplyDamage(attacker, target, this.effect2[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.8f, 1, false, false, attacker);
                SpellEffectCreate(out asdf, out _, "caitlyn_entrapment_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, owner, default, default, true);
                DestroyMissile(missileNetworkID);
            }
            else
            {
                if(target is Champion)
                {
                    BreakSpellShields(target);
                    AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, this.effect3[level], BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                    AddBuff(attacker, target, new Buffs.CaitlynEntrapmentMissile(), 100, 1, this.effect4[level], BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, true);
                    ApplyDamage(attacker, target, this.effect5[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.8f, 1, false, false, attacker);
                    SpellEffectCreate(out asdf, out _, "caitlyn_entrapment_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, owner, default, default, true);
                    DestroyMissile(missileNetworkID);
                }
                else
                {
                    canSee = CanSeeTarget(owner, target);
                    if(canSee)
                    {
                        BreakSpellShields(target);
                        AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, this.effect6[level], BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                        AddBuff(attacker, target, new Buffs.CaitlynEntrapmentMissile(), 100, 1, this.effect7[level], BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, true);
                        ApplyDamage(attacker, target, this.effect8[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.8f, 1, false, false, attacker);
                        SpellEffectCreate(out asdf, out _, "caitlyn_entrapment_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, owner, default, default, true);
                        DestroyMissile(missileNetworkID);
                    }
                }
            }
        }
    }
}