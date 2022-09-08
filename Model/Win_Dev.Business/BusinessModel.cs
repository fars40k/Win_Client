using System;
using System.Collections.Generic;
using Win_Dev.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Win_Dev.Business
{
    /// <summary>
    /// Contains all intermediate methods conecting UI and data access layers
    /// </summary>
    public partial class BusinessModel
    {

        private DataAccessObject _dataAccessObject;

        public BusinessModel(DataAccessObject dataAccessObject)
        {
            _dataAccessObject = dataAccessObject;
        }

        #region Project_related
        
        public void CreateProject(Action<BusinessProject,Exception> callback)
        {
  
        }
        
        public void GetProjectsList(Action<List<BusinessProject>, Exception> callback)
        {

            List<BusinessProject> businessProjects = new List<BusinessProject>();

             Exception error = null;

             try
             {
                 List<Project> fromDataList = _dataAccessObject.Projects.FindAll().ToList<Project>();

                 foreach (Project item in fromDataList)
                 {
                     item.Name = item.Name.TrimEnd(' ');
                     item.Description = item.Description.TrimEnd(' ');

                     businessProjects.Add(new BusinessProject(item));
                 }
                 error = null;
             }
             catch (Exception ex)
             {
                 error = ex;
             }

             callback.Invoke(businessProjects, error);
        }
        
        public void UpdateProject(BusinessProject projectFromUI,Action<Exception> callback)
        {
            
        }

        public void DeleteProject(Guid forDelete, Action<Exception> callback)
        {
           
        }

        public void GetProjectByID(Guid findID, Action<BusinessProject,Exception> callback)
        {
           
        }
        
        #endregion

        #region Personel_related
        
        public void CreatePerson(Action<BusinessPerson, Exception> callback)
        {
            Exception error = null;

            BusinessPerson businessPerson = new BusinessPerson();

            try
            {
                businessPerson.Person = new Person();

                Random rnd = new Random();
                businessPerson.PersonID = Guid.NewGuid();
                businessPerson.FirstName = "First" + rnd.Next(0, 100).ToString();
                businessPerson.SurName = "Sur" + rnd.Next(0, 100).ToString();
                businessPerson.LastName = "Last" + rnd.Next(0, 100).ToString();
                businessPerson.Division = "div" + rnd.Next(0, 100).ToString();
                businessPerson.Occupation = "occ" + rnd.Next(0, 100).ToString();

                _dataAccessObject.Personel.Insert(businessPerson.Person);

                error = null;
            }
            catch (Exception ex)
            {
                error = ex;
            }

            callback.Invoke(businessPerson, error);
        }
        
        public void GetPersonelList(Action<List<BusinessPerson>, Exception> callback)
        {
            Exception error = null;

            List<BusinessPerson> businessPersonel = new List<BusinessPerson>();

            try
            {

                List<Person> fromDataList = _dataAccessObject.Personel.FindAll().ToList<Person>();       
        
                foreach (Person item in fromDataList)
                {
                    item.FirstName = item.FirstName.TrimEnd(' ');
                    item.SurName = item.SurName.TrimEnd(' ');
                    item.LastName = item.LastName.TrimEnd(' ');
                    item.Division = item.Division.TrimEnd(' ');
                    item.Occupation = item.Occupation.TrimEnd(' ');

                    businessPersonel.Add(new BusinessPerson(item));
                }
                error = null;
            }
            catch (Exception ex)
            {
                error = ex;
            }

            callback.Invoke(businessPersonel, error);
        }
        
        public void UpdatePersonel(IEnumerable<BusinessPerson> UIList, Action<Exception> callback)
        {
            
        }

        public void DeletePerson(BusinessPerson forDelete,Action<Exception> callback)
        {
            Exception error = null;

            try
            {
                _dataAccessObject.Personel.Delete(forDelete.PersonID);
            }
            catch (Exception ex)
            {
                error = ex;
            }

            callback.Invoke(error);
        }

        public void GetPersonelForProject(Guid projectGUID, Action<List<BusinessPerson>, Exception> callback)
        {
          

        }

        #endregion

        #region Goal_related

        public void CreateGoal(Guid forProject, Action<BusinessGoal, Exception> callback)
        {
           
        }

        public void GetPersonelForGoal(Guid goalGUID, Action<List<BusinessPerson>, Exception> callback)
        {
           
        }       
        
        public void UpdateGoals(IEnumerable<BusinessGoal> UIList, Action<Exception> callback)
        {
        
        }

        public void DeleteGoal(BusinessGoal forDelete, Action<Exception> callback)
        {
          
        }

        public void GetGoalsListForProject(Guid ProjectGUID, Action<List<BusinessGoal>, Exception> callback)
        {

      
        }

        #endregion

        public void AssignPersonToProject(Guid personGUID, Guid projectGUID, Action<Exception> callback)
        {
          
        }

        /// <summary>
        /// Provide goals personel deletion when a person's project assignation removed
        /// </summary>
        public void UnassignPersonFromProject(Guid personGUID, Guid projectGUID, Action<Exception> callback)
        {
           
        }

        public void AssignPersonToGoal(Guid personGUID, Guid goalGUID, Action<Exception> callback)
        {

        }
        public void UnassignPersonFromGoal(Guid personGUID, Guid goalGUID, Action<Exception> callback)
        {
           
        }

    }

}

