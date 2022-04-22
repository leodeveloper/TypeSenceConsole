
namespace ConsoleApp5.Repository
{
    internal interface ITypeSenseRepository
    {
        Task CreateSchemaProd();
        Task CreateSchemaUAT();
        string getQuery();      
        Task IndexJobSeekerProd();
        Task IndexJobSeekerUAT();
    }
}