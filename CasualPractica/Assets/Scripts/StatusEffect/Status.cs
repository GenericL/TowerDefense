

public abstract class Status
{
    public abstract void ApplyStatus();
    public abstract void UpdateStatus();
    public abstract void RemoveStatus();
}

public abstract class MultiTargetStatus<DataType> : Status
{
    public DataType data;
    public Character source;
    public Character[] targets;
}

public abstract class SingleTargetStatus<DataType> : Status
{
    public DataType data;
    public Character source;
    public Character target;
}

