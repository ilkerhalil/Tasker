using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.ObjectBuilder2;
using Quartz;
using Tasker.Common.Abstraction;

namespace Tasker.QuartzAdapter.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class JobHelper
    {
        /// <summary>
        /// <see cref="ITask"/> interface'ini implemente eden bir class'a ait bir instance parametre olarak verildiğinde 
        /// parametre olarak verilen obje ye dinamik olarak <see cref="IJob"/> interface'ini implemente eder. dönüşte IJob
        /// türünde bir obje olarak geri döndürür.
        /// </summary>
        /// <typeparam name="T"><see cref="ITask"/> interface yada ITask' tan türeyen bir tip</typeparam>
        /// <param name="task">T type parametresinde belirtilmiş olan tip'e ait bir instance</param>
        /// <returns>IJob tipine ait bir instance</returns>
        //public static IJob ImplementIJob<T>(this T task)
        //    where T : ITask
        //{
        //    var compilerParameters = CreateCompilerParameters(task);
        //    var provider = CodeDomProvider.CreateProvider("C#");
        //    var targetUnit = new CodeCompileUnit();
        //    var codeNameSpace = CreateCodeNameSpace(task);
        //    var codeTypeDeclaration = CreateTypeDeclaration(task);
        //    var codeMemberMethod = CreateExecuteMethod();
        //    codeTypeDeclaration.Members.Add(codeMemberMethod);
        //    codeTypeDeclaration.Members.AddRange(CreateConstructor(task).ToArray());
        //    codeNameSpace.Types.Add(codeTypeDeclaration);
        //    targetUnit.Namespaces.Add(codeNameSpace);
        //    //var stringWriter = new StringWriter();
        //    //provider.GenerateCodeFromCompileUnit(targetUnit, stringWriter, new CodeGeneratorOptions());

        //    var result = provider.CompileAssemblyFromDom(compilerParameters, targetUnit);
        //    Assembly compiledAssembly = null;
        //    if (result.Errors != null && result.Errors.Count == 0)
        //    {
        //        compiledAssembly = result.CompiledAssembly;
        //    }
        //    if (compiledAssembly == null) return null;
        //    var targetType = compiledAssembly.GetTypes().Single(w => w.Name == task.JobName);
        //    //if (task.GetType().GetConstructors().Length <= 1)
        //    //    return MapProperties(task,(IJob)Activator.CreateInstance(targetType));
        //    var constructorArguments = new Dictionary<Type, object>();
        //    foreach (var constructorInfo in task.GetType().GetConstructors())
        //    {
        //        var methodBody =constructorInfo.GetMethodBody();
                

        //    }


        //    var ctor = targetType.GetConstructor(constructorArguments.Keys.ToArray());
        //    return MapProperties(task, ctor.Invoke(constructorArguments.Values.ToArray()) as IJob);
        //}
        //private static CodeNamespace CreateCodeNameSpace<T>(T task) where T : ITask
        //{
        //    var codeNameSpace = new CodeNamespace(task.GetType().Namespace);
        //    //codeNameSpace.Imports.Add(new CodeNamespaceImport("System"));
        //    //codeNameSpace.Imports.Add(new CodeNamespaceImport("Quartz"));
        //    return codeNameSpace;
        //}

        //private static CompilerParameters CreateCompilerParameters<T>(T task) where T : ITask
        //{
        //    var compilerParameters = new CompilerParameters
        //    {
        //        GenerateInMemory = true
        //    };
        //    compilerParameters.ReferencedAssemblies.Add("System.dll");
        //    compilerParameters.ReferencedAssemblies.Add("Common.Logging.dll");
        //    compilerParameters.ReferencedAssemblies.Add("Common.Logging.Core.dll");
        //    compilerParameters.ReferencedAssemblies.Add("Quartz.dll");
        //    compilerParameters.ReferencedAssemblies.Add("Tasker.Common.dll");
        //    compilerParameters.ReferencedAssemblies.Add(new Uri(task.GetType().Assembly.CodeBase).LocalPath);
        //    return compilerParameters;
        //}

        //private static CodeTypeDeclaration CreateTypeDeclaration<T>(T task) where T : ITask
        //{
        //    var codeTypeDeclaration = new CodeTypeDeclaration(task.JobName)
        //    {
        //        IsClass = true,
        //        TypeAttributes = TypeAttributes.Public,
        //    };
        //    codeTypeDeclaration.BaseTypes.Add(task.GetType().FullName);
        //    codeTypeDeclaration.BaseTypes.Add(typeof(IJob));
        //    return codeTypeDeclaration;
        //}

        //private static IEnumerable<CodeConstructor> CreateConstructor<T>(T type)
        //{
        //    var constructors = type.GetType().GetConstructors();
        //    foreach (var constructorInfo in constructors)
        //    {
        //        var codeConstructor = new CodeConstructor();
        //        switch (constructorInfo.Attributes)
        //        {
        //            case MethodAttributes.Private:
        //                codeConstructor.Attributes = MemberAttributes.Private;
        //                break;
        //            case MethodAttributes.Public:
        //                codeConstructor.Attributes = MemberAttributes.Public;
        //                break;
        //            case MethodAttributes.Static:
        //                codeConstructor.Attributes = MemberAttributes.Static;
        //                break;
        //            default:
        //                throw new ArgumentOutOfRangeException();
        //        }
        //        foreach (var parameterInfo in constructorInfo.GetParameters())
        //        {
        //            var codeParameter = new CodeParameterDeclarationExpression(parameterInfo.ParameterType,
        //                parameterInfo.Name);
        //            codeConstructor.Parameters.Add(codeParameter);
        //            codeConstructor.BaseConstructorArgs.Add(new CodeArgumentReferenceExpression(parameterInfo.Name));
        //        }
        //        yield return codeConstructor;
        //    }
        //}


        //private static CodeMemberMethod CreateExecuteMethod()
        //{
        //    var codeMemberMethod = new CodeMemberMethod
        //    {
        //        Name = "Execute",
        //        Attributes = (MemberAttributes)24582
        //    };
        //    codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(IJobExecutionContext), "context"));
        //    codeMemberMethod.ImplementationTypes.Add(typeof(IJob));
        //    codeMemberMethod.Statements.Add(new CodeConditionStatement(new CodeBinaryOperatorExpression(new CodeArgumentReferenceExpression("context"), CodeBinaryOperatorType.ValueEquality, new CodeDefaultValueExpression(new CodeTypeReference(typeof(IJobExecutionContext)))), new CodeMethodReturnStatement()));
        //    codeMemberMethod.Statements.Add(new CodeConditionStatement(new CodePropertyReferenceExpression(new CodeArgumentReferenceExpression("context"), "NextFireTimeUtc.HasValue"), new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "NextFireTime"), new CodePropertyReferenceExpression(new CodeArgumentReferenceExpression("context"), "NextFireTimeUtc.Value.LocalDateTime"))));
        //    codeMemberMethod.Statements.Add(new CodeConditionStatement(new CodePropertyReferenceExpression(new CodeArgumentReferenceExpression("context"), "PreviousFireTimeUtc.HasValue"), new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "PreviousFireTime"), new CodePropertyReferenceExpression(new CodeArgumentReferenceExpression("context"), "PreviousFireTimeUtc.Value.LocalDateTime"))));
        //    codeMemberMethod.Statements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "TaskRunTime"), new CodePropertyReferenceExpression(new CodeArgumentReferenceExpression("context"), "JobRunTime")));
        //    codeMemberMethod.Statements.Add(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeThisReferenceExpression(), "Run")));
        //    return codeMemberMethod;
        //}

        ///// <summary>
        ///// Girişte verilen ITask türündeki obje üzerindeki property'leri IJob türünde olan objeye map eder. 
        ///// taşıdıkları değerleri eşitler
        ///// </summary>
        ///// <param name="sourceInstance"><see cref="ITask"/> türünde bir instance</param>
        ///// <param name="targetInstance"><see cref="IJob"/> türünde bir instance</param>
        ///// <returns>Girişte verilen IJob türündeki instance geriye döner</returns>
        //private static IJob MapProperties(ITask sourceInstance, IJob targetInstance)
        //{
        //    var sourceInstanceProperties = sourceInstance.GetType().GetProperties();
        //    sourceInstanceProperties.ForEach(propertyInfo =>
        //    {
        //        var sourceInstancePropertyValue = propertyInfo.GetValue(sourceInstance);
        //        if (sourceInstancePropertyValue == null) return;
        //        var backingFieldName = $"<{propertyInfo.Name}>k__BackingField";
        //        if (propertyInfo.DeclaringType == null) return;
        //        var backingField = propertyInfo.DeclaringType.GetField(backingFieldName, BindingFlags.Instance | BindingFlags.NonPublic);
        //        backingField?.SetValue(targetInstance, sourceInstancePropertyValue);
        //    });
        //    return targetInstance;
        //}
    }
}
