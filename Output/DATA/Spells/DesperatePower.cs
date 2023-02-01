#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class DesperatePower : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "DesperatePower",
            BuffTextureName = "Ryze_DesperatePower.dds",
            NonDispellable = true,
        };
        float vamp;
        int level;
        Particle asdf;
        public DesperatePower(float vamp = default, int level = default)
        {
            this.vamp = vamp;
            this.level = level;
        }
        public override void OnActivate()
        {
            int level; // UNUSED
            TeamId teamID;
            //RequireVar(this.vamp);
            //RequireVar(this.level);
            level = this.level;
            IncPercentSpellVampMod(owner, this.vamp);
            teamID = GetTeamID(owner);
            SpellEffectCreate(out this.asdf, out _, "ManaLeach_tar2.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            int level; // UNUSED
            SpellEffectRemove(this.asdf);
            level = this.level;
        }
        public override void OnUpdateStats()
        {
            IncPercentSpellVampMod(owner, this.vamp);
        }
    }
}
namespace Spells
{
    public class DesperatePower : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {0.15f, 0.2f, 0.25f};
        int[] effect1 = {5, 6, 7};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_Vamp;
            int nextBuffVars_Level;
            nextBuffVars_Vamp = this.effect0[level];
            nextBuffVars_Level = level;
            AddBuff(attacker, target, new Buffs.DesperatePower(nextBuffVars_Vamp, nextBuffVars_Level), 1, 1, this.effect1[level], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}