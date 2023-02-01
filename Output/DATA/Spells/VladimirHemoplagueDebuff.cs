#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VladimirHemoplagueDebuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "VladimirHemoplagueDebuff",
            BuffTextureName = "Vladimir_Hemoplague.dds",
            SpellFXOverrideSkins = new[]{ "BloodkingVladimir", },
        };
        float damageIncrease;
        float damagePerLevel;
        int vladSkinID;
        Particle varrr1;
        public VladimirHemoplagueDebuff(float damageIncrease = default, float damagePerLevel = default)
        {
            this.damageIncrease = damageIncrease;
            this.damagePerLevel = damagePerLevel;
        }
        public override void OnActivate()
        {
            ObjAIBase caster;
            caster = SetBuffCasterUnit();
            //RequireVar(this.damageIncrease);
            //RequireVar(this.damagePerLevel);
            ApplyAssistMarker(attacker, owner, 10);
            this.vladSkinID = GetSkinID(caster);
            if(this.vladSkinID == 5)
            {
                SpellEffectCreate(out this.varrr1, out _, "VladHemoplague_BloodKing_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "spine", default, owner, default, default, false, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out this.varrr1, out _, "VladHemoplague_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "spine", default, owner, default, default, false, false, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            Particle varrr; // UNUSED
            if(this.vladSkinID == 5)
            {
                SpellEffectCreate(out varrr, out _, "VladHemoplague_BloodKing_proc.troy", default, TeamId.TEAM_NEUTRAL, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out varrr, out _, "VladHemoplague_proc.troy", default, TeamId.TEAM_NEUTRAL, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            }
            if(expired)
            {
                ApplyDamage(attacker, owner, this.damagePerLevel, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 1, false, false, attacker);
            }
            SpellEffectRemove(this.varrr1);
        }
        public override void OnUpdateStats()
        {
            IncPercentPhysicalReduction(owner, this.damageIncrease);
            IncPercentMagicReduction(owner, this.damageIncrease);
        }
    }
}