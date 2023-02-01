#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class MonkeyKingDecoySwipe : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = {70, 115, 160, 205, 250};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float baseDamage;
            TeamId teamID;
            Champion caster;
            float monkeyKingAP;
            float damageToDeal;
            baseDamage = this.effect0[level];
            teamID = GetTeamID(owner);
            caster = GetChampionBySkinName("MonkeyKing", teamID);
            monkeyKingAP = GetFlatMagicDamageMod(caster);
            monkeyKingAP *= 0.6f;
            damageToDeal = baseDamage + monkeyKingAP;
            ApplyDamage(caster, target, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, true, false, caster);
        }
    }
}