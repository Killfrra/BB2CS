#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class UpgradeSlow : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "UpgradeSlow",
            BuffTextureName = "Heimerdinger_UPGRADE.dds",
        };
        Particle frostTurrets;
        bool willPop;
        int redShift;
        public override void OnActivate()
        {
            TeamId teamID;
            teamID = GetTeamID(owner);
            SpellEffectCreate(out this.frostTurrets, out _, "heimerdinger_slowAura_self.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
            this.willPop = false;
            if(GetBuffCountFromCaster(owner, attacker, nameof(Buffs.H28GEvolutionTurret)) > 0)
            {
                this.willPop = true;
                this.redShift = PushCharacterData("HeimerTBlue", owner, true);
            }
            if(GetBuffCountFromCaster(owner, default, nameof(Buffs.H28GEvolutionTurretSpell1)) == 0)
            {
                if(GetBuffCountFromCaster(owner, default, nameof(Buffs.H28GEvolutionTurretSpell2)) == 0)
                {
                    foreach(AttackableUnit unit in GetClosestUnitsInArea(attacker, owner.Position, 425, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, default, true))
                    {
                        SpellBuffClear(owner, nameof(Buffs.H28GEvolutionTurretSpell3));
                        CancelAutoAttack(owner, true);
                        AddBuff((ObjAIBase)unit, owner, new Buffs.H28GEvolutionTurretSpell2(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.frostTurrets);
            if(this.willPop)
            {
                PopCharacterData(owner, this.redShift);
            }
            else
            {
                if(GetBuffCountFromCaster(owner, attacker, nameof(Buffs.ExplosiveCartridges)) > 0)
                {
                    int rShift; // UNUSED
                    rShift = PushCharacterData("HeimerTRed", owner, true);
                }
                else
                {
                    if(GetBuffCountFromCaster(owner, attacker, nameof(Buffs.UrAniumRounds)) > 0)
                    {
                        int gShift; // UNUSED
                        gShift = PushCharacterData("HeimerTGreen", owner, true);
                    }
                    else
                    {
                        int yShift; // UNUSED
                        yShift = PushCharacterData("HeimerTYellow", owner, true);
                    }
                }
            }
        }
    }
}