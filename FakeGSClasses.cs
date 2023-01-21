using System.Numerics;

public class GameObject
{
    public Vector3 Position;
    public TeamId Team;
}
public class AttackableUnit: GameObject
{
    public bool IsDead;
}
public class ObjAIBase: AttackableUnit {}
public class Minion: ObjAIBase {}
public class Champion: ObjAIBase {}
public class BaseTurret: ObjAIBase {}
public class Pet: Minion {}

public class Spell {}
public class Buff {}
public class Item {}
public class Particle {}
public class Region {}
public class SpellMissile {}
public class Fade {}
public class DamageData {}
public class DeathData {}

public class Table {}