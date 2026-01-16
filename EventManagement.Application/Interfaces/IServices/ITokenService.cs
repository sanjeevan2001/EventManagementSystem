using EventManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.Interfaces.IServices
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
