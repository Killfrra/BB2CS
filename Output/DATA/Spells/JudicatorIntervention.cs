#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class JudicatorIntervention : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 90f, 90f, 90f, 90f, 90f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {2, 2.5f, 3};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.KayleInterventionAnim(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, target, new Buffs.JudicatorIntervention(), 1, 1, this.effect0[level], BuffAddType.RENEW_EXISTING, BuffType.INVULNERABILITY, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class JudicatorIntervention : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "JudicatorIntervention",
            BuffTextureName = "Judicator_EyeforanEye.dds",
        };
        Particle self;
        Particle cas;
        public override void OnActivate()
        {
            SetInvulnerable(owner, true);
            if(attacker == owner)
            {
                SpellEffectCreate(out this.self, out _, "eyeforaneye_self.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false, false);
            }
            else
            {
                SpellEffectCreate(out this.cas, out _, "eyeforaneye_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false, false);
            }
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SetInvulnerable(owner, false);
            if(attacker == owner)
            {
                SpellEffectRemove(this.self);
            }
            else
            {
                SpellEffectRemove(this.cas);
            }
        }
        public override void OnUpdateStats()
        {
            SetInvulnerable(owner, true);
        }
    }
}