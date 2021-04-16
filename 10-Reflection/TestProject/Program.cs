using Reflection.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            CommonTests t = new CommonTests();
            t.GetPublicObsoleteClasses_Should_Return_Right_List();
            t.GetProperty_Should_Return_Property_Value_For_Single_Path();
            t.GetProperty_Should_Return_Property_Value_For_Complex_Path();
            t.SetProperty_Should_Assign_Value_For_Single_Public_Path();
            t.GetProperty_Should_Assign_Value_For_Complex_Path();
            t.SetProperty_Should_Assign_Value_For_Single_Private_Path();
            CodeGenerationTests cg = new CodeGenerationTests();
            cg.GetVectorMultiplyFunction_Returns_Function_For_Int();
            cg.GetVectorMultiplyFunction_Returns_Function_For_Long();
            cg.GetVectorMultiplyFunction_Returns_Function_For_Double();
            cg.CodeGeneration_PerformanceTest();

            Console.WriteLine("Press any key");
            Console.ReadKey();
        }
    }
}
