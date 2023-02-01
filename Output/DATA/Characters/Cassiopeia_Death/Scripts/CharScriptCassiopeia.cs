#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptCassiopeia : BBCharScript
    {
        float lastTimeExecuted;
        Particle particle; // UNUSED
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.4f, ref this.lastTimeExecuted, false))
            {
                if(owner.IsDead)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.CassiopeiaDeathParticle(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
                else
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.CassiopeiaDeathParticle)) > 0)
                    {
                        SpellBuffRemove(owner, nameof(Buffs.CassiopeiaDeathParticle), (ObjAIBase)owner);
                    }
                }
            }
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            if(!spellVars.DoesntTriggerSpellCasts)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.CassiopeiaDeadlyCadence(), 5, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false);
                SpellEffectCreate(out this.particle, out _, "CassDeadlyCadence_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, attacker, "root", default, attacker, default, default, false);
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            charVars.SecondSkinArmor = 11;
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
        public override void OnResurrect()
        {
            SpellBuffRemove(owner, nameof(Buffs.CassiopeiaDeathParticle), (ObjAIBase)owner);
        }
    }
}