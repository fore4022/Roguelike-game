using System;
[Obsolete]
public interface IStatus
{
    public float MoveSpeed { get; }
    public float Range { get; }
    public int Damage { get; }
    public int MaxHp { get; }
    public int Hp { get; }
}