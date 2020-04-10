using System.Web.Mvc;

namespace LigaSoft.Models.Attributes.GPRPattern
{
    public class ModelStateTempDataTransfer : ActionFilterAttribute
    {
        protected static readonly string Key = typeof(ModelStateTempDataTransfer).FullName;
    }
}