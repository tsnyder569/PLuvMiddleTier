using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;

namespace TIC.PuppyLove.Profile.DataModel
{
    public class FormattedEntityValidationException : Exception
    {
        public FormattedEntityValidationException(DbEntityValidationException innerException) :
            base(null, innerException)
        {
        }

        public FormattedEntityValidationException(DbUpdateException innerException) :
            base(null, innerException)
        {
        }

        public override string Message
        {
            get
            {
                //var innerException = InnerException as DbEntityValidationException;
                string errMessage = String.Empty;

                if (InnerException is DbEntityValidationException)
                {
                    var innerException = InnerException as DbEntityValidationException;
                    if (innerException != null)
                    {
                        StringBuilder sb = new StringBuilder();

                        sb.AppendLine();
                        sb.AppendLine();
                        foreach (var eve in innerException.EntityValidationErrors)
                        {
                            sb.AppendLine(string.Format("- Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().FullName, eve.Entry.State));
                            foreach (var ve in eve.ValidationErrors)
                            {
                                sb.AppendLine(string.Format("-- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                                    ve.PropertyName,
                                    eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                                    ve.ErrorMessage));
                            }
                        }
                        sb.AppendLine();

                        errMessage = sb.ToString();
                    }
                }
                else
                {
                    if (InnerException.InnerException != null)
                    {
                        var sqlException = InnerException.InnerException;
                        if (sqlException.InnerException != null)
                            errMessage = sqlException.InnerException.Message;
                    }
                    
                }

                
                return errMessage;
            }
        }
    }
}
 
