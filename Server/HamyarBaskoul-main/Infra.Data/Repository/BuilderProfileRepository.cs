using AutoMapper;
using Domain.Interfaces;
using Domain.Models.BuilderProfile;
using Infra.Data.Context;

namespace Infra.Data.Repository
{
	public class BuilderProfileRepository : IBuilderProfileRepository
	{
		private readonly WriteDbContext _Context;
		private readonly IMapper _mapper;
		public BuilderProfileRepository(WriteDbContext cleanArchDataBaseContext, IMapper mapper)
        {
			_Context = cleanArchDataBaseContext;
			_mapper = mapper;

		}

        public IEnumerable<BuilderProfile> GetAll()
		{
			return _Context.BuilderProfiles;
		}

		public BuilderProfile? GetById(int id)
		{
			return _Context.BuilderProfiles.FirstOrDefault(x => x.Id == id);
		}

		public void Add(BuilderProfile profile)
		{
			_Context.Add(profile);
		}

		public void Update(BuilderProfile profile)
		{
			var existingProfile = GetById(profile.Id);
			if (existingProfile != null)
			{
				// Update properties
				existingProfile.FullName = profile.FullName;
				existingProfile.EducationBaseTableId = profile.EducationBaseTableId;
				// سایر ویژگی‌ها را اینجا به‌روزرسانی کنید
			}
		}

		public void Delete(int id)
		{
			var profile = GetById(id);
			if (profile != null)
			{
				_Context.Remove(profile);
			}
		}
	}
}
