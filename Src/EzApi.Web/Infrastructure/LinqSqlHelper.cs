using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Ez.Web.Infrastructure
{
    public static class LinqSQLHelper
    {
        public static string DynamicSQLTemplate(string sql)
        {
            return DynamicSQLTemplate(sql, false);
        }
        public static string DynamicSQLTemplate(string sql, bool WrapInTransaction)
        {
            var sSQL = @"SET ANSI_WARNINGS OFF;
            BEGIN TRY ";
            sSQL += sql;
            sSQL += @"
END TRY
BEGIN CATCH
    DECLARE @ErrorNumber INT = ERROR_NUMBER();
    DECLARE @ErrorLine INT = ERROR_LINE(); 
    DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
    DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
    DECLARE @ErrorState INT = ERROR_STATE();
    SET @ErrorMessage = @ErrorMessage + ', Actual error number: ' + CAST(@ErrorNumber AS VARCHAR(10)) + ', Actual line number: ' + CAST(@ErrorLine AS VARCHAR(10));

    RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
END CATCH";
            return sSQL;
        }
        public static string ParmToLinqCond(string FieldName, string searchParm)
        {
            string sWhere = "";
            if ((searchParm.StartsWith("*")) && (searchParm.EndsWith("*")))
            {
                sWhere = string.Format(FieldName + ".Contains(\"{0}\")", searchParm.Replace("*", ""));
            }
            else if (searchParm.StartsWith("*"))
            {
                sWhere = string.Format(FieldName + ".EndsWith(\"{0}\")", searchParm.Replace("*", ""));
            }
            else if (searchParm.EndsWith("*"))
            {
                sWhere = string.Format(FieldName + ".StartsWith(\"{0}\")", searchParm.Replace("*", ""));
            }
            else
            {
                sWhere = string.Format(FieldName + " == \"{0}\"", searchParm.Replace("*", ""));
            }
            return sWhere;
        }


        public static string Quoted(string str)
        {
            return "\"" + str.Replace("\"", "\"\"") + "\"";
        }

        public static string SingleQuoted(string str)
        {
            return "'" + str.Replace("'", "''") + "'";
        }

        //This will take the a query string parameter in the format of :
        // filter=FIELDA,FIELDB,FIELDC and change it to FieldName="FIELDA" OR FieldName = "FIELDB" OR FieldName= "FIELDC"
        public static string SQLFilterExploderIN(string FieldName, string str)
        {
            List<string> lstValues = new List<string>();
            string[] strarr = str.Split(',');
            foreach (string sVal in strarr)
            {
                lstValues.Add("(" + FieldName + "==" + Quoted(sVal) + ")");
            }
            return String.Join(" || ", lstValues);
        }

        //This will take the a query string parameter in the format of :
        // order=FIELDA|FIELDB:A|FIELDC:D and change it to FIELDA ASC, FIELDB ASC, FIELDC DESC
        public static string SQLWhereExploder(string str)
        {
            return SQLWhereExploder(str, "");
        }

        public static string SQLWhereExploder(string str, string DefaultTablePrefix)
        {
            List<string> lstOrder = new List<string>();
            string[] strarr = str.Split(',');
            foreach (string sOrderClause in strarr)
            {
                string sOrderDir = "ASC";
                string sOrderField = "";
                if (sOrderClause.IndexOf('|') < 0)
                {
                    sOrderField = sOrderClause;
                }
                else
                {
                    string[] arrFieldParms = sOrderClause.Split('|');
                    if (arrFieldParms.Length > 0)
                    {
                        sOrderField = arrFieldParms[0];
                        if (arrFieldParms.Length > 1)
                        {
                            if (arrFieldParms[1].StartsWith("D"))
                            {
                                sOrderDir = "DESC";
                            }
                        }
                    }
                }
                lstOrder.Add(((DefaultTablePrefix.Length > 0) ? DefaultTablePrefix + "." : "") + sOrderField + " " + sOrderDir);
            }
            return String.Join(", ", lstOrder);
        }

        public static async Task<List<T>> ToListAsync<T>(this ObjectResult<T> source)
        {
            //THANK YOU: http://www.jeremychild.com/post/2014/03/18/Entity-Framework-asyncawait-on-an-Stored-Procedure-or-Function-Import
            var list = new List<T>();
            await Task.Run(() => list.AddRange(source.ToList()));
            return list;
        }

        public static async Task<int> ToIntAsync(this int source)
        {
            //THANK YOU: http://www.jeremychild.com/post/2014/03/18/Entity-Framework-asyncawait-on-an-Stored-Procedure-or-Function-Import
            var retInt = 0;
            await Task.Run(() => retInt = source);
            return retInt;
        }

        public static string RemoveConnectionStringSecurity(string inString)
        {
            string[] securityQualifiers = new string[] { "user", "uid",
                                      "password", "pwd" };
            string retStr = inString;

            foreach (string qualifier in securityQualifiers)
            {
                if (retStr.IndexOf(qualifier + "=") > 0)
                {
                    // Remove Security Qualifier
                    try
                    {
                        retStr = retStr.Substring(0,
                                 retStr.ToLower().IndexOf(qualifier + "=") +
                                 qualifier.Length + 1)
                                + "*HIDDEN*"
                                + retStr.Substring
                                (
                                    retStr.ToLower().IndexOf(qualifier + "="),
                                    retStr.Length - retStr.ToLower().IndexOf(qualifier + "=")
                                ).Substring
                                (
                                    retStr.Substring
                                    (
                                        retStr.ToLower().IndexOf(qualifier + "="),
                                        retStr.Length - retStr.ToLower().IndexOf(qualifier + "=")
                                    ).IndexOf(";")
                                );
                    }
                    catch
                    {
                        // Last element and no terminating ';'
                        retStr = retStr.Substring(0,
                          retStr.ToLower().IndexOf(qualifier + "=") + qualifier.Length + 1)
                          + "*HIDDEN*";
                    }
                }
            }

            return retStr;
        }

    }
    public static class Extensions
    {
        //Thank you http://stackoverflow.com/questions/3511780/system-linq-dynamic-and-sql-in-operator
        public static IQueryable<TEntity> WhereIn<TEntity, TValue>
        (
            this ObjectQuery<TEntity> query,
            Expression<Func<TEntity, TValue>> selector,
            IEnumerable<TValue> collection
        )
        {
            if (selector == null) throw new ArgumentNullException("selector");
            if (collection == null) throw new ArgumentNullException("collection");
            ParameterExpression p = selector.Parameters.Single();

            if (!collection.Any()) return query;

            IEnumerable<Expression> equals = collection.Select(value =>
              (Expression)Expression.Equal(selector.Body,
               Expression.Constant(value, typeof(TValue))));

            Expression body = equals.Aggregate((accumulate, equal) =>
            Expression.Or(accumulate, equal));

            return query.Where(Expression.Lambda<Func<TEntity, bool>>(body, p));
        }

        static Regex underscore = new Regex(@"(^|_)(.)");
        static string convertName(string s)
        {
            return underscore.Replace(s.ToLower(), m => m.Groups[0].ToString().ToUpper().Replace("_", ""));
        }

        static T ToObject<T>(this IDataRecord r) where T : new()
        {
            T obj = new T();
            for (int i = 0; i < r.FieldCount; i++)
            {
                if (r.GetName(i).Equals("Amendment"))
                {
                    //var k = 10;
                }
                var p = typeof(T).GetProperty(r.GetName(i));
                //var p = typeof(T).GetProperty(convertName(r.GetName(i)));
                if (p != null)
                {
                    if (p.PropertyType == r[i].GetType())
                        p.SetValue(obj, r[i], null);
                    else
                    {
                        if (p.PropertyType.GenericTypeArguments.Contains(r[i].GetType()))
                        {
                            p.SetValue(obj, r[i], null);
                        }
                        else
                        {
                            var c = TypeDescriptor.GetConverter(r[i]);
                            if (c.CanConvertTo(p.PropertyType))
                                p.SetValue(obj, c.ConvertTo(r[i], p.PropertyType), null);

                        }
                    }
                }
            }
            return obj;
        }

        public static IEnumerable<T> GetObjects<T>(this IDbCommand c) where T : new()
        {
            using (IDataReader r = c.ExecuteReader())
            {
                while (r.Read())
                {
                    yield return r.ToObject<T>();
                }
            }
        }

        public static IEnumerable<T> GetObjects<T>(this IDataReader r) where T : new()
        {
            while (r.Read())
            {
                yield return r.ToObject<T>();
            }
        }

        public static IEnumerable<T> GetObjectsNextRS<T>(this IDataReader r) where T : new()
        {
            if (r.NextResult())
            {
                while (r.Read())
                {
                    yield return r.ToObject<T>();
                }
            }
        }


    }



}