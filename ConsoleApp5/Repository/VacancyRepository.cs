using ConsoleApp5.DapperUnitOfWork;
using ConsoleApp5.Model;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Typesense;
using Typesense.Setup;

namespace ConsoleApp5.Repository
{
    internal class VacancyRepository : IVacancyRepository
    {
        ITypesenseClient? _typesenseClient, _typesenseClientProd;

        public VacancyRepository()
        {
            //UAT
            var _serviceProvider = new ServiceCollection().AddTypesenseClient(config =>
            {
                config.ApiKey = "";
                config.Nodes = new List<Node>
                         {
                            new Node
                            {
                                Host = "",
                                Port = "",
                                Protocol = ""
                            }
                         };
            }).BuildServiceProvider();
            _typesenseClient = _serviceProvider.GetService<ITypesenseClient>();

            //Prod
            var _serviceProviderProd = new ServiceCollection().AddTypesenseClient(config =>
            {
                config.ApiKey = "";
                config.Nodes = new List<Node>
                         {
                            new Node
                            {
                                Host = "",
                                Port = "",
                                Protocol = ""
                            }
                         };
            }).BuildServiceProvider();
            _typesenseClientProd = _serviceProviderProd.GetService<ITypesenseClient>();
        }

        #region uat
        public async Task CreateSchemaUAT()
        {
            try
            {
                Console.WriteLine("Schema created started");
                ////////  vacancy_resume_schema.Name = "vacancy_resume";
                List<Field> _fields = new List<Field>();

                IList<typeSenceSchema> _schemafields = Read(@"C:\TypeSencevacancySchema\vacancyindexschema.json");

                foreach (var field in _schemafields)
                {
                    string fieldName = field.name;
                    FieldType fieldType = field.type;
                    bool optional = field.optional;
                    bool facet = field.facet;

                    _fields.Add(new Field(fieldName.Replace('.', '_'), fieldType, facet, optional));


                }
                var delete = _typesenseClientProd.DeleteCollection("vacancy");
                var vacancy_resume_schema = new Schema { Name = "vacancy", Fields = _fields };
                var createCollectionResult2 = await _typesenseClientProd.CreateCollection(vacancy_resume_schema);

                Console.WriteLine("Schema created");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public async Task IndexvacancyUAT()
        {
            try
            {
                IUnitOfWork _iUnitOfWork = new UnitOfWork();
                string sql = "";
                Console.WriteLine("Getting records from database");
                IEnumerable<VacancyList> hC_Vacancy = await _iUnitOfWork.ConnectionProd.QueryAsync<VacancyList>(sql,
                            new
                            {
                                clientid = 0,
                                contactid = 0,
                            },
                    commandType: System.Data.CommandType.StoredProcedure, commandTimeout: 1000);
                Console.WriteLine($"Total count from query {hC_Vacancy.Count()}");
                int totCount = hC_Vacancy.Count();
                DateTime indexStartDate = DateTime.Now;
                //https://timdeschryver.dev/blog/process-your-list-in-parallel-to-make-it-faster-in-dotnet
                int count = 0;
                foreach (var vacancy in hC_Vacancy)
                {
                    await doIndexUAT(vacancy);
                    if (count == 10000)
                    {
                        count = 0;
                        Console.WriteLine($" Total {totCount} / {hC_Vacancy.Count()} document index rid {vacancy.rid}");
                    }
                    count += 1;
                    totCount--;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }


        public static IList<typeSenceSchema> ReadUAT(string path)
        {
            using (StreamReader file = new StreamReader(path))
            {
                try
                {
                    string json = file.ReadToEnd();

                    var serializerSettings = new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    };

                    return JsonConvert.DeserializeObject<List<typeSenceSchema>>(json, serializerSettings);
                }
                catch (Exception)
                {
                    Console.WriteLine("Problem reading file");

                    return null;
                }
            }
        }
        private async Task doIndexUAT(VacancyList vacancy)
        {
            try
            {
                var createDocumentResult = await _typesenseClientProd.CreateDocument<VacancyList>("vacancy", vacancy);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error document index rid {vacancy.rid} -- error message {ex.Message}");
                Console.ReadLine();
            }
        }
        #endregion

        #region Prod
        public async Task CreateSchemaProd()
        {
            try
            {
                Console.WriteLine("Schema created started");
                ////////  vacancy_resume_schema.Name = "vacancy_resume";
                List<Field> _fields = new List<Field>();               

                IList<typeSenceSchema> _schemafields = Read(@"C:\TypeSencevacancySchema\vacancyindexschema.json");

                foreach (var field in _schemafields)
                {
                    string fieldName = field.name;
                    FieldType fieldType = field.type;
                    bool optional = field.optional;
                    bool facet = field.facet;

                    _fields.Add(new Field(fieldName.Replace('.', '_'), fieldType, facet, optional));                   


                }
                var delete = _typesenseClientProd.DeleteCollection("vacancy");
                var vacancy_resume_schema = new Schema { Name = "vacancy", Fields = _fields };
                var createCollectionResult2 = await _typesenseClientProd.CreateCollection(vacancy_resume_schema);
              
                Console.WriteLine("Schema created");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public async Task IndexvacancyProd()
        {
            try
            {
                IUnitOfWork _iUnitOfWork = new UnitOfWork();
                string sql = "";
                Console.WriteLine("Getting records from database");               
                IEnumerable<VacancyList> hC_Vacancy = await _iUnitOfWork.ConnectionProd.QueryAsync<VacancyList>(sql,
                            new
                            {
                                clientid = 0,
                                contactid = 0,
                            },
                    commandType: System.Data.CommandType.StoredProcedure, commandTimeout: 1000);
                Console.WriteLine($"Total count from query {hC_Vacancy.Count()}");
                int totCount = hC_Vacancy.Count();
                DateTime indexStartDate = DateTime.Now;
                //https://timdeschryver.dev/blog/process-your-list-in-parallel-to-make-it-faster-in-dotnet
                int count = 0;
                foreach (var vacancy in hC_Vacancy)
                {
                    await doIndex(vacancy);
                    if (count == 10000)
                    {
                        count = 0;
                        Console.WriteLine($" Total {totCount} / {hC_Vacancy.Count()} document index rid {vacancy.rid}");
                    }
                    count += 1;
                    totCount--;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }


        public static IList<typeSenceSchema> Read(string path)
        {
            using (StreamReader file = new StreamReader(path))
            {
                try
                {
                    string json = file.ReadToEnd();

                    var serializerSettings = new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    };

                    return JsonConvert.DeserializeObject<List<typeSenceSchema>>(json, serializerSettings);
                }
                catch (Exception)
                {
                    Console.WriteLine("Problem reading file");

                    return null;
                }
            }
        }
        private async Task doIndex(VacancyList vacancy)
        {
            try
            {
                var createDocumentResult = await _typesenseClientProd.CreateDocument<VacancyList>("vacancy", vacancy);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error document index rid {vacancy.rid} -- error message {ex.Message}");
                Console.ReadLine();
            }
        }
        #endregion
    }
}
