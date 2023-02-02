#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OrianaGhostEnemy : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", "", },
        };
        ObjAIBase caster;
        Particle temp;
        bool pickUp; // UNUSED
        float[] effect0 = {-0.2f, -0.25f, -0.3f, -0.35f, -0.4f};
        public override void OnActivate()
        {
            Vector3 currentPos;
            ObjAIBase caster;
            currentPos = GetUnitPosition(owner);
            caster = SetBuffCasterUnit();
            this.caster = caster;
            if(caster != owner)
            {
                SpellEffectCreate(out this.temp, out _, "Oriana_Ghost_bind.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, currentPos, owner, default, currentPos, false, default, default, false);
            }
            this.pickUp = false;
        }
        public override void OnDeactivate(bool expired)
        {
            AttackableUnit caster;
            bool dropBall;
            if(caster != owner)
            {
                SpellEffectRemove(this.temp);
            }
            dropBall = false;
            if(expired)
            {
                dropBall = true;
            }
            else if(owner.IsDead)
            {
                dropBall = true;
            }
            if(dropBall)
            {
                TeamId teamID;
                Vector3 targetPos;
                Minion other3;
                caster = this.caster;
                teamID = GetTeamID(caster);
                targetPos = GetUnitPosition(owner);
                other3 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", targetPos, teamID ?? TeamId.TEAM_BLUE, false, true, false, true, true, true, 0, default, true, (Champion)caster);
                AddBuff(attacker, other3, new Buffs.OrianaGhost(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff(attacker, other3, new Buffs.OrianaGhostMinion(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnUpdateActions()
        {
            float distance;
            distance = DistanceBetweenObjects("Attacker", "Owner");
            if(distance > 1250)
            {
                Vector3 castPos;
                this.pickUp = true;
                SpellBuffClear(owner, nameof(Buffs.OrianaGhostEnemy));
                castPos = GetUnitPosition(owner);
                AddBuff(attacker, attacker, new Buffs.OrianaReturn(), 1, 1, 1.25f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                SpellCast(attacker, attacker, attacker.Position, attacker.Position, 4, SpellSlotType.ExtraSlots, 1, false, true, false, false, false, true, castPos);
            }
            else
            {
                bool noRender;
                noRender = GetNoRender(owner);
                if(owner is Champion)
                {
                    if(noRender)
                    {
                        SpellBuffClear(owner, nameof(Buffs.OrianaGhostEnemy));
                    }
                }
            }
        }
        public override void OnUpdateStats()
        {
            int level;
            level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            IncPercentMovementSpeedMod(owner, this.effect0[level]);
        }
    }
}