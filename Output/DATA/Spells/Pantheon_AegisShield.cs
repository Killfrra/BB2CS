#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Pantheon_AegisShield : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "C_BUFFBONE_GLB_CENTER_LOC", },
            AutoBuffActivateEffect = new[]{ "pantheon_aoz_passive.troy", },
            BuffName = "PantheonAegisShield",
            BuffTextureName = "Pantheon_AOZ.dds",
        };
        int damageThreshold;
        int[] effect0 = {40, 43, 46, 49, 52, 55, 58, 61, 64, 67, 70, 73, 76, 79, 82, 85, 88, 91, 94, 97, 100};
        public override void OnActivate()
        {
            this.damageThreshold = 40;
            OverrideAnimation("Run", "Run2", owner);
        }
        public override void OnBeingHit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(hitResult != HitResult.HIT_Dodge)
            {
                if(hitResult != HitResult.HIT_Miss)
                {
                    if(this.damageThreshold <= damageAmount)
                    {
                        damageAmount = 0;
                        Say(attacker, "game_lua_Aegis_Block");
                        Say(owner, "game_lua_Aegis_Block");
                        SpellBuffRemove(owner, nameof(Buffs.Pantheon_AegisShield), (ObjAIBase)owner);
                    }
                }
            }
        }
        public override void OnLevelUp()
        {
            int level;
            level = GetLevel(owner);
            this.damageThreshold = this.effect0[level];
        }
        public override void OnDeactivate(bool expired)
        {
            ClearOverrideAnimation("Run", owner);
        }
    }
}