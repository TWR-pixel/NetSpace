﻿
namespace NetSpace.EmailSender.Application;

public interface IEmailSender
{
    Task SendAsync();
}
