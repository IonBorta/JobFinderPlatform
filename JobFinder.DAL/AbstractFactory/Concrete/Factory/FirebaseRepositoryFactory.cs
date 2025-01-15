using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using JobFinder.DAL.AbstractFactory.Abstract.Factory;
using JobFinder.DAL.AbstractFactory.Abstract.Product;
using JobFinder.DAL.AbstractFactory.Concrete.Repositories;
using JobFinder.DAL.Bridge.Implementation;
using JobFinder.DAL.Entities;

namespace JobFinder.DAL.AbstractFactory.Concrete.Factory
{
    public class FirebaseRepositoryFactory : IRepositoryFactory
    {
        private readonly FirestoreDb _firestoreDb;

        public FirebaseRepositoryFactory(FirestoreDb firestoreDb)
        {
            _firestoreDb = firestoreDb;
        }

        public IUserRepository CreateUserRepository()
        {
            return new UserRepository(new FirebaseRepositoryImplementor<User>(_firestoreDb, "users"));
        }

        public IJobRepository CreateJobRepository()
        {
            return new JobRepository(new FirebaseRepositoryImplementor<Job>(_firestoreDb, "jobs"));
        }

        public ICompanyRepository CreateCompanyRepository()
        {
            return new CompanyRepository(new FirebaseRepositoryImplementor<Company>(_firestoreDb, "companies"));
        }

        public IApplicationRepository CreateApplicationRepository()
        {
            return new ApplicationRepository(new FirebaseRepositoryImplementor<ApplicationEntity>(_firestoreDb,"applications"));
        }
    }
}
