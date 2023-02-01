#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Ravage : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "RendingShot_buf.troy", },
            BuffName = "Ravage",
            BuffTextureName = "Evelynn_Ravage.dds",
        };
        float armorMod;
        public Ravage(float armorMod = default)
        {
            this.armorMod = armorMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.armorMod);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, this.armorMod);
            IncFlatSpellBlockMod(owner, this.armorMod);
        }
    }
}
namespace Spells
{
    public class Ravage : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {80, 135, 190, 255, 320};
        int[] effect1 = {-10, -14, -18, -22, -26};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_ArmorMod;
            ApplyDamage(attacker, target, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 1, 1, false, false, attacker);
            nextBuffVars_ArmorMod = this.effect1[level];
            AddBuff(attacker, target, new Buffs.Ravage(nextBuffVars_ArmorMod), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.SHRED, 0, true, false);
        }
    }
}