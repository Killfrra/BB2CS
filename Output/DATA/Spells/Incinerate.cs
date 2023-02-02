#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Incinerate : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = {80, 130, 180, 230, 280};
        public override void SelfExecute()
        {
            int count;
            charVars.SpellWillStun = false;
            count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.Pyromania_particle));
            if(count >= 1)
            {
                charVars.SpellWillStun = true;
                SpellBuffRemove(owner, nameof(Buffs.Pyromania_particle), (ObjAIBase)owner, 0);
            }
            AddBuff((ObjAIBase)owner, owner, new Buffs.Pyromania(), 5, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            int annieSkinID;
            Particle a; // UNUSED
            ApplyDamage(attacker, target, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.75f, 0, false, false, attacker);
            if(charVars.SpellWillStun)
            {
                ApplyStun(attacker, target, charVars.StunDuration);
            }
            teamID = GetTeamID(owner);
            annieSkinID = GetSkinID(owner);
            if(annieSkinID == 5)
            {
                SpellEffectCreate(out a, out _, "Incinerate_buf_frost.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
            }
            else
            {
                SpellEffectCreate(out a, out _, "Incinerate_buf.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
            }
        }
    }
}