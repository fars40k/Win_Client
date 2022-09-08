

namespace Win_Dev.Data
{
    /// <summary>
    /// Contains Repositories with basic database operations and worker for complex
    /// </summary>
    public class DataAccessObject : IDataAccessService
    {
        public NetworkClient Client;
        public ProjectsRepository Projects;
        public PersonelRepository Personel;
        public GoalsRepository Goals;
        public LinkedDataWorker LinkedData;
        public DataAccessObject(NetworkClient client)
        {

            Client = client;

            Projects = new ProjectsRepository();
            Personel = new PersonelRepository();
            Goals = new GoalsRepository();
            LinkedData = new LinkedDataWorker();

            Client.TokenChanged += (s) =>
            {
                Projects = new ProjectsRepository();
                Personel = new PersonelRepository();
                Goals = new GoalsRepository();
                LinkedData = new LinkedDataWorker();

            };

        }

    }
}
