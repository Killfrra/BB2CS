#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class H28GEvolutionTurretSpell3 : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "H28GEvolutionTurret",
            BuffTextureName = "Jester_DeathWard.dds",
        };
        float lastAttackTime;
        float retaunts;
        public override void OnActivate()
        {
            ApplyTaunt(attacker, owner, 25000);
            ApplyDamage((ObjAIBase)owner, attacker, 0, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, false, (ObjAIBase)owner);
            this.lastAttackTime = GetGameTime();
            this.retaunts = 0;
        }
        public override void OnDeactivate(bool expired)
        {
            SpellBuffClear(owner, nameof(Buffs.Taunt));
        }
        public override void OnUpdateActions()
        {
            float distance;
            bool targetable;
            float curTime;
            float timeElapsed;
            distance = DistanceBetweenObjects("Attacker", "Owner");
            targetable = GetTargetable(attacker);
            if(distance > 625)
            {
                SpellBuffRemoveCurrent(owner);
            }
            else if(attacker.IsDead)
            {
                SpellBuffRemoveCurrent(owner);
            }
            else if(!targetable)
            {
                SpellBuffRemoveCurrent(owner);
            }
            curTime = GetGameTime();
            timeElapsed = curTime - this.lastAttackTime;
            if(timeElapsed >= 0.75f)
            {
                if(this.retaunts == 0)
                {
                    ApplyTaunt(attacker, owner, 250);
                    this.retaunts++;
                }
                else
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
        public override void OnPreAttack()
        {
            this.lastAttackTime = GetGameTime();
        }
    }
}