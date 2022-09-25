using System;
using System.Collections.Generic;
using Win_Dev.Data;
using System.Linq;

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
            Exception error = null;

            BusinessProject project = new BusinessProject();

            try
            {
                Random rnd = new Random();

                project.Project = new Project();

                string projectName = "Project-" + rnd.Next(0, 1000).ToString();
                project.Name = projectName;
                project.CreationDate = DateTime.Today;
                project.ExpireDate = project.CreationDate.AddDays(1);

                project.ProjectID = Guid.NewGuid();
                project.Description = "!";
                project.Percentage = 0;
                project.StatusKey = 0;

                _dataAccessObject.Projects.Insert(project.Project);

                error = null;
            }
            catch (Exception ex)
            {
                error = ex;
            }

            callback.Invoke(project, error);
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
            Exception error = null;
            try
            {
                _dataAccessObject.Projects.Update(projectFromUI.Project);

                error = null;

            }
            catch (Exception ex)
            {
                error = ex;
            }

            callback.Invoke(error);
        }

        public void DeleteProject(Guid forDelete, Action<Exception> callback)
        {
            Exception error = null;

            try
            {              
                _dataAccessObject.Projects.Delete(forDelete);
            }
            catch (Exception ex)
            {
                error = ex;
            }

            callback.Invoke(error);
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
            Exception error = null;
            try
            {
                foreach (BusinessPerson item in UIList)
                {

                    _dataAccessObject.Personel.Update(item.Person);

                }

                error = null;

            }
            catch (Exception ex)
            {
                error = ex;
            }      

        callback.Invoke(error);
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

            Exception error = null;

            List<BusinessPerson> businessPersonel = new List<BusinessPerson>();

            try
            {

                List<Person> fromDataList = _dataAccessObject.Personel.FindForProject(projectGUID).ToList<Person>();

                foreach (Person item in fromDataList)
                {

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

        #endregion

        #region Goal_related

        public void CreateGoal(Guid forProject, Action<BusinessGoal, Exception> callback)
        {
            Exception error = null;

            BusinessGoal businessGoal = new BusinessGoal();

            try
            {
                businessGoal.Goal = new Goal();

                Random rnd = new Random();

                businessGoal.GoalID = Guid.NewGuid();

                businessGoal.Name = "Goal" + rnd.Next(0, 100).ToString();
                businessGoal.Description = "!";
                businessGoal.CreationDate = DateTime.Today;
                businessGoal.ExpireDate = businessGoal.CreationDate.AddDays(1);
                businessGoal.Percentage = 0;
                businessGoal.StatusKey = 0;

                _dataAccessObject.Goals.Insert(businessGoal.Goal);
                _dataAccessObject.LinkedData.AddGoalToProject(businessGoal.GoalID, forProject);

                error = null;
            }
            catch (Exception ex)
            {
                error = ex;
            }

            callback.Invoke(businessGoal, error);
        }
        
        public void UpdateGoals(IEnumerable<BusinessGoal> UIList, Action<Exception> callback)
        {
            Exception error = null;

            try
            {
                foreach (BusinessGoal item in UIList)
                {

                    _dataAccessObject.Goals.Update(item.Goal);

                }

                error = null;

            }
            catch (Exception ex)
            {
                error = ex;
            }

            callback.Invoke(error);
        }

        public void DeleteGoal(BusinessGoal forDelete, Action<Exception> callback)
        {
            Exception error = null;

            try
            {

                _dataAccessObject.Goals.Delete(forDelete.GoalID);

            }
            catch (Exception ex)
            {
                error = ex;
            }

            callback.Invoke(error);
        }

        public void GetPersonelForGoal(Guid goalGUID, Action<List<BusinessPerson>, Exception> callback)
        {
            Exception error = null;

            List<BusinessPerson> businessPersonel = new List<BusinessPerson>();

            try
            {

                List<Person> fromDataList = _dataAccessObject.Personel.FindForGoal(goalGUID).ToList<Person>();

                foreach (Person item in fromDataList)
                {

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

        public void GetGoalsForProject(Guid ProjectGUID, Action<List<BusinessGoal>, Exception> callback)
        {

            List<BusinessGoal> businessGoals = new List<BusinessGoal>();

            Exception error = null;

            try
            {

                IEnumerable<Goal> fromDataList = _dataAccessObject.Goals.FindGoalsFor(ProjectGUID);

                if (fromDataList != null)
                {
                    foreach (Goal item in fromDataList)
                    {

                        businessGoals.Add(new BusinessGoal(item));

                    }
                }

                error = null;
            }
            catch (Exception ex)
            {
                error = ex;
            }

            callback.Invoke(businessGoals, error);

        }

        #endregion

        public void AssignPersonToProject(Guid personGUID, Guid projectGUID, Action<Exception> callback)
        {
            Exception error = null;

            try
            {
                _dataAccessObject.LinkedData.AddPersonToProject(personGUID, projectGUID);

                error = null;
            }
            catch (Exception ex)
            {
                error = ex;
            }

            callback.Invoke(error);
        }

        public void UnassignPersonFromProject(Guid personGUID, Guid projectGUID, Action<Exception> callback)
        {
            Exception error = null;

            try
            {
                _dataAccessObject.LinkedData.RemovePersonFromProject(personGUID, projectGUID);

                error = null;
            }
            catch (Exception ex)
            {
                error = ex;
            }

            callback.Invoke(error);
        }

        public void AssignPersonToGoal(Guid personGUID, Guid goalGUID, Action<Exception> callback)
        {
            Exception error = null;

            try
            {
                _dataAccessObject.LinkedData.AddPersonToGoal(personGUID, goalGUID);

                error = null;
            }
            catch (Exception ex)
            {
                error = ex;
            }

            callback.Invoke(error);
        }
        public void UnassignPersonFromGoal(Guid personGUID, Guid goalGUID, Action<Exception> callback)
        {
            Exception error = null;

            try
            {
                _dataAccessObject.LinkedData.RemovePersonFromGoal(personGUID, goalGUID);             

                error = null;
            }
            catch (Exception ex)
            {
                error = ex;
            }

            callback.Invoke(error);
        }

    }

}

