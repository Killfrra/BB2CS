#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class BlindingDart : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = {1.5f, 1.75f, 2, 2.25f, 2.5f};
        int[] effect1 = {80, 125, 170, 215, 260};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            AddBuff(attacker, target, new Buffs.BlindingDart(), 100, 1, this.effect0[level], BuffAddType.STACKS_AND_OVERLAPS, BuffType.BLIND, 0, true, false);
            ApplyDamage(attacker, target, this.effect1[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.8f, 1, false, false, attacker);
        }
    }
}
namespace Buffs
{
    public class BlindingDart : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "head", },
            AutoBuffActivateEffect = new[]{ "Global_miss.troy", },
            BuffName = "Blind",
            BuffTextureName = "Teemo_TranquilizingShot.dds",
            PopupMessage = new[]{ "game_floatingtext_Blinded", },
            SpellFXOverrideSkins = new[]{ "AstronautTeemo", },
        };
        public override void OnActivate()
        {
            IncFlatMissChanceMod(owner, 1);
        }
        public override void OnUpdateStats()
        {
            IncFlatMissChanceMod(owner, 1);
        }
    }
}