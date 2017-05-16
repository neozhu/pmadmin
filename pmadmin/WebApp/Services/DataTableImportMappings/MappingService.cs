﻿



using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Repository.Pattern.Repositories;
using Service.Pattern;

using WebApp.Models;
using WebApp.Repositories;
using System.Reflection;
using Repository.Pattern.Ef6;
using WebApp.Extensions;

namespace WebApp.Services
{
   public class DataTableImportMappingService : Service< DataTableImportMapping >, IDataTableImportMappingService
    {

        private readonly IRepositoryAsync<DataTableImportMapping> _repository;
        public  DataTableImportMappingService(IRepositoryAsync< DataTableImportMapping> repository)
            : base(repository)
        {
            _repository=repository;
        }




        public IEnumerable<EntityInfo> GetAssemblyEntities()
        {
            List<EntityInfo> list = new List<EntityInfo>();

            Assembly asm = Assembly.GetExecutingAssembly();

             list = asm.GetTypes()
                    .Where(type => typeof(Entity).IsAssignableFrom(type))
                    .SelectMany(type => type.GetProperties(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                    .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                    .Select(x => new EntityInfo { EntitySetName = x.DeclaringType.Name, FieldName = x.Name, FieldTypeName = x.PropertyType.Name, IsRequired = x.GetCustomAttributes().Where(f => f.TypeId.ToString().IndexOf("Required")>=0).Any() })
                    .OrderBy(x => x.EntitySetName).ThenBy(x => x.FieldName).ToList();
            



            return list;
        }


        public void GenerateByEnityName(string entityName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            var list = asm.GetTypes()
                   .Where(type => typeof(Entity).IsAssignableFrom(type))
                   .SelectMany(type => type.GetProperties(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                   .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                   .Select(x => new EntityInfo { EntitySetName = x.DeclaringType.Name, FieldName = x.Name, FieldTypeName = x.PropertyType.Name, IsRequired = x.GetCustomAttributes().Where(f => f.TypeId.ToString().IndexOf("Required") >= 0).Any() })
                   .OrderBy(x => x.EntitySetName).ThenBy(x => x.FieldName)
                   .Where(x => x.EntitySetName == entityName && x.FieldTypeName!="ICollection`1").ToList();

            var entityType = asm.GetTypes()
                   .Where(type => typeof(Entity).IsAssignableFrom(type)).Where(x => x.Name == entityName).First();
            foreach (var item in list)
            {
                var exist = this.Queryable().Where(x => x.EntitySetName == item.EntitySetName && x.FieldName == item.FieldName).Any();
                if (!exist)
                {
                    DataTableImportMapping row = new DataTableImportMapping();
                    row.EntitySetName = item.EntitySetName;
                    row.FieldName = item.FieldName;
                    row.IsRequired = item.IsRequired;
                    row.TypeName = item.FieldTypeName;
                    row.IsEnabled = item.IsRequired;
                    row.SourceFieldName = AttributeHelper.GetDisplayName(entityType, item.FieldName);
                    this.Insert(row);
                }
            }
        }


        public DataTableImportMapping FindMapping(string entitySetName, string sourceFieldName)
        {
            return this.Queryable().Where(x => x.EntitySetName == entitySetName && x.SourceFieldName == sourceFieldName).FirstOrDefault();
        }
    }
}



