using Mapster;
using MapsterMapper;

namespace NetSpace.Community.Tests.Unit.Application;

public static class TestMapper
{
    public static Mapper Create()
    {
        var config = new TypeAdapterConfig();
        var registers = config.Scan(typeof(ResponseBase).Assembly);
        config.Apply(registers);

        return new Mapper(config);
    }
}
