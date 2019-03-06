using APIErrorHandling;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using StatusCodeResponseService.Helpers;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace StatusCodeResponseService
{
    public static class ResponseService
    {
        public static async Task<R> GetResponse<R, T>(T _object, ModelStateDictionary modelState)
        {
            Type objectType = _object.GetType();
            Type R_instanceType = typeof(R);

            object[] args = null;

            bool hasCodeProp = _object.HasProperty(ResponseServiceConstants.Code);
            bool hasDescriptionProp = _object.HasProperty(ResponseServiceConstants.Description);

            if (hasCodeProp == true && hasDescriptionProp == true)
            {

                PropertyInfo codeInfo = objectType.GetProperty(ResponseServiceConstants.Code);
                PropertyInfo descriptionInfo = objectType.GetProperty(ResponseServiceConstants.Description);

                string codeValue = codeInfo.GetValue(_object).ToString();
                string descriptionValue = descriptionInfo.GetValue(_object).ToString();

                args = new object[]
                {
                    Errors.AddErrorToModelState(codeValue, descriptionValue, modelState)
                };
            }
            else
            {
                args = new object[]
               {
                    Errors.AddErrorToModelState(
                        ResponseServiceConstants.DefaultCode,
                        ResponseServiceConstants.DefaultDescription,
                        modelState
                        )
               };
            }

            R R_objectInstance = await Task.FromResult((R)Activator.CreateInstance(R_instanceType, args));

            return R_objectInstance;
        }

        //Helper
        private static bool HasProperty(this object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName) != null;
        }
    }
}
