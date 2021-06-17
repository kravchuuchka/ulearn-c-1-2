using System;
using System.Linq;
using System.Reflection;

namespace Documentation
{
    public class Specifier<T> : ISpecifier
    {
        private Type type = typeof(T);

        public string GetApiDescription()
        {
            return type.GetCustomAttributes<ApiDescriptionAttribute>().FirstOrDefault()?.Description;
        }

        public string[] GetApiMethodNames()
        {
            return type.GetMethods().Where(method =>
            method.GetCustomAttributes(true).OfType<ApiMethodAttribute>().Any())
            .Select(method => method.Name)
            .ToArray();
        }

        public string GetApiMethodDescription(string methodName)
        {
            var method = GetMethod(methodName);
            return method?.GetCustomAttributes<ApiDescriptionAttribute>().FirstOrDefault()?.Description;
        }

        public string[] GetApiMethodParamNames(string methodName)
        {
            var method = GetMethod(methodName);
            return method.GetParameters().Select(parameter => parameter.Name).ToArray();
        }

        public string GetApiMethodParamDescription(string methodName, string paramName)
        {
            var method = GetMethod(methodName);
            var parameter = method?.GetParameters().FirstOrDefault(param => param.Name == paramName);
            return parameter?.GetCustomAttributes<ApiDescriptionAttribute>().FirstOrDefault()?.Description;
        }

        public ApiParamDescription GetApiMethodParamFullDescription(string methodName, string paramName)
        {
            var paramDescription = new ApiParamDescription();
            paramDescription.ParamDescription = new CommonDescription(paramName);
            var method = GetMethod(methodName);
            if (method?.GetCustomAttribute<ApiMethodAttribute>() == null)
                return paramDescription;
            var parameter = method.GetParameters().Where(param => param.Name == paramName);
            if (!parameter.Any())
                return paramDescription;
            return GetParamDescription(parameter.First(), paramDescription);
        }

        public ApiMethodDescription GetApiMethodFullDescription(string methodName)
        {
            var fullDescription = new ApiMethodDescription();
            var method = GetMethod(methodName);
            if (method?.GetCustomAttribute<ApiMethodAttribute>() == null)
                return null;
            fullDescription.MethodDescription = new CommonDescription(methodName);
            fullDescription.ParamDescriptions = method.GetParameters()
            .Select(param => GetApiMethodParamFullDescription(methodName, param.Name))
            .ToArray();
            fullDescription.MethodDescription.Description =
            method.GetCustomAttributes<ApiDescriptionAttribute>().FirstOrDefault()?.Description;
            var parameter = method.ReturnParameter;
            if (parameter.GetCustomAttributes<ApiIntValidationAttribute>().FirstOrDefault() ==
            null && parameter.GetCustomAttributes<ApiRequiredAttribute>().FirstOrDefault() == null)
                return fullDescription;
            var paramDescription = new ApiParamDescription();
            paramDescription.ParamDescription = new CommonDescription();
            fullDescription.ReturnDescription = GetParamDescription(parameter, paramDescription);
            return fullDescription;
        }

        public ApiParamDescription GetParamDescription(ParameterInfo parameter, ApiParamDescription paramDescription)
        {
            var description = parameter.GetCustomAttributes<ApiDescriptionAttribute>().FirstOrDefault();
            if (description != null)
                paramDescription.ParamDescription.Description = description.Description;
            var intValidationAttribute = parameter.GetCustomAttributes<ApiIntValidationAttribute>().FirstOrDefault();
            if (intValidationAttribute != null)
            {
                paramDescription.MinValue = intValidationAttribute.MinValue;
                paramDescription.MaxValue = intValidationAttribute.MaxValue;
            }
            var requiredAttribute = parameter.GetCustomAttributes<ApiRequiredAttribute>().FirstOrDefault();
            if (requiredAttribute != null)
                paramDescription.Required = requiredAttribute.Required;
            return paramDescription;
        }

        public MethodInfo GetMethod(string methodName)
        {
            return type.GetMethod(methodName);
        }
    }
}