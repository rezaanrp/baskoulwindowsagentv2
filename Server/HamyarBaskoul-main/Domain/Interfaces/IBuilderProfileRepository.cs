using Domain.Models.BuilderProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
	public interface IBuilderProfileRepository
	{
		IEnumerable<BuilderProfile> GetAll();
		BuilderProfile? GetById(int id);
		void Add(BuilderProfile profile);
		void Update(BuilderProfile profile);
		void Delete(int id);
	}
}
