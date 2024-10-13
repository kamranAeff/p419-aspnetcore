using Application.Services;
using Domain.Entities.Membership;
using Domain.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace Application.Modules.AccountModule.Commands.SignupCommand
{
    public class SignupRequest : IRequest
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
