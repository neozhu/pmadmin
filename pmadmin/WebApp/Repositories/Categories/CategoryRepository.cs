﻿
                    
      
    
 
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Repository.Pattern.Repositories;

using WebApp.Models;
using System.Threading.Tasks;

namespace WebApp.Repositories
{
  public static class CategoryRepository  
    {


      public static IEnumerable<Product> GetProductsByCategoryId(this IRepositoryAsync<Category> repository, int categoryid)
      {
          var productRepository = repository.GetRepository<Product>();
          return productRepository.Queryable().Include(x => x.Category).Where(n => n.CategoryId == categoryid);
      }


         
	}
}



