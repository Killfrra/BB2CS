#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinShamanAura : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            BuffName = "OdinShamanAura",
            BuffTextureName = "Sona_SongofDiscordGold.dds",
            PersistsThroughDeath = true,
        };
        Particle shamanAuraParticle;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.shamanAuraParticle, out _, "SonaSongofDiscord_aura.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.shamanAuraParticle);
        }
        public override void OnUpdateActions()
        {
            string unitName;
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted, true))
            {
                if(!owner.IsDead)
                {
                    foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.NotAffectSelf, default, true))
                    {
                        unitName = GetUnitSkinName(unit);
                        if(unitName == "Red_Minion_Melee")
                        {
                            AddBuff((ObjAIBase)owner, unit, new Buffs.OdinShamanBuff(), 1, 1, 0.25f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                        }
                        if(unitName == "Blue_Minion_Melee")
                        {
                            AddBuff((ObjAIBase)owner, unit, new Buffs.OdinShamanBuff(), 1, 1, 0.25f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                        }
                    }
                }
            }
        }
    }
}