using Application.Interfaces;
using Application.ViewModels.Applicant;
using Application.ViewModels.BuilderProfile;
using AutoMapper;
using Domain.Models.BuilderProfile;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
	public class BuilderProfileService : IBuilderProfileService
	{
		private readonly IRepository<BuilderProfile> _repository;
        private readonly IMapper mapper;


        public BuilderProfileService(IMapper mapper, IRepository<BuilderProfile> repository)
		{
			_repository = repository;
            this.mapper = mapper;

        }

        public async Task<IEnumerable<BuilderProfile>> GetAllBuilderProfilesAsync()
		{
			return await _repository.GetAllAsync();
		}

		public async Task<BuilderProfileViewModel?> GetBuilderProfileByIdAsync(int id)
		{
           var profile =  await _repository.GetByIdAsync(id);

            var result = mapper.Map<BuilderProfileViewModel>(profile);
			return result;
		}

		public async Task AddBuilderProfileAsync(BuilderProfileViewModel profile)
		{
            var result = mapper.Map<BuilderProfile>(profile);

            await _repository.AddAsync(result);
		}
		public void UpdateBuilderProfile(BuilderProfileViewModel profile)
		{
            var result = mapper.Map<BuilderProfile>(profile);

            _repository.Update(result);
		}

		public async Task DeleteBuilderProfile(int id)
		{
            //var result = mapper.Map<BuilderProfile>(profile);

            var result = await _repository.GetByIdAsync(id);
			if (result != null)
			{
				result.IsDeleted = true;
				_repository.Update(result);
			}
            //_repository.Remove(result);
        }
    }

}
