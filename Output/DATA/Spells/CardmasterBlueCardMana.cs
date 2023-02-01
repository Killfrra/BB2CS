#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CardmasterBlueCardMana : BBBuffScript
    {
        bool hasDealtDamage;
        public override void OnActivate()
        {
            this.hasDealtDamage = false;
        }
        public override void OnPreDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            TeamId teamID;
            ObjAIBase caster;
            Particle a; // UNUSED
            float manaRestore;
            if(!this.hasDealtDamage)
            {
                if(damageSource != default)
                {
                    if(target is ObjAIBase)
                    {
                        teamID = GetTeamID(owner);
                        caster = SetBuffCasterUnit();
                        SpellEffectCreate(out a, out _, "soraka_infuse_ally_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, attacker, default, default, true, default, default, false, false);
                        SpellEffectCreate(out a, out _, "soraka_infuse_ally_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, caster, default, default, attacker, default, default, true, default, default, false, false);
                        manaRestore = damageAmount * 0.65f;
                        IncPAR(owner, manaRestore, PrimaryAbilityResourceType.MANA);
                        this.hasDealtDamage = true;
                    }
                }
            }
        }
    }
}