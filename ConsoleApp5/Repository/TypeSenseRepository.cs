using ConsoleApp5.DapperUnitOfWork;
using ConsoleApp5_Model;
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
    internal class TypeSenseRepository : ITypeSenseRepository
    {

        ITypesenseClient? _typesenseClient, _typesenseClientProd;

        public TypeSenseRepository()
        {

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

        #region UAT
        public async Task CreateSchemaUAT()
        {
            try
            {
                Console.WriteLine("Schema created started");
                
                List<Field> _fields = new List<Field>();

                IList<JobSeekerSchema> _schemafields = ReadUAT(@"C:\TypeSenceJobSeekerSchema\jobseekerindexschema.json");

             

                foreach (var field in _schemafields)
                {
                    string fieldName = field.name;
                    FieldType fieldType = field.type;
                    bool optional = field.optional;
                    bool facet = field.facet;

                    _fields.Add(new Field(fieldName.Replace('.', '_'), fieldType, facet, optional));

                    // File.AppendAllText(@"C:\Logs\csc.txt", "Public "+fieldType.ToString()+" "+fieldName.ToLower()+" {get;set;}");

                    // File.WriteAllLines(fullPath, );


                }
                var delete = _typesenseClient.DeleteCollection("jobseeker");

              //  var z = _schemafields.GroupBy(x => new { x.name }).Select(x => new { x.Key.name, Count = x.Count() });              

                var jobseeker_resume_schema = new Schema { Name = "jobseeker", Fields = _fields, DefaultSortingField = "yearofregistration" };
                var createCollectionResult2 = await _typesenseClient.CreateCollection(jobseeker_resume_schema);


                Console.WriteLine("Schema created");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public async Task IndexJobSeekerUAT()
        {
            try
            {
                IUnitOfWork _iUnitOfWork = new UnitOfWork();
                string sql = getQuery();
                Console.WriteLine("Getting records from database");
                //_iUnitOfWork.ConnectionProd.Database.SetCommandTimeout(120);
                IEnumerable<jobseeker_resume> hC_ResumeBanks = await _iUnitOfWork.Connection.QueryAsync<jobseeker_resume>(sql, commandTimeout: 1000);
                Console.WriteLine($"Total count from query {hC_ResumeBanks.Count()}");
                int totCount = hC_ResumeBanks.Count();
                DateTime indexStartDate = DateTime.Now;
                //https://timdeschryver.dev/blog/process-your-list-in-parallel-to-make-it-faster-in-dotnet
                int count = 0;
                foreach (var jobseeker in hC_ResumeBanks)
                {
                    await doIndexUAT(jobseeker);
                    if (count == 10000)
                    {
                        count = 0;
                        Console.WriteLine($" Total {totCount} / {hC_ResumeBanks.Count()} document index rid {jobseeker.rid}");
                    }
                    count += 1;
                    totCount--;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //Console.ReadLine();
            }


        }

        private async Task doIndexUAT(jobseeker_resume jobseeker)
        {
            try
            {
                var createDocumentResult = await _typesenseClient.CreateDocument<jobseeker_resume>("jobseeker", jobseeker);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error document index rid {jobseeker.rid} -- error message {ex.Message}");
                //Console.ReadLine();
            }
        }
        #endregion

        #region Prod
        public async Task CreateSchemaProd()
        {
            try
            {
                Console.WriteLine("Schema created started");
                //     var retrieveCollections = await _typesenseClientProd.RetrieveCollection("jobseeker_resume_prod_v1");
                //    Console.WriteLine("Schema deleted jobseeker_resume_prod_v2");
                //   var retrieveCollections = await _typesenseClientProd.RetrieveCollection("jobseeker_resume");
                //   var delete = _typesenseClientProd.DeleteCollection("jobseeker_resume_prod_v2");




                ////////  jobseeker_resume_schema.Name = "jobseeker_resume";
                List<Field> _fields = new List<Field>();
                ////_fields.Add(new Field("id", FieldType.Int32, false));
                ////_fields.Add(new Field("rid", FieldType.Int32, false));
                //// _fields.Add(new Field("JPCAssessmentStatus", FieldType.String, false, true));
                ////  _fields.Add(new Field("JPCAssessment", FieldType.Int32, false, true));

                //_fields.Add(new Field("jobseekercvmongoid", FieldType.String, false, true));             
                //_fields.Add(new Field("iscvattached", FieldType.String, true, true));


                //_fields.Add(new Field("isssabeneficiary", FieldType.String, true, true));

                //_fields.Add(new Field("ismilitarycompleted", FieldType.String, true, true));
                //_fields.Add(new Field("isdrivinglicence", FieldType.String, true, true));


                //_fields.Add(new Field("emirateen", FieldType.String, true, true));
                //_fields.Add(new Field("emirateae", FieldType.String, true, true));

                //_fields.Add(new Field("cityen", FieldType.String, true, true));
                //_fields.Add(new Field("cityae", FieldType.String, true, true));

                //_fields.Add(new Field("locationen", FieldType.String, true, true));
                //_fields.Add(new Field("locationen", FieldType.String, true, true));

                //_fields.Add(new Field("iscvsummary", FieldType.String, true, true));

                IList<JobSeekerSchema> _schemafields = Read(@"C:\TypeSenceJobSeekerSchema\jobseekerindexschema.json");

                foreach (var field in _schemafields)
                {
                    string fieldName = field.name;
                    FieldType fieldType = field.type;
                    bool optional = field.optional;
                    bool facet = field.facet;

                    _fields.Add(new Field(fieldName.Replace('.', '_'), fieldType, facet, optional));

                    // File.AppendAllText(@"C:\Logs\csc.txt", "Public "+fieldType.ToString()+" "+fieldName.ToLower()+" {get;set;}");

                    // File.WriteAllLines(fullPath, );


                }
                var delete = _typesenseClientProd.DeleteCollection("jobseeker");
                var jobseeker_resume_schema = new Schema { Name = "jobseeker", Fields = _fields, DefaultSortingField = "yearofregistration" };
                var createCollectionResult2 = await _typesenseClientProd.CreateCollection(jobseeker_resume_schema);

                ////    jobseeker_resume_schema.Fields = this._fields;

                //var retrieveCollection = await _typesenseClient.RetrieveCollection("MyAddresses");
                //if(retrieveCollection == null)
                //{
                //    var schema = new Schema
                //    {
                //        Name = "MyAddresses",
                //        Fields = new List<Field>
                //        {
                //            new Field("id", FieldType.String, false),
                //            new Field("houseNumber", FieldType.Int32, false),
                //            new Field("accessAddress", FieldType.String, false, true),
                //        },
                //        DefaultSortingField = "houseNumber"
                //    };

                //    var createCollectionResult = await _typesenseClient.CreateCollection(schema);
                //}
                // var deleteCollection = await _typesenseClient.DeleteCollection("MyAddresses");
                Console.WriteLine("Schema created");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public async Task IndexJobSeekerProd()
        {
            try
            {
                IUnitOfWork _iUnitOfWork = new UnitOfWork();
                string sql = getQuery();
                Console.WriteLine("Getting records from database");
                //_iUnitOfWork.ConnectionProd.Database.SetCommandTimeout(120);
                IEnumerable<jobseeker_resume> hC_ResumeBanks = await _iUnitOfWork.ConnectionProd.QueryAsync<jobseeker_resume>(sql, commandTimeout: 1000);
                Console.WriteLine($"Total count from query {hC_ResumeBanks.Count()}");
                int totCount = hC_ResumeBanks.Count();
                DateTime indexStartDate = DateTime.Now;
                //https://timdeschryver.dev/blog/process-your-list-in-parallel-to-make-it-faster-in-dotnet
                int count = 0;
                foreach (var jobseeker in hC_ResumeBanks)
                {
                    await doIndex(jobseeker);
                    if (count == 10000)
                    {
                        count = 0;
                        Console.WriteLine($" Total {totCount} / {hC_ResumeBanks.Count()} document index rid {jobseeker.rid}");
                    }
                    count += 1;
                    totCount--;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //Console.ReadLine();
            }


        }

        private async Task doIndex(jobseeker_resume jobseeker)
        {
            try
            {
                var createDocumentResult = await _typesenseClientProd.CreateDocument<jobseeker_resume>("jobseeker", jobseeker);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error document index rid {jobseeker.rid} -- error message {ex.Message}");
                //Console.ReadLine();
            }
        }
        #endregion
        /// <summary>
        ///  //when ever change the the query here change in typesenceapi project as well
        /// </summary>
        /// <returns></returns>
        public string getQuery()
        {
            string strQry = "select ";
            
            return strQry;
        }

       

        public static IList<JobSeekerSchema> Read(string path)
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

                    return JsonConvert.DeserializeObject<List<JobSeekerSchema>>(json, serializerSettings);
                }
                catch (Exception)
                {
                    Console.WriteLine("Problem reading file");

                    return null;
                }
            }
        }

        public static IList<JobSeekerSchema> ReadUAT(string path)
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

                    return JsonConvert.DeserializeObject<List<JobSeekerSchema>>(json, serializerSettings);
                }
                catch (Exception)
                {
                    Console.WriteLine("Problem reading file");

                    return null;
                }
            }
        }


        public class JobSeekerSchema
        {
            public bool facet { get; set; }
            public string index { get; set; }
            public string name { get; set; }
            public bool optional { get; set; }
            public FieldType type { get; set; }
        }

    }
}
