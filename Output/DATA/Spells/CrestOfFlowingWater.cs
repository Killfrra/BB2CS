#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CrestOfFlowingWater : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Crest Of Flowing Water",
            BuffTextureName = "WaterWizard_Typhoon.dds",
            NonDispellable = true,
        };
        Particle buffParticle;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.buffParticle, out _, "invis_runes_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            IncPermanentPercentMovementSpeedMod(owner, 0.3f);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.buffParticle);
            IncPermanentPercentMovementSpeedMod(owner, -0.3f);
        }
        public override void OnDeath()
        {
            int count;
            float newDuration;
            ObjAIBase caster;
            count = GetBuffCountFromAll(attacker, nameof(Buffs.APBonusDamageToTowers));
            newDuration = 60;
            if(attacker is Champion)
            {
                if(!attacker.IsDead)
                {
                    if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.MonsterBuffs)) > 0)
                    {
                        newDuration *= 1.2f;
                    }
                    AddBuff(attacker, attacker, new Buffs.CrestOfFlowingWater(), 1, 1, newDuration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
            else if(count != 0)
            {
                caster = GetPetOwner((Pet)attacker);
                if(caster is Champion)
                {
                    if(!caster.IsDead)
                    {
                        if(GetBuffCountFromCaster(caster, caster, nameof(Buffs.MonsterBuffs)) > 0)
                        {
                            newDuration *= 1.2f;
                        }
                        AddBuff(caster, caster, new Buffs.CrestOfFlowingWater(), 1, 1, newDuration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    }
                }
            }
        }
    }
}