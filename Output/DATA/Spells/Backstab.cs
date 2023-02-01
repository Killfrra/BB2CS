#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Backstab : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Backstab",
            BuffTextureName = "Jester_CarefulStrikes.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float[] effect0 = {-0.2f, -0.225f, -0.25f, -0.275f, -0.3f};
        float[] effect1 = {0.2f, 0.225f, 0.25f, 0.275f, 0.3f};
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            spellName = GetSpellName();
            if(spellName == nameof(Spells.TwoShivPoison))
            {
                if(IsInFront(owner, target))
                {
                    if(IsBehind(target, owner))
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.CastFromBehind(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            Particle particle; // UNUSED
            TeamId teamID;
            int level;
            float time;
            float nextBuffVars_MoveSpeedMod;
            float nextBuffVars_MissChance;
            if(target is ObjAIBase)
            {
                if(target is BaseTurret)
                {
                }
                else
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.FromBehind)) > 0)
                    {
                        damageAmount *= 1.2f;
                        SpellEffectCreate(out particle, out _, "AbsoluteZero_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, false, false, false, false);
                    }
                    else
                    {
                        if(IsInFront(owner, target))
                        {
                            if(IsBehind(target, owner))
                            {
                                damageAmount *= 1.2f;
                                SpellEffectCreate(out particle, out _, "AbsoluteZero_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, false, false, false, false);
                            }
                        }
                    }
                    teamID = GetTeamID(owner);
                    attacker = GetChampionBySkinName("Shaco", teamID);
                    level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    time = GetSlotSpellCooldownTime(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if(level >= 1)
                    {
                        if(time <= 0)
                        {
                            if(hitResult != HitResult.HIT_Dodge)
                            {
                                if(hitResult != HitResult.HIT_Miss)
                                {
                                    nextBuffVars_MoveSpeedMod = this.effect0[level];
                                    nextBuffVars_MissChance = this.effect1[level];
                                    AddBuff(attacker, target, new Buffs.TwoShivPoison(nextBuffVars_MoveSpeedMod, nextBuffVars_MissChance), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
                                }
                            }
                        }
                    }
                }
            }
        }
        public override void OnPreAttack()
        {
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    if(IsInFront(owner, target))
                    {
                        if(IsBehind(target, owner))
                        {
                            AddBuff((ObjAIBase)owner, owner, new Buffs.FromBehind(), 1, 1, 0.75f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        }
                    }
                }
            }
        }
    }
}