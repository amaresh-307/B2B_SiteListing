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
        private IMapper _mapper{ get;}
        private readonly IRepository<LogInDetails> _repository;
        private readonly UserService _userService;
        public LogInDetailsService(IMapper mapper, IRepository<LogInDetails> repository, UserService userService)
        {
            _mapper = mapper;
            _repository = repository;
            _userService = userService;
        }
        public async Task<Guid> AddLogInDetails(LogInDetailsViewModel model)
        {
            var loggedInUser = await _userService.GetCurrentUserAsync();
            var isDataExist = await GetLogInDetails(loggedInUser.Id);
            if (isDataExist is not null) throw new AlreadyExistException();
            var logInDetails = _mapper.Map<LogInDetails>(model);
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

        public async Task<LogInDetailsViewModel> GetLogInDetails(string userId)
        {
            var isDataExist = await _repository.FindOne(x => x.ApplicationUserId == userId);
            if (isDataExist is null) return null;
            return _mapper.Map<LogInDetailsViewModel>(isDataExist);

        }
    }
}
