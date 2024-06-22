using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Repositories;
using Services.Common;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Services.Implementation
{
    class SubscribeService : ISubscribeService
    {
        private readonly ISubscriberRepository subscriberRepository;
        private readonly IEmailService emailService;
        private readonly ICryptoService cryptoService;
        private readonly IHttpContextAccessor ctx;

        public SubscribeService(ISubscriberRepository subscriberRepository,
            IEmailService emailService,
            ICryptoService cryptoService,
            IHttpContextAccessor ctx)
        {
            this.subscriberRepository = subscriberRepository;
            this.emailService = emailService;
            this.cryptoService = cryptoService;
            this.ctx = ctx;
        }
        public async Task<Tuple<bool, string>> Subscribe(string email)
        {
            //var entity = await db.Set<Subscribe>().FirstOrDefaultAsync(x => x.Email.Equals(email));
            var entity = await subscriberRepository.GetAsync(m => m.Email.Equals(email));

            //var data = new Tuple<bool, string>(true,"");
            var data = Tuple.Create(true, "");


            if (entity?.ApprovedAt is not null)
            {
                return Tuple.Create(true, "Siz artıq abunə olmusunuz!");
            }
            else if (entity is not null)
            {
                return Tuple.Create(true, "Siz abunəlik üçün e-poçt unvanınızı təsdiq etməlisiniz!");
            }


            entity = new Subscribe { Email = email };
            await subscriberRepository.AddAsync(entity);
            await subscriberRepository.SaveAsync();

            var token = $"id={entity.Email}|expire={DateTime.Now.AddHours(1):yyyy.MM.dd HH:mm:ss}";

            token = cryptoService.Encrypt(token, true);

            string redirectUrl = $"{ctx.HttpContext.Request.Scheme}://{ctx.HttpContext.Request.Host}/subscribe-approve?token={token}";

            var msg = $"Salam,<br/>Abuneliyinizi <a href=\"{redirectUrl}\">link</a>`lə tamamlayin";

            await emailService.SendEmail(entity.Email, "Ogani Subscription", msg);

            return Tuple.Create(false, "E-poçt ünvanınıza təsdiq linki göndərildi.1 saat ərzidə abunəliyinizi tamamlamağı unutmayın!");
        }

        public async Task<Tuple<bool, string>> SubscribeApprove(string token)
        {
            token = cryptoService.Decrypt(token);

            string patterns = @"id=(?<email>[^|]*)\|expire=(?<date>\d{4}\.\d{2}\.\d{2}\s\d{2}:\d{2}:\d{2})";

            var match = Regex.Match(token, patterns);

            if (!match.Success)
                goto l1;

            var email = match.Groups["email"].Value;
            var date = match.Groups["date"].Value;

            if (string.IsNullOrWhiteSpace(email))
                goto l1;

            if (string.IsNullOrWhiteSpace(date) || !DateTime.TryParseExact(date, "yyyy.MM.dd HH:mm:ss", null, DateTimeStyles.None, out DateTime expireDate))
                goto l1;

            if (expireDate < DateTime.Now)
            {
                return Tuple.Create(true, "Sorğunun istifadə müddəti bitmişdir");
            }

            var entity = await subscriberRepository.GetAsync(m => m.Email.Equals(email));

            if (entity is null)
                goto l1;

            if (entity.ApprovedAt is not null)
            {
                return Tuple.Create(true, "Artıq abunə olmusunuz");
            }

            entity.ApprovedAt = DateTime.Now;
            await subscriberRepository.SaveAsync();
            return Tuple.Create(false, "Abunəliyiniz təsdiq olundu");

        l1:
            return Tuple.Create(true, "Qadağan edilmiş sorğu!");
        }
    }
}
