#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MoltenShield : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "GhastlyShield_buf.troy", },
            BuffName = "Molten Shield",
            BuffTextureName = "Annie_GhastlyShield.dds",
        };
        float damageReturn;
        float armorAmount;
        public MoltenShield(float damageReturn = default, float armorAmount = default)
        {
            this.damageReturn = damageReturn;
            this.armorAmount = armorAmount;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageReturn);
            //RequireVar(this.armorAmount);
        }
        public override void OnUpdateStats()
        {
            IncFlatSpellBlockMod(owner, this.armorAmount);
            IncFlatArmorMod(owner, this.armorAmount);
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            Particle particle; // UNUSED
            if(owner.Team != attacker.Team)
            {
                if(damageSource == default)
                {
                    SpellEffectCreate(out particle, out _, "AnnieSparks.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, target, default, default, false);
                    ApplyDamage((ObjAIBase)owner, attacker, this.damageReturn, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.2f, 1, false, false, (ObjAIBase)owner);
                }
            }
        }
    }
}
namespace Spells
{
    public class MoltenShield : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {20, 30, 40, 50, 60};
        int[] effect1 = {10, 20, 30, 40, 50};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_DamageReturn;
            float nextBuffVars_ArmorAmount;
            nextBuffVars_DamageReturn = 1 + this.effect0[level];
            nextBuffVars_ArmorAmount = this.effect1[level];
            AddBuff(attacker, target, new Buffs.MoltenShield(nextBuffVars_DamageReturn, nextBuffVars_ArmorAmount), 1, 1, 15, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Pyromania_particle)) == 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.Pyromania(), 5, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false);
            }
        }
    }
}