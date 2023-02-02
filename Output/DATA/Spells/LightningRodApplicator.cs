#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LightningRodApplicator : BBBuffScript
    {
        float attackCounter;
        Particle particleID; // UNUSED
        public override void OnActivate()
        {
            this.attackCounter = 0;
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            TeamId teamID;
            teamID = GetTeamID(owner);
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    if(hitResult != HitResult.HIT_Dodge)
                    {
                        if(hitResult != HitResult.HIT_Miss)
                        {
                            if(this.attackCounter == 3)
                            {
                                ObjAIBase caster; // UNUSED
                                int nextBuffVars_BounceCounter;
                                caster = SetBuffCasterUnit();
                                if(attacker is not Champion)
                                {
                                    caster = GetPetOwner((Pet)attacker);
                                }
                                SpellEffectCreate(out this.particleID, out _, "kennen_btl_beam.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, target, false, attacker, "head", default, target, "root", default, true, false, false, false, false);
                                nextBuffVars_BounceCounter = 1;
                                AddBuff(attacker, target, new Buffs.LightningRodChain(nextBuffVars_BounceCounter), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                                this.attackCounter = 0;
                            }
                            else
                            {
                                this.attackCounter++;
                            }
                        }
                    }
                }
            }
        }
    }
}