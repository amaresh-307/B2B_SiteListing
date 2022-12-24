using AutoMapper;
using B2B_SiteListing.Data.Entities;
using B2B_SiteListing.Data.Repositories;
using B2B_SiteListing.Service.Contracts;
using B2B_SiteListing.Service.Exceptions;
using B2B_SiteListing.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace B2B_SiteListing.Service.Implementation
{
    public class LogInDetailsService : ILogInDetailsService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private IMapper _mapper{ get;}
        private readonly IRepository<LogInDetails> _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogInDetailsService(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, IMapper mapper, IRepository<LogInDetails> repository)
        {
            _mapper = mapper;
            _repository = repository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Guid> AddLogInDetails(LogInDetailsViewModel model)
        {
            var logInDetails = _mapper.Map<LogInDetails>(model);
            var loggedInUser = await GetCurrentUserAsync();
            if (loggedInUser is null) throw new NotFoundException("User not found.");
            var isDataExist = _repository.FindOne(x => x.ApplicationUserId == loggedInUser.Id);
            if (isDataExist is not null) throw new AlreadyExistException();
            logInDetails.ApplicationUserId = loggedInUser?.Id;
            logInDetails.CreatedOn = DateTime.Now;
            var res = await _repository.Insert(logInDetails);
            return res;
        }

        public async Task DeleteLogInDetails(string userId)
        {
            var logindetail = await _repository.FindOne(x => x.ApplicationUserId == userId);
            if(logindetail is null)
            {
                throw new NotFoundException();
            }
            await _repository.Delete(logindetail);
        }

        public async Task UpdateLogInDetails(LogInDetailsViewModel model, string userId)
        {
            var existinglogindetail = await _repository.FindOne(x => x.ApplicationUserId == userId);
            if(existinglogindetail is null)
            {
                throw new NotFoundException();
            }
            var detail = _mapper.Map<LogInDetails>(model);
            detail.Id = existinglogindetail.Id;
            await _repository.Update(detail);
        }

        private async Task<ApplicationUser> GetCurrentUserAsync() => await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

    }
}
