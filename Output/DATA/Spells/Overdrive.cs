#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Overdrive : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 90f, 90f, 90f, 18f, 14f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {0.16f, 0.2f, 0.24f, 0.28f, 0.32f};
        float[] effect1 = {0.3f, 0.38f, 0.46f, 0.54f, 0.62f};
        int[] effect2 = {8, 8, 8, 8, 8};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_MoveSpeedMod;
            float nextBuffVars_AttackSpeedMod;
            nextBuffVars_MoveSpeedMod = this.effect0[level];
            nextBuffVars_AttackSpeedMod = this.effect1[level];
            AddBuff(attacker, target, new Buffs.Overdrive(nextBuffVars_AttackSpeedMod, nextBuffVars_MoveSpeedMod), 1, 1, this.effect2[level], BuffAddType.RENEW_EXISTING, BuffType.HASTE, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class Overdrive : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Overdrive",
            BuffTextureName = "Blitzcrank_Overdrive.dds",
            SpellFXOverrideSkins = new[]{ "PiltoverCustomsBlitz", },
        };
        float attackSpeedMod;
        float moveSpeedMod;
        int blitzcrankID;
        Particle one;
        Particle two;
        Particle three;
        Particle four;
        Particle five;
        Particle six;
        Particle seven;
        Particle eight;
        Particle classicOverdrive;
        Particle wheelOne;
        Particle wheelTwo;
        public Overdrive(float attackSpeedMod = default, float moveSpeedMod = default)
        {
            this.attackSpeedMod = attackSpeedMod;
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.attackSpeedMod);
            //RequireVar(this.moveSpeedMod);
            this.blitzcrankID = GetSkinID(owner);
            if(this.blitzcrankID == 4)
            {
                SpellEffectCreate(out this.one, out _, "SteamGolem_Piltover_Overdrive.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_PIPE_L_1", default, owner, default, default, false);
                SpellEffectCreate(out this.two, out _, "SteamGolem_Piltover_Overdrive.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_PIPE_L_2", default, owner, default, default, false);
                SpellEffectCreate(out this.three, out _, "SteamGolem_Piltover_Overdrive.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_PIPE_L_3", default, owner, default, default, false);
                SpellEffectCreate(out this.four, out _, "SteamGolem_Piltover_Overdrive.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_PIPE_L_4", default, owner, default, default, false);
                SpellEffectCreate(out this.five, out _, "SteamGolem_Piltover_Overdrive.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_PIPE_R_1", default, owner, default, default, false);
                SpellEffectCreate(out this.six, out _, "SteamGolem_Piltover_Overdrive.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_PIPE_R_2", default, owner, default, default, false);
                SpellEffectCreate(out this.seven, out _, "SteamGolem_Piltover_Overdrive.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_PIPE_R_3", default, owner, default, default, false);
                SpellEffectCreate(out this.eight, out _, "SteamGolem_Piltover_Overdrive.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_PIPE_R_4", default, owner, default, default, false);
                SpellEffectCreate(out this.classicOverdrive, out _, "Overdrive_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
                SpellEffectCreate(out this.wheelOne, out _, "SteamGolem_Piltover_Overdrive_Tires.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BL_wheel", default, owner, default, default, false);
                SpellEffectCreate(out this.wheelTwo, out _, "SteamGolem_Piltover_Overdrive_Tires.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BR_wheel", default, owner, default, default, false);
            }
            else
            {
                SpellEffectCreate(out this.classicOverdrive, out _, "Overdrive_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            if(this.blitzcrankID == 4)
            {
                SpellEffectRemove(this.one);
                SpellEffectRemove(this.two);
                SpellEffectRemove(this.three);
                SpellEffectRemove(this.four);
                SpellEffectRemove(this.five);
                SpellEffectRemove(this.six);
                SpellEffectRemove(this.seven);
                SpellEffectRemove(this.eight);
                SpellEffectRemove(this.wheelOne);
                SpellEffectRemove(this.wheelTwo);
                SpellEffectRemove(this.classicOverdrive);
            }
            else
            {
                SpellEffectRemove(this.classicOverdrive);
            }
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, this.moveSpeedMod);
            IncPercentAttackSpeedMod(owner, this.attackSpeedMod);
        }
    }
}