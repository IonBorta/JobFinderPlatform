using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using JobFinder.DAL.AbstractFactory.Concrete.Factory;
using JobFinder.DAL.Entities;

namespace JobFinder.DAL.Bridge.Implementation
{
    public class FirebaseRepositoryImplementor<T> : IRepositoryImplementor<T>
    {
        private readonly FirestoreDb _firestoreDb;
        private readonly string _collectionName;

        public FirebaseRepositoryImplementor(FirestoreDb firestoreDb, string collectionName)
        {
            _firestoreDb = firestoreDb;
            _collectionName = collectionName;
        }

        public async Task<bool> Add(T entity)
        {
            var collectionRef = _firestoreDb.Collection(_collectionName);
            var result = await collectionRef.AddAsync(entity);

            return !string.IsNullOrEmpty(result.Id);
        }

        public Task<IList<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IList<T>> GetByCompanyId(int companyId)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Company> GetCompanyByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<Job> GetJobByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
