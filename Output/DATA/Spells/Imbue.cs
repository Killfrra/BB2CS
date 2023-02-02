#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Imbue : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = {60, 100, 140, 180, 220};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float baseHealAmount;
            float aP;
            float aPMod;
            float healAmount;
            baseHealAmount = this.effect0[level];
            aP = GetFlatMagicDamageMod(owner);
            aPMod = 0.6f * aP;
            healAmount = baseHealAmount + aPMod;
            if(target == owner)
            {
                healAmount *= 1.4f;
                IncHealth(owner, healAmount, owner);
            }
            else
            {
                float temp1;
                Particle self; // UNUSED
                IncHealth(owner, healAmount, owner);
                temp1 = GetHealthPercent(target, PrimaryAbilityResourceType.MANA);
                if(temp1 < 1)
                {
                    IncHealth(target, healAmount, owner);
                    ApplyAssistMarker(attacker, target, 10);
                }
                SpellEffectCreate(out self, out _, "Global_Heal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            }
        }
    }
}