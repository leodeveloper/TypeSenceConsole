// See https://aka.ms/new-console-template for more information
using ConsoleApp5.Repository;

Console.WriteLine("Hello, World!");
try
{
   // Console.WriteLine("Start on UAT");
    ////UAT
  //  ITypeSenseRepository typeSenseRepository = new TypeSenseRepository();
  //  await typeSenseRepository.CreateSchemaUAT();
 //   await typeSenseRepository.IndexJobSeekerUAT();



    // Console.WriteLine("Start on prod");
    // Prod
    //  ITypeSenseRepository typeSenseRepository = new TypeSenseRepository();
    //  await typeSenseRepository.CreateSchemaProd();
    // await typeSenseRepository.IndexJobSeekerProd();

   

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

