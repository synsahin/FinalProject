using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        public static IResult Run(params IResult[] logics)
        {
            //bu sayede kullanılacak birden fazzla IResult parametresini params IResult'a gönderir ve
            //birden fazla ayrı ayrı metod oluşturmadan virgül ile aynı anda kullanılabilir  "if(xParametresi,yParametresi)"

            foreach (var logic in logics) 
            {
                if (!logic.Success)
                {
                    return logic;
                }
            }
            return null;
        }
    }
}
