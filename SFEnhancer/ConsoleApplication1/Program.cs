using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            DependentPickListGenerator.PickDependentPicklists(@"D:\Domain model detalization (commented).txt", @"D:\result1.txt");
            //CodeGen.GenerateInsertStatementForSWAPI(@"D:\fields3.txt");
            //CodeGen.ReverseCode(@"D:\fields2.txt");
            //FieldsGen.GenerateFields(@"D:\test1.txt", @"D:\result1.txt");
            //FieldsGen.GenerateFields(@"D:\OCPMonthlyExtract_DecemberSample.csv", @"D:\result.txt");
            //CodeGen.GenerateCode(@"D:\source.txt", "ocpCase", "fileRecord");
            //CodeGen.GenerateAnonymousCode(@"D:\OCPMonthlyExtract_DecemberSample.csv", "ocp");
        }
    }
}
