#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class TwoShivPoison : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {40, 80, 120, 160, 200};
        float[] effect1 = {-0.2f, -0.225f, -0.25f, -0.275f, -0.3f};
        float[] effect2 = {0.2f, 0.225f, 0.25f, 0.275f, 0.3f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float attackDamage;
            float attackDamageMod;
            float backstabBonus;
            float nextBuffVars_MoveSpeedMod;
            float nextBuffVars_MissChance;
            attackDamage = GetTotalAttackDamage(owner);
            attackDamageMod = attackDamage * 0.5f;
            backstabBonus = 0;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.CastFromBehind)) > 0)
            {
                backstabBonus = 0.2f;
            }
            else
            {
                if(IsInFront(owner, target))
                {
                    if(IsBehind(target, owner))
                    {
                        backstabBonus = 0.2f;
                    }
                }
            }
            ApplyDamage(attacker, target, attackDamageMod + this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1 + backstabBonus, 1, 1, false, false, attacker);
            nextBuffVars_MoveSpeedMod = this.effect1[level];
            nextBuffVars_MissChance = this.effect2[level];
            AddBuff((ObjAIBase)owner, target, new Buffs.TwoShivPoison(nextBuffVars_MoveSpeedMod, nextBuffVars_MissChance), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class TwoShivPoison : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "global_slow.troy", },
            BuffName = "Two Shiv Poison",
            BuffTextureName = "Jester_IncrediblyPrecise.dds",
            PopupMessage = new[]{ "game_floatingtext_Slowed", },
        };
        float moveSpeedMod;
        float missChance;
        public TwoShivPoison(float moveSpeedMod = default, float missChance = default)
        {
            this.moveSpeedMod = moveSpeedMod;
            this.missChance = missChance;
        }
        public override void OnActivate()
        {
            //RequireVar(this.missChance);
            //RequireVar(this.moveSpeedMod);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, this.moveSpeedMod);
            if(owner is Champion)
            {
            }
            else
            {
                IncFlatMissChanceMod(owner, this.missChance);
            }
        }
    }
}