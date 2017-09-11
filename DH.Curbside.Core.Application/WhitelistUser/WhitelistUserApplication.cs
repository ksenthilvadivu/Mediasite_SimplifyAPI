using AutoMapper;
using DH.Curbside.Core.Domain;
using DH.Curbside.Core.Enterprise.Email;
using DH.Curbside.Core.Persistence.RepositoryInterfaces;
using DH.Curbside.Core.Application.Exceptions;
using DH.Curbside.Core.Application.WhitelistUser.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DH.Curbside.Core.Application.WhitelistUser
{
    public class WhitelistUserApplication : IWhitelistUserApplication
    {
        private readonly IWhitelistUserRepository _whitelistUserRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        /// <summary>
        /// WhitelistManager Constructor
        /// </summary>
        /// <param name="whitelistRepository">WhitelistUserRepository Interface</param>
        /// <param name="mapper">Mapper Interface</param>
        /// <param name="emailService"></param>
        public WhitelistUserApplication(IWhitelistUserRepository whitelistRepository
                                      , IMapper mapper, IEmailService emailService)
        {
            _whitelistUserRepository = whitelistRepository;
            _mapper = mapper;
            _emailService = emailService;
        }

        /// <summary>
        /// Gets Whitelist Users based on Tenant Id
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <returns>List of Whitelist Users</returns>
        public UserViewModel GetWhitelistUsers(string tenantId)
        {
            Guid tenantGuid = ParseTenantGuid(tenantId);

            var whitelistUsers = (from user in _whitelistUserRepository.All()
                                  where user.TenantId == tenantGuid
                                  select user).ToList();

            if (whitelistUsers == null || !whitelistUsers.Any())
                throw new WhitelistUserNotAvaliableException();

            var userInfo = _mapper.Map<List<User>, List<UserInfo>>(whitelistUsers);

            var userViewModel = new UserViewModel();
            foreach (var user in userInfo)
            {
                userViewModel.Users.Add(user);
            }

            return userViewModel;
        }

        /// <summary>
        /// Gets Whitelist User based on Tenant Id and Email
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="email"></param>
        /// <returns>Whitelist User</returns>
        public UserInfo GetWhitelistUser(string tenantId, string email)
        {
            Guid tenantGuid = ParseTenantGuid(tenantId);

            //todo: Required to review from Radhika
            if (!(new EmailAddressAttribute().IsValid(email)))
                throw new InvalidEmailException();

            var whitelistUser = _whitelistUserRepository.All()
                .FirstOrDefault(f => f.TenantId == tenantGuid && f.EmailAddress == email);

            if (whitelistUser == null)
                throw new WhitelistUserNotAvaliableException();

            return _mapper.Map<User, UserInfo>(whitelistUser);

        }

        /// <summary>
        /// De-provisions User
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="email">Email</param>
        /// <param name="appId">Application Id</param>
        /// <returns>True or False</returns>
        public async Task<bool> DeprovisionUser(string tenantId, string email, int appId)
        {
            Guid tenantGuid = ParseTenantGuid(tenantId);

            //todo: validataion for Email -Review from Radhika
            if (!(new EmailAddressAttribute().IsValid(email)))
                throw new InvalidEmailException();

            var deprovisionUser = _whitelistUserRepository.All()
                .FirstOrDefault(f => f.TenantId == tenantGuid && f.EmailAddress == email);

            if (deprovisionUser == null)
                throw new WhitelistUserNotAvaliableException();

            var userClientApplication = new List<UserClientApplication>();
            foreach (var item in deprovisionUser.UserClientApplication)
            {
                if (item.ClientApplicationId == appId)
                {
                    item.DeProvisionedDate = DateTime.Now;
                    userClientApplication.Add(item);
                }
            }
            if (!userClientApplication.Any())
            {
                throw new InvalidApplicationIdException();
            }
            return await _whitelistUserRepository.Update(deprovisionUser);
        }

        /// <summary>
        /// Adds New Whitelist User
        /// </summary>
        /// <param name="whitelistUserViewModel">WhitelistUser View Model</param>
        /// <param name="tenantId">Tenant Id</param>
        /// <returns>True or False</returns>
        public async Task<bool> CreateWhitelistUser(List<RequestUserViewModel> whitelistUserViewModel, string tenantId)
        {
            Guid tenantGuid = ParseTenantGuid(tenantId);

            var whitelistUserModels = _mapper.Map<List<RequestUserViewModel>, List<User>>(whitelistUserViewModel);

            if (whitelistUserModels == null || !whitelistUserModels.Any())
                throw new InvalidJsonException();

            var whitelistUsers = new List<User>();

            foreach (var user in whitelistUserModels)
            {
                var whitelistUserExisted = _whitelistUserRepository.All()
                        .FirstOrDefault(f => f.TenantId == tenantGuid && f.EmailAddress == user.EmailAddress);

                if (whitelistUserExisted == null)
                {
                    user.TenantId = tenantGuid;
                    // ToDo: Need to replace with AD User
                    user.CreatedBy = "System User";
                    user.CreatedOn = DateTime.Now;
                    whitelistUsers.Add(user);
                }               
            }
            return await _whitelistUserRepository.Add(whitelistUsers);

        }

        public void InviteWhitelistUser(List<RequestInviteViewModel> requestInviteViewModel, string tenantId)
        {
            var tenantGuid = ParseTenantGuid(tenantId);
            if (requestInviteViewModel == null || !requestInviteViewModel.Any())
                throw new InvalidJsonException();

            foreach (var item in requestInviteViewModel)
            {

                var whitelistUsers = (from user in _whitelistUserRepository.All()
                                      where user.UserId == item.UserId && user.TenantId == tenantGuid
                                      from app in user.UserClientApplication
                                      where app.ClientApplicationId == item.AppId
                                      select new { user, app }).ToList();

                if (whitelistUsers == null || !whitelistUsers.Any())
                {
                    throw new InValidData(item.UserId.ToString());
                }

                foreach (var user in whitelistUsers)
                {
                    _emailService.SendEmailTemplate(EmailTemplateType.SubmitterRegistration, user.user.EmailAddress);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        private static Guid ParseTenantGuid(string tenantId)
        {
            Guid tenantGuid;
            Guid.TryParse(tenantId, out tenantGuid);

            if (tenantGuid == Guid.Empty)
                throw new InvalidTenantIdException();
            return tenantGuid;
        }
    }
}
