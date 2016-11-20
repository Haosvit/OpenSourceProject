using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenSourceProject.OpenSource
{
	public class VerificationDa
	{
		private VerifyDbContext dbContext = new VerifyDbContext();

		public List<Verification> GetAll()
		{
			return dbContext.Verifications.ToList();
		}

		public Verification Add(Verification verification)
		{
			 var addVerification = dbContext.Verifications.Add(verification);
			 dbContext.SaveChanges();
			 return addVerification;
		}


	}
}