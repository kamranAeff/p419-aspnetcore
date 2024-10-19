using Application.Extensions;
using Application.Services;
using Domain.Entities.Membership;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Modules.AccountModule.Commands.SignupCommand
{
    class SignupRequestHandler(UserManager<OganiUser> userManager, ICryptoService cryptoService, IEmailService emailService, IHttpContextAccessor ctx) : IRequestHandler<SignupRequest>
    {
        public async Task Handle(SignupRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.UserName))
                request.UserName = $"{request.Name}.{request.Surname}".ToLower();


            var user = await userManager.FindByEmailAsync(request.Email);
            if (user is not null)
                throw new BadRequestException("BADREG", new Dictionary<string, IEnumerable<string>>
                {
                    ["Email"] = ["Email is already exists"]
                });

            user = await userManager.FindByNameAsync(request.UserName);
            if (user is not null)
                throw new BadRequestException("BADREG", new Dictionary<string, IEnumerable<string>>
                {
                    ["UserName"] = ["UserName is already exists"]
                });

            user = new OganiUser
            {
                Name = request.Name,
                Surname = request.Name,
                UserName = request.UserName,
                Email = request.Email,
                EmailConfirmed = false,
                IsSubscriber = request.SubscribeForNews
            };

            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors
                    .GroupBy(m => m.Code)
                    .ToDictionary(m => m.Key, m => m.Select(m => m.Description).AsEnumerable());

                throw new BadRequestException("BADREG", errors);
            }

            string confirmationUrl = $"{ctx.GetHost()}/approve-account?token={cryptoService.Encrypt($"{user.Id}-{user.Email}-{DateTime.UtcNow.AddMinutes(20):yyyy.MM.dd HH:mm:ss}", true)}";

            string content = File.ReadAllText(Path.Combine("wwwroot", "email-templates", "email-confirmation-ticket.html"));

            content = string.Format(content, $"{request.Name} {request.Surname}", confirmationUrl);

            await emailService.SendEmailQueue(user.Email, "Ogani user registration", content);
        }
    }
}
