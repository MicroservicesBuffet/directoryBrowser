﻿//this was autogenerated by a tool. Do not modify! Use partial
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated;

public partial class SearchModifiedFile:  GeneratorFromDB.Search<eModifiedFileColumns,ModifiedFile>
{
    //private ApplicationDBContext _context;
    //public SearchModifiedFile(ApplicationDBContext context){
    //    _context= context;
    //}
public static SearchModifiedFile FromSearch(GeneratorFromDB.SearchCriteria sc, eModifiedFileColumns colToSearch, string value)
    {
        var search = new SearchModifiedFile();
        var orderBy = new GeneratorFromDB.OrderBy<eModifiedFileColumns>();
                orderBy.FieldName = eModifiedFileColumns.IDFile ;;
        
        orderBy.Asc = false;
        search.OrderBys = new[] { orderBy };
        search.PageNumber = 1;
        search.PageSize = int.MaxValue - 1;
        var s = new GeneratorFromDB.SearchField<eModifiedFileColumns>();
        s.Criteria = sc;
        s.FieldName = colToSearch;
        s.Value = value;
        search.SearchFields = new[] { s };
        return search;
    }
   public override IOrderedQueryable<ModifiedFile> TransformToOrder(IQueryable<ModifiedFile> data){
        if(OrderBys == null || OrderBys.Length ==0){
            OrderBys =new GeneratorFromDB.OrderBy<eModifiedFileColumns>[1];
            OrderBys[0]= new GeneratorFromDB.OrderBy<eModifiedFileColumns>()
            {
                //maybe find PK ...
                FieldName = eModifiedFileColumns.IDFile,
                Asc=false
            };
        }
        var order = OrderBys[0]!;
        IOrderedQueryable<ModifiedFile> ret;
        //TODO: maybe utilize EF.Property ? 
        switch(order.FieldName){
                    case eModifiedFileColumns.IDFile:
                if(order.Asc)
                    ret = data.OrderBy(it=>it.IDFile);
                else
                    ret = data.OrderByDescending(it=>it.IDFile);
                
                break;

                    case eModifiedFileColumns.FullPathFile:
                if(order.Asc)
                    ret = data.OrderBy(it=>it.FullPathFile);
                else
                    ret = data.OrderByDescending(it=>it.FullPathFile);
                
                break;

                    default:
                throw new ArgumentException(" cannot order ModifiedFile by "+ order.FieldName);
            
        }
        for(var i=1;i<OrderBys.Length;i++){
            order=OrderBys[i];
            switch(order.FieldName){
                    case eModifiedFileColumns.IDFile:
                if(order.Asc)
                    ret = ret.ThenBy(it=>it.IDFile);
                else
                    ret = ret.ThenByDescending(it=>it.IDFile);
                
                break;
                    case eModifiedFileColumns.FullPathFile:
                if(order.Asc)
                    ret = ret.ThenBy(it=>it.FullPathFile);
                else
                    ret = ret.ThenByDescending(it=>it.FullPathFile);
                
                break;
                    default:
                throw new ArgumentException(" cannot order ModifiedFile by "+ order.FieldName);
            
        }
        }
        return ret;
        
    }
    public override  IQueryable<ModifiedFile> TransformToWhere(IQueryable<ModifiedFile> data){
        if(SearchFields == null || SearchFields.Length ==0)        
            return data;
        var returnValue = data;
        foreach(var s in SearchFields){
            switch(s.FieldName ){
                case eModifiedFileColumns.None :
                    continue;
                    
            case eModifiedFileColumns.IDFile:
                //long isNullable False
                if(s.Value == null)
        {
                            throw new ArgumentException("ModifiedFile.IDFile cannot be null");
                                }//end if s.value is null -search for null
        //if we are here, s.Value is not null
        { //use this to define value in smaller scope
                        var valueArray = s.Value
                    .Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(it=>long.Parse(it))
                    .ToArray();
                var value = valueArray[0];
                  
          
        switch(s.Criteria){

            case GeneratorFromDB.SearchCriteria.Between:
                if(valueArray?.Length != 2){
                    throw new ArgumentException("between must have 2 args, separated by comma => value is:" + s.Value);
                }
                                    returnValue =returnValue.Where(it=>it.IDFile >= valueArray[0] && it.IDFile <= valueArray[1]);
                  
                continue;
            case GeneratorFromDB.SearchCriteria.NotBetween:
            
            if(valueArray?.Length != 2){
                    throw new ArgumentException("not between must have 2 args, separated by comma => value is:" + s.Value);
                }
                                    returnValue =returnValue.Where(it=>it.IDFile < valueArray[0] || it.IDFile > valueArray[1]);
                  
                continue;
            case GeneratorFromDB.SearchCriteria.InArray:
                        returnValue =returnValue.Where(it=> valueArray!.Contains(it.IDFile));
                  
                continue;
            case GeneratorFromDB.SearchCriteria.NotInArray:
                        returnValue =returnValue.Where(it=> !valueArray!.Contains(it.IDFile));
                  
                continue;
            case GeneratorFromDB.SearchCriteria.Equal:
                returnValue =returnValue.Where(it=>it.IDFile==value);
                continue;
            case GeneratorFromDB.SearchCriteria.Different:
                returnValue =returnValue.Where(it=>it.IDFile!=value);
                continue;
            
            case GeneratorFromDB.SearchCriteria.Less:
                                                returnValue =returnValue.Where(it=>it.IDFile<value);
                                                
                        continue;
                    case GeneratorFromDB.SearchCriteria.LessOrEqual:
                                                
                        returnValue =returnValue.Where(it=>it.IDFile<=value);
                                                
                        continue;
                    case GeneratorFromDB.SearchCriteria.Greater:
                                                
                        returnValue =returnValue.Where(it=>it.IDFile>value);
                                                continue;
                    case GeneratorFromDB.SearchCriteria.GreaterOrEqual:
                                                returnValue =returnValue.Where(it=>it.IDFile>=value);
                                                continue;
                                                                        

            default:
                throw new ArgumentException($"not found Criteria {(int)s.Criteria} {s.Criteria} for {s.FieldName}");
        }//end switch after s.Criteria

                //continue;
        } //end use this to define value in smaller scope
            
                    
            case eModifiedFileColumns.FullPathFile:
                //string isNullable False
                if(s.Value == null)
        {
                            switch(s.Criteria){
                    case GeneratorFromDB.SearchCriteria.Equal:
                        returnValue =returnValue.Where(it=>it.FullPathFile==null);
                        continue;
                    case GeneratorFromDB.SearchCriteria.Different:
                        returnValue =returnValue.Where(it=>it.FullPathFile!=null);
                        continue;
                    default:
                        throw new ArgumentException($"null cannot have {(int)s.Criteria} {s.Criteria} for {s.FieldName}");
                    }
                
            
                                }//end if s.value is null -search for null
        //if we are here, s.Value is not null
        { //use this to define value in smaller scope
                                var value = s.Value;
                var valueArray= s.Value?.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .ToArray();
                ;
          
          
        switch(s.Criteria){

            case GeneratorFromDB.SearchCriteria.Between:
                if(valueArray?.Length != 2){
                    throw new ArgumentException("between must have 2 args, separated by comma => value is:" + s.Value);
                }
                                    returnValue =returnValue.Where(it=>String.Compare(it.FullPathFile,valueArray[0]) >= 0  && String.Compare(it.FullPathFile,valueArray[1]) <= 0);
                  
                continue;
            case GeneratorFromDB.SearchCriteria.NotBetween:
            
            if(valueArray?.Length != 2){
                    throw new ArgumentException("not between must have 2 args, separated by comma => value is:" + s.Value);
                }
                                    returnValue =returnValue.Where(it=>String.Compare(it.FullPathFile,valueArray[0]) < 0  || String.Compare(it.FullPathFile,valueArray[1]) > 0);
                  
                continue;
            case GeneratorFromDB.SearchCriteria.InArray:
                        returnValue =returnValue.Where(it=> valueArray!.Contains(it.FullPathFile));
                  
                continue;
            case GeneratorFromDB.SearchCriteria.NotInArray:
                        returnValue =returnValue.Where(it=> !valueArray!.Contains(it.FullPathFile));
                  
                continue;
            case GeneratorFromDB.SearchCriteria.Equal:
                returnValue =returnValue.Where(it=>it.FullPathFile==value);
                continue;
            case GeneratorFromDB.SearchCriteria.Different:
                returnValue =returnValue.Where(it=>it.FullPathFile!=value);
                continue;
            
            case GeneratorFromDB.SearchCriteria.Less:
                                                    returnValue =returnValue.Where(it=>String.Compare(it.FullPathFile,value) < 0 );
                                                
                        continue;
                    case GeneratorFromDB.SearchCriteria.LessOrEqual:
                                                    returnValue =returnValue.Where(it=>String.Compare(it.FullPathFile,value) <= 0 );
                                                
                        continue;
                    case GeneratorFromDB.SearchCriteria.Greater:
                                                    returnValue =returnValue.Where(it=>String.Compare(it.FullPathFile,value) > 0 );
                                                continue;
                    case GeneratorFromDB.SearchCriteria.GreaterOrEqual:
                                                    returnValue =returnValue.Where(it=>String.Compare(it.FullPathFile,value) >= 0 );
                                                continue;
                                            case GeneratorFromDB.SearchCriteria.Contains:
                        returnValue =returnValue.Where(it=>it.FullPathFile != null && it.FullPathFile.Contains(value));
                        continue;
                    case GeneratorFromDB.SearchCriteria.StartsWith:
                        returnValue =returnValue.Where(it=>it.FullPathFile != null &&  it.FullPathFile.StartsWith(value));
                        continue;
                    case GeneratorFromDB.SearchCriteria.EndsWith:
                        returnValue =returnValue.Where(it=>it.FullPathFile != null && it.FullPathFile.EndsWith(value));
                        continue;
                    /*case SearchCriteria.Contains:
                        returnValue =returnValue.Where(it=> it.FullPathFile != null && it.FullPathFile.Contains(value));
                        continue;
                    */
                                                            

            default:
                throw new ArgumentException($"not found Criteria {(int)s.Criteria} {s.Criteria} for {s.FieldName}");
        }//end switch after s.Criteria

                //continue;
        } //end use this to define value in smaller scope
            
                }//end switch  
       }//end foreach
    return returnValue;
    //throw new NotImplementedException("not");
    }

    public long IDFile { get; set; }

    public string FullPathFile { get; set; } = null!;

    //[InverseProperty("IDFileNavigation")]
//    public virtual ICollection<ModifiedUserFile> ModifiedUserFile { get; } = new List<ModifiedUserFile>();
}
