



public abstract class MultiTargetStatusFactory
{
    public abstract Status GetStatus(Character[] characters, Character source);
}

public class MultiTargetStatusFactory<DataType, StatusType> : MultiTargetStatusFactory where StatusType : MultiTargetStatus<DataType>, new()
{
    public DataType data;

    public override Status GetStatus(Character[] characters, Character source)
    {
        return new StatusType { data = this.data, targets = characters, source = source };
    }
}

public abstract class SingleTargetStatusFactory
{
    public abstract Status GetStatus(Character target, Character source);
}

public class SingleTargetStatusFactory<DataType, StatusType> : SingleTargetStatusFactory where StatusType : SingleTargetStatus<DataType>, new()
{
    public DataType data;
    public override Status GetStatus(Character target, Character source)
    {
        return new StatusType { data = this.data, target = target, source = source };
    }
}

