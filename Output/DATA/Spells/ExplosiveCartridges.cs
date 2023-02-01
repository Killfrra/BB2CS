#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ExplosiveCartridges : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "ExplosiveCartridges",
            BuffTextureName = "Heimerdinger_Level3Turret.dds",
        };
        public override void OnSpellHit()
        {
            TeamId teamID;
            Particle asdf; // UNUSED
            float dmg;
            float thirdDA;
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    teamID = GetTeamID(owner);
                    attacker = GetChampionBySkinName("Heimerdinger", teamID);
                    AddBuff(attacker, target, new Buffs.UrAniumRoundsHit(), 50, 1, 3, BuffAddType.STACKS_AND_RENEWS, BuffType.SHRED, 0, true, false, false);
                    SpellEffectCreate(out asdf, out _, "TiamatMelee_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, false, false, false, false);
                    dmg = GetTotalAttackDamage(owner);
                    foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, target.Position, 210, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                    {
                        if(target != unit)
                        {
                            thirdDA = 0.4f * dmg;
                            ApplyDamage(attacker, unit, thirdDA, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, attacker);
                        }
                    }
                }
            }
        }
    }
}