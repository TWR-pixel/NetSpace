using Mapster;
using MapsterMapper;
using NetSpace.Community.Application.Community;

namespace NetSpace.Community.Tests.Unit.Application;

public static class TestMapper
{
    public static Mapper Create()
    {
        var config = new TypeAdapterConfig();
        var registers = config.Scan(typeof(RegisterCommunityMapper).Assembly);
        config.Apply(registers);

        return new Mapper(config);
    }
}
