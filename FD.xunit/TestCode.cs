using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using System.IO;
using System.Reflection;

namespace FD.xunit
{
    public class TestCode
    {
        [Fact]
        void Test() {
            if (!Directory.Exists("dd"))
            {
                Directory.CreateDirectory("dd");
            }
            else
            {
                Directory.Delete("dd", true);
            }
            MetadataReference[] _ref = DependencyContext.Default.CompileLibraries.SelectMany(a=>a.ResolveReferencePaths().Select(b=> MetadataReference.CreateFromFile(b)).ToArray()).ToArray();

              

          
            string testClass = @"using System; 
              namespace test{
               public class tes
               {
                 public string unescape(string Text)
                    { 
                      return Uri.UnescapeDataString(Text);
                    } 
               }
              }";

            var compilation = CSharpCompilation.Create(Guid.NewGuid().ToString() + ".dll")
                .WithOptions(new CSharpCompilationOptions(
                    Microsoft.CodeAnalysis.OutputKind.DynamicallyLinkedLibrary,
                    usings: null,
                    optimizationLevel: OptimizationLevel.Debug, // TODO
                    checkOverflow: false,                       // TODO
                    allowUnsafe: true,                          // TODO
                    platform: Platform.AnyCpu,
                    warningLevel: 4,
                    xmlReferenceResolver: null // don't support XML file references in interactive (permissions & doc comment includes)
                    ))
                .AddReferences(_ref)
              
              .AddSyntaxTrees(CSharpSyntaxTree.ParseText(testClass));
            Directory.CreateDirectory("dd");
            var eResult = compilation.Emit("dd/test.dll");
            AssemblyName assembly = AssemblyName.GetAssemblyName("dd/test.dll");
          
             var dpsssp = Type.GetType("System.Collections.Generic.List`1[[tes, test]]");
        }
    }
}
