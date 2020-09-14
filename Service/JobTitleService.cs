using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using NetCoreProject.Model;

namespace NetCoreProject.Service
{
    public class JobTitleService
    {
        private readonly IMongoCollection<JobTitle> _jobTitles;

        public JobTitleService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("MyDemo"));
            var database = client.GetDatabase("MyDemo");

            _jobTitles = database.GetCollection<JobTitle>("JobTitle");
        }

        public List<JobTitle> GetAll() =>
            _jobTitles.Find(JobTitle => true).ToList();

        public List<JobTitle> GetByCodes(List<string> jobTitleCodes)
        {
            var query = Builders<JobTitle>.Filter.In(jobtitle => jobtitle.JobTitleCode, jobTitleCodes);

            var listJob = _jobTitles.Find(query).ToList();

            return listJob;
        }

        public JobTitle GetByOne(string jobTitleCode) =>
            _jobTitles.Find<JobTitle>(jobtitle => jobtitle.JobTitleCode == jobTitleCode).FirstOrDefault();

        public JobTitle Create(JobTitle jobTitle)
        {
            _jobTitles.InsertOne(jobTitle);
            return jobTitle;
        }

        public void Update(JobTitle jobTitleUpdate) =>
            _jobTitles.ReplaceOne(jobTitle => jobTitle.Id == jobTitleUpdate.Id, jobTitleUpdate);

        public void Delete(string jobTitleCode) =>
            _jobTitles.DeleteOne(jobTitle => jobTitle.Id == jobTitleCode);
    }
}
