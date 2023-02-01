#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BrandFissureRoot : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SwainShadowGraspRoot",
            BuffTextureName = "SwainNevermove.dds",
            PopupMessage = new[]{ "game_floatingtext_Snared", },
        };
        float mRDebuff;
        Particle rootParticleEffect2;
        Particle rootParticleEffect;
        int[] effect0 = {0, 0, 0};
        public override void OnActivate()
        {
            int level;
            TeamId teamOfOwner; // UNUSED
            level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.SwainMetamorphism)) > 0)
            {
                this.mRDebuff = this.effect0[level];
            }
            else
            {
                this.mRDebuff = 0;
            }
            teamOfOwner = GetTeamID(owner);
            SetCanMove(owner, false);
            SpellEffectCreate(out this.rootParticleEffect2, out _, "SwainShadowGraspRootTemp.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
            SpellEffectCreate(out this.rootParticleEffect, out _, "swain_shadowGrasp_magic.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanMove(owner, true);
            SpellEffectRemove(this.rootParticleEffect2);
            SpellEffectRemove(this.rootParticleEffect);
        }
        public override void OnUpdateStats()
        {
            IncFlatSpellBlockMod(owner, this.mRDebuff);
            SetCanMove(owner, false);
        }
    }
}