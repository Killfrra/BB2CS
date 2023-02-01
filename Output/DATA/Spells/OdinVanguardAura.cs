#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinVanguardAura : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Divineblessing_buf.troy", },
            BuffName = "VanguardAura",
            BuffTextureName = "Summoner_rally.dds",
        };
        float bonusHealth;
        float lastTimeExecuted;
        public OdinVanguardAura(float bonusHealth = default)
        {
            this.bonusHealth = bonusHealth;
        }
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            returnValue = false;
            return returnValue;
        }
        public override void OnActivate()
        {
            Particle ar; // UNUSED
            //RequireVar(this.bonusHealth);
            SpellEffectCreate(out ar, out _, "summoner_flash.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false);
            SetTargetable(owner, false);
            SetMagicImmune(owner, true);
            IncPermanentFlatHPPoolMod(owner, this.bonusHealth);
        }
        public override void OnDeactivate(bool expired)
        {
            SetTargetable(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 5000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted, false))
            {
                AddBuffToEachUnitInArea(owner, owner.Position, 850, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, attacker, new Buffs.OdinVanguardAuraBuff(), BuffAddType.RENEW_EXISTING, BuffType.AURA, 1, 1, 0.5f, 0, false, true);
            }
        }
    }
}