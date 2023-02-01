#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GarenJusticePreCast : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "root", },
            AutoBuffActivateEffect = new[]{ "dr_mundo_burning_agony_cas_02.troy", },
        };
        Particle kIRHand;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.kIRHand, out _, "garen_damacianJustice_cas_instant.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false, false);
            SpellEffectCreate(out this.kIRHand, out _, "garen_damacianJustice_cas_sword.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_hand", default, target, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.kIRHand);
        }
    }
}
namespace Spells
{
    public class GarenJusticePreCast : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.GarenBladestorm)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.GarenBladestorm), (ObjAIBase)owner, 0);
            }
            AddBuff((ObjAIBase)owner, owner, new Buffs.GarenJusticePreCast(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellCast((ObjAIBase)owner, target, default, default, 1, SpellSlotType.ExtraSlots, level, false, false, false, false, false, false);
        }
    }
}