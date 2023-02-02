#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class IreliaTranscendentBladesSpell : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {80, 120, 160};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float baseDamage;
            float physPreMod;
            float physPostMod;
            float aPPreMod;
            float aPPostMod;
            TeamId ireliaTeamID;
            Particle a; // UNUSED
            float damageToDeal1;
            float damageToDeal2;
            float nextBuffVars_DrainPercent;
            BreakSpellShields(target);
            baseDamage = this.effect0[level];
            physPreMod = GetFlatPhysicalDamageMod(owner);
            physPostMod = 0.6f * physPreMod;
            aPPreMod = GetFlatMagicDamageMod(owner);
            aPPostMod = 0.5f * aPPreMod;
            ireliaTeamID = GetTeamID(owner);
            SpellEffectCreate(out a, out _, "irelia_ult_tar.troy", default, ireliaTeamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, target, false, target, "root", default, target, default, default, true, default, default, false, false);
            damageToDeal1 = physPostMod + baseDamage;
            damageToDeal2 = aPPostMod + damageToDeal1;
            if(target is Champion)
            {
                nextBuffVars_DrainPercent = 0.25f;
            }
            else
            {
                nextBuffVars_DrainPercent = 0.1f;
            }
            AddBuff((ObjAIBase)owner, owner, new Buffs.GlobalDrain(nextBuffVars_DrainPercent), 1, 1, 0.01f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            ApplyDamage(attacker, target, damageToDeal2, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
        }
    }
}
namespace Buffs
{
    public class IreliaTranscendentBladesSpell : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "IreliaTranscendentBlades",
            BuffTextureName = "Irelia_TranscendentBladesReady.dds",
        };
    }
}