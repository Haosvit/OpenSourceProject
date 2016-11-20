using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenSourceProject.OpenSource
{

	public class UserDa
	{
		private VerifyDbContext dbContext = new VerifyDbContext();

		public List<User> GetAll()
		{
			return dbContext.Users.ToList();
		}

		public User Add(User user)
		{
			var addedUser = dbContext.Users.Add(user);
			dbContext.SaveChanges();
			return addedUser;
		}

		public bool HasMailExisted(string email)
		{
			return dbContext.Users.Any(u => string.Compare(u.Email, email, true) == 0);
		}



	}
}