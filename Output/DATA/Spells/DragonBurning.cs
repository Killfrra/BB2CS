#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class DragonBurning : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Cape_neg_buf.troy", "GLOBAL_SLOW.TROY", },
            BuffName = "DragonBurning",
            BuffTextureName = "Annie_Incinerate.dds",
            IsDeathRecapSource = true,
            PopupMessage = new[]{ "game_floatingtext_Slowed", },
        };
        float tickDamage;
        float attackSpeedMod;
        float lastTimeExecuted;
        public DragonBurning(float tickDamage = default, float attackSpeedMod = default)
        {
            this.tickDamage = tickDamage;
            this.attackSpeedMod = attackSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.tickDamage);
            //RequireVar(this.attackSpeedMod);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeAttackSpeedMod(owner, this.attackSpeedMod);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                ApplyDamage(attacker, owner, this.tickDamage, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
            }
        }
    }
}