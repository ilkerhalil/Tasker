using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using Quartz;
using Tasker.Common.Abstraction;

namespace Tasker.QuartzAdapter
{
    public static class JobHelper
    {
        public static IJob ImplementIJob<T>(T task)
            where T : ITask
        {
            //var @nameSpace = task.GetType().Namespace;
            //var @name = task.GetType().Name;
            //https://github.com/punitganshani/codeinject
            var provider = new Microsoft.CSharp.CSharpCodeProvider();
            var parameters = new System.CodeDom.Compiler.CompilerParameters
            {
                GenerateExecutable = false,
                GenerateInMemory = true,
                IncludeDebugInformation = true
            };

            parameters.ReferencedAssemblies.Add("Tasker.Common.dll");
            parameters.ReferencedAssemblies.Add(typeof(IJob).Assembly.Location);
            var stringWriter = new StringWriter();
            var codeTypeDeclaration = new CodeTypeDeclaration(task.JobName);
            //{Name = "TestTask" FullName = "Tasker.Common.TestTask"}
            foreach (var fieldInfo in task.GetType().GetFields())
            {
                codeTypeDeclaration.Members.Add(new CodeMemberField { Name = fieldInfo.Name });
            }



            #region ImplementIJob

            codeTypeDeclaration.BaseTypes.Add(new CodeTypeReference(typeof(IJob)));
            var cs1 = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "Run");
            var codeMemberMethod = new CodeMemberMethod
            {
                Name = "Execute",
                Parameters = { new CodeParameterDeclarationExpression(typeof(IJobExecutionContext), "context") },
                ReturnType = new CodeTypeReference(typeof(void)),
                Attributes = MemberAttributes.Public
            };
            codeMemberMethod.Statements.Add(cs1);
            codeTypeDeclaration.Members.Add(codeMemberMethod);

            #endregion

            provider.GenerateCodeFromType(codeTypeDeclaration, stringWriter, new CodeGeneratorOptions());

            return null;
        }
        public static Type GetMonoType(this TypeReference type)
        {
            return Type.GetType(type.GetReflectionName(), true);
        }

        private static string GetReflectionName(this TypeReference type)
        {
            if (!type.IsGenericInstance) return type.FullName;
            var genericInstance = (GenericInstanceType)type;
            return
                $"{genericInstance.Namespace}.{type.Name}[{String.Join(",", genericInstance.GenericArguments.Select(p => p.GetReflectionName()).ToArray())}]";
        }
    }
}
