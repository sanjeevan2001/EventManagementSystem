using EventManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Interfaces.IServices
{
    public interface IJwtTokenService
    {
        string CreateToken(User user);
    }
}
