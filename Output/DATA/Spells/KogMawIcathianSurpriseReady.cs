#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class KogMawIcathianSurpriseReady : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "KogMawIcathianSurpriseReady",
            BuffTextureName = "KogMaw_IcathianSurprise.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        Particle a;
        public override void OnActivate()
        {
            TeamId teamID;
            int kogMawSkinID;
            teamID = GetTeamID(owner);
            kogMawSkinID = GetSkinID(attacker);
            if(kogMawSkinID == 4)
            {
                SpellEffectCreate(out this.a, out _, "KogNoseGlow.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_nose", default, owner, default, default, false, false, false, false, false);
            }
            else if(kogMawSkinID == 6)
            {
                SpellEffectCreate(out this.a, out _, "Kogmaw_deepsea_glow.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_ANGLER", default, owner, default, default, false, false, false, false, false);
            }
        }
        public override void OnDeath()
        {
            bool becomeZombie; // UNUSED
            if(owner is Champion)
            {
                becomeZombie = true;
            }
        }
        public override void OnZombie()
        {
            int kogMawSkinID;
            kogMawSkinID = GetSkinID(owner);
            if(kogMawSkinID == 4)
            {
                SpellEffectRemove(this.a);
            }
            else if(kogMawSkinID == 6)
            {
                SpellEffectRemove(this.a);
            }
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.KogMawIcathianSurprise)) == 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.KogMawIcathianSurprise(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.KogMawIcathianSurprise)) > 0)
            {
                damageAmount = 0;
            }
        }
    }
}