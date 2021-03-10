using AlkUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlkUniversity.Filter
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeUser : AuthorizeAttribute
    {
        private users oUser;
        private AlkUniversity_dbEntities db = new AlkUniversity_dbEntities();
        private int idOperation;

        public AuthorizeUser(int idOperation = 0)
        {
            this.idOperation = idOperation;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            String operation_name = "";
            String module_name = "";
            try
            {
                oUser = (users)HttpContext.Current.Session["User"];
                var lst = from m in db.role_operation
                                        where m.role_id == oUser.role_id && m.operation_id == idOperation
                                        select m;


                if (lst.ToList().Count() == 0)
                {
                    var oOperation = db.operation.Find(idOperation);
                    int? idModule = oOperation.module_id;
                    operation_name = getOperationName(idOperation);
                    module_name = getModuleName(idModule);
                    filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation?operation=" + operation_name + "&module=" + module_name + "&msgeErrorExcepcion=");
                }
            }
            catch (Exception ex)
            {
                filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation?operation=" + operation_name + "&module=" + module_name + "&msgeErrorExcepcion=" + ex.Message);
            }
        }
        public string getOperationName(int idOperation)
        {
            var ope = from op in db.operation
                      where op.id == idOperation
                      select op.name;
            String operation_name;
            try
            {
                operation_name = ope.First();
            }
            catch (Exception)
            {
                operation_name = "";
            }
            return operation_name;
        }
        public string getModuleName(int? idModule)
        {
            var module = from m in db.module
                         where m.id == idModule
                         select m.name;
            String module_name;
            try
            {
                module_name = module.First();
            }
            catch (Exception)
            {
                module_name = "";
            }
            return module_name;
        }
    }
}