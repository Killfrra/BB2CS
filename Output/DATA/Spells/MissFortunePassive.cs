#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MissFortunePassive : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MissFortunePassive",
            BuffTextureName = "MissFortune_ImpureShots.dds",
            IsDeathRecapSource = true,
            PersistsThroughDeath = true,
        };
        float damageCounter;
        int[] effect0 = {6, 8, 10, 12, 14};
        public override void OnActivate()
        {
            int level;
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            this.damageCounter = this.effect0[level];
            //RequireVar(this.missFortunePassive);
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(hitResult != HitResult.HIT_Dodge)
            {
                if(hitResult != HitResult.HIT_Miss)
                {
                    if(target is ObjAIBase)
                    {
                        if(target is not BaseTurret)
                        {
                            float aPMod;
                            float moddedDmg;
                            float preCount;
                            TeamId teamID;
                            int count;
                            float damageDealt;
                            Particle asdf; // UNUSED
                            aPMod = GetFlatMagicDamageMod(owner);
                            moddedDmg = aPMod * 0.05f;
                            preCount = moddedDmg + this.damageCounter;
                            teamID = GetTeamID(owner);
                            AddBuff((ObjAIBase)owner, target, new Buffs.MissFortunePassiveStack(), 4, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.DAMAGE, 0, false, false, false);
                            count = GetBuffCountFromAll(target, nameof(Buffs.MissFortunePassiveStack));
                            damageDealt = preCount * count;
                            ApplyDamage((ObjAIBase)owner, target, damageDealt, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, (ObjAIBase)owner);
                            SpellEffectCreate(out asdf, out _, "missFortune_passive_tar_indicator.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                        }
                    }
                }
            }
        }
    }
}