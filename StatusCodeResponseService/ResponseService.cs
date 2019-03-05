using Microsoft.AspNetCore.Mvc;
using System;

namespace StatusCodeResponseService
{
    public class ResponseService : ControllerBase
    {
        public T GetResponse<T>(int statusCode)
        {

            switch (statusCode)
            {
                case 400:
                    //
                    break;
                case 401:
                    //
                    break;
                case 403:
                    //
                    break;
                case 422:
                    //
                    break;

                default:
                    //
                    break;
            }

            return;
        }
    }
}
