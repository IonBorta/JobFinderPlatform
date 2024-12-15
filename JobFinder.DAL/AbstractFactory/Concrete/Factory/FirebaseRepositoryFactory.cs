using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.DAL.AbstractFactory.Abstract.Factory;
using JobFinder.DAL.AbstractFactory.Abstract.Product;
using JobFinder.DAL.Bridge.Implementation;
using JobFinder.DAL.Entities;
using JobFinder.DAL.Repositories;

namespace JobFinder.DAL.AbstractFactory.Concrete.Factory
{
    public class FirestoreDb
    {
    }
    public class FirebaseRepositoryFactory : IRepositoryFactory
    {
        private readonly FirestoreDb _firestoreDb;

        public FirebaseRepositoryFactory(FirestoreDb firestoreDb)
        {
            _firestoreDb = firestoreDb;
        }

        public IUserRepository CreateUserRepository()
        {
            //return new UserSqlRepository(new FirebaseRepositoryImplementor<User>(_firestoreDb, "users"));
            throw new NotImplementedException();
        }

        public IJobRepository CreateJobRepository()
        {
            //return new JobRepository(new FirebaseRepositoryImplementor<Job>(_firestoreDb, "jobs"));
            throw new NotImplementedException();
        }

        public ICompanyRepository CreateCompanyRepository()
        {
            //return new CompanyRepository(new FirebaseRepositoryImplementor<Company>(_firestoreDb, "companies"));
            throw new NotImplementedException();
        }

        public IApplicationRepository CreateApplicationRepository()
        {
            throw new NotImplementedException();
        }
    }
}
