﻿using NetSpace.Common.Application;

namespace NetSpace.User.Application;

public sealed record UserRequest<TResponse> : RequestBase<TResponse>
{

}
